using APIBackend.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace APIBackend.Utilities.Reports;
public class Receipt
{
    public byte[] Generate(BillDetails billDetails)
    {
        var receiptPdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.Size(PageSizes.A6);
                page.DefaultTextStyle(x => x.FontFamily(Common.CourierFont).FontSize(6).FontColor(Colors.Grey.Medium));


                page.Header()
                    .PaddingTop(1, Unit.Millimetre)
                    .Column(x =>
                    {
                        x.Item().Text(Common.Header).AlignCenter().FontSize(15);
                        x.Item().PaddingVertical(1, Unit.Millimetre);
                        x.Item().Text(Common.HeaderAddress).AlignCenter();
                        x.Item().Text(Common.HeaderLine).AlignLeft();
                        x.Item().PaddingVertical(1, Unit.Millimetre);
                    });


                page.Content()
                    .Column(x =>
                    {

                        //Detail section
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(7);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(5);
                            });

                            table.Cell().Row(1).Column(1).Text("Name").FontSize(7);
                            table.Cell().Row(1).Column(2).ColumnSpan(3).Text(Common.Colon + billDetails.FullName).FontSize(7).SemiBold();

                            table.Cell().Row(2).Column(1).ColumnSpan(4).Text("");

                            table.Cell().Row(3).Column(1).Text("Receipt No");
                            table.Cell().Row(3).Column(2).Text(Common.Colon + billDetails.ReceiptNumber.ToString());
                            table.Cell().Row(3).Column(3).Text("Phone");
                            table.Cell().Row(3).Column(4).Text(Common.Colon + billDetails.Phone);

                            table.Cell().Row(4).Column(1).Text("Date");
                            table.Cell().Row(4).Column(2).Text(Common.Colon + billDetails.BillDate.ToString("dd-MMM-yyyy HH:mm:ss")); //string formattedDateTime = dateTime.ToString("dd-MMM-yyyy HH:mm:ss");
                            table.Cell().Row(4).Column(3).Text("Age");
                            table.Cell().Row(4).Column(4).Text(Common.Colon + billDetails.Age.ToString());

                            table.Cell().Row(5).Column(1).Text("Served By");
                            table.Cell().Row(5).Column(2).Text(Common.Colon + billDetails.ServedBy);
                            table.Cell().Row(5).Column(3).Text("Note");
                            table.Cell().Row(5).Column(4).Text(Common.Colon + "");

                        });

                        //Test Section
                        x.Item().PaddingVertical(2, Unit.Millimetre);
                        x.Item().Row(row =>
                        {
                            row.AutoItem().Text("Test").AlignLeft();
                            row.RelativeItem();
                            row.AutoItem().Text("Price (LKR)").AlignRight();
                        });

                        x.Item().Text(Common.UnderscoreLine).AlignLeft();
                        //Test List
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            // To DO: Dynamic section
                            uint rw = 1;
                            foreach (var test in billDetails.BillTests)
                            {
                                table.Cell().Row(rw).Column(1).Text(test.TestName).AlignLeft();
                                table.Cell().Row(rw).Column(2).Text(test.Price.ToString("F2")).AlignRight();
                                rw++;
                            }
                            //table.Cell().Row(1).Column(1).Text("FBS").AlignLeft();
                            //table.Cell().Row(1).Column(2).Text("250.00").AlignRight();

                            //table.Cell().Row(2).Column(1).Text("HE-CULTURE").AlignLeft();
                            //table.Cell().Row(2).Column(2).Text("1,350.00").AlignRight();

                        });
                        x.Item().Text(Common.UnderscoreLine).AlignLeft();
                        x.Item().Text(Common.TestCount + billDetails.BillTests.Count.ToString()).AlignLeft();

                        x.Item().PaddingVertical(1, Unit.Millimetre);
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Cell().Row(1).Column(1).Text("Total").AlignLeft().FontSize(8).SemiBold();
                            table.Cell().Row(1).Column(2).Text(Common.LKR + billDetails.TotalAmount.ToString("F2")).AlignRight().FontSize(8).SemiBold();

                        });


                        //Footer
                        x.Item().PaddingVertical(2, Unit.Millimetre);
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(5);
                                columns.RelativeColumn(3);
                            });
                            table.Cell().Row(1).Column(1).Text("Cash").AlignLeft();

                            table.Cell().Row(2).Column(1).Text("Tendered :").AlignLeft();
                            table.Cell().Row(2).Column(2).Text(Common.LKR + billDetails.TenderedAmount?.ToString("F2") ?? "0.00").AlignLeft();

                            if (billDetails.Change == null)
                            {
                                table.Cell().Row(3).Column(1).Text("Change :").AlignLeft();
                                table.Cell().Row(3).Column(2).Text(Common.LKR + "0.00").AlignLeft();
                            }
                            else if (billDetails.Change < 0)
                            {
                                table.Cell().Row(3).Column(1).Text("Due Amount :").AlignLeft();
                                table.Cell().Row(3).Column(2).Text(Common.LKR + (billDetails.Change * -1)?.ToString("F2")).AlignLeft();
                            }
                            else
                            {
                                table.Cell().Row(3).Column(1).Text("Change :").AlignLeft();
                                table.Cell().Row(3).Column(2).Text(Common.LKR + billDetails.Change?.ToString("F2")).AlignLeft();
                            }


                        });

                        x.Item().PaddingVertical(1, Unit.Millimetre);
                        x.Item().Text("Reference Id : " + billDetails.BillId.ToString()).FontSize(5);
                    });
            });
        });

        return receiptPdf.GeneratePdf();
    }
}
