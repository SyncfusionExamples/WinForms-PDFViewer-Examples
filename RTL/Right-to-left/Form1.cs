using System.Windows.Forms;


namespace Right_to_left
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pdfViewerControl1.RightToLeft = RightToLeft.Yes;
            pdfViewerControl1.Load(@"../../../RTLText.pdf");
        }
    }
}
