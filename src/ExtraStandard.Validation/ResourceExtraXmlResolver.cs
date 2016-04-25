using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Net;
using System.Xml;

namespace ExtraStandard.Validation
{
    /// <summary>
    /// Spezialisierung des <see cref="XmlResolver"/>, die die referenzierten Schema-Dateien
    /// aus Assembly-Ressourcen lädt.
    /// </summary>
    public class ResourceExtraXmlResolver : XmlResolver
    {
        private readonly System.Reflection.Assembly _resourceAssembly;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ResourceExtraXmlResolver"/> Klasse.
        /// </summary>
        /// <param name="rootUrl">Die <see cref="Uri"/> die als Basis für referenzierte XSD-Dateien verwendet wird.</param>
        /// <param name="resourceAssembly">Die Assembly aus der die XSD-Dateien geladen werden</param>
        public ResourceExtraXmlResolver(Uri rootUrl, System.Reflection.Assembly resourceAssembly)
        {
            RootUrl = rootUrl;
            _resourceAssembly = resourceAssembly;
        }

        /// <summary>
        /// Die Authentifizierungsinformationen falls die Daten von einem Server geladen werden müssen.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override ICredentials Credentials
        {
            set { }
        }

        /// <summary>
        /// Holt den Zeitstempel des letzten Ladens einer XSD-Entität
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTimeOffset? LastGetEntityTimestamp { get; private set; }

        /// <summary>
        /// Holt die Basis-URL für die nachzuladenden XSD-Dateien
        /// </summary>
        public Uri RootUrl { get; }

        /// <summary>
        /// Lädt die XSD-Datei
        /// </summary>
        /// <param name="absoluteUri">Absolute URI der zu ladenden XSD-Datei</param>
        /// <param name="role">Derzeit nicht verwendet</param>
        /// <param name="ofObjectToReturn">Der Typ des zurückzugebenden Objekts.Die aktuelle Version gibt nur <see cref="Stream"/>-Objekte zurück.</param>
        /// <returns></returns>
        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            var ressourceName = absoluteUri.GetComponents(UriComponents.Path, UriFormat.Unescaped).TrimStart('/').Replace("/", ".");
            var stream = _resourceAssembly.GetManifestResourceStream(ressourceName);
            if (stream == null) {
                var errorMessage = new StringBuilder($"Schema from resource {absoluteUri} not found.");
#if DEBUG
                errorMessage.AppendLine().Append("Resources in resource file:");
                foreach (var resName in _resourceAssembly.GetManifestResourceNames())
                    errorMessage.AppendLine().Append(resName);
#endif                
                throw new System.Xml.Schema.XmlSchemaException(errorMessage.ToString());
            }
            LastGetEntityTimestamp = DateTimeOffset.Now;
            return stream;
        }
    }
}
