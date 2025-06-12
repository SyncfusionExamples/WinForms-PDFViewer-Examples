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

namespace SaveAndPrintButtonInToolbar
{
    public partial class Form1 : Form
    {
        ElementHost elementHost = null;
        WPFControl wpfControl = null;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            elementHost = new ElementHost();
            elementHost.Margin = new Padding(0, 0, 0, 0);
            elementHost.Region = new Region();
            elementHost.BackColor = System.Drawing.Color.White;
            elementHost.AutoSize = true;
            elementHost.Size = this.Size;
            elementHost.Dock = DockStyle.Fill;
            this.Controls.Add(elementHost);

            wpfControl = new WPFControl();
            elementHost.Child = wpfControl;

            this.WindowState = FormWindowState.Maximized;
        }
    }
}
