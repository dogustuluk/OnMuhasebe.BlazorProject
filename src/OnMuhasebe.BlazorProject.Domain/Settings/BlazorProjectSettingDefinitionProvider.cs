using Volo.Abp.Settings;

namespace OnMuhasebe.BlazorProject.Settings;

public class BlazorProjectSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BlazorProjectSettings.MySetting1));
    }
}
