using System;
using System.Linq;

using ExtraStandard.Extra11;
using ExtraStandard.Validation;
using ExtraStandard.Validation.Extra11;

using Xunit;

namespace ExtraStandard.Tests.Extra11
{
    public class TestXmlError
    {
        private readonly byte[] _xmlData;

        public TestXmlError()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var resName = asm.GetManifestResourceNames().Single(x => x.EndsWith(".Extra11.xml_KomServer_0_error.xml"));
            using (var temp = new System.IO.MemoryStream())
            {
                using (var resStream = asm.GetManifestResourceStream(resName))
                {
                    Assert.NotNull(resStream);
                    resStream.CopyTo(temp);
                }

                _xmlData = temp.ToArray();
            }
        }

        [Fact]
        public void DeserializeError()
        {
            var error = ExtraUtilities.Deserialize<ExtraErrorType>(_xmlData);
            Assert.NotNull(error);
            Assert.True(error.versionSpecified);
            Assert.Equal(SupportedVersionsType.Item11, error.version);
            Assert.NotNull(error.ResponseDetails);
            Assert.NotNull(error.ResponseDetails.ResponseID);
            Assert.Equal("String", error.ResponseDetails.ResponseID.@class);
            Assert.Equal("A(3794b25e-cafe-4dbe-872f-513c2e6d7ff8)", error.ResponseDetails.ResponseID.Value);
            Assert.Equal(new DateTime(2011, 1, 25, 12, 15, 57), error.ResponseDetails.TimeStamp);
            Assert.NotNull(error.ResponseDetails.Report);
            Assert.Equal(ExtraFlagWeight.Error, error.ResponseDetails.Report.highestWeight);
            Assert.NotNull(error.ResponseDetails.Report.Flag);
            Assert.Equal(1, error.ResponseDetails.Report.Flag.Length);
            Assert.Equal(ExtraFlagWeight.Error, error.ResponseDetails.Report.Flag[0].weight);
            Assert.Equal("E003", error.ResponseDetails.Report.Flag[0].Code.Value);
            Assert.Equal("Fehler in der eXTra-XML Struktur: Bei der XSD-Prüfung der gesendeten XML-Nachricht sind folgende Fehler aufgetreten:  # Zeile: 10 - Position: 5 - Das Element 'Receiver' in Namespace 'http://www.extra-standard.de/namespace/components/1' hat ein ungültiges untergeordnetes Element 'Name' in Namespace 'http://www.extra-standard.de/namespace/components/1'. Erwartet wurde die Liste möglicher Elemente: 'ReceiverID' in Namespace 'http://www.extra-standard.de/namespace/components/1'. ## FehlerCode: E003 ## Workflow-ID: A(3794b25e-5d4d-4dbe-872f-513c2e6d7ff8)", error.ResponseDetails.Report.Flag[0].Text.Value);
        }

        [Fact]
        public void ValidateError()
        {
            var validator = new GenericExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Response, true);
            validator.Validate(_xmlData);
        }
    }
}
