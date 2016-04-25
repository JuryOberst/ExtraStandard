using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace ExtraStandard.Validation
{
    /// <summary>
    /// Implementation eines <see cref="ExtraValidator"/> der Ressourcen-Dateien für das Nachladen von
    /// XSD-Dateien verwendet.
    /// </summary>
    public class ResourceExtraValidator : ExtraValidator
    {
        private readonly IExtraValidationResources _validationResources;
        private readonly ResourceExtraXmlResolver _resolver;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ResourceExtraValidator"/> Klasse.
        /// </summary>
        /// <param name="validationResources">Die für die Validierung zu verwendenden Ressourcen</param>
        public ResourceExtraValidator(IExtraValidationResources validationResources)
        {
            _validationResources = validationResources;
            _resolver = new ResourceExtraXmlResolver(_validationResources.RootUrl, _validationResources.ResourceAssembly);
        }

        /// <summary>
        /// Holt den Zeitstempel des letzten Ladens einer XSD-Entität
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTimeOffset? LastGetEntityTimestamp => _resolver.LastGetEntityTimestamp;

        /// <summary>
        /// Validiert das eXTra-Dokument
        /// </summary>
        /// <param name="document">Das zu validierende Dokument</param>
        public override void Validate(XDocument document)
        {
            Validate(document, _resolver, _validationResources.StartXmlSchemaFileName);
        }
    }
}
