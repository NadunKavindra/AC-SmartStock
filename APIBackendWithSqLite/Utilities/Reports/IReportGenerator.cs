namespace APIBackend.Utilities.Reports;

public interface IReportGenerator
{
    string ReportType { get; }  // Unique identifier for the report
    byte[] Generate(Dictionary<string, object> parameters);
}
