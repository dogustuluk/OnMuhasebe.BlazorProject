using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Depolar;
public class DepoListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid SubeId { get; set; }
    public bool Durum { get; set; }
}
