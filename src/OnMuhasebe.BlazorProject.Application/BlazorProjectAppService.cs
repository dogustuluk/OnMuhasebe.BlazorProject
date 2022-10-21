using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.Localization;
using Volo.Abp.Application.Services;

namespace OnMuhasebe.BlazorProject;

/* Inherit your application services from this class.
 */
public abstract class BlazorProjectAppService : ApplicationService
{
    protected BlazorProjectAppService()
    {
        LocalizationResource = typeof(BlazorProjectResource);
    }
}
