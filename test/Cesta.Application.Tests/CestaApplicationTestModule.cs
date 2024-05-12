using Volo.Abp.Modularity;

namespace Cesta;

[DependsOn(
    typeof(CestaApplicationModule),
    typeof(CestaDomainTestModule)
)]
public class CestaApplicationTestModule : AbpModule
{

}
