using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace ExtraStandard.Extra11
{
    /// <summary>
    /// Hilfsfunktionen für die Arbeit mit eXTra 1.1-Dokumenten
    /// </summary>
    public static class ExtraUtilities
    {
        /// <summary>
        /// Versucht die Daten als <see cref="ExtraErrorType"/> zu deserialisieren
        /// </summary>
        /// <param name="data">Die zu deserialisierenden Daten</param>
        /// <param name="error">Das Ausgabe-Objekt</param>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Es ist nicht klar, welche Fehler hier bei der Deserialisierung auftreten können, weshalb einfach alle Exceptions abgefangen werden.")]
        public static bool TryDeserializeError(byte[] data, out ExtraErrorType error)
        {
            try
            {
                using (var input = new MemoryStream(data))
                {
                    var serializer = ExtraStandard.ExtraUtilities.GetSerializer<ExtraErrorType>();
                    var errorData = (ExtraErrorType)serializer.Deserialize(input);
                    error = errorData;
                    return true;
                }
            }
            catch
            {
                error = null;
                return false;
            }
        }

        /// <summary>
        /// Ist das Kennzeichen ein Fehlerkennzeichen?
        /// </summary>
        /// <param name="flag">Das zu prüfende Kennzeichen</param>
        /// <returns><code>true</code> wenn das zu prüfende Kennzeichen ein Fehlerkennzeichen ist</returns>
        public static bool IsError(this FlagType flag)
        {
            return ExtraStandard.ExtraUtilities.IsError(flag.weight);
        }

        /// <summary>
        /// Ist das Kennzeichen ein Informationskennzeichen?
        /// </summary>
        /// <param name="flag">Das zu prüfende Kennzeichen</param>
        /// <returns><code>true</code> wenn das zu prüfende Kennzeichen ein Informationskennzeichen ist</returns>
        public static bool IsInfo(this FlagType flag)
        {
            return ExtraStandard.ExtraUtilities.IsInfo(flag.Code.Value, flag.weight);
        }

        /// <summary>
        /// Ermittlung aller Kennzeichen für den <see cref="ReportType"/>
        /// </summary>
        /// <param name="report">Der <see cref="ReportType"/> für den die Kennzeichen ermittelt werden sollen</param>
        /// <returns>Die ermittelten Kennzeichen</returns>
        public static IEnumerable<FlagType> GetReportFlags(this ReportType report)
        {
            var result = new List<FlagType>();
            if (report?.Flag != null)
                result.AddRange(report.Flag);
            return result;
        }

        /// <summary>
        /// Ermittlung aller Kennzeichen für den <see cref="ResponseDetailsType"/>
        /// </summary>
        /// <param name="responseDetails">Der <see cref="ResponseDetailsType"/> für den die Kennzeichen ermittelt werden sollen</param>
        /// <returns>Die ermittelten Kennzeichen</returns>
        public static IEnumerable<FlagType> GetReportFlags(this ResponseDetailsType responseDetails)
        {
            var result = new List<FlagType>();
            if (responseDetails != null)
                result.AddRange(GetReportFlags(responseDetails.Report));
            return result;
        }

        /// <summary>
        /// Ermittlung aller Kennzeichen für den <see cref="TransportResponseHeaderType"/>
        /// </summary>
        /// <param name="header">Der <see cref="TransportResponseHeaderType"/> für den die Kennzeichen ermittelt werden sollen</param>
        /// <returns>Die ermittelten Kennzeichen</returns>
        public static IEnumerable<FlagType> GetReportFlags(this TransportResponseHeaderType header)
        {
            var result = new List<FlagType>();
            if (header != null)
                result.AddRange(GetReportFlags(header.ResponseDetails));
            return result;
        }

        /// <summary>
        /// Ermittlung aller Kennzeichen für den <see cref="PackageResponseHeaderType"/>
        /// </summary>
        /// <param name="packageHeader">Der <see cref="PackageResponseHeaderType"/> für den die Kennzeichen ermittelt werden sollen</param>
        /// <returns>Die ermittelten Kennzeichen</returns>
        public static IEnumerable<FlagType> GetReportFlags(this PackageResponseHeaderType packageHeader)
        {
            var result = new List<FlagType>();
            if (packageHeader?.ResponseDetails != null)
                result.AddRange(GetReportFlags(packageHeader.ResponseDetails.Report));
            return result;
        }

        /// <summary>
        /// Ermittlung aller Kennzeichen für den <see cref="TransportResponseType"/>
        /// </summary>
        /// <param name="response">Der <see cref="TransportResponseType"/> für den die Kennzeichen ermittelt werden sollen</param>
        /// <returns>Die ermittelten Kennzeichen</returns>
        public static IEnumerable<FlagType> GetReportFlags(this TransportResponseType response)
        {
            var result = new List<FlagType>();
            result.AddRange(GetReportFlags(response.TransportHeader));
            if (response.TransportBody?.Items != null)
                result.AddRange(response.TransportBody.Items.OfType<PackageResponseType>().SelectMany(x => GetReportFlags(x.PackageHeader)));
            return result;
        }

        /// <summary>
        /// Ermittlung aller Fehler für den <see cref="ReportType"/>
        /// </summary>
        /// <param name="report">Der <see cref="ReportType"/> für den die Fehlerkennzeichen ermittelt werden</param>
        /// <returns>Die ermittelten Fehlerkennzeichen</returns>
        public static IEnumerable<FlagType> GetErrors(this ReportType report)
        {
            return GetReportFlags(report).Where(IsError);
        }

        /// <summary>
        /// Ermittlung aller Fehler für den <see cref="TransportResponseHeaderType"/>
        /// </summary>
        /// <param name="header">Der <see cref="TransportResponseHeaderType"/> für den die Fehlerkennzeichen ermittelt werden</param>
        /// <returns>Die ermittelten Fehlerkennzeichen</returns>
        public static IEnumerable<FlagType> GetErrors(this TransportResponseHeaderType header)
        {
            return GetReportFlags(header).Where(IsError);
        }

        /// <summary>
        /// Ermittlung aller Fehler für den <see cref="PackageResponseHeaderType"/>
        /// </summary>
        /// <param name="packageHeader">Der <see cref="PackageResponseHeaderType"/> für den die Fehlerkennzeichen ermittelt werden</param>
        /// <returns>Die ermittelten Fehlerkennzeichen</returns>
        public static IEnumerable<FlagType> GetErrors(this PackageResponseHeaderType packageHeader)
        {
            return GetReportFlags(packageHeader).Where(IsError);
        }

        /// <summary>
        /// Ermittlung aller Fehler für den <see cref="TransportResponseType"/>
        /// </summary>
        /// <param name="response">Der <see cref="TransportResponseType"/> für den die Fehlerkennzeichen ermittelt werden</param>
        /// <returns>Die ermittelten Fehlerkennzeichen</returns>
        public static IEnumerable<FlagType> GetErrors(this TransportResponseType response)
        {
            return GetReportFlags(response).Where(IsError);
        }

        /// <summary>
        /// Hat der <see cref="ReportType"/> Fehlerkennzeichen?
        /// </summary>
        /// <param name="report">Der zu überprüfende <see cref="ReportType"/></param>
        /// <returns><code>true</code> wenn der <paramref name="report"/> Fehlerkennzeichen enthält</returns>
        public static bool HasErrors(this ReportType report)
        {
            return GetErrors(report).Any();
        }

        /// <summary>
        /// Hat der <see cref="TransportResponseHeaderType"/> Fehlerkennzeichen?
        /// </summary>
        /// <param name="header">Der zu überprüfende <see cref="TransportResponseHeaderType"/></param>
        /// <returns><code>true</code> wenn der <paramref name="header"/> Fehlerkennzeichen enthält</returns>
        public static bool HasErrors(this TransportResponseHeaderType header)
        {
            return GetErrors(header).Any();
        }

        /// <summary>
        /// Hat der <see cref="TransportResponseType"/> Fehlerkennzeichen?
        /// </summary>
        /// <param name="response">Der zu überprüfende <see cref="TransportResponseType"/></param>
        /// <returns><code>true</code> wenn der <paramref name="response"/> Fehlerkennzeichen enthält</returns>
        public static bool HasErrors(this TransportResponseType response)
        {
            return GetErrors(response).Any();
        }
    }
}
