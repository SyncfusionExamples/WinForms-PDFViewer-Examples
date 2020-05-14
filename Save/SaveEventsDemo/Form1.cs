using System.Windows.Forms;
using Syncfusion.Windows.Forms.PdfViewer;

namespace SaveEventsDemo
{
    public partial class Form1 : Form
    {
        #region Constructor
        public Form1()
        {
            InitializeComponent();

            //Wire the `BeginSave` event.
            pdfViewerControl1.BeginSave += PdfViewerControl1_BeginSave;

            //Wire the `EndSave` event.
            pdfViewerControl1.EndSave += PdfViewerControl1_EndSave;

            //Load the PDF file.
#if NETCORE
            pdfViewerControl1.Load("../../../Data/HTTP Succinctly.pdf");
#else
            pdfViewerControl1.Load("../../Data/HTTP Succinctly.pdf");
#endif
        }
        #endregion

        #region Events
        private void PdfViewerControl1_BeginSave(object sender, BeginSaveEventArgs e)
        {
            //Insert your code here    
        }

        private void PdfViewerControl1_EndSave(object sender, EndSaveEventArgs e)
        {
            //Insert your code here
        }
        # endregion
    }
}