using System;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.PdfViewer;

namespace SilentPrintPDF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Load the PDF document 
            PdfLoadedDocument ldoc = new PdfLoadedDocument(@"../../Data/F# Succinctly.pdf");
            PdfViewerControl viewer = new PdfViewerControl();
            //Load the PDFLoadedDocument in PDF viewer control
            viewer.Load(ldoc);
            Console.WriteLine("Printing started...");
            //Silent printing the PDF document using the PDF viewer control
            viewer.Print(false);
            Console.WriteLine("Printing is done…");
        }
    }
}
