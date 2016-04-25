using System;
using System.IO;

using ExtraStandard.Extra14;
using ExtraStandard.DrvKomServer.Validation.Extra14;
using ExtraStandard.Validation;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestDrvMessages
    {
        [Theory]
        [InlineData("03-request.xml")]
        public void TestConfirmationRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.AcknowledgeProcessingResult, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("03-response.xml")]
        public void TestConfirmationResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.AcknowledgeProcessingResult, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("01-request.xml")]
        public void TestMessageRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("01-response.xml")]
        public void TestMessageResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.SupplyData, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("02-request.xml")]
        public void TestProcessingResultRequest(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.GetProcessingResult, ExtraTransportDirection.Request);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("02-response.xml")]
        public void TestProcessingResultResponse(string resourceName)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(ExtraMessageType.GetProcessingResult, ExtraTransportDirection.Response);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        private static byte[] LoadData(string resourceName)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var resName = $"Dataline.ExtraStandard.Tests.Resources.Extra14.Drv.{resourceName}";
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
