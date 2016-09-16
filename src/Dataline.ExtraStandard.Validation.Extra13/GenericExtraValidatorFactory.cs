using ExtraStandard.Extra13;

namespace ExtraStandard.Validation.Extra13
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.3-Dokumente
    /// </summary>
    public class GenericExtraValidatorFactory : IExtra13ValidatorFactory
    {
        /// <inheritdoc />
        public IExtra13Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new GenericExtraValidator(messageType, transportDirection, isError);
        }
    }
}
