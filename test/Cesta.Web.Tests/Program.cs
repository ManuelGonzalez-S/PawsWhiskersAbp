using Microsoft.AspNetCore.Builder;
using Cesta;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<CestaWebTestModule>();

public partial class Program
{
}
