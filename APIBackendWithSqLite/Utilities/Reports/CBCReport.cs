using APIBackend.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace APIBackend.Utilities.Reports;

public class CBCReport : IReportGenerator
{
    private readonly ApplicationDbContext _context;
    public CBCReport(ApplicationDbContext context)
    {
        _context = context;
    }
    public string ReportType => "CBC";

    public byte[] Generate(Dictionary<string, object> parameters)
    {
        //int invoiceId = Convert.ToInt32(parameters["invoiceId"]);
        // Fetch from database
        //var invoice = _context.Invoices
        //                 .Include(i => i.Customer)
        //                 .FirstOrDefault(i => i.Id == invoiceId);

        var logs = _context.Logs.OrderByDescending(l => l.Timestamp).Take(35).ToList();

        if (logs == null)
            throw new Exception($"Logs not found.");


        var testPdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontFamily(Common.CourierFont).FontSize(11).FontColor(Colors.Grey.Darken4));

                page.Header()
                    .PaddingTop(1, Unit.Millimetre)
                    .Column(x =>
                    {
                        x.Item().Text(Common.Header).AlignCenter().FontSize(15);
                        x.Item().PaddingVertical(1, Unit.Millimetre);
                        x.Item().Text(Common.HeaderAddress).AlignCenter();
                        //x.Item().Column(c =>
                        //{
                        //    c.Item().LineHorizontal(1).LineDashPattern([4f, 4f]);
                        //});
                        x.Item().PaddingVertical(4, Unit.Millimetre);

                        //Detail section
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(7);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(5);
                            });

                            table.Cell().Row(1).Column(1).Text("Patient");
                            table.Cell().Row(1).Column(2).ColumnSpan(3).Text(Common.Colon + "Patient Full Name").SemiBold();

                            table.Cell().Row(2).Column(1).Text("Reference No");
                            table.Cell().Row(2).Column(2).Text(Common.Colon + "35 0122 17/10/25");

                            table.Cell().Row(3).Column(1).Text("Sample Date");
                            table.Cell().Row(3).Column(2).Text(Common.Colon + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss")); //string formattedDateTime = dateTime.ToString("dd-MMM-yyyy HH:mm:ss");
                            table.Cell().Row(3).Column(3).Text("Age");
                            table.Cell().Row(3).Column(4).Text(Common.Colon + "27");

                            table.Cell().Row(4).Column(1).Text("Report Date");
                            table.Cell().Row(4).Column(2).Text(Common.Colon + DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"));
                            table.Cell().Row(4).Column(3).Text("Phone");
                            table.Cell().Row(4).Column(4).Text(Common.Colon + "0711234567");

                            //To Do: Put this based on condition
                            table.Cell().Row(5).Column(1).Text("Test/Profile");
                            table.Cell().Row(5).Column(2).Text(Common.Colon + "Complete Blood Count (CBC)").SemiBold();
                            table.Cell().Row(5).Column(3).Text("Referred By");
                            table.Cell().Row(5).Column(4).Text(Common.Colon + "Dr. Doctor Name");

                            table.Cell().Row(6).Column(1).ColumnSpan(4).Text("");
                        });

