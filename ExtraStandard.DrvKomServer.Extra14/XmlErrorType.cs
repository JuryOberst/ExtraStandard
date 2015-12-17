using ExtraStandard.Extra14;

namespace ExtraStandard.DrvKomServer.Extra14
{
    /// <summary>
    /// Sonderfall für die Fehler-Antwort der DRV
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.extra-standard.de/namespace/error/1")]
    [System.Xml.Serialization.XmlRootAttribute("XMLError", Namespace = "http://www.extra-standard.de/namespace/error/1", IsNullable = false)]
    public class XmlErrorType : ElementWithOptionalVersionType
    {
        private FlagType[] _flagField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Flag", Namespace = "http://www.extra-standard.de/namespace/components/1", Order = 0)]
        public FlagType[] Flag
        {
            get
            {
                return this._flagField;
            }
            set
            {
                this._flagField = value;
                this.RaisePropertyChanged("Flag");
            }
        }
    }
}
