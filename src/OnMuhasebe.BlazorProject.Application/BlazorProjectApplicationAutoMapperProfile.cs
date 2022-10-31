using AutoMapper;
using OnMuhasebe.BlazorProject.BankaHesaplar;
using OnMuhasebe.BlazorProject.Bankalar;
using OnMuhasebe.BlazorProject.BankaSubeler;

namespace OnMuhasebe.BlazorProject;

public class BlazorProjectApplicationAutoMapperProfile : Profile
{
    public BlazorProjectApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        //buradaki kodlara ekleme yapılacaktır.
        //Banka
        CreateMap<Banka, SelectBankaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        
        CreateMap<Banka, ListBankaDto>()
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));
        
        CreateMap<CreateBankaDto, Banka>();
        CreateMap<UpdateBankaDto, Banka>();

        //BankaSube
        CreateMap<BankaSube, SelectBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<BankaSube, ListBankaSubeDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.Banka.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<CreateBankaSubeDto, BankaSube>();
        CreateMap<UpdateBankaSubeDto, BankaSube>();

        //BankaHesap
        //UI tarafına geçince ek kodlar yazılacaktır.
        CreateMap<BankaHesap, SelectBankaHesapDto>()
            .ForMember(x => x.BankaId, y => y.MapFrom(z => z.BankaSube.Banka.Id))
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
            .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<BankaHesap, ListBankaHesapDto>()
            .ForMember(x => x.BankaAdi, y => y.MapFrom(z => z.BankaSube.Banka.Ad))
            .ForMember(x => x.BankaSubeAdi, y => y.MapFrom(z => z.BankaSube.Ad))
            .ForMember(x => x.OzelKod1Adi, y => y.MapFrom(z => z.OzelKod1.Ad))
            .ForMember(x => x.OzelKod2Adi, y => y.MapFrom(z => z.OzelKod2.Ad));

        CreateMap<CreateBankaHesapDto, BankaHesap>();
        CreateMap<UpdateBankaHesapDto, BankaHesap>();
    }
}