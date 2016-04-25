using System;
using System.Reflection;

using ExtraStandard.Validation;

namespace ExtraStandard.GkvKomServer.Validation.Extra13
{
    /// <summary>
    /// Implementation von <see cref="IExtraValidationResources"/> für eXTra 1.3-Dokumente
    /// </summary>
    public class GkvExtraValidationResources : IExtraValidationResources
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="GkvExtraValidationResources"/> Klasse.
        /// </summary>
        /// <param name="messageType">Art der Meldung</param>
        /// <param name="transportDirection">Sendung oder Antwort?</param>
        public GkvExtraValidationResources(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
        {
            var type = GetType();
#if HAS_FULL_TYPE
            ResourceAssembly = type.Assembly;
#else
            ResourceAssembly = type.GetTypeInfo().Assembly;
#endif
            RootUrl = new Uri($"res:///Dataline.{type.Namespace?.Replace('.', '/')}/Schemas/");
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

        private static string GetXsdFileName(ExtraMessageType messageType, ExtraTransportDirection transportDirection)
        {
            string src;
            switch (messageType)
            {
                case ExtraMessageType.Error:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Response:
                            src = "xsd_KomServer_0_error.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                case ExtraMessageType.SupplyData:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Request:
                            src = "xsd_KomServer_1_request_senden_datenlieferungen.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                case ExtraMessageType.GetProcessingResultQuery:
                    src = "xsd_KomServer_3_Body_request_anfordern_rueckmeldungen.xsd";
                    break;
                case ExtraMessageType.GetProcessingResult:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Request:
                            src = "xsd_KomServer_3_request_anfordern_rueckmeldungen.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                case ExtraMessageType.AcknowledgeProcessingResultQuery:
                    src = "xsd_KomServer_5_Body_request_senden_empfangsquittungen.xsd";
                    break;
                case ExtraMessageType.AcknowledgeProcessingResult:
                    switch (transportDirection)
                    {
                        case ExtraTransportDirection.Request:
                            src = "xsd_KomServer_5_request_senden_empfangsquittungen.xsd";
                            break;
                        default:
                            throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
                    }
                    break;
                default:
                    throw new NotSupportedException($"The combination {messageType}/{transportDirection} is not supported yet.");
            }
            return src;
        }
    }
}
