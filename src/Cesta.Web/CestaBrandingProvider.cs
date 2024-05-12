using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Cesta.Web;

[Dependency(ReplaceServices = true)]
public class CestaBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Cesta";
}
