using Cesta.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Cesta.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CestaEntityFrameworkCoreModule),
    typeof(CestaApplicationContractsModule)
    )]
public class CestaDbMigratorModule : AbpModule
{
}
