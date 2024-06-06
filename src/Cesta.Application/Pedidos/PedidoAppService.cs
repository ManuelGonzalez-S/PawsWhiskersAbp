using AutoMapper;
using AutoMapper.Internal.Mappers;
using Cesta.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Cesta.Pedidos
{
    public class PedidoAppService : IPedidoAppService

    {

        private readonly IPedidoAppRepository _pedidoAppRepository;

        private readonly PedidoManager _pedidoManager;

        private readonly ICurrentUser _currentUser;

        public IMapper _mapper;

        public PedidoAppService(IRepository<Pedido, Guid> repository, IMapper mapper, ICurrentUser currentUser, PedidoManager pedidoManager)
        {
            //GetPolicyName = CestaPermissions.Productos.Default;
            //GetListPolicyName = CestaPermissions.Productos.Default;
            //CreatePolicyName = CestaPermissions.Productos.Create;
            //UpdatePolicyName = CestaPermissions.Productos.Edit;
            //DeletePolicyName = CestaPermissions.Productos.Delete;

            _mapper = mapper;

            _currentUser = currentUser;

            _pedidoManager = pedidoManager;

        }

        public async Task<PedidoDto> CreateAsync(PedidoDto dto)
        {
            var pedido = await _pedidoManager.CreateAsync(
                dto.UsuarioId,
                dto.Cantidad,
                dto.ProductoId
            );

            await _pedidoAppRepository.InsertAsync(pedido);

            return ObjectMapper.Map<Pedido, PedidoDto>(dto);

        }

        public Task<PedidoDto> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PedidoDto>> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<PedidoDto> UpdateAsync(Guid id, PedidoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
