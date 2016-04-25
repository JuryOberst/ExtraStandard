using System.Diagnostics;
using System.Xml.Serialization;

namespace ExtraStandard.Extra11
{
    /// <summary>
    /// Repräsentiert eine Fehler-Antwort
    /// </summary>
    [DebuggerStepThrough]
    [XmlType(Namespace = "http://www.extra-standard.de/namespace/error/1")]
    [XmlRoot("XMLError", Namespace = "http://www.extra-standard.de/namespace/error/1", IsNullable = false)]
    public class ExtraErrorType : ElementWithOptionalVersionType
    {
        private ResponseDetailsType _responseDetailsField;

        /// <summary>
        /// Holt oder setzt die Details für eine Fehler-Antwort
        /// </summary>
        [XmlElement(Namespace = "http://www.extra-standard.de/namespace/error/1", Order = 0)]
        public ResponseDetailsType ResponseDetails
        {
            get
            {
                return _responseDetailsField;
            }

            set
            {
                _responseDetailsField = value;
                RaisePropertyChanged("ResponseDetails");
            }
        }
    }
}
