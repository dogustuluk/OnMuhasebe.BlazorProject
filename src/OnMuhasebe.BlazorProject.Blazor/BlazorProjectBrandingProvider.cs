using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace OnMuhasebe.BlazorProject.Blazor;

[Dependency(ReplaceServices = true)]
public class BlazorProjectBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BlazorProject";
}
