using Cesta.Productos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cesta.Pedidos
{
    public interface IPedidoAppService : ICrudAppService< //Defines CRUD methods
        PedidoDto, //Used to show products
        Guid, //Primary key of the product entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdatePedidoDto>
    {
        Task<PedidoDto> GetById(Guid id);

        Task<List<PedidoDto>> GetListByCurrentUser();

        Task<PedidoDto> CreateAsyncGuidProducto(Guid idProducto);

        Task<ProductoDto> DeleteByProductoUserId(Guid idProducto);

        Task<PedidoDto> ModificarAsync(PedidoDto input);

        Task<PedidoDto> GetPedidoDtoByProductoId(Guid idProducto);

        Task<bool> borrarPedidosCurrentUser();
    }
}
