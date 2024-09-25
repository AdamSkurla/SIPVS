using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace XmlToPdfApp.Controllers
{
    public class XmlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateXml(string rootElement, string childElement, string childValue)
        {
            // Create a simple XML document
            XDocument xmlDocument = new XDocument(
                new XElement(rootElement,
                    new XElement(childElement, childValue)
                )
            );

            // Save XML to a string (you can also save to a file if needed)
            string xmlString = xmlDocument.ToString();

            // Convert XML to a MemoryStream for PDF generation
            MemoryStream xmlStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(xmlStream);
            writer.Write(xmlString);
            writer.Flush();
            xmlStream.Position = 0;

            // Option to Convert to PDF
            var pdfBytes = ConvertXmlToPdf(xmlString);

            // Download PDF
            return File(pdfBytes, "application/pdf", "generated.pdf");
        }

        // Method to convert XML to PDF
        private byte[] ConvertXmlToPdf(string xml)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(ms);
                PdfDocument pdfDoc = new PdfDocument(writer);
                Document document = new Document(pdfDoc);

                // Adding XML content to PDF as paragraphs
                document.Add(new Paragraph("XML Data"));
                document.Add(new Paragraph(xml));

                document.Close();
                return ms.ToArray();
            }
        }
    }
}
