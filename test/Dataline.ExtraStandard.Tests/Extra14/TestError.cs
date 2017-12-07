using System.IO;
using System.Xml;

using ExtraStandard.Extra14;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestError
    {
        [Fact]
        public void TestExtraError14()
        {
            var error = ExtraUtilities.Deserialize<ExtraErrorType>(LoadData("extra-error-1.xml"));
            Assert.NotNull(error);
            Assert.True(error.versionSpecified);
            Assert.Equal(ExtraErrorTypeVersion.Item10, error.version);
            Assert.Equal(ExtraErrorReasonType.INVALID_REQUEST, error.Reason);
            Assert.NotNull(error.RequestID);
            Assert.Equal("reqid", error.RequestID.Value);
            Assert.NotNull(error.ResponseID);
            Assert.Equal("resid", error.ResponseID.Value);
            Assert.Equal(XmlConvert.ToDateTime("2010-02-21T21:15:00", XmlDateTimeSerializationMode.Local), error.TimeStamp);
            Assert.NotNull(error.Application);
            Assert.NotNull(error.Application.Product);
            Assert.Equal("my-extra-server 1.0", error.Application.Product.Value);
            Assert.NotNull(error.Report);
            Assert.Equal(ExtraFlagWeight.Error, error.Report.highestWeight);
            Assert.NotNull(error.Report.Flag);
            Assert.Single(error.Report.Flag);
            var flag = error.Report.Flag[0];
            Assert.NotNull(flag);
            Assert.Equal(ExtraFlagWeight.Error, flag.weight);
            Assert.NotNull(flag.Code);
            Assert.Equal("0100", flag.Code.Value);
            Assert.NotNull(flag.Text);
            Assert.Equal("Der Request ist nicht wohlgeformt", flag.Text.Value);
            Assert.NotNull(flag.XPath);
            Assert.Equal("/a/b[4]/c[2]/d", flag.XPath.Value);
        }

        private static byte[] LoadData(string resourceName)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
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
