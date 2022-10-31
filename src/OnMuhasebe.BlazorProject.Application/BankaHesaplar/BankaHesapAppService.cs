using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class BankaHesapAppService : BlazorProjectAppService, IBankaHesapAppService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly BankaHesapManager _bankaHesapManager;
    public BankaHesapAppService(IBankaHesapRepository bankaHesapRepository, BankaHesapManager bankaHesapManager)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaHesapManager = bankaHesapManager;
    }

    public virtual async Task<SelectBankaHesapDto> GetAsync(Guid id)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id, x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2);

        var mappedDto = ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
        #region description
        /*burada SelectBankaHesapDto'da HesapTuruAdi dolu olarak gelmiyor, AutoMapperProfile içerisinden yapamıyoruz, localize olarak halledebiliyoruz.
         * Aşağıdaki gibi abp framework'ün localization için sağladığı "L" ifadesi ile ulaşmak istediğimiz hedef, SelectBankaHesapTuru içerisindeki HesapTuru property'sinin sayısal olarak enum değerine ulaşmaktır. "Enum:BankaHesapTuru:" ismi ise tr.json dosyasına yazmış olduğumuz alandır.
         */
        #endregion
        mappedDto.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)mappedDto.HesapTuru}"];

        return mappedDto;
    }
    /// <summary>
    /// <para>Banka Hesaplarını listeleyen metottur. parametre olarak BankaHesapListParameterDto aldığından dolayı input içerisinde SubeId ve Durum olmak zorundadır. BankaHesapTuru olmayabilir. entity'leri alırken GetPagedListAsync içerisinde; SkipCount, MaxResultCount, predicate, orderBy ve includeProperties geçilmelidir.</para>
    /// </summary>
    public virtual async Task<PagedResultDto<ListBankaHesapDto>> GetListAsync(BankaHesapListParameterDto input)
    {
        #region description
        /*#1 -> BankaHesapTuruAdi'nin tr.json dosyasından okunup UI tarafına yazmak için kullanılır.
         * includeProperties olarak MakbuzHareketler'i çekmemizin nedeni ise; ListBankaHesapDto sınıfında bulunan Borc ve Alacak property'lerinin bu sınıf yüklenirken hesaplanıp gelmesi için gereklidir. Bndan dolayı bu iki property'nin hesaplanabilmesi için MakbuzHareketler'i includeProperties olarak ekleriz.
         */
        #endregion
        var entities = await _bankaHesapRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => input.HesapTuru == null
                ?
                x.SubeId == input.SubeId && x.Durum == input.Durum
                :
                x.HesapTuru == input.HesapTuru && x.SubeId == input.SubeId && x.Durum == input.Durum, //predicate
            x => x.Kod, //orderBy 
            x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2, x => x.MakbuzHareketler);

        var totalCount = await _bankaHesapRepository.CountAsync
            (
                x => input.HesapTuru == null
                ?
                x.SubeId == input.SubeId && x.Durum == input.Durum
                :
                x.HesapTuru == input.HesapTuru && x.SubeId == input.SubeId && x.Durum == input.Durum
            );

        var mappedDtos = ObjectMapper.Map<List<BankaHesap>, List<ListBankaHesapDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)x.HesapTuru}"]; //#1

            x.Borc = x.MakbuzHareketler.Where
            (
                y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi
                ||
                y.OdemeTuru == OdemeTuru.Pos && y.BelgeDurumu == BelgeDurumu.Portfoyde)
                .Sum(y => y.Tutar);

            x.Alacak = x.MakbuzHareketler.Where
            (
                y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi)
                .Sum(y => y.Tutar);
        });

        return new PagedResultDto<ListBankaHesapDto>(totalCount, mappedDtos);
    }
    public virtual async Task<SelectBankaHesapDto> CreateAsync(CreateBankaHesapDto input)
    {
        await _bankaHesapManager.CheckCreateAsync(input.Kod, input.BankaSubeId, input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateBankaHesapDto, BankaHesap>(input);
        await _bankaHesapRepository.InsertAsync(entity);
        #region descriptionMap
        /*UI'dan gelen dto'yu veri tabanına yollamak için mapleme yapılır. Maplemede UI'dan gelen CreateBankaHesapDto'yu veri tabanına gidecek olan entity'e map'liyoruz (BankaHesap). Daha sonra veri tabanına inserAsync ile kaydediyoruz. Bundan sonra veri tabanında oluşan id'si ile beraber UI katmanına yollayıp kullanıcıya göndermek için tekrardan bir map işlemi uygulanır. Burada veri tabanına yollanan entity'i (BankaHesap) UI katmanda gösterebileceğimiz entity olan SelectBankaHesapDto'ya map'liyoruz.
         */
        #endregion
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
    }
    public virtual async Task<SelectBankaHesapDto> UpdateAsync(Guid id, UpdateBankaHesapDto input)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id);

        await _bankaHesapManager.CheckUpdateAsync(id, input.Kod, entity, input.BankaSubeId, input.OzelKod1Id, input.OzelKod2Id);
        #region descriptionMap
        //Eğer elimizde birbirine map edilebilecek entity'ler hazır ise generic bir yapıyı kullanmayız. UI'dan gelen input ve veri tabanında çektiğimiz entity.
        #endregion
        var mappedEntity = ObjectMapper.Map(input, entity);

        await _bankaHesapRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(mappedEntity);
    }
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaHesapManager.CheckDeleteAsync(id);
        await _bankaHesapRepository.DeleteAsync(id);
    }
    public virtual async Task<string> GetCodeAsync(BankaHesapCodeParameterDto input)
    {
        return await _bankaHesapRepository.GetCodeAsync(x => x.Kod, x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}