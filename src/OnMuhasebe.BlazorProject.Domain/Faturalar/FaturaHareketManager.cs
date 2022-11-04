using System.Threading.Tasks;
using OnMuhasebe.BlazorProject.Extensions;
using Volo.Abp.Domain.Services;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class FaturaHareketManager : DomainService
{
    private readonly IStokRepository _stokRepository;
    private readonly IDepoRepository _depoRepository;
    private readonly IMasrafRepository _masrafRepository;
    private readonly IHizmetRepository _hizmetRepository;

    public FaturaHareketManager(IHizmetRepository hizmetRepository, IMasrafRepository masrafRepository, IDepoRepository depoRepository, IStokRepository stokRepository)
    {
        _hizmetRepository = hizmetRepository;
        _masrafRepository = masrafRepository;
        _depoRepository = depoRepository;
        _stokRepository = stokRepository;
    }
    public async Task CheckCreateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId, Guid? depoId)
    {
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }
    public async Task CheckUpdateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId, Guid? depoId)
    {
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }
}