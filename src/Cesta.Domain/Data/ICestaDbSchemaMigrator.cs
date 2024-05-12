using System.Threading.Tasks;

namespace Cesta.Data;

public interface ICestaDbSchemaMigrator
{
    Task MigrateAsync();
}
