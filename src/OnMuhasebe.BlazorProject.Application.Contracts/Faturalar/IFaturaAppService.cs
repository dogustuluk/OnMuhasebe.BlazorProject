using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Faturalar;
public interface IFaturaAppService : ICrudAppService<SelectFaturaDto, ListFaturaDto, FaturaListParameterDto, CreateFaturaDto, UpdateFaturaDto, FaturaNoParameterDto>
{
}