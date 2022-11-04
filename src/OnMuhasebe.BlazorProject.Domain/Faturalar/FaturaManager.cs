﻿using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Extensions;
using Volo.Abp.Domain.Services;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class FaturaManager : DomainService
{
    private readonly IFaturaRepository _faturaRepository;
    private readonly ICariRepository _cariRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;
    private readonly IDonemRepository _donemRepository;
    public FaturaManager(IFaturaRepository faturaRepository, ICariRepository cariRepository, IOzelKodRepository ozelKodRepository, ISubeRepository subeRepository, IDonemRepository donemRepository)
    {
        _faturaRepository = faturaRepository;
        _cariRepository = cariRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
        _donemRepository = donemRepository;
    }
    public async Task CheckCreateAsync(string faturaNo, Guid? cariId, Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? subeId, Guid? donemId)
    {
        #region description
        //öncelikle Sube ve Donem alanlarının kontrolü sağlanmalıdır. Daha sonra Kod alanının kontrolü sağlanmalıdır.
        #endregion
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);

        await _faturaRepository.KodAnyAsync(faturaNo, x => x.FaturaNo == faturaNo && x.SubeId == subeId && x.DonemId == donemId);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.Fatura);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.Fatura);
    }
    public async Task CheckUpdateAsync(Guid id, string faturaNo, Fatura entity, Guid? cariId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _faturaRepository.KodAnyAsync(faturaNo, x => x.Id != id && x.FaturaNo == faturaNo && x.SubeId == entity.SubeId && x.DonemId == entity.DonemId, entity.FaturaNo != faturaNo);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1, KartTuru.Fatura, entity.OzelKod1Id != ozelKod1Id);
        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2, KartTuru.Fatura, entity.OzelKod2Id != ozelKod2Id);
    }
}