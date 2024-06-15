using Cesta.Pedidos;
using Cesta.Productos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Volo.Abp.Users;
using Microsoft.Extensions.Localization;
using Cesta.Localization;

namespace Cesta.Web.Pages.Cesta
{
    public class IndexModel : PageModel
    {
        #region Binding
        private readonly IProductoAppService _productoAppService;
        private readonly IPedidoAppService _pedidoAppService;
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<CestaResource> _localizer;

        public List<PedidoDto> ListaPedidos { get; set; } = new List<PedidoDto>();
        public List<ProductoDto> ListaProductos { get; set; } = new List<ProductoDto>();

        public decimal totalPrecio { get; set; } = 0;

        public Guid idUsuarioActual { get; set; }

        public string AlertMessage { get; set; }
        #endregion

        #region Constructor
        public IndexModel(IProductoAppService productoAppService, IPedidoAppService pedidoAppService, ICurrentUser currentUser, IStringLocalizer<CestaResource> localizer)
        {
            _productoAppService = productoAppService;
            _pedidoAppService = pedidoAppService;
            _currentUser = currentUser;
            _localizer = localizer;

            idUsuarioActual = _currentUser.Id == null ? Guid.Empty : (Guid)_currentUser.Id;
        }
        #endregion

        #region Get
        public async Task OnGetAsync()
        {

            try
            {
                if (idUsuarioActual == Guid.Empty)
                {
                    AlertMessage = _localizer["NecesitasInicioSesion"];
                }
                else
                {
                    // Lógica para cargar la lista de pedidos y productos
                    ListaPedidos = await _pedidoAppService.GetListByCurrentUser();

                    foreach (var item in ListaPedidos)
                    {
                        var producto = await _productoAppService.GetByIdAsync(item.ProductoId);
                        ListaProductos.Add(producto);

                        var pedido = await _pedidoAppService.GetPedidoDtoByProductoId(item.ProductoId);

                        totalPrecio += (decimal)(producto.Price * pedido.Cantidad); // Utilizando decimal

                        // Redondear totalPrecio a dos decimales
                        totalPrecio = Math.Round(totalPrecio, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                AlertMessage = $"An error occurred: {ex.Message}";
            }
        }
        #endregion

        public async Task<PedidoDto> GetProductoAsync(Guid productoId)
        {
            return await _pedidoAppService.GetPedidoDtoByProductoId(productoId);
        }
    }
}