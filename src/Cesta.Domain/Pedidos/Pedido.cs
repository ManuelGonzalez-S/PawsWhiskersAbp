using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Cesta.Pedidos
{
    public class Pedido : AuditedAggregateRoot<Guid>
    {
        public Guid UsuarioId { get; set; }

        public int Cantidad { get; set; }

        public Guid ProductoId { get; set; }

        internal Pedido(
            Guid id, Guid usuarioId, int cantidad, Guid productoId) : base(id)
        {
            UsuarioId = usuarioId;
            Cantidad = cantidad;
            ProductoId = productoId;
        }

        public Pedido()
        {
        }

        internal Pedido ChangeCantidad(int Cantidad)
        {
            SetCantidad(Cantidad);
            return this;
        }

        private void SetCantidad(int cantidad)
        {
            Cantidad = Check.NotNull(

                cantidad, nameof(cantidad));
        }
    }
}
