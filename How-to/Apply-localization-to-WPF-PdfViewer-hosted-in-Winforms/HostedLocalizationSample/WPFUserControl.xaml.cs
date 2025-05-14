using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HostedLocalizationSample
{
    /// <summary>
    /// Interaction logic for WPFUserControl.xaml
    /// </summary>
    public partial class WPFUserControl : UserControl
    {
        string path;
        public WPFUserControl()
        {
            InitializeComponent();
            //set the current culture to the en
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            //Set the custom class library assmebly and and namespace for the localization
            LocalizationManager.SetResources(
                typeof(LocalizationClassLibrary.Class1).Assembly,
                "LocalizationClassLibrary"
            );
#if NETCOREAPP
            path = "../../../Data/F#.pdf";
#else
            path = "../../Data/F#.pdf";
#endif
            //Load the pdf
            pdfViewer.Load(path);

        }
    }
}
