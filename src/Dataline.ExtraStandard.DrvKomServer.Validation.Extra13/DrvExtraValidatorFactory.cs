using ExtraStandard.DrvKomServer.Extra13;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra13
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.3-Dokumente nach DRV-Standard
    /// </summary>
    public class DrvExtraValidatorFactory : IDrvExtra13ValidatorFactory
    {
        /// <inheritdoc />
        public IDrvExtra13Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new DrvExtraValidator(messageType, transportDirection, isError);
        }
    }
}
