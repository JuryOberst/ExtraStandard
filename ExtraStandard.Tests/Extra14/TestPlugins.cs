using System;
using System.IO;
using System.Reflection;
using System.Xml;

using ExtraStandard.Extra14;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestPlugins
    {
        [Fact]
        public void DeserializePluginCertificates14()
        {
            var certificates = ExtraUtilities.Deserialize<CertificatesType>(LoadData("plugin-certificates.xml"));
            Assert.NotNull(certificates);
            Assert.True(certificates.versionSpecified);
            Assert.Equal(CertificatesTypeVersion.Item10, certificates.version);
            Assert.NotNull(certificates.X509EncCertificate);
            Assert.Equal(1, certificates.X509EncCertificate.Length);
            var cert = certificates.X509EncCertificate[0];
            Assert.NotNull(cert);
            Assert.NotNull(cert.Value);
            Assert.Equal("b64value", Convert.ToBase64String(cert.Value));
        }

        [Fact]
        public void DeserializePluginContacts14()
        {
            var contacts = ExtraUtilities.Deserialize<ContactsType>(LoadData("plugin.contact.xml"));
            Assert.NotNull(contacts);
            Assert.True(contacts.versionSpecified);
            Assert.Equal(ContactsTypeVersion.Item10, contacts.version);
            Assert.NotNull(contacts.SenderContact);
            Assert.Equal(2, contacts.SenderContact.Length);

            var contact = contacts.SenderContact[0];
            Assert.NotNull(contact);
            Assert.Equal("Technischer Support 24/7", contact.usageHint);
            Assert.NotNull(contact.Endpoint);
            Assert.Equal(1, contact.Endpoint.Length);
            var endpoint = contact.Endpoint[0];
            Assert.NotNull(endpoint);
            Assert.Equal(EndpointTypeType.SMTP, endpoint.type);
            Assert.Equal("support@extra.is.great.com", endpoint.Value);

            contact = contacts.SenderContact[1];
            Assert.NotNull(contact);
            Assert.Equal("Allgemeine Informationen", contact.usageHint);
            Assert.NotNull(contact.Endpoint);
            Assert.Equal(1, contact.Endpoint.Length);
            endpoint = contact.Endpoint[0];
            Assert.NotNull(endpoint);
            Assert.Equal(EndpointTypeType.SMTP, endpoint.type);
            Assert.Equal("info@extra.is.great.com", endpoint.Value);
        }

        [Fact]
        public void DeserializePluginDataSource14()
        {
            var datasource = ExtraUtilities.Deserialize<DataSourceType>(LoadData("plugin.datasource.xml"));
            Assert.NotNull(datasource);
            Assert.True(datasource.versionSpecified);
            Assert.Equal(DataSourceTypeVersion.Item10, datasource.version);

            var dataContainer = datasource.DataContainer;
            Assert.NotNull(dataContainer);
            Assert.Equal("http://www.extra-standard.de/container/FILE", dataContainer.type);
            Assert.Equal("abc.xml", dataContainer.name);
            Assert.True(dataContainer.createdSpecified);
            Assert.Equal(XmlConvert.ToDateTime("2008-11-07T12:48:26", XmlDateTimeSerializationMode.Local), dataContainer.created);
            Assert.Equal("UTF-8", dataContainer.encoding);

            var dataSet = dataContainer.DataSet;
            Assert.NotNull(dataSet);
            Assert.Equal("abcde", dataSet.name);
            Assert.Equal("KKS-Auftragssatz", dataSet.type);
            Assert.True(dataSet.lastModifiedSpecified);
            Assert.Equal(XmlConvert.ToDateTime("2008-11-04T09:34:11", XmlDateTimeSerializationMode.Local), dataSet.lastModified);
        }

        [Fact]
        public void DeserializePluginDataTransforms14()
        {
            var dataTransforms = ExtraUtilities.Deserialize<DataTransformsType>(LoadData("plugin.datatransforms.xml"));
            Assert.NotNull(dataTransforms);
            Assert.True(dataTransforms.versionSpecified);
            Assert.Equal(DataTransformsTypeVersion.Item12, dataTransforms.version);

            Assert.NotNull(dataTransforms.Compression);
            Assert.NotNull(dataTransforms.Encryption);
            Assert.NotNull(dataTransforms.Signature);

            Assert.Equal(1, dataTransforms.Compression.Length);
            Assert.Equal(1, dataTransforms.Encryption.Length);
            Assert.Equal(1, dataTransforms.Signature.Length);
            {
                var item = dataTransforms.Compression[0];
                Assert.NotNull(item);
                Assert.Equal("1", item.order);
                Assert.NotNull(item.InputData);
                Assert.Equal("6456734", item.InputData.bytes);
                Assert.NotNull(item.OutputData);
                Assert.Equal("345933", item.OutputData.bytes);
                Assert.NotNull(item.Algorithm);
                Assert.Equal("http://www.extra-standard.de/transforms/compression#GZIP", item.Algorithm.id);
                AbstractAlgorithmType algorithm = item.Algorithm;
                Assert.Equal("GZIP", algorithm.name);
                Assert.Equal(string.Empty, algorithm.version);
                var specification = algorithm.Specification;
                Assert.NotNull(specification);
                Assert.Equal("Security...", specification.name);
                Assert.Equal("http://my.domain.de/resource", specification.url);
                Assert.Equal("1.5.1", specification.version);
            }

            {
                var item = dataTransforms.Encryption[0];
                Assert.NotNull(item);
                Assert.Equal("2", item.order);
                Assert.NotNull(item.InputData);
                Assert.Equal("345933", item.InputData.bytes);
                Assert.NotNull(item.OutputData);
                Assert.Equal("386003", item.OutputData.bytes);
                Assert.NotNull(item.Algorithm);
                Assert.Equal("http://www.extra-standard.de/transforms/encryption/PKCS7", item.Algorithm.id);
                var algorithm = item.Algorithm;
                Assert.Equal("PKCS#7", algorithm.name);
                Assert.Equal("1.5", algorithm.version);
                var specification = algorithm.Specification;
                Assert.NotNull(specification);
                Assert.Equal("Security...", specification.name);
                Assert.Equal("http://my.domain.de/resource", specification.url);
                Assert.Equal("1.5.1", specification.version);
            }

            {
                var item = dataTransforms.Signature[0];
                Assert.NotNull(item);
                Assert.Equal("3", item.order);
                Assert.NotNull(item.X509Certificate);
                Assert.NotNull(item.X509Certificate.Value);
                Assert.Equal("b64value", Convert.ToBase64String(item.X509Certificate.Value));
                Assert.NotNull(item.Algorithm);
                Assert.Equal("http://www.extra-standard.de/transforms/encryption/PKCS7", item.Algorithm.id);
                var algorithm = item.Algorithm;
                Assert.Equal("PKCS#7", algorithm.name);
                Assert.Equal("1.5", algorithm.version);
                var specification = algorithm.Specification;
                Assert.NotNull(specification);
                Assert.Equal("Security...", specification.name);
                Assert.Equal("http://my.domain.de/resource", specification.url);
                Assert.Equal("1.5.1", specification.version);
            }
        }

        private static byte[] LoadData(string resourceName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resName = $"ExtraStandard.Tests.Resources.Extra14.{resourceName}";
            var output = new MemoryStream();
            using (var input = asm.GetManifestResourceStream(resName))
            {
                Assert.NotNull(input);
                input.CopyTo(output);
            }
            return output.ToArray();
        }
    }
}
