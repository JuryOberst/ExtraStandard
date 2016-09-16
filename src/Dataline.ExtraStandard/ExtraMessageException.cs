using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraStandard
{
    /// <summary>
    /// Zurückgemeldete Fehler
    /// </summary>
    public class ExtraMessageException : ExtraException
    {
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="ExtraMessageException"/> Klasse.
        /// </summary>
        /// <param name="flags">Die zurückgemeldeten Status-Codes</param>
        public ExtraMessageException(IEnumerable<ExtraFlag> flags)
        {
#if PCL
#if PCL40
            Flags = (flags as ICollection<ExtraFlag>) ?? flags.ToList();
#else
            Flags = (IEnumerable<ExtraFlag>)(flags as IReadOnlyCollection<ExtraFlag>) ?? flags as ICollection<ExtraFlag> ?? flags.ToList();
#endif
#else
#if NET40
            Flags = (flags as ICollection<ExtraFlag>) ?? flags.ToList().AsReadOnly();
#else
            Flags = (IEnumerable<ExtraFlag>)(flags as IReadOnlyCollection<ExtraFlag>) ?? flags as ICollection<ExtraFlag> ?? flags.ToList().AsReadOnly();
#endif
#endif
        }

        /// <summary>
        /// Holt die zurückgemeldeten Status-Codes
        /// </summary>
        public IEnumerable<ExtraFlag> Flags { get; }

#if PCL45
        /// <inheritdoc />
        public override string Message => string.Join("\r\n", Flags.Select(x => $"{x.Code}: {x.Text}"));
#else
        /// <inheritdoc />
        public override string Message => string.Join(Environment.NewLine, Flags.Select(x => $"{x.Code}: {x.Text}"));
#endif
    }
}
