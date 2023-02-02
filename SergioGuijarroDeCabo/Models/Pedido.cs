using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SergioGuijarroDeCabo.Models
{
    public class Pedido
    {

        public string CodigoPedido { get; set; }
        public string CodigoCliente { get; set; }
        public string FechaEntrega { get; set; }
        public string FormaEnvio { get; set; }
        public int Importe { get; set; }


        public Pedido(string codigopedido, string codigocliente, string fechaentrega, string formaenvio, int importe)
        {
            this.CodigoPedido = codigopedido;
            this.CodigoCliente = codigocliente;
            this.FechaEntrega = fechaentrega;
            this.FormaEnvio = formaenvio;
            this.Importe = importe;
        }


    }
}
