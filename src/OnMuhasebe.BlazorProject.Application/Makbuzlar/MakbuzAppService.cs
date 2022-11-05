using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.MakbuzHareketler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public class MakbuzAppService : BlazorProjectAppService, IMakbuzAppService
{
    private readonly IMakbuzRepository _makbuzRepository;
    public MakbuzAppService(IMakbuzRepository makbuzRepository)
    {
        _makbuzRepository = makbuzRepository;
    }

    public virtual async Task<SelectMakbuzDto> CreateAsync(CreateMakbuzDto input)
    {
        var entity = ObjectMapper.Map<CreateMakbuzDto, Makbuz>(input);
        await _makbuzRepository.InsertAsync(entity);
        return ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id, x => x.MakbuzHareketler);

        await _makbuzRepository.DeleteAsync(entity);
        entity.MakbuzHareketler.RemoveAll(entity.MakbuzHareketler);
    }

    public virtual async Task<SelectMakbuzDto> GetAsync(Guid id)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id);
        var mappedDto = ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);

        mappedDto.MakbuzHareketler.ForEach(x =>
       {

           x.OdemeTuruAdi = L[$"Enum:OdemeTuru:{ (byte)x.OdemeTuru}"];
           x.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)x.BelgeDurumu}"];
       });

        return mappedDto;
    }

    public virtual async Task<string> GetCodeAsync(MakbuzNoParameterDto input)
    {
        return await _makbuzRepository.GetCodeAsync(x => x.MakbuzNo, x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum);
    }

    public virtual async Task<PagedResultDto<ListMakbuzDto>> GetListAsync(MakbuzListParameterDto input)
    {
        var entities = await _makbuzRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum,
            x => x.MakbuzNo,
            x => x.Cari, x => x.Kasa, x => x.BankaHesap, x => x.OzelKod1, x => x.OzelKod2
            );

        var totalCount = await _makbuzRepository.CountAsync(x => x.MakbuzTuru == input.MakbuzTuru && x.SubeId == input.SubeId && x.DonemId == input.DonemId && x.Durum == input.Durum);

        return new PagedResultDto<ListMakbuzDto>(totalCount, ObjectMapper.Map<List<Makbuz>, List<ListMakbuzDto>>(entities));
    }

    public virtual async Task<SelectMakbuzDto> UpdateAsync(Guid id, UpdateMakbuzDto input)
    {
        var entity = await _makbuzRepository.GetAsync(id, x => x.Id == id, x => x.MakbuzHareketler);//MakbuzHareketler'i include etmemizin sebebi aşağıdaki kodlarda onla ilgili işlem yapacak olmamız.
        foreach (var makbuzHareketDto in input.MakbuzHareketler)
        {
            //FirstOrDefault kullandığımız için null gelip gelmediğini kontrol etmemiz gerekmektedir.
            var makbuzHareket = entity.MakbuzHareketler.FirstOrDefault(
                x => x.Id == makbuzHareketDto.Id);

            if (makbuzHareket == null)
            {
                entity.MakbuzHareketler.Add(ObjectMapper.Map<MakbuzHareketDto, MakbuzHareket>(makbuzHareketDto));
                continue;
            }
            ObjectMapper.Map(makbuzHareketDto, makbuzHareket);
        }
        var deletedEntities = entity.MakbuzHareketler.Where(
            x => input.MakbuzHareketler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);//database'de olup UI'da olmayan MakbuzHareketler'i getirmiş olacak. Çünkü bunların silinmesi gerekir.
        entity.MakbuzHareketler.RemoveAll(deletedEntities);//silinmesi gereken hareketler silinmiş oldu.
        
        ObjectMapper.Map(input, entity);
        await _makbuzRepository.UpdateAsync(entity);
        return ObjectMapper.Map<Makbuz, SelectMakbuzDto>(entity);
    }
}
