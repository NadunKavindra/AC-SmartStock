namespace APIBackend.Models;

public class ReportRequest
{
    public string ReportType { get; set; }
    public Dictionary<string, object> Parameters { get; set; }
    public string OutputMode { get; set; } // pdf | print
}
