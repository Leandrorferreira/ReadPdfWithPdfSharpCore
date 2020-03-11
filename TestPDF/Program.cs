using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.AcroForms;
using PdfSharpCore.Pdf.IO;
using System;

namespace TestPDF
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Templates_POC\template.pdf";

            var doc = PdfReader.Open(path, PdfDocumentOpenMode.Modify);

            if(doc.AcroForm == null)
            {
                Console.WriteLine("Error, PDF Form Not Found!");
                return ;
            }

            PdfAcroForm form = doc.AcroForm;

            if (form.Elements.ContainsKey("/NeedAppearances"))
            {
                form.Elements["/NeedAppearances"] = new PdfBoolean(true);
            }
            else
            {
                form.Elements.Add("/NeedAppearances", new PdfBoolean(true));
            }

            //Create fields 
            PdfTextField merchantPorfileOwnerField = (PdfTextField)(form.Fields["merchant_profile_owners_email_2"]);
            PdfCheckBoxField checkField = (PdfCheckBoxField)(form.Fields["seasson_Mar"]);
                    
            merchantPorfileOwnerField.ReadOnly = false;
            checkField.ReadOnly = false;
            checkField.Checked = true;
            merchantPorfileOwnerField.Value = new PdfString("TEST ON C#");

            doc.Save(@"C:\Templates_POC\newcopy.pdf");
                       
            Console.WriteLine("Hello World! " + merchantPorfileOwnerField.Text);
        }
    }
}
