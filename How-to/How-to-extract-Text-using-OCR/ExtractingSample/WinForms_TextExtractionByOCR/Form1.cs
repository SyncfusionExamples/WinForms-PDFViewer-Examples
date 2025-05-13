using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace WinForms_TextExtractionByOCR
{
    public partial class Form1 : Form
    {
        ElementHost elementHost = new ElementHost();
        PdfViewer pdfViewer;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            pdfViewer = new PdfViewer();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = pdfViewer;
            this.Controls.Add(elementHost);

        }
    }
}
