using AutoMapper;
using Cesta.Productos;

namespace Cesta.Web;

public class CestaWebAutoMapperProfile : Profile
{
    public CestaWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<ProductoDto, CreateUpdateProductoDto>();
    }
}
