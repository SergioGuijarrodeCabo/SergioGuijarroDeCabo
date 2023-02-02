using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SergioGuijarroDeCabo.Models
{
    public class Cliente
    {
        public string CodigoCliente { get; set; }
        public string Empresa { get; set; }
        public string Contacto { get; set; }
        public string Cargo { get; set; }
        public string Ciudad { get; set; }
        public int Telefono { get; set; }

        public Cliente(string codigocliente, string empresa, string contacto, string cargo, string ciudad, int telefono)
        {
            this.CodigoCliente = codigocliente;
            this.Empresa = empresa;
            this.Contacto = contacto;
            this.Cargo = cargo;
            this.Ciudad = ciudad;
            this.Telefono = telefono;

        }


    }
}
