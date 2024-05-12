using Cesta.Samples;
using Xunit;

namespace Cesta.EntityFrameworkCore.Domains;

[Collection(CestaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<CestaEntityFrameworkCoreTestModule>
{

}
