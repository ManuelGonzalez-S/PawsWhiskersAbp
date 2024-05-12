using AutoMapper;
using Cesta.Productos;

namespace Cesta;

public class CestaApplicationAutoMapperProfile : Profile
{
    public CestaApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Producto, ProductoDto>();
        CreateMap<CreateUpdateProductoDto, Producto>();

    }
}
