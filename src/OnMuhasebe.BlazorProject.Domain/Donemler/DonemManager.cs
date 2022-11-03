using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Extensions;
using Volo.Abp.Domain.Services;

namespace OnMuhasebe.BlazorProject.Donemler;
public class DonemManager : DomainService
{
    private readonly IDonemRepository _donemRepository;
    public DonemManager(IDonemRepository donemRepository)
    {
        _donemRepository = donemRepository;
    }
    public async Task CheckCreateAsync(string kod)
    {
        await _donemRepository.KodAnyAsync(kod, x => x.Kod == kod);
    }
    public async Task CheckUpdateAsync(Guid id, string kod, Donem entity)
    {
        await _donemRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod, entity.Kod != kod);
    }
    public async Task CheckDeleteAsync(Guid id)
    {
        await _donemRepository.RelationalEntityAnyAsync
            (
            x => 
                x.Faturalar.Any(y => y.DonemId == id) 
                || 
                x.Makbuzlar.Any(y => y.DonemId == id)
            );
    }
}
