using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cesta.Productos;
using Cesta.Web.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cesta.Web.Pages.ProductosCrud
{
    public class CreateModalModel : CestaPageModel
    {
        [BindProperty]
        public CreateUpdateProductoDto Producto { get; set; }

        private readonly IMapper _mapper;

        [BindProperty]
        public IFormFile auxiliar { get; set; }

        public List<SelectListItem> SelectListMascotaType { get; set; }
        public List<SelectListItem> SelectlistProductoType { get; set; }

        private readonly IProductoAppService _productoAppService;

        public CreateModalModel(IProductoAppService productoAppService, IMapper mapper)
        {
            _productoAppService = productoAppService;
            _mapper = mapper;
        }

        public void OnGet()
        {
            Producto = new CreateUpdateProductoDto();

            SelectListMascotaType = Enum.GetValues(typeof(MascotaType))
                                         .Cast<MascotaType>()
                                         .Select(e => new SelectListItem
                                         {
                                             Value = e.ToString(),
                                             Text = e.ToString()
                                         }).ToList();

            SelectlistProductoType = Enum.GetValues(typeof(ProductoType))
                                         .Cast<ProductoType>()
                                         .Select(e => new SelectListItem
                                         {
                                             Value = e.ToString(),
                                             Text = e.ToString()
                                         }).ToList();
        }

        public async Task<IActionResult> OnPostCreateAsync(ProductoDto producto)
        {
            if (ModelState.IsValid || (ModelState.ErrorCount == 1 && ModelState["auxiliar"]?.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
            {
                try
                {
                    //var createUpdateProducto = _mapper.Map<ProductoDto, CreateUpdateProductoDto>(producto);
                    await _productoAppService.CreateAsync(producto);
                    return new JsonResult(new { success = true });
                }
                catch (Exception ex)
                {
                    return new JsonResult(new { success = false, message = ex.Message });
                }
            }
            else
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return new JsonResult(new { success = false, errors = errorMessages });
            }


        }
    }
}