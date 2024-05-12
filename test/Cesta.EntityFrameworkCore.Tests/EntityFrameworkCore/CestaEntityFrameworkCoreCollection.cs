using Xunit;

namespace Cesta.EntityFrameworkCore;

[CollectionDefinition(CestaTestConsts.CollectionDefinitionName)]
public class CestaEntityFrameworkCoreCollection : ICollectionFixture<CestaEntityFrameworkCoreFixture>
{

}
