using ExtraStandard.GkvKomServer.Extra13;
using ExtraStandard.Validation;

namespace ExtraStandard.GkvKomServer.Validation.Extra13
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.3-Dokumente nach GKV-Standard
    /// </summary>
    public class GkvExtraValidatorFactory : IGkvExtra13ValidatorFactory
    {
        /// <inheritdoc />
        public IGkvExtra13Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new GkvExtraValidator(messageType, transportDirection, isError);
        }
    }
}
