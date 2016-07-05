using System;
using System.Reflection;

using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Extra14.Dsv
{
    /// <summary>
    /// Implementation von <see cref="IExtraValidationResources"/> für eXTra 1.4-Dokumente für die Versicherungsnummernabfrage
    /// </summary>
    public class DrvDsvExtraValidationResources : IExtraValidationResources
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DrvExtraValidationResources"/> Klasse.
        /// </summary>
        /// <param name="messageType">Art der Meldung</param>
        /// <param name="transportDirection">Sendung oder Antwort?</param>
        /// <param name="isError">Liegt eine Fehlermeldung vor?</param>
        public DrvDsvExtraValidationResources(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            var type = GetType();
#if HAS_FULL_TYPE
            ResourceAssembly = type.Assembly;
#else
            ResourceAssembly = type.GetTypeInfo().Assembly;
#endif
            var baseUrl = new Uri($"res:///Dataline.{type.Namespace}/Schemas/");
            RootUrl = GetRootUrl(baseUrl, messageType, transportDirection);
            StartXmlSchemaFileName = GetXsdFileName(messageType, transportDirection, isError);
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

        private static Uri GetRootUrl(Uri baseUrl, ExtraMessageType messageType, ExtraTransportDirection transportDirection)
        {
            string step;
            switch (messageType)
            {
                case ExtraMessageType.SupplyData:
                    step = "01";
                    break;
                case ExtraMessageType.GetProcessingResultQuery:
                case ExtraMessageType.GetProcessingResult:
                    step = "02";
                    break;
                case ExtraMessageType.AcknowledgeProcessingResultQuery:
                case ExtraMessageType.AcknowledgeProcessingResult:
                    step = "03";
                    break;
                default:
                    throw new NotSupportedException();
            }

            Uri result;
            switch (transportDirection)
            {
                case ExtraTransportDirection.Request:
                    result = new Uri(baseUrl, new Uri($"_{step}_Request/", UriKind.Relative));
                    break;
                case ExtraTransportDirection.Response:
                    result = new Uri(baseUrl, new Uri($"_{step}_Response/", UriKind.Relative));
                    break;
                default:
                    throw new NotSupportedException();
            }

            return result;
        }

        private static string GetXsdFileName(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            string src;
            if (isError)
            {
                src = "extra-error-1.xsd";
            }
            else
            {
                switch (messageType)
                {
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
            }
            return src;
        }
    }
}
