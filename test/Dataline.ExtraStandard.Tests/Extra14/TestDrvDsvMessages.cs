using System;
using System.IO;
using System.Xml.Schema;

using ExtraStandard.Extra14;
using ExtraStandard.DrvKomServer.Validation.Extra14.Dsv;
using ExtraStandard.Validation;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestDrvDsvMessages
    {
        [Theory]
        [InlineData("01-request.xml", ExtraMessageType.SupplyData)]
        [InlineData("02-request.xml", ExtraMessageType.GetProcessingResult)]
        [InlineData("03-request.xml", ExtraMessageType.AcknowledgeProcessingResult)]
        public void TestRequests(string resourceName, ExtraMessageType messageType)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportRequestType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(messageType, ExtraTransportDirection.Request, false);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        [Theory]
        [InlineData("01-response.xml", ExtraMessageType.SupplyData)]
        [InlineData("02-response.xml", ExtraMessageType.GetProcessingResult)]
        [InlineData("03-response.xml", ExtraMessageType.AcknowledgeProcessingResult)]
        [InlineData("01-response-fehler.xml", ExtraMessageType.SupplyData)]
        [InlineData("02-response-leer.xml", ExtraMessageType.GetProcessingResult)]
        [InlineData("03-response-fehler.xml", ExtraMessageType.AcknowledgeProcessingResult)]
        public void TestResponses(string resourceName, ExtraMessageType messageType)
        {
            var data = LoadData(resourceName);
            var document = ExtraUtilities.Deserialize<TransportResponseType>(data);
            Assert.NotNull(document);
            var validator = new DrvExtraValidator(messageType, ExtraTransportDirection.Response, false);
            var now = DateTimeOffset.Now;
            validator.Validate(data);
            Assert.True(now <= validator.LastGetEntityTimestamp);
        }

        private static byte[] LoadData(string resourceName)
        {
            var step = resourceName.Substring(0, 2);
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var resName = $"Dataline.ExtraStandard.Tests.Resources.Extra14.Drv.Dsv._{step}.{resourceName}";
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
