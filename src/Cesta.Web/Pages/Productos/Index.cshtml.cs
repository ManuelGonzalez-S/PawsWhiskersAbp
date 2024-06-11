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
        private readonly ILogger<IndexModel> _logger;

        public List<ProductoDto> ListaProductos { get; set; } = new List<ProductoDto>();
        #endregion

        #region Constructor

        public IndexModel(IProductoAppService productoAppService, IPedidoAppService pedidoAppService, ILogger<IndexModel> logger)
        {
            _productoAppService = productoAppService;
            _pedidoAppService = pedidoAppService;
            _logger = logger;
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
                var startTime = DateTime.UtcNow;
                ListaProductos = await _productoAppService.ListAsync();
                var endTime = DateTime.UtcNow;
                _logger.LogInformation($"Time taken to load products: {endTime - startTime}ms");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading products: {ex.Message}");
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
