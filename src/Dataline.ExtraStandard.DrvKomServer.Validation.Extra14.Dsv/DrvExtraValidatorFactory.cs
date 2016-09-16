using ExtraStandard.DrvKomServer.Extra14.Dsv;
using ExtraStandard.Validation;

namespace ExtraStandard.DrvKomServer.Validation.Extra14.Dsv
{
    /// <summary>
    /// Standard-Implementation einer Factory für einen <see cref="ExtraValidator"/> für eXTra-1.4-Dokumente nach DRV-Standard für die SV-Nummern-Abfrage
    /// </summary>
    public class DrvExtraValidatorFactory : IDrvDsvExtra14ValidatorFactory
    {
        /// <inheritdoc />
        public IDrvDsvExtra14Validator Create(ExtraMessageType messageType, ExtraTransportDirection transportDirection, bool isError)
        {
            return new DrvExtraValidator(messageType, transportDirection, isError);
        }
    }
}
