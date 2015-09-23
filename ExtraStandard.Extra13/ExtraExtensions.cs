using System.Collections.Generic;

namespace ExtraStandard.Extra13
{
    /// <summary>
    /// Hilfsfunktionen für die Ermittlung der eXTra-Flags
    /// </summary>
    public static class ExtraExtensions
    {
        /// <summary>
        /// Holt alle Flags aus dem Transport-Antwort-Header (<see cref="TransportResponseHeaderType"/>)
        /// </summary>
        /// <param name="header">Der Header aus dem die Status-Kennzeichen ermittelt werden</param>
        /// <returns>Die gefundenen Status-Kennzeichen</returns>
        public static IEnumerable<FlagType> GetFlags(this TransportResponseHeaderType header)
        {
            var result = new List<FlagType>();
            if (header.ResponseDetails?.Report?.Flag != null)
                result.AddRange(header.ResponseDetails.Report.Flag);
            return result;
        }

        /// <summary>
        /// Holt alle Flags aus dem Package-Antwort-Header (<see cref="PackageResponseHeaderType"/>)
        /// </summary>
        /// <param name="header">Der Header aus dem die Status-Kennzeichen ermittelt werden</param>
        /// <returns>Die gefundenen Status-Kennzeichen</returns>
        public static IEnumerable<FlagType> GetFlags(this PackageResponseHeaderType header)
        {
            var result = new List<FlagType>();
            if (header.ResponseDetails?.Report?.Flag != null)
                result.AddRange(header.ResponseDetails.Report.Flag);
            return result;
        }

        /// <summary>
        /// Holt alle Flags aus dem Fehler-Antwort-Header (<see cref="ExtraErrorType"/>)
        /// </summary>
        /// <param name="error">Der Header aus dem die Status-Kennzeichen ermittelt werden</param>
        /// <returns>Die gefundenen Status-Kennzeichen</returns>
        public static IEnumerable<FlagType> GetFlags(this ExtraErrorType error)
        {
            var result = new List<FlagType>();
            if (error.Report?.Flag != null)
                result.AddRange(error.Report.Flag);
            return result;
        }
    }
}
