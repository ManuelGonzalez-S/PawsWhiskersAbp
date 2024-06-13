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

        public async Task OnGetAsync()
        {
            var productoDto = await _productoAppService.GetAsync(Id);
            Producto = ObjectMapper.Map<ProductoDto, CreateUpdateProductoDto>(productoDto);

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productoAppService.UpdateAsync(Id, Producto);
            return NoContent();
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
