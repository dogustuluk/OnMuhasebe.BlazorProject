using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public class BankaHesapListParameterDto : PagedResultRequestDto, IEntityDto, IDurum
{
    public BankaHesapTuru? HesapTuru { get; set; }
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}
