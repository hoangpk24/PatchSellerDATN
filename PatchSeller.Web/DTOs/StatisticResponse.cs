namespace PatchSeller.Web.DTOs
{
    public class PatchSoldItemResponse
    {
        public int PatchId { get; set; }
        public string PatchName { get; set; } = string.Empty;
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int Unit { get; set; }
        public double Revenue { get; set; }
    }

    public class CategorySoldItemResponse
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class DownloadTopItemResponse
    {
        public int PatchId { get; set; }
        public string PatchName { get; set; } = string.Empty;
        public int GameId { get; set; }
        public string GameName { get; set; } = string.Empty;
        public int DownloadCount { get; set; }
    }

    public class ReportSplitItemResponse
    {
        public string Date { get; set; } = string.Empty;
        public int TotalPatchSold { get; set; }
        public double TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public int OrderSuccess { get; set; }
        public int OrderCanceled { get; set; }
        public int DownloadCount { get; set; }
        public List<PatchSoldItemResponse> PatchSold { get; set; } = new List<PatchSoldItemResponse>();
        public List<DownloadTopItemResponse> Top10Download { get; set; } = new List<DownloadTopItemResponse>();
    }

    public class StatisticResponse
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SplitData { get; set; } = string.Empty; // year or month

        public List<ReportSplitItemResponse> DataSplited { get; set; } = new List<ReportSplitItemResponse>();
        public List<CategorySoldItemResponse> CategorySoldTop10 { get; set; } = new List<CategorySoldItemResponse>();

        public int TotalPatchSold { get; set; }
        public double TotalRevenue { get; set; }
        public int TotalOrder { get; set; }
        public int OrderSuccess { get; set; }
        public int OrderCanceled { get; set; }
        public int DownloadCount { get; set; }
        public List<PatchSoldItemResponse> PatchSold { get; set; } = new List<PatchSoldItemResponse>();
        public List<DownloadTopItemResponse> Top10Download { get; set; } = new List<DownloadTopItemResponse>();
    }
}
