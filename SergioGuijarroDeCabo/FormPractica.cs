using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SergioGuijarroDeCabo.Models;
using SergioGuijarroDeCabo.Repositories;

namespace SergioGuijarroDeCabo
{
    public partial class FormPractica : Form
    {

        private Repository repo;
        public List<Cliente> listaClientes;

        public FormPractica()
        {

            InitializeComponent();
            this.repo = new Repository();
            this.listaClientes = new List<Cliente>();
            this.cargarClientes();
        }

        public void cargarClientes()
        {
            this.listaClientes = this.repo.cargarClientes();


            for (int i = 0; i < listaClientes.Count; i++)
            {
                cmbclientes.Items.Add(listaClientes[i].Contacto.ToString());
            }
        }

        public void cargarPedidos(string codigocliente)
        {
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = this.repo.cargarPedidos(codigocliente);

            for (int i = 0; i < pedidos.Count; i++)
            {
                lstpedidos.Items.Add(pedidos[i].FechaEntrega);

            }

        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nombre = cmbclientes.SelectedItem.ToString();
            string codigoCliente = "";
            for(int i = 0; i < this.listaClientes.Count; i++)
            {
                if (nombre.Equals(this.listaClientes[i].Contacto))
                {
                    txtcargo.Text = this.listaClientes[i].Cargo;
                    txtciudad.Text = this.listaClientes[i].Ciudad;
                    txttelefono.Text = this.listaClientes[i].Telefono.ToString();
                    txtcontacto.Text = this.listaClientes[i].Contacto;
                    txtempresa.Text = this.listaClientes[i].Empresa;
                    codigoCliente = this.listaClientes[i].CodigoCliente;

                }

            }

            this.cargarPedidos(codigoCliente);
        }
    }
}
