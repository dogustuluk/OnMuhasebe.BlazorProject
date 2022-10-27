using System;
using Volo.Abp.Application.Services;

namespace OnMuhasebe.BlazorProject.Services;
/*descriptions
 * TGetOutpuDto -> Geriye bir entity döndürülmesi durumunda onu karşılayacak olan parametre.
 * TGetListInput -> Veri tabanından verileri çekerken hangi parametrelere göre verilerin geleceğini belirtmemizi sağlar. Oluşturduğumuz dto'lardan  BankaHesapListParameterDto'ya ve BankaListParameterDto'ya karşılık gelmektedir. Input olanlara da "in" olarak ayarlamasını yapıyoruz.
 */
public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TGetListInput, in TCreateInput, in TUpdateInput> :
    IReadOnlyAppService<TGetOutputDto,TGetListOutputDto,Guid,TGetListInput>,
    ICreateAppService<TGetOutputDto,TCreateInput>,
    IUpdateAppService<TGetOutputDto,Guid,TUpdateInput>
{
}

public interface ICrudAppService<TGetOutputDto, TGetListOutputDto, in TGetListInput, in TCreateInput, in TUpdateInput, in TGetCodeInput> :
    ICrudAppService<TGetOutputDto, TGetListOutputDto, TGetListInput, TCreateInput, TUpdateInput>,
    IDeleteAppService<Guid>,
    ICodeAppService<TGetCodeInput>
{
}