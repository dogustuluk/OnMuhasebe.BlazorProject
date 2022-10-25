using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Cariler;
public class CariListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}
