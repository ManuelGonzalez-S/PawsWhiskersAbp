using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace Cesta.Pedidos
{
    public class PedidoManager : DomainService
    {
        private readonly IPedidoAppRepository _pedidoRepository;

        private ICurrentUser _currentUser;

        public PedidoManager(IPedidoAppRepository pedidoRepository, ICurrentUser currentUser)
        {
            _pedidoRepository = pedidoRepository;
            _currentUser = currentUser;
        }

        public async Task<Pedido> CreateAsync(int idProducto)
        {
            Check.NotNull(idProducto, nameof(idProducto));

            var existeProducto = await _pedidoRepository.getByPedidoId(idProducto);

            if (existeProducto == null)
            {
                throw new PedidoAlreadyExistsException("PedidoUserAlreadyExists");
            }

            if(_currentUser == null)
            {
                throw new NotImplementedException();
            }

            byte[] bytes = new byte[16];
            BitConverter.GetBytes(idProducto).CopyTo(bytes, 0);
            var idGuidProducto = new Guid(bytes);

            return new Pedido(
                GuidGenerator.Create(),
                (Guid)_currentUser.Id,
                0,
                idGuidProducto
             );

        }


        public async Task ChangeCantidadAsync(Pedido pedido, int cantidadNueva)
        {
            Check.NotNull(pedido, nameof(pedido));
            Check.NotNull(cantidadNueva, nameof(cantidadNueva));

            //No puede ser 0 o negativo
            Check.Positive(cantidadNueva, nameof(cantidadNueva));

            var pedidoIdAsInt = pedido.Id.GetHashCode();

            var pedidoExistente = await _pedidoRepository.getByPedidoId(pedidoIdAsInt); // Asegúrate de que tu repositorio tiene este método

            // Si el pedido existe y tiene el mismo usuario y producto, se cambia la cantidad
            if (pedidoExistente != null &&
                pedidoExistente.UsuarioId == pedido.UsuarioId &&
                pedidoExistente.ProductoId == pedido.ProductoId)
            {
                pedido.ChangeCantidad(cantidadNueva);
            }
            else
            {
                throw new PedidoNotFoundException(pedido.Id.ToString()); // Excepción adecuada en caso de que el pedido no exista
            }
        }



    }
}
