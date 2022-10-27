using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Makbuzlar;
public interface IMakbuzAppService : ICrudAppService<SelectMakbuzDto, ListMakbuzDto, MakbuzListParameterDto, CreateMakbuzDto, UpdateMakbuzDto, MakbuzNoParameterDto>
{
}
