using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.OzelKodlar;
public interface IOzelKodAppService : ICrudAppService<SelectOzelKodDto, ListOzelKodDto, OzelKodListParameterDto, CreateOzelKodDto, UpdateOzelKodDto, OzelKodCodeParameterDto>
{
}