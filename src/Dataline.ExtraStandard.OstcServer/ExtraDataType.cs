using System;
using System.Collections.Generic;
using System.Text;

namespace ExtraStandard.OstcServer
{
    /// <summary>
    /// eXTra-Datentyp für den OSTC-Server
    /// </summary>
    public static class ExtraDataType
    {
        /// <summary>
        /// Über diese URL werden die eXTra-Nachrichten zur Abwicklung der 
        /// Online-Beantragung mit den Antragsdaten (1a und 1b) ausgetauscht. 
        /// </summary>
        public static readonly string Antrag = "https://www.itsg-trust.de/ostcv2/antrag.php";

        /// <summary>
        /// Über diese URL werden die eXTra-Nachrichten zur Abwicklung 
        /// der Auftragsbestätigung (2a und 2b) ausgetauscht. 
        /// </summary>
        public static readonly string Auftrag = "https://www.itsg-trust.de/ostcv2/auftrag.php";

        /// <summary>
        /// Über diese URL werden die eXTra-Nachrichten zur 
        /// Abholung eines Zertifikats (3a und 3b) ausgetauscht. 
        /// </summary>
        public static readonly string Schluessel = "https://www.itsg-trust.de/ostcv2/schluessel.php";

        /// <summary>
        /// Über diese URL werden die eXTra-Nachrichten zur 
        /// Abholung einer Schlüsselliste (4a und 4b) ausgetauscht.  
        /// </summary>
        public static readonly string Liste = "https://www.itsg-trust.de/ostcv2/liste.php";
    }
}
