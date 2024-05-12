using Cesta.Samples;
using Xunit;

namespace Cesta.EntityFrameworkCore.Applications;

[Collection(CestaTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<CestaEntityFrameworkCoreTestModule>
{

}
