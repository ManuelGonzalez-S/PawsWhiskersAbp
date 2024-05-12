using System.Threading.Tasks;
using Cesta.Productos;
using Cesta.Web.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Cesta.Web.Pages.Productos
{
    public class CreateModalModel : CestaPageModel
    {
        [BindProperty]
        public CreateUpdateProductoDto Producto { get; set; }

        private readonly IProductoAppService _productoAppService;

        public CreateModalModel(IProductoAppService productoAppService)
        {
            _productoAppService = productoAppService;
        }

        public void OnGet()
        {
            Producto = new CreateUpdateProductoDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _productoAppService.CreateAsync(Producto);
            return NoContent();
        }
    }
}