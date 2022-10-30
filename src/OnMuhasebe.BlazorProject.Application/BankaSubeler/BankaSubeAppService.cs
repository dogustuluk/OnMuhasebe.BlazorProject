using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.BankaHesaplar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class BankaSubeAppService : BlazorProjectAppService, IBankaSubeAppService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;

    public BankaSubeAppService(IBankaSubeRepository bankaSubeRepository)
    {
        _bankaSubeRepository = bankaSubeRepository;
    }

    public virtual async Task<SelectBankaSubeDto> CreateAsync(CreateBankaSubeDto input)
    {
        var entity = ObjectMapper.Map<CreateBankaSubeDto, BankaSube>(input);
        
        await _bankaSubeRepository.InsertAsync(entity);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaSubeRepository.DeleteAsync(id);
    }

    public virtual async Task<SelectBankaSubeDto> GetAsync(Guid id)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, bs => bs.Id == id, x => x.Banka, x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }

    public virtual async Task<string> GetCodeAsync(BankaSubeCodeParameterDto input)
    {
        return await _bankaSubeRepository.GetCodeAsync(x => x.Kod, x => x.BankaId == input.BankaId && x.Durum == input.Durum);
    }
    /// <summary>
    /// Banka şubelerini listeler. Listeleme yapabilmek için; BankaId ile Durum property'lerini dolu olarak yollamamız zorunludur.
    /// </summary>
    public virtual async Task<PagedResultDto<ListBankaSubeDto>> GetListAsync(BankaSubeListParameterDto input)
    {
        var entities = await _bankaSubeRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.BankaId == input.BankaId && x.Durum == input.Durum, //predicates
            x => x.Kod, //orderBy
            x => x.Banka, x => x.OzelKod1, x => x.OzelKod2 //navigation properties -include properties -
            );

        var totalCount = await _bankaSubeRepository.CountAsync(x => x.BankaId == input.BankaId && x.Durum == input.Durum);

        return new PagedResultDto<ListBankaSubeDto>(totalCount, ObjectMapper.Map<List<BankaSube>, List<ListBankaSubeDto>>(entities));
    }

    public virtual async Task<SelectBankaSubeDto> UpdateAsync(Guid id, UpdateBankaSubeDto input)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, bs => bs.Id == id);
        
        var mappedEntity = ObjectMapper.Map(input, entity);

        await _bankaSubeRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(mappedEntity);
    }
}