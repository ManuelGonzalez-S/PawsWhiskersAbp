using AutoMapper;
using Cesta.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cesta.Web.Pages.Productos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {

        #region Binding
        private readonly IProductoAppService _productoAppService;

        public List<ProductoDto> ListaProductos { get; private set; } = new List<ProductoDto>();
        #endregion

        #region Constructor

        public IndexModel(IProductoAppService productoAppService)
        {
            _productoAppService = productoAppService;
        }
        #endregion

        #region Get
        public async void OnGetAsync()
        {
            ListaProductos = await _productoAppService.ListAsync();
        }

        #endregion
    }
}
