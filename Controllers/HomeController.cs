
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace SIPVS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Add a message that will be passed to the view
            ViewBag.Message = "Welcome to the XML to PDF Converter Web App!";
            return View();
        }

        [HttpPost]
        public IActionResult GenerateXml(string surname, string name, string birthNumber, 
                                         string street, string postalCode, string city, 
                                         string childSurname, string childName, string childBirthDate, 
                                         string schoolName, string schoolAddress, string schoolYear,
                                         string grade, string enrollmentDate, string academicYearStart,
                                         string schoolWorkerName, string signatureDate, string signature)
        {
            // Build XML document based on the form data
            XDocument xmlDocument = new XDocument(
                new XElement("Form",
                    new XElement("Applicant",
                        new XElement("Surname", surname),
                        new XElement("Name", name),
                        new XElement("BirthNumber", birthNumber),
                        new XElement("Address", 
                            new XElement("Street", street),
                            new XElement("PostalCode", postalCode),
                            new XElement("City", city)
                        )
                    ),
                    new XElement("Child",
                        new XElement("Surname", childSurname),
                        new XElement("Name", childName),
                        new XElement("BirthDate", childBirthDate)
                    ),
                    new XElement("SchoolInfo",
                        new XElement("SchoolName", schoolName),
                        new XElement("SchoolAddress", schoolAddress),
                        new XElement("SchoolYear", schoolYear),
                        new XElement("Grade", grade),
                        new XElement("EnrollmentDate", enrollmentDate),
                        new XElement("AcademicYearStart", academicYearStart),
                        new XElement("SchoolWorkerName", schoolWorkerName),
                        new XElement("SignatureDate", signatureDate),
                        new XElement("Signature", signature)
                    )
                )
            );

            // Validate the XML against the XSD
            string xsdPath = Path.Combine(Directory.GetCurrentDirectory(), "Schema", "form.xsd");
            string validationResult = ValidateXml(xmlDocument, xsdPath);

            // If valid, transform XML using XSLT (optional next step)
            if (string.IsNullOrEmpty(validationResult))
            {
                TempData["Xml"] = xmlDocument.ToString();
                return RedirectToAction("DisplayXml");
            }
            else
            {
                TempData["ValidationError"] = validationResult;
                return RedirectToAction("Index");
            }
        }

        private string ValidateXml(XDocument xml, string xsdPath)
        {
            string validationErrors = string.Empty;

            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", xsdPath);

            xml.Validate(schemas, (sender, e) =>
            {
                validationErrors += e.Message + "\n";
            });

            return validationErrors;
        }

        public IActionResult DisplayXml()
        {
            var xml = TempData["Xml"] as string;
            return Content(xml, "text/xml");
        }
    }
}
