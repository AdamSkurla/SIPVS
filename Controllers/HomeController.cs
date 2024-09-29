using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace SIPVS.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Generovanie XML
        [HttpPost]
        public IActionResult GenerateXml(string zamestnavatel, string priezviskoMenoTitul, string bydlisko, 
                                        string osobneCislo, string utvar, string telefon, string pracovnyCas, 
                                        string odTime, string doTime, // Pridanie parametrov pre 'Od' a 'Do'
                                        string[] zaciatokCesty, string[] mestoRokovania, string[] ucelCesty, string[] koniecCesty,
                                        string spolucestujuci, string dopravnyProstriedok, string ciastkaVydavkov, string preddavok,
                                        string podpisPokladnika, string datumPodpisPokladnika)
        {
            // Definovanie namespace
            XNamespace ns = "http://travelorder.example.com/sipvs";

            // Zabezpečenie formátu času HH:MM:SS
            if (!string.IsNullOrEmpty(odTime) && odTime.Length == 5) // Ak je čas vo formáte HH:MM
            {
                odTime += ":00"; // Pridáme sekundy
            }

            if (!string.IsNullOrEmpty(doTime) && doTime.Length == 5) // Ak je čas vo formáte HH:MM
            {
                doTime += ":00"; // Pridáme sekundy
            }

            // Vytvorenie XML dokumentu s opakujúcimi sa sekciami Cesta a pridaním namespace
            XDocument xmlDocument = new XDocument(
                new XElement(ns + "CestovnyPrikaz",
                    new XElement(ns + "Zamestnavatel", zamestnavatel),
                    new XElement(ns + "PriezviskoMenoTitul", priezviskoMenoTitul),
                    new XElement(ns + "Bydlisko", bydlisko),
                    new XElement(ns + "OsobneCislo", osobneCislo),
                    new XElement(ns + "Utvar", utvar),
                    new XElement(ns + "Telefon", telefon),
                    new XElement(ns + "PracovnyCas", pracovnyCas),
                    new XElement(ns + "Od", odTime), 
                    new XElement(ns + "Do", doTime), 
                    new XElement(ns + "Cesty", 
                        from i in Enumerable.Range(0, zaciatokCesty.Length)
                        select new XElement(ns + "Cesta",
                            new XElement(ns + "ZaciatokCesty", zaciatokCesty[i]),
                            new XElement(ns + "MestoRokovania", mestoRokovania[i]),
                            new XElement(ns + "UcelCesty", ucelCesty[i]),
                            new XElement(ns + "KoniecCesty", koniecCesty[i])
                        )
                    ),
                    new XElement(ns + "Spolucestujuci", spolucestujuci),
                    new XElement(ns + "DopravnyProstriedok", dopravnyProstriedok),
                    new XElement(ns + "CiastkaVydavkov", ciastkaVydavkov),
                    new XElement(ns + "Preddavok", preddavok),
                    new XElement(ns + "PodpisPokladnika", podpisPokladnika),
                    new XElement(ns + "DatumPodpisPokladnika", datumPodpisPokladnika)
                )
            );

            // Uloženie XML do wwwroot
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cestovnyprikaz.xml");
            try
            {
                xmlDocument.Save(xmlPath);
            }
            catch (Exception ex)
            {
                return Content("Chyba pri ukladaní XML: " + ex.Message);
            }

            return Content("XML bolo úspešne uložené do " + xmlPath);
        }

        // Overenie XML voči XSD
        [HttpPost]
        public IActionResult ValidateXml()
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cestovnyprikaz.xml");
            string xsdPath = Path.Combine(Directory.GetCurrentDirectory(), "Schema", "travelorder.xsd");

            // namespace
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("http://travelorder.example.com/sipvs", xsdPath);

            XDocument xmlDocument;
            try
            {
                xmlDocument = XDocument.Load(xmlPath);  // Načítanie XML súboru
            }
            catch (Exception ex)
            {
                return Content ("Chyba pri načítaní XML: " + ex.Message );
            }

            string validationResult = string.Empty;
            try
            {
                // Spustenie validácie
                xmlDocument.Validate(schemas, (sender, e) =>
                {
                    validationResult += e.Message + "\n";
                });
            }
            catch (Exception ex)
            {
                return Content ("Chyba pri validácií XML: " + ex.Message );
            }

            if (string.IsNullOrEmpty(validationResult))
            {
                return Content ("XML je platné!" );
            }
            else
            {
                return Content("XML obsahuje chyby: " + validationResult );
            }
        }

        // Transformácia XML do HTML
        [HttpPost]
        public IActionResult TransformXmlToHtml()
        {
            string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cestovnyprikaz.xml");
            string xslPath = Path.Combine(Directory.GetCurrentDirectory(), "Schema", "travelorder.xsl");

            XslCompiledTransform xslTransform = new XslCompiledTransform();
            try
            {
                xslTransform.Load(xslPath);
            }
            catch (Exception ex)
            {
                return Content("Chyba pri načítaní XSL súboru: " + ex.Message);
            }

            string htmlOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cestovnyprikaz.html");
            try
            {
                using (XmlReader reader = XmlReader.Create(xmlPath))
                using (StreamWriter writer = new StreamWriter(htmlOutputPath))
                {
                    xslTransform.Transform(reader, null, writer);
                }
            }
            catch (Exception ex)
            {
                return Content("Chyba pri transformácii XML na HTML: " + ex.Message);
            }

            return Content("XML bolo úspešne transformované do HTML a uložené do " + htmlOutputPath);
        }
    }
}
