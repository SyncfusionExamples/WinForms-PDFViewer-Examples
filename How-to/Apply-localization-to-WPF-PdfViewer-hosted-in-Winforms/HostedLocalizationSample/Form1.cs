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

namespace HostedLocalizationSample
{
    public partial class Form1 : Form
    {
        ElementHost elementHost = new ElementHost();
        WPFUserControl pdfViewer;
        public Form1()
        {
            InitializeComponent();
            pdfViewer = new WPFUserControl();
            elementHost.Dock = DockStyle.Fill;
            elementHost.Child = pdfViewer;
            this.Controls.Add(elementHost);
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
