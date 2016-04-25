using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraStandard.GkvKomServer
{
    /// <summary>
    /// eXTra-Datentyp für den GKV-Kommunikationsserver
    /// </summary>
    public static class ExtraDataType
    {
        /// <summary>
        /// Versand einer Meldung
        /// </summary>
        public static readonly string Meldung = "http://kommunikationsserver.itsg.de/meldung";

        /// <summary>
        /// Abfrage des Verarbeitungsergebnisses
        /// </summary>
        public static readonly string Anfrage = "http://kommunikationsserver.itsg.de/anfrage";

        /// <summary>
        /// Quittierung der Abholung der Verarbeitungsergebnisse
        /// </summary>
        public static readonly string Quittung = "http://kommunikationsserver.itsg.de/quittung";
    }
}
