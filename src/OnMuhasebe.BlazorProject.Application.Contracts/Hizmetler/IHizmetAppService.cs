using OnMuhasebe.BlazorProject.CommonDtos;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.Hizmetler;
public interface IHizmetAppService : ICrudAppService<SelectHizmetDto, ListHizmetDto, HizmetListParameterDto, CreateHizmetDto, UpdateHizmetDto, CodeParameterDto>
{ 
}