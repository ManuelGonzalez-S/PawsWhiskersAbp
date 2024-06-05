using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace Cesta.Pedidos
{
    public class Pedido : AuditedAggregateRoot<Guid>
    {
        public Guid UsuarioId { get; set; }

        public int Cantidad { get; set; }

        public Guid ProductoId { get; set; }
    }
}
