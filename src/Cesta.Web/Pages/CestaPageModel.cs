using Cesta.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Cesta.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class CestaPageModel : AbpPageModel
{
    protected CestaPageModel()
    {
        LocalizationResourceType = typeof(CestaResource);
    }
}
