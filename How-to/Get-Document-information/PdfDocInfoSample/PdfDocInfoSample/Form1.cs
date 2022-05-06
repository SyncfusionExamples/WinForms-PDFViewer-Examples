using System.Windows.Forms;

namespace PdfDocInfoSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Loads the PDF document in PDF Viewer
            pdfViewer.Load("../../Data/HTTP Succinctly.pdf");

            //Gets the filename of loaded PDF document
            string fileName = pdfViewer.DocumentInformation.FileName;

            //Gets the file path of loaded PDF document
            string filePath = pdfViewer.DocumentInformation.FilePath;
        }
    }
}
