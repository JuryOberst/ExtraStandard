using ExtraStandard.Extra14;

namespace ExtraStandard.Validation.Extra14
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.4-Dokumente
    /// </summary>
    public class GenericExtraValidatorFactory : IExtra14ValidatorFactory
    {
        public IExtra14Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new GenericExtraValidator(messageType, transportDirection, isError);
        }
    }
}
