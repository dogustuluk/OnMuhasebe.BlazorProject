using Volo.Abp.Application.Dtos;

namespace OnMuhasebe.BlazorProject.CommonDtos;
public class CodeParameterDto: IDurum,IEntityDto
{//bazı entity'lerimizde şube bilgisi(ekstra property'e) gerekmediği için BankaHesapCodeParameterDto ihtiyacımızı karşılamayacağı için bunu kullandık
    public bool Durum { get; set; }
}
