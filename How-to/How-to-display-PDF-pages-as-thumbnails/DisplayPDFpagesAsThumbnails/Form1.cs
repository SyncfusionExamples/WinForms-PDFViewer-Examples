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
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create a parent TableLayoutPanel with two columns and one row.
            var parentLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            parentLayout.ColumnStyles.Clear();
            //Create rows and columns with specific size to view the thumbnail images and pdfviewer effectively
            parentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            parentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84F));
            parentLayout.RowStyles.Clear();
            parentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            //Remove the existing controls from the Form, so that we can re‐insert them into our new parentLayout.
            this.Controls.Remove(tableLayoutPanel1);
            this.Controls.Remove(pdfViewerControl1);

            //Prepare tableLayoutPanel1 to Auto‐size vertically. 
            tableLayoutPanel1.AutoSize = true;
            //Set Dock style of tableLayoutPane to Top to grow downward as thumbnail images are added.
            tableLayoutPanel1.Dock = DockStyle.Top;

            //Create a scrollable Panel, and put tableLayoutPanel1 inside it.
            var thumbnailScrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            thumbnailScrollHost.Controls.Add(tableLayoutPanel1);

            // Configure pdfViewerControl to dock Fill.
            pdfViewerControl1.Dock = DockStyle.Fill;
            // Load the PDF file.
#if NETCOREAPP
            filePath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filePath = @"../../Data/PDF_Succinctly.pdf";
#endif
            pdfViewerControl1.Load(filePath);
            pdfViewerControl1.DocumentLoaded += PdfViewerControl1_DocumentLoaded;

            // Add both the scroll host and the PDF viewer into parentLayout
            parentLayout.Controls.Add(thumbnailScrollHost, 0, 0);
            parentLayout.Controls.Add(pdfViewerControl1, 1, 0);

            // Finally, add parentLayout to the form.
            this.Controls.Add(parentLayout);
            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Event triggers once the document has been loaded
        /// </summary>
        private void PdfViewerControl1_DocumentLoaded(object sender, EventArgs args)
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;
            ExportAsImage();
        }

        /// <summary>
        /// Converts the PDF pages into images
        /// </summary>
        private async void ExportAsImage()
        {
            int count = pdfViewerControl1.LoadedDocument.Pages.Count;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = count;
            //Calculate height and width for teh thumbnail panel
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
                    picture.Margin = new Padding(25, 5, 10, 0);
                    picture.Refresh();
                    picture.MouseUp += Picture_MouseUp;
                    //Add the picture control to the tablelayoutpanel
                    tableLayoutPanel1.Controls.Add(picture, 0, i);
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
            int index = tableLayoutPanel1.GetRow(pictureBox);
            //Navigate to the specified page
            pdfViewerControl1.GoToPageAtIndex(index + 1);

        }
    }
}
