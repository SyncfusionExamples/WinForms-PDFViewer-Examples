using System.Windows.Forms;
using Syncfusion.Windows.Forms.PdfViewer;

namespace PrintEventsDemo
{
    public partial class Form1 : Form
    {
        #region Constructor
        public Form1()
        {
            InitializeComponent();

            //Wire the `BeginPrint` event,
            pdfViewerControl1.BeginPrint += PdfViewerControl1_BeginPrint;

            //Wire the `EndPrint` event,
            pdfViewerControl1.EndPrint += PdfViewerControl1_EndPrint;

            //Wire the `PrintProgress` event,
            pdfViewerControl1.PrintProgress += PdfViewerControl1_PrintProgress;

            //Load the PDF file.
#if NETCORE
            pdfViewerControl1.Load("../../../Data/HTTP Succinctly.pdf");
#else
            pdfViewerControl1.Load("../../Data/HTTP Succinctly.pdf");
#endif
            //Print the file silently to the default printer. 
            pdfViewerControl1.Print(true);
        }
        #endregion

        #region Events
        private void PdfViewerControl1_BeginPrint(object sender, BeginPrintEventArgs e)
        {
            //Insert your code here
        }

        private void PdfViewerControl1_PrintProgress(object sender, PrintProgressEventArgs e)
        {
            //Find the page number which is currently printing.
            int currentPage = e.PageIndex;

            //Find the total number of pages present in the file.
            int pageCount = e.PageCount;
           
            //Insert your code here
        }

        private void PdfViewerControl1_EndPrint(object sender, EndPrintEventArgs e)
        {
            //Insert your code here
        }
        # endregion
    }
}