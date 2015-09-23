using System;
using System.Reflection;

namespace ExtraStandard
{
    /// <summary>
    /// Verweis auf die Ressourcen für die Validierung von eXTra-Dateien
    /// </summary>
    public interface IExtraValidationResources
    {
        /// <summary>
        /// Holt die Basis-URL von der ausgehend alle XSD-Dateien aus den Ressourcen geladen werden
        /// </summary>
        Uri RootUrl { get; }

        /// <summary>
        /// Holt die Assembly aus der die XSD-Dateien aus den Ressourcen geladen werden
        /// </summary>
        Assembly ResourceAssembly { get; }

        /// <summary>
        /// Holt den Namen der XSD die als Start für die XML-Valdierung genutzt wird.
        /// </summary>
        string StartXmlSchemaFileName { get; }
    }
}
