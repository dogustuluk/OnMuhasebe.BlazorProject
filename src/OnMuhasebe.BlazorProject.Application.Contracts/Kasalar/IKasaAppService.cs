using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Kasalar;
public interface IKasaAppService : ICrudAppService<SelectKasaDto, ListKasaDto, KasaListParameterDto, CreateKasaDto, UpdateKasaDto, KasaCodeParameterDto>
{
}
