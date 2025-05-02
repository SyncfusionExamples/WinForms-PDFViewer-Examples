using System;
using System.Collections.Generic;
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

namespace HostedPdfViewer
{
    /// <summary>
    /// Interaction logic for WPFPdfViewer.xaml
    /// </summary>
    public partial class WPFPdfViewer : UserControl
    {
        string filePath = string.Empty;
        public WPFPdfViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the user control is loaded.
        /// </summary>
        /// <param name="sender">User control</param>
        /// <param name="e">Routed Event arguments</param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Load a PDF file in the PDF viewer
#if !NETCOREAPP
            filePath = "../../Data/F#.pdf";
#else
            filePath = "../../../Data/F#.pdf";
#endif
            pdfViewer.Load(filePath);
        }
    }
}
