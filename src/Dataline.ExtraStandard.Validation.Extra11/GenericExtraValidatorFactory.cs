using ExtraStandard.Extra11;

namespace ExtraStandard.Validation.Extra11
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.1-Dokumente
    /// </summary>
    public class GenericExtraValidatorFactory : IExtra11ValidatorFactory
    {
        public IExtra11Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new GenericExtraValidator(messageType, transportDirection, isError);
        }
    }
}
