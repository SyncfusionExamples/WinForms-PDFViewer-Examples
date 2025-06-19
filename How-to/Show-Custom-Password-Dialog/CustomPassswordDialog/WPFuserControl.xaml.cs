using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

namespace CustomPassswordDialog
{
    /// <summary>
    /// Interaction logic for WPFuserControl.xaml
    /// </summary>
    public partial class WPFuserControl : UserControl
    {
        private passwordBox passwordDialogBox;
        public WPFuserControl()
        {
            InitializeComponent();
        }

        private void pdfViewer_GetDocumentPassword(object sender, Syncfusion.Windows.PdfViewer.GetDocumentPasswordEventArgs e)
        {
            passwordDialogBox = new passwordBox();
            passwordDialogBox.ShowDialog();
            if(passwordDialogBox.DialogResult == System.Windows.Forms.DialogResult.OK && !string.IsNullOrEmpty(passwordDialogBox.password))
            {
                SecureString secureString = ConvertToSecureString(passwordDialogBox.password);
                e.Password = secureString;
            }
            e.Handled = true;
        }

        private SecureString ConvertToSecureString( string secureString)
        {
            SecureString passsword = new SecureString();
            foreach(char c in secureString)
            {
                passsword.AppendChar(c);
            }
            passsword.MakeReadOnly();
            return passsword;
        }

        private void pdfViewer_ErrorOccurred(object sender, Syncfusion.Windows.PdfViewer.ErrorOccurredEventArgs args)
        {
            if(passwordDialogBox.DialogResult == System.Windows.Forms.DialogResult.OK && args.Message == "Can't open an encrypted document. The password is invalid.")
            {
               System.Windows.Forms.MessageBox.Show(args.Message);
                pdfViewer.Load(Form1.PreLoadPdf);
            }
        }
    }
}
