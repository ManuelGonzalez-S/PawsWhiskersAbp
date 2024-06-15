using System;
using System.Threading.Tasks;
using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Cesta.Migrations;
using System.IO;

namespace Cesta.Web.Pages.ProductosCrud
{
    public class EditModalModel : CestaPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProductoDto Producto { get; set; }

        public IFormFile auxiliar { get; set; }

        private readonly IProductoAppService _productoAppService;

        public List<SelectListItem> SelectListMascotaType { get; set; }
        public List<SelectListItem> SelectlistProductoType { get; set; }

        public EditModalModel(IProductoAppService productoAppService)
        {
            _productoAppService = productoAppService;
        }

        public async Task OnGetAsync(Guid id)
        {
            var productoDto = await _productoAppService.GetAsync(id);
            var productoEntity = ObjectMapper.Map<ProductoDto, Producto>(productoDto);
            Producto = ObjectMapper.Map<Producto, CreateUpdateProductoDto>(productoEntity);

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

            Producto.ImageBase64 = productoDto.ImageBase64;
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            try
            {
                if (ModelState.IsValid || (ModelState.ErrorCount == 1 && ModelState["auxiliar"]?.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid))
                {
                    await _productoAppService.UpdateAsync(Id, Producto);
                    return new JsonResult(new { success = true });

                }
                else
                {
                    var errorMessages = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return new JsonResult(new { success = false, errors = errorMessages });
                }
            }catch(Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }

            
        }

        public IFormFile Base64ToIFormFile(string base64String, string fileName)
        {
            // Eliminar prefijos como "data:image/png;base64," si están presentes
            if (base64String.Contains(","))
            {
                base64String = base64String.Substring(base64String.IndexOf(",") + 1);
            }

            // Convertir Base64 string a byte[] 
            byte[] fileBytes = Convert.FromBase64String(base64String);

            // Crear un MemoryStream a partir del byte[]
            var ms = new MemoryStream(fileBytes);
            IFormFile formFile = new FormFile(ms, 0, fileBytes.Length, null, fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };
            return formFile;
        }

    }
}
