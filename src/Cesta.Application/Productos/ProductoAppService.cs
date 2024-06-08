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

        private readonly ICurrentUser _currentUser;

        private readonly IRepository<Producto, Guid> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoAppService(IRepository<Producto, Guid> repository, IMapper mapper, ICurrentUser currentUser)
            : base(repository)
        {
            _productoRepository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ProductoDto> GetByIdAsync(Guid id)
        {
            var productosList = await _productoRepository.GetListAsync();
            var producto = productosList.FirstOrDefault(x => x.Id == id);
            return _mapper.Map<Producto, ProductoDto>(producto);
        }

        public async Task<List<ProductoDto>> ListAsync()
        {
            var productos = await _productoRepository.GetListAsync(); // Llamada asíncrona para obtener productos
            return _mapper.Map<List<Producto>, List<ProductoDto>>(productos); // Mapeo de productos a ProductoDto
        }


    }
}
