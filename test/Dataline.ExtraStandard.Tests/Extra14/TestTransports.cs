using System.IO;
using System.Xml;

using ExtraStandard.Extra14;

using Xunit;

namespace ExtraStandard.Tests.Extra14
{
    public class TestTransports
    {
        [Fact]
        public void TestTransportRequest14()
        {
            var transport = ExtraUtilities.Deserialize<TransportRequestType>(LoadData("extra-request-050-20070606.xml"));
            Assert.Equal(SupportedVersionsType.Item13, transport.version);
            Assert.Equal("http://www.extra-standard.de/profile/TESTPROFILE/1", transport.profile);

            var header = transport.TransportHeader;
            Assert.NotNull(header);
            Assert.Equal("http://www.extra-standard.de/test/NONE", header.TestIndicator);
            Assert.NotNull(header.Sender);
            Assert.NotNull(header.Sender.SenderID);
            Assert.Null(header.Sender.SenderID.Value);
            Assert.NotNull(header.Receiver);
            Assert.NotNull(header.Receiver.ReceiverID);
            Assert.Null(header.Receiver.ReceiverID.Value);
            Assert.NotNull(header.RequestDetails);
            Assert.NotNull(header.RequestDetails.RequestID);
            Assert.Equal("1111111", header.RequestDetails.RequestID.Value);

            var body = transport.TransportBody;
            Assert.NotNull(body);
            Assert.NotNull(body.Items);
            Assert.Equal(1, body.Items.Length);

            var bodyData = Assert.IsType<DataType1>(body.Items[0]);

            var anyXmlData = Assert.IsType<AnyXMLType>(bodyData.Item);
            Assert.NotNull(anyXmlData.Text);
            Assert.Equal(1, anyXmlData.Text.Length);
            Assert.Equal("test data", anyXmlData.Text[0]);
            Assert.Null(anyXmlData.Items);

            var logging = transport.Logging;
            Assert.NotNull(logging);
            Assert.Equal(LoggingVersionType.Item10, logging.version);
            Assert.NotNull(logging.Items);
            Assert.Equal(1, logging.Items.Length);

            var logSequence = Assert.IsType<LogSequenceType>(logging.Items[0]);
            Assert.Equal(XmlConvert.ToDateTime("2007-06-06T00:00:01", XmlDateTimeSerializationMode.Local), logSequence.TimeStamp);
            Assert.Equal("CORE.KONVERT 1.5", logSequence.ComponentID);

            Assert.Collection(
                logSequence.Items,
                item =>
                    {
                        var evt = Assert.IsType<EventType>(item);
                        Assert.True(evt.TimeStampSpecified);
                        Assert.Equal(XmlConvert.ToDateTime("2007-06-06T00:00:01", XmlDateTimeSerializationMode.Local), evt.TimeStamp);
                        Assert.Equal("CORE.SERVER", evt.ComponentID);
                    },
                item =>
                    {
                        var state = Assert.IsType<StateType>(item);
                        Assert.Equal(XmlConvert.ToDateTime("2007-06-06T00:00:01", XmlDateTimeSerializationMode.Local), state.TimeStamp);
                        var prop = Assert.IsType<PropertyType>(state.Item);
                        Assert.Equal("generated.documentid", prop.name);
                        Assert.Equal(XSDPrefixedTypeCodes.xsstring, prop.type);
                        Assert.Equal("12968438573722894", prop.Value);
                    },
                item =>
                    {
                        var op = Assert.IsType<OperationType>(item);
                        Assert.Equal("0", op.completionCode);
                        Assert.True(op.successfulSpecified);
                        Assert.True(op.successful);
                        Assert.Equal("http://www.extra-standard.de/operation/VALIDATE_XML", op.id);
                        Assert.Equal("Validierung Transferobjekt", op.description);
                        Assert.Collection(
                            op.Items,
                            opItem =>
                                {
                                    var param = Assert.IsType<ParameterType>(opItem);
                                    Assert.Equal("http://www.extra.standard.de/class/SCHEMA", param.@class);
                                    Assert.Equal("http://www.extra-standard.de/usage/IN", param.usage);
                                    Assert.Equal("XSD Schema", param.description);
                                    Assert.Equal("file://...", param.Value);
                                });
                    },
                item =>
                    {
                        var op = Assert.IsType<OperationType>(item);
                        Assert.Equal("0", op.completionCode);
                        Assert.True(op.successfulSpecified);
                        Assert.True(op.successful);
                        Assert.Equal("http://www.extra-standard.de/operation/DECRYPT", op.id);
                        Assert.Equal("Entschluesselung", op.description);
                        Assert.Collection(
                            op.Items,
                            opItem =>
                                {
                                    var obj = Assert.IsType<ObjectType1>(opItem);
                                    Assert.Equal("http://www.extra.standard.de/class/XML_CONTENT", obj.@class);
                                    Assert.Equal("XMLTransport/TransferBody", obj.location);
                                });
                    },
                item =>
                    {
                        var op = Assert.IsType<OperationType>(item);
                        Assert.Equal("12", op.completionCode);
                        Assert.True(op.successfulSpecified);
                        Assert.False(op.successful);
                        Assert.Equal("http://www.destatis.de/operation/VALIDATE", op.id);
                        Assert.Equal("Pruefung der Rohdaten", op.description);
                        Assert.Equal("CORE.INSPECTOR 1.5", op.ComponentID);
                        Assert.Collection(
                            op.Items,
                            opItem =>
                            {
                                var exc = Assert.IsType<ExceptionType>(opItem);
                                Assert.Equal(XmlConvert.ToDateTime("2007-06-06T00:00:01", XmlDateTimeSerializationMode.Local), exc.TimeStamp);
                                Assert.Collection(
                                    exc.Items,
                                    excItem =>
                                        {
                                            var msg = Assert.IsType<MessageType>(excItem);
                                            Assert.Equal(XmlConvert.ToDateTime("2007-06-06T00:00:01", XmlDateTimeSerializationMode.Local), msg.TimeStamp);
                                            Assert.NotNull(msg.Text);
                                            Assert.Equal("Invalid data", msg.Text.Value);
                                        });
                            });
                    });
        }

