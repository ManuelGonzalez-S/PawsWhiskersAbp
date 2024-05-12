using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Cesta.Pages;

public class Index_Tests : CestaWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
