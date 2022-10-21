using OnMuhasebe.BlazorProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace OnMuhasebe.BlazorProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BlazorProjectController : AbpControllerBase
{
    protected BlazorProjectController()
    {
        LocalizationResource = typeof(BlazorProjectResource);
    }
}
