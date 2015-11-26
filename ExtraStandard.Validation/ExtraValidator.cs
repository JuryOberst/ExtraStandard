using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace ExtraStandard.Validation
{
    /// <summary>
    /// Standard-Implementation von <see cref="IExtraValidator"/>
    /// </summary>
    /// <remarks>
    /// Diese Standard-Implementation fügt vor der Validierung einen Verweis auf die XML-Schema-Datei hinzu.
    /// </remarks>
    public abstract class ExtraValidator : IExtraValidator
    {
        /// <summary>
        /// Validiert das eXTra-Dokument
        /// </summary>
        /// <param name="document">Das zu validierende Dokument</param>
        /// <remarks>
        /// Diese Funktion ruft üblicherweise <see cref="Validate(XDocument, ResourceExtraXmlResolver, string)"/>
        /// für die Validierung auf. Die Hauptaufgabe dieser Funktion ist also die Erstellung einer Instanz
        /// von <see cref="ResourceExtraXmlResolver"/> und die Ermittlung der Basis-XML-Schema-Datei anhand derer
        /// das Dokument validiert werden soll.
        /// </remarks>
        public abstract void Validate(XDocument document);

        /// <summary>
        /// Validiert das eXTra-Dokument
        /// </summary>
        /// <param name="documentData">Die Binär-Fassung des zu validierenden eXTra-Dokuments</param>
        public void Validate(byte[] documentData)
        {
            using (var stream = new MemoryStream(documentData))
            {
                var doc = XDocument.Load(stream, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
                Validate(doc);
            }
        }

        /// <summary>
        /// Validiert das eXTra-Dokument
        /// </summary>
        /// <param name="document">Das zu validierende eXTra-Dokument</param>
        /// <param name="resolver">Der <see cref="ResourceExtraXmlResolver"/> der für die Validierung der
        /// vom <paramref name="schemaFileName"/> importierten XSD-Dateien</param>
        /// <param name="schemaFileName">Die Basis-XSD-Datei anhand derer das eXTra-Dokument validiert wird</param>
        protected void Validate(XDocument document, ResourceExtraXmlResolver resolver, string schemaFileName)
        {
            document = FixDocumentSchema(document, resolver, schemaFileName);
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                ValidationFlags = XmlSchemaValidationFlags.ProcessSchemaLocation,
                XmlResolver = resolver,
            };

            var output = new MemoryStream();

            var writer = XmlWriter.Create(output, new XmlWriterSettings()
            {
                Indent = false,
            });
            document.Save(writer);
            writer.Flush();
            output.Seek(0, SeekOrigin.Begin);

            var sourceUri = new Uri(resolver.RootUrl, "test.xml");
            var reader = XmlReader.Create(output, settings, sourceUri.ToString());

            while (reader.Read())
            {
            }
        }

        private static XDocument FixDocumentSchema(XDocument doc, ResourceExtraXmlResolver resolver, string schemaFileName)
        {
            XDocument result = CopyXmlDocument(doc);
            var nsXsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
            if (result.Root != null)
            {
                if (result.Root.Attribute(XNamespace.Xmlns + "xsi") == null)
                    result.Root.SetAttributeValue(XNamespace.Xmlns + "xsi", nsXsi);

                if (string.IsNullOrEmpty(result.Root.Name.NamespaceName))
                {
                    if (result.Root.Attribute(nsXsi + "noNamespaceSchemaLocation") == null)
                    {
                        var schemaUri = new Uri(resolver.RootUrl, schemaFileName);
                        var schemaLocation = schemaUri.ToString();
                        result.Root.SetAttributeValue(nsXsi + "xsi:noNamespaceSchemaLocation", schemaLocation);
                    }
                }
                else
                {
                    if (result.Root.Attribute(nsXsi + "schemaLocation") == null)
                    {
                        var schemaUri = new Uri(resolver.RootUrl, schemaFileName);
                        var schemaLocation = string.Format("{1} {0}", schemaUri, result.Root.Name.NamespaceName);
                        result.Root.SetAttributeValue(nsXsi + "schemaLocation", schemaLocation);
                    }
                }
            }
            return result;
        }

        private static XDocument CopyXmlDocument(XDocument document)
        {
            var output = new MemoryStream();
            document.Save(output);
            output.Seek(0, SeekOrigin.Begin);

            var result = XDocument.Load(output, LoadOptions.PreserveWhitespace | LoadOptions.SetLineInfo);
            return result;
        }
    }
}
