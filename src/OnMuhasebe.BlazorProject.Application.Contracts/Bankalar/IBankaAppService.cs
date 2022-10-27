using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Bankalar;
public interface IBankaAppService : ICrudAppService<SelectBankaDto, ListBankaDto, BankaListParameterDto, CreateBankaDto, UpdateBankaDto, CodeParameterDto>
{

}
