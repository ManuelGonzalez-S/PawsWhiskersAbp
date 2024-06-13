using AutoMapper;
using Cesta.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Cesta.Pedidos;

namespace Cesta.Web.Pages.Productos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        #region Binding
        private readonly IProductoAppService _productoAppService;
        private readonly IPedidoAppService _pedidoAppService;

        public List<ProductoDto> ListaProductos { get; set; } = new List<ProductoDto>();
        #endregion

        #region Constructor

        public IndexModel(IProductoAppService productoAppService, IPedidoAppService pedidoAppService)
        {
            _productoAppService = productoAppService;
            _pedidoAppService = pedidoAppService;
        }

        //public IndexModel(IProductoAppService productoAppService, ILogger<IndexModel> logger)
        //{
        //    _productoAppService = productoAppService;
        //    _logger = logger;
        //}
        #endregion

        #region Get
        public async Task OnGetAsync()
        {
            try
            {
                ListaProductos = await _productoAppService.ListAsync();
            }
            catch (Exception ex)
            {
                // Handle the error (e.g., show a message to the user)
            }
        }

        public async Task<IActionResult> OnGetAñadirProductoACestaAsync()
        {
            return new NoContentResult();
        }
        #endregion

        #region Post

        public async Task<IActionResult> OnPostAñadirProductoACestaAsync()
        {
            return new NoContentResult();
        }
        #endregion
    }
}
