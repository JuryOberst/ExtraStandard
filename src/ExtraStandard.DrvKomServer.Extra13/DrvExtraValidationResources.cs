using System;
using System.Reflection;

using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Extra13
{
    /// <summary>
    /// Implementation von <see cref="IExtraValidationResources"/> für eXTra 1.3-Dokumente
    /// </summary>
    public class DrvExtraValidationResources : IExtraValidationResources
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DrvExtraValidationResources"/> Klasse.
        /// </summary>
        /// <param name="messageType">Art der Meldung</param>
        /// <param name="transportDirection">Sendung oder Antwort?</param>
        public DrvExtraValidationResources(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
        {
            var type = GetType();
#if HAS_FULL_TYPE
            ResourceAssembly = type.Assembly;
#else
            ResourceAssembly = type.GetTypeInfo().Assembly;
#endif
            RootUrl = GetRootUrl(messageType);
            StartXmlSchemaFileName = GetXsdFileName(messageType, transportDirection);
        }

        /// <summary>
        /// Holt die Basis-URL von der ausgehend alle XSD-Dateien aus den Ressourcen geladen werden
        /// </summary>
        public Uri RootUrl { get; }

        /// <summary>
        /// Holt die Assembly aus der die XSD-Dateien aus den Ressourcen geladen werden
        /// </summary>
        public Assembly ResourceAssembly { get; }

        /// <summary>
        /// Holt den Namen der XSD die als Start für die XML-Valdierung genutzt wird.
        /// </summary>
        public string StartXmlSchemaFileName { get; }

        private static Uri GetRootUrl(ExtraMessageType messageType)
        {
            var type = typeof(DrvExtraValidationResources);
            var result = new Uri($"res:///Dataline.{type.Namespace?.Replace('.', '/')}/Schemas/");
            switch (messageType)
            {
                case ExtraMessageType.Error:
                case ExtraMessageType.SupplyData:
                    result = new Uri(result, new Uri("DataSupply/", UriKind.Relative));
                    break;
                case ExtraMessageType.GetProcessingResultQuery:
                case ExtraMessageType.GetProcessingResult:
                    result = new Uri(result, new Uri("DataResponse/", UriKind.Relative));
                    break;
                case ExtraMessageType.AcknowledgeProcessingResultQuery:
                case ExtraMessageType.AcknowledgeProcessingResult:
                    result = new Uri(result, new Uri("DataAcknowledge/", UriKind.Relative));
                    break;
                default:
                    throw new NotSupportedException();
            }
            return result;
        }

        private static string GetXsdFileName(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
        {
            string src;
            switch (messageType)
            {
                case ExtraMessageType.Error:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Response:
                            src = "eXTra-error-1.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                case ExtraMessageType.SupplyData:
                case ExtraMessageType.GetProcessingResult:
                case ExtraMessageType.AcknowledgeProcessingResult:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Request:
                            src = "eXTra-request-1.xsd";
                            break;
                        case ExtraTransportDirection.Response:
                            src = "eXTra-response-1.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                case ExtraMessageType.GetProcessingResultQuery:
                case ExtraMessageType.AcknowledgeProcessingResultQuery:
                    src = "eXTra-messages-1.xsd";
                    break;
                default:
                    throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
            }
            return src;
        }
    }
}
