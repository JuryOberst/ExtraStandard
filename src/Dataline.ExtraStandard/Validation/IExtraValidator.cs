using System.Xml.Linq;

namespace ExtraStandard.Validation
{
    /// <summary>
    /// Schnittstelle für die Validierung einer eXTra-Datei
    /// </summary>
    public interface IExtraValidator
    {
        /// <summary>
        /// Validiert eine eXTra-Datei und löst eine Exception aus, wenn die Validierung fehlschlägt
        /// </summary>
        /// <param name="document">Das zu validierende eXTra-Dokument</param>
        void Validate(XDocument document);
    }
}
