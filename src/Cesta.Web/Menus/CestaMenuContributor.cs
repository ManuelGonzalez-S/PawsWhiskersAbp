using System.Threading.Tasks;
using Cesta.Localization;
using Cesta.MultiTenancy;
using Cesta.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Cesta.Web.Menus;

public class CestaMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<CestaResource>();

        administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                CestaMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);
        administration.AddItem(new ApplicationMenuItem(
                "Productos",
                l["Menu:CRUDProductos"],
                url: "/Productos",
                icon: "fas fa-shopping-basket",
                order: 0
            ).RequirePermissions(CestaPermissions.Productos.Default));


        return Task.CompletedTask;
    }
}
