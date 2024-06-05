using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Cesta.Pedidos
{
    public class PedidoDto : AuditedEntityDto<Guid>
    {
        public Guid UsuarioId { get; set; }

        public int Cantidad { get; set; }

        public Guid ProductoId { get; set; }
    }
}
