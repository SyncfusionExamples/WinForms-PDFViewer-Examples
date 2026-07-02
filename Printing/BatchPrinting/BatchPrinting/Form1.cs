using Syncfusion.Windows.Forms.PdfViewer;

namespace BatchPrinting
{
    public partial class Form1 : Form
    {

        private System.Windows.Forms.Timer batchTimer;
        private string pdfFolder = @"../../../Data";
        private int intervalMinutes = 2;
        public Form1()
        {
            InitializeComponent();
            InitializeBatchProcess();
        }

        private void InitializeBatchProcess()
        {
            batchTimer = new System.Windows.Forms.Timer();
            batchTimer.Interval = intervalMinutes * 60 * 1000; // Convert minutes to ms
            batchTimer.Tick += BatchTimer_Tick;
            batchTimer.Start();
        }
        private async void BatchTimer_Tick(object sender, EventArgs e)
        {
            batchTimer.Stop(); 
            await Task.Run(() => PrintAllPdfFiles());
            batchTimer.Start();
        }
        private void PrintAllPdfFiles()
        {
            if (!Directory.Exists(pdfFolder))
                return;

            var files = Directory.GetFiles(pdfFolder, "*.pdf");
            PdfViewerControl viewer = new PdfViewerControl();
            foreach (string file in files)
            {
                viewer.Load(file);
                viewer.Print(false);
            }
        }
    }
}
