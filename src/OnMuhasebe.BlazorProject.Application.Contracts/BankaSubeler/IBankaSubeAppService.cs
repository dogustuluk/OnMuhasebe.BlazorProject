using OnMuhasebe.BlazorProject.BankaHesaplar;
using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.BankaSubeler;
public interface IBankaSubeAppService : ICrudAppService<SelectBankaSubeDto, ListBankaSubeDto, BankaSubeListParameterDto, CreateBankaSubeDto, UpdateBankaSubeDto, BankaSubeCodeParameterDto>
{
}