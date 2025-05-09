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

namespace HideToolbarButton
{
    public partial class Form1 : Form
    {
        PdfViewerControl pdfViewerControl;
        string file;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            //Creating PdfViewer control obkect and add it to the forms
            pdfViewerControl = new PdfViewerControl();
            pdfViewerControl.Dock = DockStyle.Fill;
            this.Controls.Add(pdfViewerControl);
#if NETCOREAPP
            file = "../../../Data/PDF_Succinctly.pdf";
#else
            file = "../../Data/PDF_Succinctly.pdf";
#endif
            //Load the PDF
            pdfViewerControl.Load(file);

            //Hide the open and save toolbar button
            pdfViewerControl.ToolbarSettings.OpenButton.IsVisible = false;
            pdfViewerControl.ToolbarSettings.SaveButton.IsVisible = false;

        }
    }
}
