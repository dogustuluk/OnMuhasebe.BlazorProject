using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Bankalar;
public class BankaAppService : BlazorProjectAppService, IBankaAppService
{
    /*İnclude properties
     * entity'lerimizin navigation property'lerine karşılık gelir.
     */
    private readonly IBankaRepository _bankaRepository;
    private readonly BankaManager _bankaManager;
    public BankaAppService(IBankaRepository bankaRepository, BankaManager bankaManager)
    {
        _bankaRepository = bankaRepository;
        _bankaManager = bankaManager;
    }
    /// <summary>
    /// <para>Banka nesnesini oluşturmak için kullanılan metot. burada üç adet kontrol yapılmaktadır; aynı kodun kullanılması, OzelKod1Id database'de olup olmadığı ve OzelKod2Id'nin var olup olmadığı</para>
    /// </summary>
    public virtual async Task<SelectBankaDto> CreateAsync(CreateBankaDto input)
    {
        await _bankaManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateBankaDto, Banka>(input);
        await _bankaRepository.InsertAsync(entity);
        //database'de create olan entity'i alıp tekrar map edip geri göndermemiz gerekiyor.
        return ObjectMapper.Map<Banka, SelectBankaDto>(entity);//Banka olarak gelen entity'i SelectBankaDto'ya dönüştü.
        //henüz bitmedi, ek yapılacak.
    }
    /// <summary>
    /// <para>Metot hard delete işlemini yapmamaktadır, soft delete olarak çalışmaktadır.</para>
    /// </summary>
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaManager.CheckDeleteAsync(id);

        await _bankaRepository.DeleteAsync(id);
    }
    /// <summary>
    /// belirli bir Banka'yı çekmek için kullanılır.
    /// <para>b.id ile parametreden gelen id eşleşir.</para>
    /// <para>b.OzelKod1 ve b.OzelKod2 ile navigation property eşleştirilir. Bu sayede Banka verisi çekilirken özel kod adları da alınır.</para>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task<SelectBankaDto> GetAsync(Guid id)
    {
        var entity = await _bankaRepository.GetAsync(id, b => b.Id == id, b => b.OzelKod1, b => b.OzelKod2);
        return ObjectMapper.Map<Banka, SelectBankaDto>(entity);
    }

    /// <summary>
    /// Hangi Durum geliyorsa veri tabanında Banka entitysine gidecek ve onların içerisindeki en büyük kodu alıp bir arttırmış olacak.
    /// <para>Kodlarda isDeleted durumunu kontrol etmemize gerek yoktur, ABP Framework bunu kendisi otomatik olarak yapmaktadır.</para>
    /// </summary>
    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _bankaRepository.GetCodeAsync(b => b.Kod, b => b.Durum == input.Durum);
    }

    /// <summary>
    /// <param>parametreden gelen 'Durum' property'si predicate olmaktadır.</param>
    /// <param>parametreden gelen 'Kod' property'si orderBy olmaktadır.</param>
    /// <param>Geri dönüş olarak ise PagedResultDto veriyoruz. PagedResultDto, TotalCount istemektedir.</param>
    /// </summary>
    public virtual async Task<PagedResultDto<ListBankaDto>> GetListAsync(BankaListParameterDto input)
    {
        var entities = await _bankaRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            b => b.Durum == input.Durum,
            b => b.Kod,
            b => b.OzelKod1,//include properties
            b => b.OzelKod2//include properties
            );
        var totalCount = await _bankaRepository.CountAsync(b => b.Durum == input.Durum);
        return new PagedResultDto<ListBankaDto>
            (
            totalCount,
            ObjectMapper.Map<List<Banka>, List<ListBankaDto>>(entities)
            );
    }

    /// <summary> 
    /// <para>mappedEntity bir Banka'dır.Update işleminden sonra geriye SelectBankaDto dönmesi gerekir. Tekrardan maplenir. </para>
    /// </summary>
    public virtual async Task<SelectBankaDto> UpdateAsync(Guid id, UpdateBankaDto input)
    {
        #region normal mapleme yapmamızın sebebi
        /*
         * çünkü iki tane hazır, içerisi dolu entity elimizde mevcut(input ve entity).UI'dan geleni entity'e mapliyoruz.
         * eğer iki tane instance'ı yapılmış, hazır entity'miz varsa bu şekilde mapleme yap
         */
        #endregion

        var entity = await _bankaRepository.GetAsync(id, b => b.Id == id);

        await _bankaManager.CheckUpdateAsync(id,input.Kod,entity,input.OzelKod1Id,input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Banka, SelectBankaDto>(mappedEntity);
    }
}