using Volo.Abp;

namespace OnMuhasebe.BlazorProject.Exceptions;
public class DuplicateCodeException : BusinessException
{
    /*
     * oluşturduğumuz exception'a hataya sebep olan kodu göndermemiz gerekmektedir. Kodu constructor ile alıyoruz, ve bu alınan kodu oluşan hata mesajına göndermemiz gerekmektedir. bunu ABP Framework'ün hazır olarak sunmuş olduğu WithData function'ı ile yapabiliriz.
     */
    public DuplicateCodeException( string kod) : base(BlazorProjectDomainErrorCodes.DuplicateKod )
    {
        WithData("kod",kod);
    }
}