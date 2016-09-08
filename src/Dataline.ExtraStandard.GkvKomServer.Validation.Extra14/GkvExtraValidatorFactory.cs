using ExtraStandard.GkvKomServer.Extra14;
using ExtraStandard.Validation;

namespace ExtraStandard.GkvKomServer.Validation.Extra14
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.4-Dokumente nach GKV-Standard
    /// </summary>
    public class GkvExtraValidatorFactory : IGkvExtra14ValidatorFactory
    {
        public IGkvExtra14Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new GkvExtraValidator(messageType, transportDirection, isError);
        }
    }
}
