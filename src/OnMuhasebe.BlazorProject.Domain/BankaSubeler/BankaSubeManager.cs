using System.Linq;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Extensions;
using Volo.Abp.Domain.Services;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class BankaSubeManager : DomainService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IBankaRepository _bankaRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BankaSubeManager(IOzelKodRepository ozelKodRepository, IBankaRepository bankaRepository, IBankaSubeRepository bankaSubeRepository)
    {
        _ozelKodRepository = ozelKodRepository;
        _bankaRepository = bankaRepository;
        _bankaSubeRepository = bankaSubeRepository;
    }
    public async Task CheckCreateAsync(string kod, Guid? bankaId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        #region Description
        /* İlk önce veri tabanında bir Banka olup olmadığını kontrol etmemiz gerekir. Çünkü kod üretebilmemiz için -Create aşamasında test edebilmemiz için Banka'nın olması gerekmektedir. - Banka gerekir.
         * Daha sonra Kod ile ilgili kontrol yapılır. Kod üretebilmemiz için; Sube bilgisi, BankaId bilgisine ve Durum bilgisine ihtiyacımız vardır. BankaSubeAppService sınıfında bulunan GetCodeAsync metodunun parametresi olan BankaSubeCodeParameterDto sınıfında belirtilir.
         * Daha sonra OzelKod alanları ile ilgili kontrol yapılır.
         */
        #endregion
        await _bankaRepository.EntityAnyAsync(bankaId, x => x.Id == bankaId); //
        
        await _bankaSubeRepository.KodAnyAsync(kod, x => x.Kod == kod && x.BankaId == bankaId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.BankaSube);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.BankaSube);

    }
    public async Task CheckUpdateAsync(Guid id, string kod, BankaSube entity, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        //Kod ile ilgili kontrol
        await _bankaSubeRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod && x.BankaId == entity.BankaId, entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.BankaSube, entity.OzelKod1Id != ozelKod1Id);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.BankaSube, entity.OzelKod2Id != ozelKod2Id);
    }
    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaSubeRepository.RelationalEntityAnyAsync(x => 
            x.BankaHesaplar.Any(y => y.BankaSubeId == id) 
            || 
            x.MakbuzHareketler.Any(y => y.CekBankaSubeId == id));
    }
}
