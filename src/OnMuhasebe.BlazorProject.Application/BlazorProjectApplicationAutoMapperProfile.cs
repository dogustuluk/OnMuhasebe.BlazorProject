using AutoMapper;
using OnMuhasebe.BlazorProject.Bankalar;

namespace OnMuhasebe.BlazorProject;

public class BlazorProjectApplicationAutoMapperProfile : Profile
{
    public BlazorProjectApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        //buradaki kodlara ekleme yapılacaktır.
        CreateMap<Banka, SelectBankaDto>();
        CreateMap<Banka, ListBankaDto>();
        CreateMap<CreateBankaDto, Banka>();
        CreateMap<UpdateBankaDto, Banka>();
        
    }
}