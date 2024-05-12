using System;
using System.Collections.Generic;
using System.Text;
using Cesta.Localization;
using Volo.Abp.Application.Services;

namespace Cesta;

/* Inherit your application services from this class.
 */
public abstract class CestaAppService : ApplicationService
{
    protected CestaAppService()
    {
        LocalizationResource = typeof(CestaResource);
    }
}
