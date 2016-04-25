using System;
using System.IO;
using System.Xml.Schema;

using ExtraStandard.Extra14;
using ExtraStandard.GkvKomServer.Validation.Extra14;
using ExtraStandard.Validation;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestGkvMessages
    {
        [Fact]
        public void TestError()
        {
            var data = LoadData("KomServer-Error.xml");
            var document = ExtraUtilities.Deserialize<ExtraErrorType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.Error, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Fact]
        public void TestConfirmationBody()
        {
            var data = LoadData("KomServer-Empfangsquittung-Body.xml");
            var document = ExtraUtilities.Deserialize<ConfirmationOfReceiptType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.AcknowledgeProcessingResultQuery, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Empfangsquittung-Request-withOptional.xml")]
        [InlineData("KomServer-Empfangsquittung-Request-withoutOptional.xml")]
        public void TestConfirmationRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.AcknowledgeProcessingResult, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Empfangsquittung-Response-withOptional.xml")]
        [InlineData("KomServer-Empfangsquittung-Response-withoutOptional.xml")]
        public void TestConfirmationResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.AcknowledgeProcessingResult, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Meldung-Request-withOptional.xml")]
        [InlineData("KomServer-Meldung-Request-withOutOptional.xml")]
        public void TestMessageRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Meldung-Response-withOptional.xml")]
        [InlineData("KomServer-Meldung-Response-withoutOptional.xml")]
        public void TestMessageResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Fact]
        public void TestMessageResponseOnePackageRejected()
        {
            var data = LoadData("KomServer-Meldung-Response-EinPaketAbgelehnt.xml");
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Statusanfrage-Body-alleDavn-alleVerfahren.xml")]
        [InlineData("KomServer-Statusanfrage-Body-alleDavn-einVerfahren.xml")]
        [InlineData("KomServer-Statusanfrage-Body-eineDav-alleVerfahren.xml")]
        [InlineData("KomServer-Statusanfrage-Body-eineDav-einVerfahren.xml")]
        public void TestQueryBody(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<DataRequestType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.GetProcessingResultQuery, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Statusanfrage-Request-withOptional.xml")]
        [InlineData("KomServer-Statusanfrage-Request-withoutOptional.xml")]
        public void TestProcessingResultRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.GetProcessingResult, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("KomServer-Statusanfrage-Response-alleRueckmeldetypen-withOptional.xml")]
        [InlineData("KomServer-Statusanfrage-Response-alleRueckmeldetypen-withoutOptional.xml")]
        [InlineData("KomServer-Statusanfrage-Response-In24hAnfragen.xml")]
        [InlineData("KomServer-Statusanfrage-Response-KeineAntwortenVorhanden-withoutOptional.xml")]
        [InlineData("KomServer-Statusanfrage-Response-NurNormaleRueckmeldungen-withOptional.xml")]
        [InlineData("KomServer-Statusanfrage-Response-NurNormaleRueckmeldungen-withoutOptional.xml")]
        public void TestProcessingResultResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.GetProcessingResult, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("Custom.KomServer-Statusanfrage-Request-withProhibitedAttribute.xml")]
        [InlineData("Custom.invalid-01.xml")]
        public void TestFailure(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new GkvExtraValidator(ExtraMessageType.GetProcessingResult, ExtraTransportDirection.Request);
            Assert.Throws<XmlSchemaValidationException>(() => validator.Validate(data));
        }

        private static byte[] LoadData(string resourceName)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var resName = $"Dataline.ExtraStandard.Tests.Resources.Extra14.Gkv.{resourceName}";
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