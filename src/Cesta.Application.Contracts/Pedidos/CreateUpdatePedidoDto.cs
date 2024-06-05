using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cesta.Pedidos
{
    public class CreateUpdatePedidoDto
    {
        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public Guid ProductoId { get; set; }
    }
}
