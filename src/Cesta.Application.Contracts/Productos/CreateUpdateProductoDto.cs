using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cesta.Productos
{
    public class CreateUpdateProductoDto
    {

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ProductoType ProductoType { get; set; } = ProductoType.comida;

        [Required]
        public MascotaType MascotaType { get; set; } = MascotaType.perro;

        public string Description { get; set; }

        public float? Price { get; set; } = 0.00f;

        public string? NombreImagen { get; set; }

    }
}
