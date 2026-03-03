using Microsoft.EntityFrameworkCore;
using PatchSeller.DAL.Context;
using PatchSeller.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatchSeller.DAL.Repository
{
    public class ReportRepository
    {
        private readonly PatchSellerDbContext _context;

        public ReportRepository()
        {
            _context = new PatchSellerDbContext();
        }

        public async Task<List<Order>> GetOrdersWithDetails(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _context.Orders
                    .Include(o => o.OrderDetails)
                        .ThenInclude(od => od.Patch)
                            .ThenInclude(p => p.Game)
                                .ThenInclude(g => g.GameCategories)
                                    .ThenInclude(gc => gc.Category)
                    .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<DownloadLog>> GetDownloadLogsWithPatch(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await _context.DownloadLogs
                    .Include(d => d.PatchVersion)
                        .ThenInclude(pv => pv.Patch)
                            .ThenInclude(p => p.Game)
                    .Where(d => d.DownloadedAt >= startDate && d.DownloadedAt <= endDate)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

