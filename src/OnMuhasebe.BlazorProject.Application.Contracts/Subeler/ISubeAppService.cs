using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Subeler;
public interface ISubeAppService : ICrudAppService<SelectSubeDto, ListSubeDto, SubeListParameterDto, CreateSubeDto, UpdateSubeDto, CodeParameterDto>
{
}