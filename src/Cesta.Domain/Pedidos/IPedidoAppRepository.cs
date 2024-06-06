using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Cesta.Pedidos
{
    public interface IPedidoAppRepository : IRepository<Pedido, Guid>
    {
        Task<Pedido> getByPedidoId(int pedidoId);

        Task<List<Pedido>> GetListByUserId(Guid userId);
    }
}