        [Fact]
        public void TestTransportRequestGkv14()
        {
            var transport = ExtraUtilities.Deserialize<TransportRequestType>(LoadData("extra-request-GKV-AGV_AS_20080407.xml"));
            Assert.Equal(SupportedVersionsType.Item13, transport.version);
            Assert.Equal("http://www.gkv.de/extra/profile/GKVAGV-AS/200804", transport.profile);

            var header = transport.TransportHeader;
            Assert.NotNull(header);
            Assert.Equal("http://www.extra-standard.de/test/NONE#1", header.TestIndicator);
            Assert.NotNull(header.Sender);
            Assert.NotNull(header.Sender.SenderID);
            Assert.Equal("BetriebsnummerSender", header.Sender.SenderID.Value);
            Assert.NotNull(header.Receiver);
            Assert.NotNull(header.Receiver.ReceiverID);
            Assert.Equal("BetriebsnummerEmpfaenger", header.Receiver.ReceiverID.Value);

            var requestDetails = header.RequestDetails;
            Assert.NotNull(requestDetails);
            Assert.NotNull(requestDetails.RequestID);
            Assert.Equal("RequestID012345", requestDetails.RequestID.Value);
            Assert.Equal("gkvagv-as", requestDetails.Procedure);
            Assert.Equal("req-res", requestDetails.DataType);
            Assert.Equal("http://www.extra-standard.de/scenario/request-with-response", requestDetails.Scenario);

            Assert.NotNull(transport.TransportBody);
            Assert.Collection(
                transport.TransportBody.Items,
                item =>
                    {
                        var data = Assert.IsType<DataType1>(item);
                        var anyXml = Assert.IsType<AnyXMLType>(data.Item);
                        Assert.Collection(
                            anyXml.Items,
                            xmlItem =>
                                {
                                    Assert.Equal("RequestResult", xmlItem.Name.LocalName);
                                });
                    });
        }

        private static byte[] LoadData(string resourceName)
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var resName = $"Dataline.ExtraStandard.Tests.Resources.Extra14.{resourceName}";
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
