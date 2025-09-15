#region Copyright Syncfusion Inc. 2001 - 2011
// Copyright Syncfusion Inc. 2001 - 2011. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.Forms;

namespace GettingStarted
{
    public partial class Form1 : MetroForm
    {
        
        public Form1()
        {
            InitializeComponent();
            //Load the PDF document in PdfLoaded document
            PdfLoadedDocument document = new PdfLoadedDocument("../../../Data/Input.pdf");

            //Flatten the annotation using FlattenAnnotations method.
            document.FlattenAnnotations();

            //Flatten the form fields using FlattenFields method.
            document.Form.FlattenFields();

            //Load the PDF document in PdfViewer control.
            pdfViewerControl1.Load(document);
        }
    }
}