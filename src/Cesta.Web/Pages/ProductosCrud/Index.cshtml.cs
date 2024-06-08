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
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ProductoDto producto) {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string base64String;

            //if (auxiliar != null)
            //{
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await auxiliar.CopyToAsync(memoryStream);
            //        byte[] bytes = memoryStream.ToArray();
            //        base64String = Convert.ToBase64String(bytes);
            //    }

            //    Producto.ImageBase64 = base64String;
            //}

            // Lógica para guardar el producto

            //await _productoAppService.CreateAsync(Producto);
            //return RedirectToPage("/ProductosCrud/Index");

            return Page();

        }
    }
}
