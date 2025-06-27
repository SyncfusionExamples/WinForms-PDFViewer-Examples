using System.Windows.Forms;
using Syncfusion.Windows.Forms.PdfViewer;

namespace ViewPDFInWinforms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Opening the form in maximized state 
            this.WindowState = FormWindowState.Maximized;
            //Initializing the PdfViewerControl
            PdfViewerControl pdfViewerControl1 = new PdfViewerControl();
            //Add PdfViewerControl to the Form
            Controls.Add(pdfViewerControl1);
            //Docking the control to all edges of its containing control and sizing appropriately.
            pdfViewerControl1.Dock = DockStyle.Fill;
            //Loading the document in the PdfViewerControl
            pdfViewerControl1.Load(@"../../../Data/PDF_Succinctly.pdf");
        }
    }
}
