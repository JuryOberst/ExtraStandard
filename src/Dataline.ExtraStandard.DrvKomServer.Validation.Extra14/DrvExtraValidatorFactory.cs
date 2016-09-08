using ExtraStandard.DrvKomServer.Extra14;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra14
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.4-Dokumente nach DRV-Standard
    /// </summary>
    public class DrvExtraValidatorFactory : IDrvExtra14ValidatorFactory
    {
        public IDrvExtra14Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new DrvExtraValidator(messageType, transportDirection, isError);
        }
    }
}
