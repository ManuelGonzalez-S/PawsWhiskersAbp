using AutoMapper;
using Cesta.Pedidos;
using Cesta.Productos;

namespace Cesta;

public class CestaApplicationAutoMapperProfile : Profile
{
    public CestaApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Producto, ProductoDto>().ReverseMap();
        CreateMap<CreateUpdateProductoDto, Producto>().ReverseMap();

        CreateMap<Pedido, PedidoDto>().ReverseMap();
        CreateMap<CreateUpdatePedidoDto, Pedido>().ReverseMap();

    }
}
