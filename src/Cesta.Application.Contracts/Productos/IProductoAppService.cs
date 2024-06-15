using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Threading;

namespace Cesta.Productos
{
    public interface IProductoAppService :

        ICrudAppService< //Defines CRUD methods
        ProductoDto, //Used to show products
        Guid, //Primary key of the product entity
        PagedAndSortedResultRequestDto, //Used for paging/sorting
        CreateUpdateProductoDto>
    {

        Task<ProductoDto> CreateAsync(ProductoDto producto);

        Task<List<ProductoDto>> ListAsync();

        Task<ProductoDto> GetByIdAsync(Guid id);
    }
}
