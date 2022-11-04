using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.Hizmetler;
public class HizmetAppService : BlazorProjectAppService, IHizmetAppService
{
    private readonly IHizmetRepository _hizmetRepository;
    private readonly HizmetManager _hizmetManager;
    public HizmetAppService(IHizmetRepository hizmetRepository, HizmetManager hizmetManager)
    {
        _hizmetRepository = hizmetRepository;
        _hizmetManager = hizmetManager;
    }

    public virtual async Task<SelectHizmetDto> CreateAsync(CreateHizmetDto input)
    {
        await _hizmetManager.CheckCreateAsync(input.Kod, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);
        var entity = ObjectMapper.Map<CreateHizmetDto, Hizmet>(input);
        await _hizmetRepository.InsertAsync(entity);
        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _hizmetManager.CheckDeleteAsync(id);
        await _hizmetRepository.DeleteAsync(id);
    }

    public virtual async Task<SelectHizmetDto> GetAsync(Guid id)
    {
        var entity = await _hizmetRepository.GetAsync(id, x => x.Id == id, x => x.Birim, x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(entity);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _hizmetRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }

    public virtual async Task<PagedResultDto<ListHizmetDto>> GetListAsync(HizmetListParameterDto input)
    {
        //include properties'leri EfCoreHizmetRepository sınıfında yapıyor olucaz.
        var entities = await _hizmetRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, x => x.Durum == input.Durum, x => x.Kod);

        var totalCount = await _hizmetRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListHizmetDto>(totalCount, ObjectMapper.Map<List<Hizmet>, List<ListHizmetDto>>(entities));
    }

    public virtual async Task<SelectHizmetDto> UpdateAsync(Guid id, UpdateHizmetDto input)
    {
        var entity = await _hizmetRepository.GetAsync(id, x => x.Id == id);

        await _hizmetManager.CheckUpdateAsync(id, input.Kod, entity, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);

        await _hizmetRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(mappedEntity);
    }
}
