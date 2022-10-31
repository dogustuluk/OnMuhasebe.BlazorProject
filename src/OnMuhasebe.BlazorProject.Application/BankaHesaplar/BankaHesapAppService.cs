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

    public BankaHesapAppService(IBankaHesapRepository bankaHesapRepository)
    {
        _bankaHesapRepository = bankaHesapRepository;
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
        throw new NotImplementedException();
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }



    public virtual async Task<string> GetCodeAsync(BankaHesapCodeParameterDto input)
    {
        throw new NotImplementedException();
    }



    public virtual async Task<SelectBankaHesapDto> UpdateAsync(Guid id, UpdateBankaHesapDto input)
    {
        throw new NotImplementedException();
    }
}
