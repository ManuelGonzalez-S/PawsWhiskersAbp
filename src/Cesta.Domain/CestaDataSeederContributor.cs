using Cesta.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Cesta
{
    public class CestaDataSeederContributor
    : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Producto, Guid> _productoRepository;

        public CestaDataSeederContributor(IRepository<Producto, Guid> productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productoRepository.GetCountAsync() <= 0)
            {
                await _productoRepository.InsertAsync(
                    new Producto
                    {
                        Name = "Whiskas chuche",
                        ProductoType = ProductoType.comida,
                        MascotaType = MascotaType.gato,
                        Description = "Delicioso",
                        Price = 19.84f
                    },
                    autoSave: true
                );

                await _productoRepository.InsertAsync(
                    new Producto
                    {
                        Name = "Pelota",
                        ProductoType = ProductoType.juguete,
                        MascotaType = MascotaType.perro,
                        Description = "Ñan ñam",
                        Price = 10.84f
                    },
                    autoSave: true
                );
            }
        }
    }
}
