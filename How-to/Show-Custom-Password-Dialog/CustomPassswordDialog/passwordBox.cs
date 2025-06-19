using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomPassswordDialog
{
    public partial class passwordBox : Form
    {
        public string password;
        public passwordBox()
        {
            InitializeComponent();
            password = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }

        private void Ok_Button_Click(object sender, EventArgs e)
        {
            password = PasswordTextBox.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_button_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
