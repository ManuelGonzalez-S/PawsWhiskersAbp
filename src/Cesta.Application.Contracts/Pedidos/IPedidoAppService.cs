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
        Task<PedidoDto> GetById(int id);

        Task<List<PedidoDto>> GetListByCurrentUser();

        Task<PedidoDto> CreateAsyncGuidProducto(Guid idProducto);

    }
}
