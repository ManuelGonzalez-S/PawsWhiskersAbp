using Cesta.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Cesta.Permissions;

public class CestaPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Define your own permissions here. Example:
        //myGroup.AddPermission(CestaPermissions.MyPermission1, L("Permission:MyPermission1"));

        var cestaGroup = context.AddGroup(CestaPermissions.GroupName, L("Permission:Cesta"));

        var productosPermission = cestaGroup.AddPermission(CestaPermissions.Productos.Default, L("Permission:Productos"));
        productosPermission.AddChild(CestaPermissions.Productos.Create, L("Permission:Productos.Create"));
        productosPermission.AddChild(CestaPermissions.Productos.Edit, L("Permission:Productos.Edit"));
        productosPermission.AddChild(CestaPermissions.Productos.Delete, L("Permission:Productos.Delete"));

    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CestaResource>(name);
    }
}
