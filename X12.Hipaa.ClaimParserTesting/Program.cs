using Fonet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using X12.Hipaa.Claims;
using X12.Hipaa.Claims.Services;
using X12.Parsing;

namespace X12.Hipaa.ClaimParserTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            bool throwException = Convert.ToBoolean(ConfigurationManager.AppSettings["ThrowExceptionOnSyntaxErrors"]);

            //var opts = new ExecutionOptions(args);
            var institutionalClaimToUb04ClaimFormTransformation = new InstitutionalClaimToUb04ClaimFormTransformation("UB04_Red.gif");
            var service = new ClaimFormTransformationService(
                new ProfessionalClaimToHcfa1500FormTransformation("HCFA1500_Red.gif"),
                institutionalClaimToUb04ClaimFormTransformation,
                new DentalClaimToJ400FormTransformation("ADAJ400_Red.gif"),
                new X12Parser(throwException));




            string filename = @"E:\Projects\_temp\EdiFiles\EDI_INS_202009131900.edi";
            string OutputPath = @"E:\Projects\_temp\Output";
            bool MakePdf = true;
            try
            {
#if DEBUG
                var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                var parser = new X12Parser();
                var interchange = parser.ParseMultiple(stream).First();
                File.WriteAllText(filename + ".dat", interchange.SerializeToX12(true));
                stream.Close();
#endif
                DateTime start = DateTime.Now;
                var inputFilestream = new FileStream(filename, FileMode.Open, FileAccess.Read);

                var revenueDictionary = new Dictionary<string, string>
                {
                    ["0572"] = "Test Code"
                };
                service.FillRevenueCodeDescriptionMapping(revenueDictionary);
                var claimDoc = service.Transform837ToClaimDocument(inputFilestream);
                institutionalClaimToUb04ClaimFormTransformation.PerPageTotalChargesView = true;
                var fi = new FileInfo(filename);
                var di = new DirectoryInfo(OutputPath);

                //if (opts.MakeXml)
                //{
                //    string outputFilename = $"{di.FullName}\\{fi.Name}.xml";
                //    string xml = claimDoc.Serialize();
                //    xml = xml.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
                //    File.WriteAllText(outputFilename, xml);
                //}

                if (MakePdf)
                {
                    string outputFilename = $"{di.FullName}\\{fi.Name}.pdf";
                    using (FileStream pdfOutput = new FileStream(outputFilename, FileMode.Create, FileAccess.Write))
                    {
                        var foDoc = new XmlDocument();
                        string foXml = service.TransformClaimDocumentToFoXml(claimDoc);
                        foDoc.LoadXml(foXml);

                        FonetDriver driver = FonetDriver.Make();
                        driver.Render(foDoc, pdfOutput);
                        pdfOutput.Close();
                    }
                }

                Console.WriteLine($"{filename} parsed in {DateTime.Now - start}.");
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Exception occurred: {exc.GetType().FullName}.  {exc.Message}.  {exc.StackTrace}");
            }
            //    var stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //    var parser = new X12Parser();
            //    var interchange = parser.ParseMultiple(stream).First();
            //    File.WriteAllText(filename + ".dat", interchange.SerializeToX12(true));
            //    stream.Close();

            //}
            //private ClaimDocument Transform837ToClaimDocument(Stream stream)
            //{
            //    var parser = new X12Parser();
            //    var interchanges = parser.ParseMultiple(stream);
            //    var doc = new ClaimDocument();
            //    foreach (var interchange in interchanges)
            //    {
            //        var thisDoc = Transform837ToClaimDocument(interchange);
            //        this.AddRevenueCodeDescription(thisDoc);
            //        doc.Claims.AddRange(thisDoc.Claims);
            //    }

            //    return doc;
            //}
            Console.ReadLine();
        }
    }
}
