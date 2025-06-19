using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CustomPassswordDialog
{
    public partial class Form1 : Form
    {
        ElementHost elementHost;
        WPFuserControl userControl;
        public static string PreLoadPdf;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            elementHost = new ElementHost();
            {
                elementHost.AutoSize = true;
                elementHost.Dock = DockStyle.Fill;
                elementHost.Margin = new Padding(0, 0, 0, 0);
                elementHost.Region = new Region();
                elementHost.BackColor = Color.White;
            }
            this.WindowState = FormWindowState.Maximized;
        }

        private void CreatePdf()
        {
            userControl = new WPFuserControl();
            elementHost.Child = userControl;

            userControl.pdfViewer.IsBookmarkEnabled = false;
            userControl.pdfViewer.ToolbarSettings.ShowFileTools = true;
            userControl.pdfViewer.ToolbarSettings.ShowPageNavigationTools = true;
            userControl.pdfViewer.ToolbarSettings.ShowAnnotationTools = false;
            userControl.pdfViewer.ToolbarSettings.ShowZoomTools = true;
            userControl.pdfViewer.ThumbnailSettings.IsVisible = false;
            userControl.pdfViewer.EnableRedactionTool = false;
            userControl.pdfViewer.PageOrganizerSettings.IsIconVisible = false;
            userControl.pdfViewer.EnableLayers = false;
            userControl.pdfViewer.EnableRedactionTool = false;
            userControl.pdfViewer.FormSettings.IsIconVisible = false;
            userControl.pdfViewer.CursorMode = PdfViewerCursorMode.SelectTool;
            userControl.pdfViewer.EnableNotificationBar = false;
            userControl.pdfViewer.ShowToolbar = true;

            userControl.pdfViewer.ZoomMode = ZoomMode.FitWidth;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(elementHost);
        }

            private void getPDF()
            {
                try
                {
                        var OFD = new OpenFileDialog()
                        {
                            DefaultExt = "pdf",
                            Filter = "Pdf files|*.pdf",
                            Title = "Search a PDF",
                            FilterIndex = 1,
                            InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        };
                        if (OFD.ShowDialog() == DialogResult.OK && File.Exists(OFD.FileName))
                         {
                           PreLoadPdf = OFD.FileName;

                           userControl.pdfViewer.ReferencePath = Application.StartupPath;
                           userControl.pdfViewer.Load(PreLoadPdf);
                         }
                }
                catch(System.Exception ex) 
                {
                     Console.WriteLine("Unexpected error"+ ex.Message);
                }
            }

        private void Open_Click(object sender, EventArgs e)
        {
            CreatePdf();
            getPDF();
        }
    }
}
