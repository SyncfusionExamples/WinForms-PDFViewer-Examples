using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SampleWF
{
    public partial class Form1 : Form
    {
        PdfViewerControl pdfViewer;
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;

            pdfViewer = new PdfViewerControl();
            pdfViewer.Dock= DockStyle.Fill;
            this.Controls.Add(pdfViewer);
            
            // wire the document loaded event.
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;

            // load the PDF.
            pdfViewer.Load("../../Data/C_Sharp_Succinctly.pdf");
        }

        /// <summary>
        /// Navigates the control to a bookmark
        /// </summary>
        void GoToBookmark()
        {
            PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;

            //Get the complete bookmarks in the PDF.
            PdfBookmarkBase bookmarks = pdfLoadedDocument.Bookmarks;

            //In this example, we get the first bookmark in the PDF bookmarks collection at the index of 0.
            PdfBookmark firstBookmark = bookmarks[0];

            //Navigates to the first bookmark present in the PDF.
            pdfViewer.GoToBookmark(firstBookmark);
        }

        /// <summary>
        /// Navigates the control to the child bookmark of a bookmark
        /// </summary>
        void GoToChildBookmark()
        {
            PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;

            //Get the complete bookmarks in the PDF.
            PdfBookmarkBase bookmarks = pdfLoadedDocument.Bookmarks;

            //Gets the fourth bookmark in the PDF bookmarks collection at the index of 3.
            PdfBookmark fourthBookmark = bookmarks[3];

            //Check whether it has child bookmarks.
            if (fourthBookmark.Count > 0)
            {
                //Navigates to the first child of the fourth bookmark in the PDF.
                pdfViewer.GoToBookmark(bookmarks[3][0]);
            }
        }

        private void PdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            //Call the logic to go to bookmark
            GoToBookmark();
        }
    }
}
