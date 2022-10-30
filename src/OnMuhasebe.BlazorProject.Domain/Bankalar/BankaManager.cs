using System.Linq;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Extensions;
using Volo.Abp.Domain.Services;

namespace OnMuhasebe.BlazorProject.Bankalar;
public class BankaManager : DomainService
{
    private readonly IBankaRepository _bankaRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BankaManager(IOzelKodRepository ozelKodRepository, IBankaRepository bankaRepository)
    {
        _ozelKodRepository = ozelKodRepository;
        _bankaRepository = bankaRepository;
    }

    /// <summary>
    /// <para>Create işlemi yaparken gelen id ve kod alanlarını check etmekle sorumlu metot.</para>
    /// </summary>
    /// <returns></returns>
    /// /*metot içerisindeki parametreleri ayrı bir interface olarak ya da params şeklinde yazabiliriz fakat kullanışlı olmamaktadır. Çünkü her entity için farklı farklı parametreler olmaktadır, dolayısıyla buradaki gibi açık açık yazmamız gerekmektedir.
    /// */
    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaRepository.KodAnyAsync(kod, x => x.Kod == kod);
        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.Banka);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.Banka);

    }
    /// <summary>
    /// <para>Güncelleme esnasında hata oluşup oluşmadığını kontrol ederiz. Burada farklı bir id'ye sahip datanın veri tabanındaki aynı koda sahip olması durumunda hata fırlatılacaktır. </para>
    /// <para>Check durumunu ise entity'den gelen Kod alanının parametre ile gelen kod alanı ile karşılaştır, eğer farklı ise Check property'sini true olarak ayarla, değilse Check property'sini false'a çekip herhangi bir şekilde hata fırlatmamış oluyoruz.</para>
    /// entity'nin kendisini(banka) alıyoruz ki property'lerini kullanabilelim.
    /// </summary>
    public async Task CheckUpdateAsync(Guid id, string kod, Banka entity, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod, entity.Kod != kod);
        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.Banka, entity.OzelKod1Id != ozelKod1Id);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.Banka, entity.OzelKod2Id != ozelKod2Id);

    }
    /// <summary>
    /// <para>Bu function ile silmeye çalışılan entity'nin bir relation'ı olup olmadığı kontrol edilmeye çalışılacak. İlişkili olduğu entity tarafından kullanılıp kullanılmadığının teyiti sağlanmış olacak. Eğer kullanılmışsa silinmesine izin verilmeyecek aksi durumda silinebilir bir şekilde olacak.</para>
    /// <para>Buradaki relation'lar ICollection tipindeki ilişkiler olmalıdır.</para>
    /// </summary>
    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaRepository.RelationalEntityAnyAsync(x => 
            x.BankaSubeler.Any(y =>y.BankaId == id) 
        || 
            x.MakbuzHareketler.Any(y => y.CekBankaId == id));
    }
}
