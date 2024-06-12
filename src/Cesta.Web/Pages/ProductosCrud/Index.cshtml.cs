using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using AutoMapper;
using System.Linq;

namespace Cesta.Web.Pages.ProductosCrud
{
    public class IndexModel : PageModel
    {

        private readonly IProductoAppService _productoAppService;

        private readonly IMapper _mapper;

        public IndexModel(IProductoAppService productoAppService, IMapper mapper)
        {
            _productoAppService = productoAppService;
            _mapper = mapper;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                // Devolver un JSON indicando que el modelo es inválido
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new JsonResult(new { success = false, errors = errorMessages });
            }

            try
            {
                var createUpdateProducto = _mapper.Map<ProductoDto, CreateUpdateProductoDto>(producto);

                // Lógica para guardar el producto
                await _productoAppService.CreateAsync(createUpdateProducto);

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }


    }
}
