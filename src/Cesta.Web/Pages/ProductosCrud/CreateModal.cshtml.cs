using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        [BindProperty]
        public IFormFile auxiliar { get; set; }

        public List<SelectListItem> SelectListMascotaType { get; set; }
        public List<SelectListItem> SelectlistProductoType { get; set; }

        private readonly IProductoAppService _productoAppService;

        public CreateModalModel(IProductoAppService productoAppService)
        {
            _productoAppService = productoAppService;
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

        //public async Task<IActionResult> OnPostAsync(ProductoDto productoDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    string base64String;

        //    if (auxiliar != null)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await auxiliar.CopyToAsync(memoryStream);
        //            byte[] bytes = memoryStream.ToArray();
        //            base64String = Convert.ToBase64String(bytes);
        //        }

        //        Producto.ImageBase64 = base64String;
        //    }

        //    // Lógica para guardar el producto

        //    await _productoAppService.CreateAsync(Producto);
        //    return RedirectToPage("/ProductosCrud/Index");
        //}
    }
}
