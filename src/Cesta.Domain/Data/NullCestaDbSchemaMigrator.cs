using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Cesta.Data;

/* This is used if database provider does't define
 * ICestaDbSchemaMigrator implementation.
 */
public class NullCestaDbSchemaMigrator : ICestaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
