using Volo.Abp.Modularity;

namespace Cesta;

public abstract class CestaApplicationTestBase<TStartupModule> : CestaTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
