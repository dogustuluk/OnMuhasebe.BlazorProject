using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Faturalar;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Stoklar;
public class StokHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public FaturaHareketTuru? HareketTuru { get; set; }
    public Guid EntityId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}