namespace APIBackend.Utilities.Reports;

public class ReportFactory
{
    private readonly IEnumerable<IReportGenerator> _generators;

    public ReportFactory(IEnumerable<IReportGenerator> generators)
    {
        _generators = generators;
    }

    public IReportGenerator GetGenerator(string reportType)
    {
        var generator = _generators.FirstOrDefault(g =>
            g.ReportType.Equals(reportType, StringComparison.OrdinalIgnoreCase));

        if (generator == null)
            throw new ArgumentException($"Unknown report type: {reportType}");

        return generator;
    }
}

