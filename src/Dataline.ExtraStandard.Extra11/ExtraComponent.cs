using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Der Einfachheit halber alle Klassen-Erweiterungen in einer Datei, weil die Klassen selbst auch in einer einzigen Datei sind.")]

namespace ExtraStandard.Extra11
{
    public partial class DataType1 : IExtraComponent
    {
    }

    public partial class TransformedDataType : IExtraComponent
    {
    }

    public partial class MessageRequestType : IExtraComponent
    {
    }

    public partial class MessageResponseType : IExtraComponent
    {
    }

    public partial class EncryptedDataType : IExtraComponent
    {
    }

    public partial class PackageRequestType : IExtraComponent
    {
    }

    public partial class PackageResponseType : IExtraComponent
    {
    }
}
