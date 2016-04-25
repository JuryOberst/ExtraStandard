using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ExtraStandard
{
    /// <summary>
    /// Hilfsfunktionen für die Arbeit mit eXTra-Dokumenten
    /// </summary>
    public static partial class ExtraUtilities
    {
        private static readonly Dictionary<Type, XmlSerializer> s_serializers = new Dictionary<Type, XmlSerializer>();

        /// <summary>
        /// Holt das Standard-Encoding für eXTra-Dateien
        /// </summary>
        /// <remarks>
        /// Oft wird UTF-8 nicht unterstützt, weshalb das Standard-Encoding ISO-8859-1 ist.
        /// </remarks>
        public static Encoding DefaultExtraEncoding { get; } = Encoding.GetEncoding("ISO-8859-1");

        /// <summary>
        /// Alle Standard-Namespaces für die XML-Serialisierung und -Deserialisierung
        /// </summary>
        /// <returns></returns>
        public static XmlSerializerNamespaces CreateSerializerNamespaces()
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("xreq", "http://www.extra-standard.de/namespace/request/1");
            ns.Add("xres", "http://www.extra-standard.de/namespace/response/1");
            ns.Add("xcpt", "http://www.extra-standard.de/namespace/components/1");
            ns.Add("xplg", "http://www.extra-standard.de/namespace/plugins/1");
            ns.Add("xmsg", "http://www.extra-standard.de/namespace/message/1");
            ns.Add("xlog", "http://www.extra-standard.de/namespace/logging/1");
            ns.Add("xsrv", "http://www.extra-standard.de/namespace/service/1");
            ns.Add("xenc", "http://www.w3.org/2001/04/xmlenc#");
            ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            return ns;
        }

        /// <summary>
        /// Erstellt einen Serializer für die eXTra-Klassen
        /// </summary>
        /// <typeparam name="T">Die eXTra-Klasse für die ein Serializer erstellt werden soll</typeparam>
        /// <returns>Der neue Serializer für die eXTra-Klassen</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Wir haben einfach keinen Anhaltspunkt, damit der Typ-Parameter anhand eines Parameters ermittelt werden kann.")]
        public static XmlSerializer GetSerializer<T>()
        {
            XmlSerializer serializer;
            lock (s_serializers)
            {
                if (!s_serializers.TryGetValue(typeof(T), out serializer))
                {
                    serializer = new XmlSerializer(typeof(T));
                    s_serializers.Add(typeof(T), serializer);
                }
            }

            return serializer;
        }

        /// <summary>
        /// Ist die Gewichtung eine vom Typ "Fehler"?
        /// </summary>
        /// <param name="weight">Die Gewichtung der Information</param>
        /// <returns><code>true</code> wenn <paramref name="weight"/> eine Fehlerkennung ist</returns>
        public static bool IsError(string weight)
        {
            return weight == ExtraFlagWeight.Error;
        }

        /// <summary>
        /// Ist die Gewichtung eine vom Typ "Information"?
        /// </summary>
        /// <param name="weight">Die Gewichtung der Information</param>
        /// <param name="code">Der Status-Code</param>
        /// <returns><code>true</code> wenn <paramref name="weight"/> eine Info-Kennung und der
        /// <paramref name="code"/> nicht <code>I000</code> oder <code>I001</code> ist</returns>
        public static bool IsInfo(string code, string weight)
        {
            return weight == ExtraFlagWeight.Info && code != "I000" && code != "I001";
        }

        /// <summary>
        /// Serialisierung der Daten mit dem Standard-Encoding (<see cref="DefaultExtraEncoding"/>)
        /// </summary>
        /// <typeparam name="T">Der Typ der zu serialisierenden Daten</typeparam>
        /// <param name="data">Die zu serialisierenden Daten</param>
        /// <returns>Die serialisierten Daten</returns>
        public static byte[] Serialize<T>(T data)
        {
            return Serialize(data, DefaultExtraEncoding);
        }

        /// <summary>
        /// Serialisierung der Daten mit dem übergebenen Encoding
        /// </summary>
        /// <typeparam name="T">Der Typ der zu serialisierenden Daten</typeparam>
        /// <param name="data">Die zu serialisierenden Daten</param>
        /// <param name="encoding">Das zu verwendende Encoding</param>
        /// <returns>Die serialisierten Daten</returns>
        public static byte[] Serialize<T>(T data, Encoding encoding)
        {
            var serializer = GetSerializer<T>();
            var output = new MemoryStream();
            using (var writer = new StreamWriter(output, encoding))
            {
                serializer.Serialize(writer, data, CreateSerializerNamespaces());
                writer.Flush();
            }

            return output.ToArray();
        }

        /// <summary>
        /// Deserialiserung der Daten
        /// </summary>
        /// <typeparam name="T">Der zu deserialiserende Typ</typeparam>
        /// <param name="data">Die zu deserialiserenden Daten</param>
        /// <returns>Das deserialiserte Objekt</returns>
        public static T Deserialize<T>(byte[] data)
        {
            var serializer = GetSerializer<T>();
            using (var input = new MemoryStream(data))
            {
                return (T)serializer.Deserialize(input);
            }
        }
    }
}
