using System.Drawing.Printing;

namespace APIBackend.Utilities;
public class PrinterJob
{
    public static void PrintPdf(byte[] pdfBytes, string printerName = "")
    {
        // Get the default printer if no printer is specified
        printerName = string.IsNullOrEmpty(printerName) ? new PrinterSettings().PrinterName : printerName;

        // Use WinSpool API to send the bytes to the printer
        using (var stream = new MemoryStream(pdfBytes))
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;

            printDoc.PrintPage += (sender, e) =>
            {
                using (var pdfImage = System.Drawing.Image.FromStream(stream))
                {
                    e.Graphics.DrawImage(pdfImage, new System.Drawing.PointF(0, 0));
                }
            };

            printDoc.Print();
        }
    }
}
