using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;

namespace Cesta.Pedidos
{
    public class PedidoNotFoundException : BusinessException
    {

        public PedidoNotFoundException(string name) : base(CestaDomainErrorCodes.PedidoNotFound)
        {
            WithData("name", name);
        }

    }
}
