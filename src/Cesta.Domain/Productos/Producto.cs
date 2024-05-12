using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Cesta.Productos
{
    public class Producto : AuditedAggregateRoot<Guid>
    {

        public string Name { get; set; }

        public ProductoType ProductoType { get; set; }

        public MascotaType MascotaType { get; set; }

        public string Description { get; set; }

        public float? Price { get; set; }

        public string? NombreImagen { get; set; }

    }
}
