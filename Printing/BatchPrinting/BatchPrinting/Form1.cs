using Syncfusion.Windows.Forms.PdfViewer;

namespace BatchPrinting
{
    public partial class Form1 : Form
    {
        private string pdfFolder = @"../../../Data";
        PdfViewerControl viewer;

        private string[] files = [];
        private int currentIndex = 0;

        public Form1()
        {
            InitializeComponent();

            viewer = new PdfViewerControl();
            viewer.EndPrint += Viewer_EndPrint;

            this.Shown += Form1_Shown;
        }
        private void Form1_Shown(object? sender, EventArgs e)
        {
            LoadFilesAndStartPrinting();
        }
        private void LoadFilesAndStartPrinting()
        {
            try
            {
                if (!Directory.Exists(pdfFolder))
                {
                    return;
                }

                files = Directory
                    .GetFiles(pdfFolder, "*.pdf")
                    .ToArray();

                if (files.Length == 0)
                {
                    return;
                }

                currentIndex = 0;
                PrintCurrentFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load PDF files.\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void PrintCurrentFile()
        {
            if (currentIndex >= files.Length)
            {
                MessageBox.Show(
                    "Batch printing completed.",
                    "Completed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            try
            {
                string file = files[currentIndex];

                viewer.Load(file);
                viewer.Print(false);
            }
            catch (Exception ex)
            {
                // Log error and continue with next file
                MessageBox.Show(
                    $"Failed to print:\n{files[currentIndex]}\n\n{ex.Message}",
                    "Print Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void Viewer_EndPrint(object? sender, EndPrintEventArgs e)
        {
            currentIndex++;
            BeginInvoke(PrintCurrentFile);
        }
    }
}
