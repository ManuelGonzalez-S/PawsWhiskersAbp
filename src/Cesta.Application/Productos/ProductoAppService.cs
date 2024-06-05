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

        private readonly IRepository<Producto, Guid> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoAppService(IRepository<Producto, Guid> repository, IMapper mapper)
            : base(repository)
        {
            _productoRepository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductoDto>> ListAsync()
        {
            var productos = await _productoRepository.GetListAsync(); // Llamada asíncrona para obtener productos
            return _mapper.Map<List<Producto>, List<ProductoDto>>(productos); // Mapeo de productos a ProductoDto
        }
    }
}
