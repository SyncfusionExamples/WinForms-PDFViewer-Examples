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

namespace HostedPdfViewer
{
    public partial class Form1 : Form
    {
        ElementHost elementHost = null;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            // Create the ElementHost control.
            elementHost = new ElementHost();
            elementHost.AutoSize = true;
            elementHost.Size = panel1.Size;
            elementHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(elementHost);

            WPFPdfViewer pdfViewer = new WPFPdfViewer();
            elementHost.Child = pdfViewer;

            elementHost.Margin = new Padding(0, 0, 0, 0);
            elementHost.Region = new Region();
            elementHost.BackColor = System.Drawing.Color.White;
        }
    }
}
