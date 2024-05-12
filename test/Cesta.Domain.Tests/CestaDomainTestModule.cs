using Volo.Abp.Modularity;

namespace Cesta;

[DependsOn(
    typeof(CestaDomainModule),
    typeof(CestaTestBaseModule)
)]
public class CestaDomainTestModule : AbpModule
{

}
