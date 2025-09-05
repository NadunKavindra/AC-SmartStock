using APIBackend.Models;
using APIBackend.Utilities.Reports;
using Microsoft.AspNetCore.Mvc;

namespace APIBackend.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportController : Controller
{
    private readonly ReportFactory _factory;

    public ReportController(ReportFactory factory)
    {
        _factory = factory;
    }


    [HttpPost("print")]
    public async Task<IActionResult> PrintReport([FromBody] ReportRequest request)
    {

        try
        {
            var generator = _factory.GetGenerator(request.ReportType);
            // Generate report as PDF byte[]
            byte[] pdfBytes = generator.Generate(request.Parameters);

            if (request.OutputMode == "pdf")
            {
                return File(pdfBytes, "application/pdf", $"{request.ReportType}.pdf");
            }
            else if (request.OutputMode == "print")
            {
                //await _reportService.SendToPrinterAsync(pdfBytes);
                return Ok(new { message = "Report sent to printer" });
            }

            return BadRequest("Invalid output mode.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }

        
    }
}
