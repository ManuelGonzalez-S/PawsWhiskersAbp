using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Users;
using Cesta.Productos;
using Cesta.Pedidos;

namespace Cesta.Web.Pages.Productos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IProductoAppService _productoAppService;
        private readonly IPedidoAppService _pedidoAppService;
        private readonly ICurrentUser _currentUser;

        public int countCesta { get; set; } = 0;

        public List<ProductoDto> ListaProductos { get; set; } = new List<ProductoDto>();

        [TempData]
        public string AlertMessage { get; set; }

        public IndexModel(IProductoAppService productoAppService, IPedidoAppService pedidoAppService, ICurrentUser currentUser)
        {
            _productoAppService = productoAppService;
            _pedidoAppService = pedidoAppService;
            _currentUser = currentUser;
        }

        public async Task OnGetAsync()
        {
            try
            {
                ListaProductos = await _productoAppService.ListAsync();

                if (_currentUser.IsAuthenticated)
                {
                    var pedidosEnCarrito = await _pedidoAppService.GetListByCurrentUser();

                    foreach (var producto in ListaProductos)
                    {
                        if (pedidosEnCarrito.Any(p => p.ProductoId == producto.Id))
                        {
                            producto.enCarrito = true;
                            countCesta++;
                        }
                    }
                }
                else
                {
                    AlertMessage = "You need to log in to add products to your cart.";
                }
            }
            catch (Exception ex)
            {
                AlertMessage = $"An error occurred: {ex.Message}";
            }
        }
    }
}
