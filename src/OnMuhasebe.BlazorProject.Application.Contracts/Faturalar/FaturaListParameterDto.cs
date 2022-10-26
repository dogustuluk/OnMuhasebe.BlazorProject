using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Faturalar;
public class FaturaListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public FaturaTuru FaturaTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}
