using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraStandard
{
    public class ExtraMessageException : ExtraException
    {
        public ExtraMessageException(IEnumerable<ExtraFlag> flags)
        {
#if NET40 || PCL40
            Flags = (flags as ICollection<ExtraFlag>) ?? flags.ToList();
#else
            Flags = ((IEnumerable<ExtraFlag>)(flags as IReadOnlyCollection<ExtraFlag>)) ?? ((IEnumerable<ExtraFlag>)(flags as ICollection<ExtraFlag>)) ?? flags.ToList();
#endif
        }

        public IEnumerable<ExtraFlag> Flags { get; }

#if PCL45
        public override string Message => string.Join("\r\n", Flags.Select(x => $"{x.Code}: {x.Text}"));
#else
        public override string Message => string.Join(Environment.NewLine, Flags.Select(x => $"{x.Code}: {x.Text}"));
#endif
    }
}
