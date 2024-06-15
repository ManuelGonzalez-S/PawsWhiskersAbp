using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using AutoMapper;

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

        public async Task<IActionResult> OnPostCreateAsync(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new JsonResult(new { success = false, errors = errorMessages });
            }

            try
            {
                var createUpdateProducto = _mapper.Map<ProductoDto, CreateUpdateProductoDto>(producto);
                await _productoAppService.CreateAsync(createUpdateProducto);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
        public async Task<IActionResult> OnPostAsync(ProductoDto producto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new JsonResult(new { success = false, errors = errorMessages });
            }

            try
            {
                var updateProducto = _mapper.Map<ProductoDto, CreateUpdateProductoDto>(producto);
                await _productoAppService.UpdateAsync(producto.Id, updateProducto);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}