                        x.Item().Column(c =>
                        {
                            //c.Item().LineHorizontal(1).LineDashPattern([4f, 4f, 12f, 4f]);
                            c.Item().LineHorizontal(0.5f);
                        });
                    });


                page.Content()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(60);
                            columns.ConstantColumn(100);
                        });

                        table.Header(header =>
                        {
                            header.Cell().BorderBottom(0.5f).Padding(3).AlignLeft().Text("Test");
                            header.Cell().BorderBottom(0.5f).Padding(3).AlignLeft().Text("");
                            header.Cell().BorderBottom(0.5f).Padding(3).AlignLeft().Text("Result");
                            header.Cell().BorderBottom(0.5f).Padding(3).AlignLeft().Text("Unit");                           
                            header.Cell().BorderBottom(0.5f).Padding(3).AlignCenter().Text("Reference");
                        });


                        table.Cell().Padding(2).AlignLeft().Text("Hemoglobin");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("15");
                        table.Cell().Padding(2).AlignLeft().Text("g/dL");                        
                        table.Cell().Padding(2).AlignLeft().Text("13 - 17");

                        table.Cell().Padding(2).AlignLeft().Text("Total Leukocyte Count");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("5.1");
                        table.Cell().Padding(2).AlignLeft().Text("10⁹/L");
                        table.Cell().Padding(2).AlignLeft().Text("4.8 - 10.8");

                        table.Cell().ColumnSpan(5).Padding(2).AlignLeft().Text("Differential Leukocyte Count").Underline();

                        table.Cell().Padding(2).PaddingLeft(20).AlignLeft().Text("Neutrophils");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("79");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("40 - 80");

                        table.Cell().Padding(2).PaddingLeft(20).AlignLeft().Text("Lymphocytes");
                        table.Cell().Padding(2).AlignLeft().Text("L");
                        table.Cell().Padding(2).AlignLeft().Text("18");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("20 - 40");                        

                        table.Cell().Padding(2).PaddingLeft(20).AlignLeft().Text("Eosinophils");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("1");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("1 - 6");

                        table.Cell().Padding(2).PaddingLeft(20).AlignLeft().Text("Monocytes");
                        table.Cell().Padding(2).AlignLeft().Text("L");
                        table.Cell().Padding(2).AlignLeft().Text("1");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("2 - 10");

                        table.Cell().Padding(2).PaddingLeft(20).AlignLeft().Text("Basophil");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("1");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("<2");

                        table.Cell().Padding(2).AlignLeft().Text("Platelet Count");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("5930");
                        table.Cell().Padding(2).AlignLeft().Text("10⁹/L");
                        table.Cell().Padding(2).AlignLeft().Text("150 - 400");

                        table.Cell().Padding(2).AlignLeft().Text("Total RBC Count");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("3.5");
                        table.Cell().Padding(2).AlignLeft().Text("10¹²/L");
                        table.Cell().Padding(2).AlignLeft().Text("4.5 - 5.6");

                        table.Cell().Padding(2).AlignLeft().Text("Hematocrit Value, HCT");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("42");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("40.0 - 47.0");

                        table.Cell().Padding(2).AlignLeft().Text("Mean Corpuscular Volume, MCV"); 
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("84.0");
                        table.Cell().Padding(2).AlignLeft().Text("fL");
                        table.Cell().Padding(2).AlignLeft().Text("83 - 101");

                        table.Cell().Padding(2).AlignLeft().Text("Mean Cell Hemoglobin, MCH");
                        table.Cell().Padding(2).AlignLeft().Text("");
                        table.Cell().Padding(2).AlignLeft().Text("30.0");
                        table.Cell().Padding(2).AlignLeft().Text("Pg");
                        table.Cell().Padding(2).AlignLeft().Text("27 - 32");

                        table.Cell().Padding(2).AlignLeft().Text("Mean Cell Hemoglobin CON, MCHC");
                        table.Cell().Padding(2).AlignLeft().Text("H");
                        table.Cell().Padding(2).AlignLeft().Text("35.7");
                        table.Cell().Padding(2).AlignLeft().Text("%");
                        table.Cell().Padding(2).AlignLeft().Text("31.5 - 34.5");

                    });


                page.Footer()
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(150);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(3);
                        });

                        table.Cell().Element(CellStyle).Text("Reference Id : " + "98340004").FontSize(9).Italic();
                        table.Cell().Element(CellStyle).Text("");
                        table.Cell().Element(CellStyle).Text("MLT :- ");

                        static IContainer CellStyle(IContainer container)
                            => container.Border(0.5f).Padding(2);
                    });


            });
        });

        return testPdf.GeneratePdf();
    }
}
