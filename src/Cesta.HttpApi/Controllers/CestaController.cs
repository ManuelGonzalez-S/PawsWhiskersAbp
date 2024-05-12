using Cesta.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Cesta.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CestaController : AbpControllerBase
{
    protected CestaController()
    {
        LocalizationResource = typeof(CestaResource);
    }
}
