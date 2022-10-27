using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Donemler;
public interface IDonemAppService : ICrudAppService<SelectDonemDto, ListDonemDto, DonemListParameterDto, CreateDonemDto, UpdateDonemDto, CodeParameterDto>
{
}