using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public class BankaSubeCodeParameterDto : IEntityDto,IDurum
{
    public Guid BankaId { get; set; }
    public bool Durum { get; set; }
}
