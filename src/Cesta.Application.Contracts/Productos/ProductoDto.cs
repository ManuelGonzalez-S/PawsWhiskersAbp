using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Cesta.Productos
{
    public class ProductoDto : AuditedEntityDto<Guid>
    {

        public string Name { get; set; }

        public ProductoType ProductoType { get; set; }

        public MascotaType MascotaType { get; set; }

        public string Description { get; set; }

        public float? Price { get; set; }

        public string? NombreImagen { get; set; }

    }
}
