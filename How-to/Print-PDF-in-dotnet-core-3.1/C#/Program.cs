using Syncfusion.Windows.Forms.PdfViewer;
using System.Windows.Forms;

namespace PDFPrintDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            PdfDocumentView viewer = new PdfDocumentView();

            //Load the PDF document
            viewer.Load("../../../Data/Barcode.pdf");

            //Initialize print dialog.
            PrintDialog dialog = new PrintDialog();

            dialog.AllowPrintToFile = true;

            dialog.AllowSomePages = true;

            dialog.AllowCurrentPage = true;

            dialog.Document = viewer.PrintDocument;

            //Print the PDF document
            dialog.Document.Print();

            //Dispose the viewer
            viewer.Dispose();
        }
    }
}
