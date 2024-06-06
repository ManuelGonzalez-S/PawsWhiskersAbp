using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Cesta.Pedidos
{
    public interface IPedidoAppService : IApplicationService
    {
        Task<PedidoDto> GetById(int id);

        Task<List<PedidoDto>> GetByUserId(int userId);

        Task<PedidoDto> CreateAsync(PedidoDto dto);

        Task<PedidoDto> UpdateAsync(Guid id, PedidoDto dto);

        Task<PedidoDto> DeleteAsync(Guid id);

    }
}
