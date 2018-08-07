using ExtraStandard.OstcServer.Extra11;
using ExtraStandard.Validation;

namespace ExtraStandard.OstcServer.Validation.Extra11
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.1-Dokumente nach OSTC-Standard
    /// </summary>
    public class OstcExtraValidatorFactory : IOstcExtra11ValidatorFactory
    {
        /// <inheritdoc />
        public IOstcExtra11Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new OstcExtraValidator(messageType, transportDirection, isError);
        }
    }
}
