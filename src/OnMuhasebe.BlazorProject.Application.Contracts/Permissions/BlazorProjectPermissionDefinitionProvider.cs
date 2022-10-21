using OnMuhasebe.BlazorProject.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace OnMuhasebe.BlazorProject.Permissions;

public class BlazorProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(BlazorProjectPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(BlazorProjectPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BlazorProjectResource>(name);
    }
}
