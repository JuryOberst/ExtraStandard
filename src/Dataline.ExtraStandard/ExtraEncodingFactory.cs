using System;
using System.Text;

namespace ExtraStandard
{
    /// <summary>
    /// Factory für die Erstellung von <see cref="Encoding"/>-Instanzen für die unterschiedlichen Kodierungen für die eXTra-Daten
    /// </summary>
    public class ExtraEncodingFactory
    {
        private static readonly Lazy<Encoding> _encodingIso88591 = new Lazy<Encoding>(() => Encoding.GetEncoding("iso-8859-1"));

        private static readonly Lazy<Encoding> _encodingIso885915 = new Lazy<Encoding>(() => Encoding.GetEncoding("iso-8859-15"));

        private static readonly Lazy<Encoding> _encodingDin66003 = new Lazy<Encoding>(() => Encoding.GetEncoding("x-IA5-German"));

        private static readonly Lazy<Encoding> _encodingCp850 = new Lazy<Encoding>(() => Encoding.GetEncoding("ibm850"));

        private static readonly Lazy<Encoding> _encodingEbcdicGerman = new Lazy<Encoding>(() => Encoding.GetEncoding("ibm237"));

        private static readonly Lazy<Din66303Drv8Encoding> _encodingDin66303 = new Lazy<Din66303Drv8Encoding>(() => new Din66303Drv8Encoding());

        private static readonly Lazy<UTF8Encoding> _encodingUtf8 = new Lazy<UTF8Encoding>(() => new UTF8Encoding(false));

        private readonly bool _useIso88591ForDin66303;

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraEncodingFactory"/> Klasse.
        /// </summary>
        /// <param name="useIso88591ForDin66303">Verwendung von ISO-8859-1 statt DIN 66303</param>
        public ExtraEncodingFactory(bool useIso88591ForDin66303)
        {
            _useIso88591ForDin66303 = useIso88591ForDin66303;
        }

        /// <summary>
        /// Holt die Standard-Factory für die Kodierungen für die eXTra-Daten
        /// </summary>
        public static ExtraEncodingFactory Standard { get; } = new ExtraEncodingFactory(false);

        /// <summary>
        /// Holt die DSRV-spezifische Factory für die Kodierungen für die eXTra-Daten
        /// </summary>
        public static ExtraEncodingFactory Dsrv { get; } = new ExtraEncodingFactory(true);

        /// <summary>
        /// Holt die <see cref="Encoding"/> instanz die anhand einer <paramref name="encodingId"/> erfragt wird.
        /// </summary>
        /// <param name="encodingId">Die im eXTra-Standard definierten <see cref="ExtraContainerEncoding"/></param>
        /// <returns>Die entsprechende <see cref="Encoding"/>-Instanz</returns>
        public Encoding GetEncoding(string encodingId)
        {
            switch (encodingId)
            {
                case ExtraContainerEncoding.Iso8859_1:
                    return _encodingIso88591.Value;
                case "I5":
                    return _encodingIso885915.Value;
                case ExtraContainerEncoding.Din66003:
                    return _encodingDin66003.Value;
                case ExtraContainerEncoding.Din66303:
                    if (_useIso88591ForDin66303)
                        return _encodingIso88591.Value;
                    return _encodingDin66303.Value;
                case "P8":
                    return _encodingCp850.Value;
                case "EB":
                    return _encodingEbcdicGerman.Value;
                case ExtraContainerEncoding.Utf8:
                    return _encodingUtf8.Value;
            }

            throw new NotSupportedException();
        }
    }
}
