using OnMuhasebe.BlazorProject.Localization;
using Volo.Abp.AspNetCore.Components;

namespace OnMuhasebe.BlazorProject.Blazor;

public abstract class BlazorProjectComponentBase : AbpComponentBase
{
    protected BlazorProjectComponentBase()
    {
        LocalizationResource = typeof(BlazorProjectResource);
    }
}
