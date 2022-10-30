using System;
using System.Collections.Generic;
using System.Text;
using OnMuhasebe.BlazorProject.Localization;
using Volo.Abp.Application.Services;

namespace OnMuhasebe.BlazorProject;

/* Inherit your application services from this class.
 * kalıtım alacağımız interface'leri buradan kalıtım almamızı istiyor. Çünkü bize bu class aracılığıyla hazır olarak bir çok function sunuyor.
 * Sunulan hazır fonksiyonları incelemek için ApplicationService'e gidebiliriz.
 */
public abstract class BlazorProjectAppService : ApplicationService
{
    protected BlazorProjectAppService()
    {
        LocalizationResource = typeof(BlazorProjectResource);
    }
}
