using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace OnMuhasebe.BlazorProject.Services;
public interface ICodeAppService<in TGetCodeInput>:IApplicationService
{
    Task<string> GetCodeAsync(TGetCodeInput input);
}