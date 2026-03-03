using Microsoft.AspNetCore.Mvc;
using PatchSeller.API.DTOs;
using PatchSeller.DAL.Models;
using PatchSeller.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.API.Controllers.Admin
{
    [Route("admin/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportRepository _reportRepository;

        public ReportController()
        {
            _reportRepository = new ReportRepository();
        }

        [HttpGet("overview")]
        public async Task<ActionResult<ReportDTO>> GetOverview(
            DateTime startDate,
            DateTime endDate,
            string splitData = "month")
        {
            if (endDate < startDate)
            {
                return BadRequest("Thời gian không hợp lệ");
            }

            endDate = endDate.Date.AddDays(1).AddTicks(-1);

            splitData = splitData?.ToLower() ?? "month";
            if (splitData != "month" && splitData != "year")
            {
                return BadRequest("'month' hoặc 'year'");
            }

            var orders = await _reportRepository.GetOrdersWithDetails(startDate, endDate) ?? new List<Order>();
            var downloads = await _reportRepository.GetDownloadLogsWithPatch(startDate, endDate) ?? new List<DownloadLog>();

            var result = new ReportDTO
            {
                StartDate = startDate,
                EndDate = endDate,
                SplitData = splitData
            };

            var doneOrders = orders.Where(o => o.Status == Constant.OrderStatus.OrderDone).ToList();

            result.TotalOrder = orders.Count;
            result.OrderSuccess = doneOrders.Count;
            result.OrderCanceled = orders.Count(o => o.Status == Constant.OrderStatus.OrderCanceled);
            result.TotalPatchSold = doneOrders.SelectMany(o => o.OrderDetails ?? new List<OrderDetail>()).Count();
            result.TotalRevenue = doneOrders.Sum(o => o.FinalAmount);
            result.DownloadCount = downloads.Count;

            result.PatchSold = GetTopSellingPatches(orders);
            result.CategorySoldTop10 = GetTopSellingCategories(orders);
            result.Top10Download = GetTopDownloads(downloads);

            DateTime current = splitData == "year"
                ? new DateTime(startDate.Year, 1, 1)
                : new DateTime(startDate.Year, startDate.Month, 1);

            DateTime boundaryEnd = splitData == "year"
                ? new DateTime(endDate.Year, 12, 31, 23, 59, 59)
                : new DateTime(endDate.Year, endDate.Month, DateTime.DaysInMonth(endDate.Year, endDate.Month), 23, 59, 59);

            while (current <= boundaryEnd)
            {
                DateTime bucketStart;
                DateTime bucketEnd;
                string label;

                if (splitData == "year")
                {
                    bucketStart = new DateTime(current.Year, 1, 1);
                    bucketEnd = new DateTime(current.Year, 12, 31, 23, 59, 59);
                    label = current.Year.ToString();
                    current = current.AddYears(1);
                }
                else
                {
                    bucketStart = new DateTime(current.Year, current.Month, 1);
                    bucketEnd = new DateTime(current.Year, current.Month, DateTime.DaysInMonth(current.Year, current.Month), 23, 59, 59);
                    label = $"{current.Month:00}/{current.Year}";
                    current = current.AddMonths(1);
                }

                if (bucketEnd < startDate || bucketStart > endDate)
                {
                    continue;
                }

                var ordersBucket = orders
                    .Where(o => o.OrderDate >= bucketStart && o.OrderDate <= bucketEnd)
                    .ToList();
                var downloadsBucket = downloads
                    .Where(d => d.DownloadedAt >= bucketStart && d.DownloadedAt <= bucketEnd)
                    .ToList();

                var bucketDoneOrders = ordersBucket.Where(o => o.Status == Constant.OrderStatus.OrderDone).ToList();

                var splitItem = new ReportSplitItemDTO
                {
                    Date = label,
                    TotalOrder = ordersBucket.Count,
                    OrderSuccess = bucketDoneOrders.Count,
                    OrderCanceled = ordersBucket.Count(o => o.Status == Constant.OrderStatus.OrderCanceled),
                    TotalPatchSold = bucketDoneOrders.SelectMany(o => o.OrderDetails ?? new List<OrderDetail>()).Count(),
                    TotalRevenue = bucketDoneOrders.Sum(o => o.FinalAmount),
                    DownloadCount = downloadsBucket.Count,
                    PatchSold = GetTopSellingPatches(ordersBucket),
                    Top10Download = GetTopDownloads(downloadsBucket)
                };

                result.DataSplited.Add(splitItem);
            }

            return Ok(result);
        }

        private static List<PatchSoldItemDTO> GetTopSellingPatches(List<Order> orders)
        {
            return orders
                .Where(o => o.Status == Constant.OrderStatus.OrderDone)
                .SelectMany(o => o.OrderDetails ?? new List<OrderDetail>())
                .Where(od => od.Patch != null && od.Patch.Game != null)
                .GroupBy(od => new { od.PatchId, od.Patch!.Name, od.Patch.GameId, GameName = od.Patch.Game!.Title })
                .Select(g => new PatchSoldItemDTO
                {
                    PatchId = g.Key.PatchId,
                    PatchName = g.Key.Name,
                    GameId = g.Key.GameId,
                    GameName = g.Key.GameName,
                    Unit = g.Count(),
                    Revenue = g.Sum(x => x.Price)
                })
                .OrderByDescending(x => x.Revenue)
                .Take(10)
                .ToList();
        }

        private static List<CategorySoldItemDTO> GetTopSellingCategories(List<Order> orders)
        {
            return orders
                .Where(o => o.Status == Constant.OrderStatus.OrderDone)
                .SelectMany(o => o.OrderDetails ?? new List<OrderDetail>())
                .Where(od => od.Patch != null && od.Patch.Game != null)
                .SelectMany(od => od.Patch!.Game!.GameCategories
                    .Where(gc => !gc.Delete)
                    .Select(gc => new
                    {
                        gc.CategoryId,
                        CategoryName = gc.Category.CategoryName,
                        Revenue = od.Price
                    }))
                .GroupBy(x => new { x.CategoryId, x.CategoryName })
                .Select(g => new CategorySoldItemDTO
                {
                    CategoryId = g.Key.CategoryId,
                    CategoryName = g.Key.CategoryName,
                    Value = g.Sum(x => x.Revenue)
                })
                .OrderByDescending(x => x.Value)
                .Take(10)
                .ToList();
        }

        private static List<DownloadTopItemDTO> GetTopDownloads(List<DownloadLog> downloads)
        {
            return downloads
                .Where(d => d.PatchVersion != null && d.PatchVersion.Patch != null && d.PatchVersion.Patch.Game != null)
                .GroupBy(d => new
                {
                    PatchId = d.PatchVersion!.PatchId,
                    PatchName = d.PatchVersion.Patch!.Name,
                    GameId = d.PatchVersion.Patch.GameId,
                    GameName = d.PatchVersion.Patch.Game!.Title
                })
                .Select(g => new DownloadTopItemDTO
                {
                    PatchId = g.Key.PatchId,
                    PatchName = g.Key.PatchName,
                    GameId = g.Key.GameId,
                    GameName = g.Key.GameName,
                    DownloadCount = g.Count()
                })
                .OrderByDescending(x => x.DownloadCount)
                .Take(10)
                .ToList();
        }
    }
}
