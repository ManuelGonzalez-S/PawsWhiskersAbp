using Volo.Abp.Modularity;

namespace Cesta;

/* Inherit from this class for your domain layer tests. */
public abstract class CestaDomainTestBase<TStartupModule> : CestaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
