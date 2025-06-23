using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace CheckPDFSignatureformFieldSigned
{
    public partial class Form1 : Form
    {
        ElementHost elementHost = null;
        WPFuserControl wpfControl = null;
        string inputFilePath,outputFilePath;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            elementHost = new ElementHost();
            elementHost.Margin = new Padding(0, 0, 0, 0);
            elementHost.Region = new Region();
            elementHost.BackColor = System.Drawing.Color.White;
            elementHost.AutoSize = true;
            elementHost.Size = this.Size;
            elementHost.Dock = DockStyle.Fill;

            //this.Controls.Add(elementHost);
            tableLayoutPanel1.Controls.Add(elementHost);

            wpfControl = new WPFuserControl();
            elementHost.Child = wpfControl;

            this.WindowState = FormWindowState.Maximized;
        }

        private void btnSignature_Click(object sender, EventArgs e)
        {
#if NETCOREAPP
            inputFilePath = @"../../../Data/1.pdf";
            outputFilePath = @"../../../Data/1-output.pdf";
#else
            inputFilePath = @"../../Data/1.pdf";
            outputFilePath = @"../../Data/1-output.pdf";
#endif
            wpfControl.LoadDocument(inputFilePath, outputFilePath);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            bool isSigned = false;
            PdfLoadedDocument loadedDocument = wpfControl.pdfViewer.LoadedDocument;
            if (loadedDocument.Form != null && loadedDocument.Form.Fields.Count > 0)
            {
                foreach (var field in loadedDocument.Form.Fields)
                {
                    if (field is PdfLoadedSignatureField)
                    {
                        PdfLoadedSignatureField signatureField = field as PdfLoadedSignatureField;
                        if (signatureField.Page != null && signatureField.Page.Annotations.Count > 0)
                        {
                            foreach (var annotation in signatureField.Page.Annotations)
                            {
                                if (annotation is PdfLoadedInkAnnotation)
                                {
                                    PdfLoadedInkAnnotation signature = annotation as PdfLoadedInkAnnotation;
                                    if (signature.Name != signatureField.Name)
                                    {
                                        isSigned = false;
                                    }
                                    else
                                    {
                                        isSigned = true;
                                        break;
                                    }
                                }
                                else if (annotation is PdfInkAnnotation)
                                {
                                    PdfInkAnnotation signature = annotation as PdfInkAnnotation;
                                    if (signature.Name != signatureField.Name)
                                    {
                                        isSigned = false;
                                    }
                                    else
                                    {
                                        isSigned = true;
                                        break;
                                    }
                                }
                            }
                            if (!isSigned)
                            {
                                break;
                            }
                        }
                        else
                        {
                            isSigned = false;
                            break;
                        }
                    }
                }

                if (!isSigned)
                {
                    MessageBox.Show("There is some signature required");
                }
                else
                {
                    MessageBox.Show("All signatures are completed!");
                }
            }
        }
    }
}
