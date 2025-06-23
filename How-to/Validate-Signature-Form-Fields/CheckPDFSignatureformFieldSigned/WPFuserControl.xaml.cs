using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using UserControl = System.Windows.Controls.UserControl;

namespace CheckPDFSignatureformFieldSigned
{
    /// <summary>
    /// Interaction logic for WPFuserControl.xaml
    /// </summary>
    public partial class WPFuserControl : UserControl
    {
        public WPFuserControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            pdfViewer.IsEnabled = false;
        }

        public void LoadDocument(string inputFile, string outputFile)
        {
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(inputFile);

            var pages = loadedDocument.Pages;
            PdfLoadedPage page = pages[0] as PdfLoadedPage;

            PdfSignatureField signature = new PdfSignatureField(page, "Signature");
            signature.Bounds = new RectangleF(155, 590, 150, 24);
            signature.ToolTip = "Sign Here";
            loadedDocument.Form.Fields.Add(signature);

            PdfTextBoxField vehicleEntry = new PdfTextBoxField(page, "VehicleEntry");
            vehicleEntry.Bounds = new RectangleF(128, 400, 75, 24);
            vehicleEntry.BorderColor = new Syncfusion.Pdf.Graphics.PdfColor(0, 1, 1, 0);
            //Add the form field to the document.
            loadedDocument.Form.Fields.Add(vehicleEntry);

            PdfSignatureField initials = new PdfSignatureField(page, "Initials");
            initials.Bounds = new RectangleF(40, 775, 75, 24);
            initials.ToolTip = "Initials Here";
            loadedDocument.Form.Fields.Add(initials);

            using (FileStream output = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                loadedDocument.Save(output);
            }

            pdfViewer.Load(outputFile);
            pdfViewer.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitPage;
            pdfViewer.Visibility = Visibility.Visible;
            pdfViewer.IsEnabled = true;
        }
    }
}
