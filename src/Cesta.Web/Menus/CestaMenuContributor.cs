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

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        context.Menu.AddItem(
            new ApplicationMenuItem(
                "Cesta",
                l["Menu:Cesta"],
                icon: "fa fa-book"
            ).AddItem(
                new ApplicationMenuItem(
                    "Cesta.Productos",
                    l["Menu:Productos"],
                    icon: "fas fa-shopping-basket",
                    url: "/Productos"
                ).RequirePermissions(CestaPermissions.Productos.Default)
            )
        );


        return Task.CompletedTask;
    }
}
