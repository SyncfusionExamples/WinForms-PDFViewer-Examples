using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.PdfViewer;

namespace ViewPDFInWinforms
{
    public partial class Form1 : Form
    {
        string filePath;
        public Form1()
        {
            InitializeComponent();
            //Initializing the PdfViewerControl
            PdfViewerControl pdfViewerControl1 = new PdfViewerControl();

            //Add PdfViewerControl to the Form
            Controls.Add(pdfViewerControl1);
            //Docking the control to all edges of its containing control and sizing appropriately.
            pdfViewerControl1.Dock = DockStyle.Fill;
#if NETCOREAPP
            filePath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filePath = @"../../Data/PDF_Succinctly.pdf";
#endif
            //Loading the document in the PdfViewerControl
            pdfViewerControl1.Load(filePath);
        }
    }
}
