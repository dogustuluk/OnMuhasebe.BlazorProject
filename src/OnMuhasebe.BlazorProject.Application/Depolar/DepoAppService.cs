using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Faturalar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Depolar;
public class DepoAppService : BlazorProjectAppService, IDepoAppService
{
    private readonly IDepoRepository _depoRepository;
    private readonly DepoManager _depoManager;
    public DepoAppService(IDepoRepository depoRepository, DepoManager depoManager)
    {
        _depoRepository = depoRepository;
        _depoManager = depoManager;
    }

    public virtual async Task<SelectDepoDto> CreateAsync(CreateDepoDto input)
    {
        await _depoManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateDepoDto, Depo>(input);

        await _depoRepository.InsertAsync(entity);

        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _depoManager.CheckDeleteAsync(id);
        await _depoRepository.DeleteAsync(id);
    }

    public virtual async Task<SelectDepoDto> GetAsync(Guid id)
    {
        #region description
        //Depo class'ındaki Sube property'sini include etmiyoruz çünkü onunla ilgili herhangi bir işlem yapmıyoruz.
        #endregion
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }

    public virtual async Task<string> GetCodeAsync(DepoCodeParameterDto input)
    {
        return await _depoRepository.GetCodeAsync(x => x.Kod, x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }

    public virtual async Task<PagedResultDto<ListDepoDto>> GetListAsync(DepoListParameterDto input)
    {
        #region description
        //predicate olarak SubeId ve Durum vermemizin sebebi DepoListParameterDto sınıfında bu iki alanı zorunlu tutup bir sıralama yaptığımız için.
        
        /*include properties açıklaması
         * Burada FaturaHareketler'i ve FaturaHareketler'in içerisindeki Fatura'yı include properties olarak eklemedik çünkü ICollection olarak eklenen bir sınıfı include properties olarak burada geçemeyiz. Kısaca ICollection tipinde olan bir property'nin içerisindeki herhangi bir navigation propert'e bu şekilde ulaşamayız. Burada FaturaHareketler eklenir fakat ondan hareketler Fatura eklenemez.
         * ICollection tipindeki navigation property'lerinde include ve thenInclude işlemleri burada her zamanki gibi yapılamaz. Bu işlem için EntityFrameworkCore katmanına geçip ilgili sınıfın (Buradaki -> EfCoreDepoRepository) içerisinde WithDetailsAsync'i override edip yapabiliriz.
         */
        #endregion
        var entities = await _depoRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, x => x.SubeId == input.SubeId && x.Durum == input.Durum, x => x.Kod);
        
        var totalCount = await _depoRepository.CountAsync(x => x.SubeId == input.SubeId && x.Durum == input.Durum);

        var mappedDtos = ObjectMapper.Map<List<Depo>, List<ListDepoDto>>(entities);

        #region description2
        //ListDepoDto'nun içerisinde Giren ve Cikan property'lerinin değerlerini almamız gerekir. Bunun için mappedDtos içerisinde dolaşmamız gerekir ki her bir dto için FaturaHareketler'in içerisinde gerekli toplamları alıcaz.
        //Eğer burada Giren-Cikan değerlerini hesaplamak istersek döngüsel başvuru hatasını düzeltmek için BlazorModule'a gerekli kodlar yazılmalıdır. Bunun önüne geçmek için örnek kodlar BlazorModule sınıfı içerisinde yazılmıştır. İç içer fazlaca döngü olmaması için alternatif bir çözüm üretmiş oluyoruz. Bu çözümde buradaki işlemleri AutoMapperProfile içerisine taşıyıp farklı bir yöntem denemiş olucaz.
        #endregion
        //mappedDtos.ForEach(x =>
        //{
        //    x.Giren = x.FaturaHareketler.Where(y => y.Fatura.FaturaTuru == FaturaTuru.Alis).Sum(y => y.Miktar);

        //    x.Cikan = x.FaturaHareketler.Where(y => y.Fatura.FaturaTuru == FaturaTuru.Satis).Sum(y => y.Miktar);
        //});

        return new PagedResultDto<ListDepoDto>(totalCount, mappedDtos);
    }

    public virtual async Task<SelectDepoDto> UpdateAsync(Guid id, UpdateDepoDto input)
    {
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id);

        await _depoManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);

        await _depoRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Depo, SelectDepoDto>(mappedEntity);
    }
}
