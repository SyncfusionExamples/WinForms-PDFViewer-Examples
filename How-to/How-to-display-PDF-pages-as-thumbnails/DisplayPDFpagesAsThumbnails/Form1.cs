using Syncfusion.Windows.Forms.PdfViewer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayPDFpagesAsThumbnails
{
    public partial class Form1 : Form
    {
        string filePath;
        int thumbnailZoomfactor = 4;
        TableLayoutPanel tableLayoutPanel;
        FlowLayoutPanel thumbnailLayoutPanel;
        PdfViewerControl pdfViewerControl;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialize a new TableLayoutPanel and configure the tableLayoutPanel to hold two column and one row
            tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnStyles.Clear();
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84F));

            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            //Create a Scrolllable layout panel for the thumbnail images
            thumbnailLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
            };

            //Add the thumbnailLayout to the first column of the tableLayoutPanel
            tableLayoutPanel.Controls.Add(thumbnailLayoutPanel,0,0);

            pdfViewerControl = new PdfViewerControl();
            pdfViewerControl.Dock = DockStyle.Fill;
            //Add the pdfViewer to the second column of the tableLayoutPanel 
            tableLayoutPanel.Controls.Add(pdfViewerControl,1,0);
#if NETCOREAPP
            filePath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filePath = @"../../Data/PDF_Succinctly.pdf";
#endif
            // Load the PDF file.
            pdfViewerControl.Load(filePath);
            pdfViewerControl.DocumentLoaded += PdfViewerControl_DocumentLoaded;
            this.Controls.Add(tableLayoutPanel);
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Triggers once the document is loaded it invoke the conversion of pdf page to image 
        /// </summary>
        private void PdfViewerControl_DocumentLoaded(object sender, EventArgs args)
        {
            thumbnailLayoutPanel.Controls.Clear();
            ExportAsImage();
        }

        /// <summary>
        /// Converts the PDF pages into images
        /// </summary>
        private async void ExportAsImage()
        {
            //Calculate height and width of the panel
            float height = pdfViewerControl.LoadedDocument.Pages[0].Size.Height / thumbnailZoomfactor;
            float width = pdfViewerControl.LoadedDocument.Pages[0].Size.Width / thumbnailZoomfactor;
            this.tableLayoutPanel.ColumnStyles[0].SizeType = SizeType.Absolute;
            if (pdfViewerControl.LoadedDocument.Pages.Count > 4)
            {
                this.tableLayoutPanel.ColumnStyles[0].Width = width + 30;
            }
            else
            {
                this.tableLayoutPanel.ColumnStyles[0].Width = width + 5;
            }
                for (int i = 0; i < pdfViewerControl.LoadedDocument.Pages.Count; i++)
                {
                    PictureBox picture = new PictureBox();
                    //Convert the PDF page as image
                    Bitmap image = new Bitmap(await Task.Run(() => pdfViewerControl.LoadedDocument.ExportAsImage(i)), new Size((int)width, (int)height));
                    //Set the exported image to the picture control
                    picture.Image = image;
                    picture.Update();
                    picture.Height = (int)height;
                    picture.Width = (int)width;
                    picture.Visible = true;
                    picture.Refresh();
                    picture.MouseUp += Picture_MouseUp;
                    //Add the picture control to the thumbnailPanel
                    thumbnailLayoutPanel.Controls.Add(picture);
                }
        }

        /// <summary>
        /// Triggered whenever the thumbnail images is clicked
        /// </summary>
        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            //Get the index of the page
            int index = thumbnailLayoutPanel.Controls.IndexOf(pictureBox);
            //Navigate to the specified page
            pdfViewerControl.GoToPageAtIndex(index + 1);
        }
    }
}
