using AutoMapper;
using Cesta.Pedidos;
using Cesta.Productos;

namespace Cesta.Web;

public class CestaWebAutoMapperProfile : Profile
{
    public CestaWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<Producto, ProductoDto>().ReverseMap();
        CreateMap<CreateUpdateProductoDto, Producto>();

        CreateMap<Pedido, PedidoDto>().ReverseMap();
        CreateMap<CreateUpdatePedidoDto, Pedido>();
    }
}
