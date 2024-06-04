using System;
using System.IO;
using System.Threading.Tasks;
using Cesta.Productos;
using Cesta.Web.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cesta.Web.Pages.ProductosCrud
{
    public class CreateModalModel : CestaPageModel
    {
        [BindProperty]
        public CreateUpdateProductoDto Producto { get; set; }

        public IFormFile auxiliar { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Console.Write(auxiliar);

            string base64String;

            using (var memoryStream = new MemoryStream())
            {
                auxiliar.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                base64String = Convert.ToBase64String(bytes);
            }

            Producto.ImageBase64 = base64String;

            // Verificar el valor de ImageBase64 en el lado del servidor
            if (!string.IsNullOrEmpty(Producto.ImageBase64))
            {
                Console.WriteLine("ImageBase64 is NOT empty or null");
            }
            else
            {
                Console.WriteLine("ImageBase64 is empty or null");

            }

            await _productoAppService.CreateAsync(Producto);
            return NoContent();
        }
    }
}
