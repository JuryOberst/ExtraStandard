using System;

namespace ExtraStandard.Extra14
{
    /// <summary>
    /// Hilfsfunktionen für die Ermittlung der eXTra-Flags
    /// </summary>
    public static class ExtraFlagExtensions
    {
        /// <summary>
        /// Erstellt eine neue Instanz der <see cref="ExtraFlag"/> Klasse.
        /// </summary>
        /// <param name="flag">Das eXTra-Standard 1.4-Flag aus dem die Werte übernommen werden sollen</param>
        public static ExtraFlag AsExtraFlag(this FlagType flag)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag));
            return new ExtraFlag()
            {
                Code = flag.Code?.Value,
                Originator = flag.Originator?.Value,
                Stack = flag.Stack?.Value,
                Weight = flag.weight,
                Text = flag.Text?.Value,
            };
        }

        /// <summary>
        /// Erstellt eine neue Instanz der <see cref="ExtraFlag"/> Klasse.
        /// </summary>
        /// <param name="flag">Das eXTra-Standard 1.4-Flag aus dem die Werte übernommen werden sollen</param>
        public static ExtraFlag AsExtraFlag(this FlagType1 flag)
        {
            if (flag == null)
                throw new ArgumentNullException(nameof(flag));
            return new ExtraFlag()
            {
                Code = flag.Code?.Value,
                Weight = flag.weight,
                Text = flag.Text?.Value,
            };
        }
    }
}
