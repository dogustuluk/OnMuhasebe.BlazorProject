using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.FaturaHareketler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class FaturaAppService : BlazorProjectAppService, IFaturaAppService
{
    private readonly IFaturaRepository _faturaRepository;

    public FaturaAppService(IFaturaRepository faturaRepository)
    {
        _faturaRepository = faturaRepository;
    }

    public virtual async Task<SelectFaturaDto> CreateAsync(CreateFaturaDto input)
    {
        var entity = ObjectMapper.Map<CreateFaturaDto, Fatura>(input);
        await _faturaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id, x => x.FaturaHareketler);

        await _faturaRepository.DeleteAsync(entity);
        entity.FaturaHareketler.RemoveAll(entity.FaturaHareketler);//Fatura silindiyse FaturaHareketler'e bağlı olanları da soft delete yapmamız gerekir.
    }

    public virtual async Task<SelectFaturaDto> GetAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id);

        var mappedDto = ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
        #region description_Include property
        /*
         * Include property yapmıyor oluşumuzun sebebi;
         * Bu sınıfın içerisinde ICollection olarak alınan navigation property'lerimizin içerisindeki navigation property'lerimizin değerlerinin dolu olarak gelmesini istediğimiz için EntityFramework katmanında WithDetails'i override ederek almış olucaz.
         * -
         * FaturaHareket'lerin içerisindeki SelectFaturaHareketDto'da HareketTuruAdi var. Bunu doldurmamız gerekmektedir. Bu doldurma işlemini AutoMapperProfile'dan yapamıyoruz. HareketTuruAdi enum bir değer aldığı için tr.json dosyasından localize bir şekilde yapıcaz. Foreach içerisinde de değeri alıyor olucaz json dosyasına yazdıktan sonra.
         */
        #endregion

        mappedDto.FaturaHareketler.ForEach(x =>
        {
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });
        return mappedDto;
    }

    public virtual async Task<string> GetCodeAsync(FaturaNoParameterDto input)
    {
        return await _faturaRepository.GetCodeAsync(x => x.FaturaNo, x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum);
    }

    public virtual async Task<PagedResultDto<ListFaturaDto>> GetListAsync(FaturaListParameterDto input)
    {
        var entities = await _faturaRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum,
            x => x.FaturaNo,
            x => x.Cari, x => x.OzelKod1, x => x.OzelKod2
            );

        var totalCount = await _faturaRepository.CountAsync(x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum);

        return new PagedResultDto<ListFaturaDto>(totalCount, ObjectMapper.Map<List<Fatura>, List<ListFaturaDto>>(entities));
    }

    public virtual async Task<SelectFaturaDto> UpdateAsync(Guid id, UpdateFaturaDto input)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id, x => x.FaturaHareketler);

        #region description
        /*FaturaHareketler
         * Eğer Fatura güncellenirken Fatura'ya yeni bir hareket ekleniyor ise bunun kontrolü sağlanmalı ve eğer yeni bir FaturaHareket ekleniyor ise bunu veri tabanına kaydetmeliyiz.
         * Eğer input ile gelen faturaHareket veri tabanında kayıtlı ise direkt olarak bunu faturaHareket'e map'le ve kaydet.
         */
        #endregion

        foreach (var faturaHareketDto in input.FaturaHareketler)
        {
             var faturaHareket = entity.FaturaHareketler.FirstOrDefault(x => x.Id == faturaHareketDto.Id);

            if (faturaHareket == null)
            {
                entity.FaturaHareketler.Add(ObjectMapper.Map<FaturaHareketDto, FaturaHareket>(faturaHareketDto));
                continue;
            }
            ObjectMapper.Map(faturaHareketDto, faturaHareket);
        }
        #region deletedEntities
        //Bu kod ile UI tarafından silinecek olan FaturaHareketler tutuluyor olacak. Örneğin database tarafında 5 adet FaturaHareketler var ise, UI'da iken bunlardan ikisini silersek 3 adet FaturaHareket kalması gerekir. Bu kod kaç adet FaturaHareketler olması gerektiğini bildirecek, henüz silme işlemini yapmaz. RemoveAll dedikten sonra listeyi buraya göndermemiz gerekir. Burada soft delete işlemi uyguluyor olucaz.
        #endregion
        var deletedEntities = entity.FaturaHareketler.Where(
            x => input.FaturaHareketler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);
        entity.FaturaHareketler.RemoveAll(deletedEntities);
        #region !!!description
        //Eğer herhangi bir ayarlama işlemi yapmazsak deletedEntities listesindekilerin haricinde tüm FaturaHareketler'i soft delete olarak uygular. Bunun önüne geçmek için AutoMapperProfile'a -> ForMember(x => x.FaturaHareketler, y => y.Ignore()); <- yazmamız gerekmektedir.

        #endregion
        ObjectMapper.Map(input, entity);

        await _faturaRepository.UpdateAsync(entity);

        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }
}