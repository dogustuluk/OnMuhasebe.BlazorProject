using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Stoklar;
public interface IStokAppService : ICrudAppService<SelectStokDto, ListStokDto, StokListParameterDto, CreateStokDto, UpdateStokDto, CodeParameterDto>
{
}
