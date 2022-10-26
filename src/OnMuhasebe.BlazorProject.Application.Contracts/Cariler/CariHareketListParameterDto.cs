using System;
using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Cariler;
public class CariHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{//işlenne hareketlerin dökümünü oluşturmak için gereklidir.
    public Guid CariId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}