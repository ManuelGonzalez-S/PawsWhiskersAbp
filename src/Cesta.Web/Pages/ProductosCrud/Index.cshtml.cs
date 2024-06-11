using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cesta.Web.Pages.ProductosCrud
{
    public class IndexModel : PageModel
    {

        private readonly IProductoAppService _productoAppService;

        public IndexModel(IProductoAppService productoAppService)
        {
            _productoAppService = productoAppService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ProductoDto producto) {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Lógica para guardar el producto
            //await _productoAppService.CreateAsync(producto);

            return RedirectToPage("/ProductosCrud/Index");

        }
    }
}
