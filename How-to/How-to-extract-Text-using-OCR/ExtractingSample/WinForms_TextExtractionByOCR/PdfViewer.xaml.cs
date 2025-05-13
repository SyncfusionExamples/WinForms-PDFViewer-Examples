using Syncfusion.OCRProcessor;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinForms_TextExtractionByOCR
{
    /// <summary>
    /// Interaction logic for PdfViewer.xaml
    /// </summary>
    public partial class PdfViewer : UserControl
    {
        string file;
        string tessaractBinariesPath;
        string tessDataPath;
        RectangleF bounds;
        public PdfViewer()
        {
            InitializeComponent();
#if NETCOREAPP
            file = "../../../Data/F#.pdf";
#else
            file = "../../Data/F#.pdf";
#endif
            pdfViewer.Load(file);
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.AnnotationMode = Syncfusion.Windows.PdfViewer.PdfDocumentView.PdfViewerAnnotationMode.Rectangle;
            pdfViewer.ShapeAnnotationChanged += PdfViewer_ShapeAnnotationChanged;
        }

        private void PdfViewer_ShapeAnnotationChanged(object sender, Syncfusion.Windows.PdfViewer.ShapeAnnotationChangedEventArgs e)
        {
            if (e.Action == Syncfusion.Windows.PdfViewer.AnnotationChangedAction.Add)
            {
#if NETCOREAPP
                tessaractBinariesPath = "../../../Tesseract binaries";
                tessDataPath = @"../../../Tessdata/";
#else
                tessaractBinariesPath = "../../Tesseract binaries";
                tessDataPath = @"../../Tessdata/";
#endif
                bounds = e.NewBounds;
                PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;
                using (OCRProcessor processor = new OCRProcessor(tessaractBinariesPath))
                {
                    //Language to process the OCR
                    processor.Settings.Language = Languages.English;
                    Bitmap image = GetBitmap(pdfViewer.ExportAsImage(pdfViewer.CurrentPageIndex - 1));
                    using (Bitmap clonedImage = image.Clone(bounds, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
                    {
                        string ocrText = processor.PerformOCR(clonedImage, tessDataPath);
                    }
                    image.Dispose();
                }
            }

        }

        Bitmap GetBitmap(BitmapSource source)
        {
            Bitmap bmp = new Bitmap(
              source.PixelWidth,
              source.PixelHeight,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            BitmapData data = bmp.LockBits(
              new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
              ImageLockMode.WriteOnly,
              System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            source.CopyPixels(
              Int32Rect.Empty,
              data.Scan0,
              data.Height * data.Stride,
              data.Stride);
            bmp.UnlockBits(data);
            return bmp;
        }
    }
}
