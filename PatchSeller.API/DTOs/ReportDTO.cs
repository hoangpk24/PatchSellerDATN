using System;
using System.Collections.Generic;

namespace PatchSeller.API.DTOs
{
    public class PatchSoldItemDTO
    {
        public int PatchId { get; set; }
        public string PatchName { get; set; } = string.Empty;
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int Unit { get; set; }
        public double Revenue { get; set; }
    }

    public class CategorySoldItemDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class DownloadTopItemDTO
    {
        public int PatchId { get; set; }
        public string PatchName { get; set; } = string.Empty;
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int DownloadCount { get; set; }
    }

    public class ReportSplitItemDTO
    {
        public string Date { get; set; } = string.Empty;
        public int TotalPatchSold { get; set; }
        public double TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public int OrderSuccess { get; set; }
        public int OrderCanceled { get; set; }
        public int DownloadCount { get; set; }
        public List<PatchSoldItemDTO> PatchSold { get; set; } = new List<PatchSoldItemDTO>();
        public List<DownloadTopItemDTO> Top10Download { get; set; } = new List<DownloadTopItemDTO>();
    }

    public class ReportDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SplitData { get; set; } = string.Empty; // year or month

        public List<ReportSplitItemDTO> DataSplited { get; set; } = new List<ReportSplitItemDTO>();
        public List<CategorySoldItemDTO> CategorySoldTop10 { get; set; } = new List<CategorySoldItemDTO>();

        public int TotalPatchSold { get; set; }
        public double TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public int OrderSuccess { get; set; }
        public int OrderCanceled { get; set; }
        public int DownloadCount { get; set; }
        public List<PatchSoldItemDTO> PatchSold { get; set; } = new List<PatchSoldItemDTO>();
        public List<DownloadTopItemDTO> Top10Download { get; set; } = new List<DownloadTopItemDTO>();
    }
}

