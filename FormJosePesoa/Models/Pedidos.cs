using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormJosePesoa.Models
{
    public class Pedidos
    {
        public string CodigoPedido { get; set; }
        public string CodigoCliente { get; set; }
        public string FechaEntrega { get; set; }

        public string FormaEnvio { get; set; }
        public int Importe { get; set; }

        public Pedidos(string codigoPedido, string codigoCliente, string fechaEntrega, string formaEnvio, int importe)
        {
            CodigoPedido = codigoPedido;
            CodigoCliente = codigoCliente;
            FechaEntrega = fechaEntrega;
            FormaEnvio = formaEnvio;
            Importe = importe;
        }
    }
}
