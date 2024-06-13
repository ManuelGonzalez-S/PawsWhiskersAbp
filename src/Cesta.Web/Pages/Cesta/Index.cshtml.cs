using Cesta.Pedidos;
using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Cesta.Web.Pages.Cesta
{
    public class IndexModel : PageModel
    {
        #region Binding
        private readonly IProductoAppService _productoAppService;
        private readonly IPedidoAppService _pedidoAppService;

        public List<PedidoDto> ListaPedidos { get; set; } = new List<PedidoDto>();
        public List<ProductoDto> ListaProductos { get; set; } = new List<ProductoDto>();
        #endregion

        #region Constructor
        public IndexModel(IProductoAppService productoAppService, IPedidoAppService pedidoAppService)
        {
            _productoAppService = productoAppService;
            _pedidoAppService = pedidoAppService;
        }
        #endregion

        #region Get
        public async Task OnGetAsync()
        {
            try
            {
                ListaPedidos = await _pedidoAppService.GetListByCurrentUser();
            }
            catch (Exception ex)
            {
                // Handle the error (e.g., show a message to the user)
            }
        }
        #endregion
    }
}
