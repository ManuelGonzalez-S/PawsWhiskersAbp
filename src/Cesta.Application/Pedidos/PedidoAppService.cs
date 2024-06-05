using AutoMapper;
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
    public class PedidoAppService :


        CrudAppService<
            Pedido, 
            PedidoDto, 
            Guid, 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdatePedidoDto>, //Used to create/update a producto
        IPedidoAppService

    {

        private readonly IPedidoAppService _pedidoAppService;

        private readonly ICurrentUser _currentUser;

        public IMapper _mapper;

        public PedidoAppService(IRepository<Pedido, Guid> repository, IMapper mapper, ICurrentUser currentUser) : base(repository)
        {
            //GetPolicyName = CestaPermissions.Productos.Default;
            //GetListPolicyName = CestaPermissions.Productos.Default;
            //CreatePolicyName = CestaPermissions.Productos.Create;
            //UpdatePolicyName = CestaPermissions.Productos.Edit;
            //DeletePolicyName = CestaPermissions.Productos.Delete;

            _mapper = mapper;

            _currentUser = currentUser;

        }
    }
}
