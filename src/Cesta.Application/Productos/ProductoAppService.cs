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

namespace Cesta.Productos
{
    public class ProductoAppService :


        CrudAppService<
            Producto, //The Producto entity
            ProductoDto, //Used to show productos
            Guid, //Primary key of the producto entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateProductoDto>, //Used to create/update a producto
        IProductoAppService

    {

        private readonly IProductoAppService _productoAppService;

        public IMapper _mapper;

        public ProductoAppService(IRepository<Producto, Guid> repository, IMapper mapper) : base(repository)
        {
            GetPolicyName = CestaPermissions.Productos.Default;
            GetListPolicyName = CestaPermissions.Productos.Default;
            CreatePolicyName = CestaPermissions.Productos.Create;
            UpdatePolicyName = CestaPermissions.Productos.Edit;
            DeletePolicyName = CestaPermissions.Productos.Delete;

            _mapper = mapper;

        }
    }
}
