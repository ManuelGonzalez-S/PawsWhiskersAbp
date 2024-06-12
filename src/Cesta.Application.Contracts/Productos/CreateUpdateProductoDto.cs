using System;
using System.ComponentModel.DataAnnotations;

namespace Cesta.Productos
{
    public class CreateUpdateProductoDto
    {

        //PONER VALORES POR DEFECTO AQUÍ

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public ProductoType ProductoType { get; set; }

        [Required]
        public MascotaType MascotaType { get; set; }

        public string Description { get; set; }

        public float? Price { get; set; } = 0.00f;

        public string? ImageBase64 { get; set; }
    }
}
