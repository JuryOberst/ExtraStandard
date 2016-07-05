using System;
using System.Reflection;

using ExtraStandard.Validation;

namespace ExtraStandard.Extra11
{
    /// <summary>
    /// Implementation von <see cref="IExtraValidationResources"/> für eXTra 1.1-Dokumente
    /// </summary>
    public class GenericExtraValidationResources : IExtraValidationResources
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="GenericExtraValidationResources"/> Klasse.
        /// </summary>
        /// <param name="messageType">Art der Meldung</param>
        /// <param name="transportDirection">Sendung oder Antwort?</param>
        /// <param name="isError">Liegt eine Fehlermeldung vor?</param>
        public GenericExtraValidationResources(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            var type = GetType();
#if HAS_FULL_TYPE
            ResourceAssembly = type.Assembly;
#else
            ResourceAssembly = type.GetTypeInfo().Assembly;
#endif
            RootUrl = new Uri($"res:///Dataline.{type.Namespace}/Schemas/");
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

        private static string GetXsdFileName(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            string src;
            if (isError)
            {
                src = "eXTra-error-1.xsd";
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
