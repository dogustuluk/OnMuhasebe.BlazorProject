using OnMuhasebe.BlazorProject.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.Bankalar;
public class BankaListParameterDto : PagedResultRequestDto, IEntityDto, IDurum
{
    public bool Durum { get; set; }
}