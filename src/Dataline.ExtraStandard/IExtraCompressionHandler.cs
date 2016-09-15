namespace ExtraStandard
{
    /// <summary>
    /// Schnittstelle für die de-/kompression von Daten
    /// </summary>
    public interface IExtraCompressionHandler
    {
        /// <summary>
        /// Holt die ID für das Verfahren
        /// </summary>
        string AlgorithmId { get; }

        /// <summary>
        /// Komprimiert die Daten
        /// </summary>
        /// <param name="data">Die zu komprimierenden Daten</param>
        /// <param name="dataName">Ein Name für die Daten</param>
        /// <returns>Die komprimierten Daten</returns>
        byte[] Compress(byte[] data, string dataName);

        /// <summary>
        /// Dekomprimiert die Daten
        /// </summary>
        /// <param name="data">Die zu dekomprimierenden Daten</param>
        /// <returns>Die dekomprimierten Daten</returns>
        byte[] Decompress(byte[] data);
    }
}
