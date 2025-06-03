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
        FlowLayoutPanel thumbnailLayoutPanel;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Configure the existing tableLayoutPanel to two columns and one row.
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84F));

            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            //Create a scrollable FlowlayoutPanel for the thumbnails  
            thumbnailLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
            };

            //Add the thumbnailLayout to the first column of the tableLayoutPanel
            tableLayoutPanel1.Controls.Add(thumbnailLayoutPanel, 0, 0);

            //Remove the existing pdfViewercontrol from the form, so that we can newly insert everytime into the tableLayoutPanel.
            this.Controls.Remove(pdfViewerControl1);
            //Add the pdfViewer to the second column of the tableLayoutPanel 
            pdfViewerControl1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(pdfViewerControl1, 1, 0);
            // Load the PDF file.
#if NETCOREAPP
            filePath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filePath = @"../../Data/PDF_Succinctly.pdf";
#endif
            pdfViewerControl1.Load(filePath);
            pdfViewerControl1.DocumentLoaded += PdfViewerControl1_DocumentLoaded;
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Event triggers once the document has been loaded
        /// </summary>
        private void PdfViewerControl1_DocumentLoaded(object sender, EventArgs args)
        {
            thumbnailLayoutPanel.Controls.Clear();
            ExportAsImage();
        }

        /// <summary>
        /// Converts the PDF pages into images
        /// </summary>
        private async void ExportAsImage()
        {
            //Calculate height and width for the thumbnail panel
            float height = pdfViewerControl1.LoadedDocument.Pages[0].Size.Height / thumbnailZoomfactor;
            float width = pdfViewerControl1.LoadedDocument.Pages[0].Size.Width / thumbnailZoomfactor;
            this.tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
            if(pdfViewerControl1.LoadedDocument.Pages.Count > 4)
            {
                this.tableLayoutPanel1.ColumnStyles[0].Width = width + 30;
            }
            else
            {
                this.tableLayoutPanel1.ColumnStyles[0].Width = width + 5;
            }
                for (int i = 0; i < pdfViewerControl1.LoadedDocument.Pages.Count; i++)
                {
                    PictureBox picture = new PictureBox();
                    //Convert the PDF page as image
                    Bitmap image = new Bitmap(await Task.Run(() => pdfViewerControl1.LoadedDocument.ExportAsImage(i)), new Size((int)width, (int)height));
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
        /// Event triggered once clicked the thumbnail images
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Picture_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            //Get the index of the page
            int index = thumbnailLayoutPanel.Controls.IndexOf(pictureBox);
            //Navigate to the specified page
            pdfViewerControl1.GoToPageAtIndex(index + 1);

        }
    }
}
