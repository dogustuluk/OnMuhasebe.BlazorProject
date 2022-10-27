using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Depolar;
public interface IDepoAppService : ICrudAppService<SelectDepoDto, ListDepoDto, DepoListParameterDto, CreateDepoDto, UpdateDepoDto, DepoCodeParameterDto>
{
}
