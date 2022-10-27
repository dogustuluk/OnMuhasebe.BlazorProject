using OnMuhasebe.BlazorProject.Services;

namespace OnMuhasebe.BlazorProject.BankaHesaplar;
public interface IBankaHesapAppService : ICrudAppService<SelectBankaHesapDto, ListBankaHesapDto, BankaHesapListParameterDto, CreateBankaHesapDto, UpdateBankaHesapDto, BankaHesapCodeParameterDto>
{
}