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

        public List<Pedido> listaPedidos;
        public string codigoClienteSeleccionado;
        public FormPractica()
        {

            InitializeComponent();
            this.repo = new Repository();

            this.cargarClientes();
        }

        public void cargarClientes()
        {
            this.cmbclientes.Items.Clear();
            this.txtcargo.Text = "";
            this.txtciudad.Text = "";
            this.txttelefono.Text = "";
            this.txtcontacto.Text = "";
            this.txtempresa.Text = "";
            this.codigoClienteSeleccionado = "";
            List<string> clientes= this.repo.cargarClientes();


            for (int i = 0; i < clientes.Count; i++)
            {
                this.cmbclientes.Items.Add(clientes[i].ToString());
            }
        }

        public void cargarPedidos()
        {
            this.lstpedidos.Items.Clear();
            this.txtcodigopedido.Clear();
            this.txtfechaentrega.Clear();
            this.txtformaenvio.Clear();
            this.txtimporte.Clear();
            List<Pedido> pedidos = new List<Pedido>();
            pedidos = this.repo.cargarPedidos(this.codigoClienteSeleccionado);

            for (int i = 0; i < pedidos.Count; i++)
            {
                this.lstpedidos.Items.Add(pedidos[i].FechaEntrega);

            }

        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargarCliente();
            
        }

        public void cargarCliente()
        {
            string nombre = cmbclientes.SelectedItem.ToString();

            Cliente cliente = this.repo.cargarCliente(nombre);


          
                    this.txtcargo.Text = cliente.Cargo;
                    this.txtciudad.Text = cliente.Ciudad;
                    this.txttelefono.Text = cliente.Telefono.ToString();
                    this.txtcontacto.Text = cliente.Contacto;
                    this.txtempresa.Text = cliente.Empresa;
                    this.codigoClienteSeleccionado = cliente.CodigoCliente;

               

            this.cargarPedidos();
        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {


            string fecha = lstpedidos.Text;
            Pedido pedido = this.repo.cargarPedido(fecha);
            this.txtcodigopedido.Text = pedido.CodigoPedido;
            this.txtfechaentrega.Text = pedido.FechaEntrega;
            this.txtformaenvio.Text = pedido.FormaEnvio;
            this.txtimporte.Text = pedido.Importe.ToString(); 
        }

        private void btnmodificarcliente_Click(object sender, EventArgs e)
        {
            Cliente cliente = new Cliente(this.codigoClienteSeleccionado, this.txtempresa.Text, this.txtcontacto.Text, this.txtcargo.Text, this.txtciudad.Text, int.Parse(this.txttelefono.Text));

                int clienteModificado = this.repo.modificarCliente(cliente);
            MessageBox.Show(clienteModificado + " cliente modificado");

            this.cargarClientes();
        }

        private void FormPractica_Load(object sender, EventArgs e)
        {

        }

        private void btnnuevopedido_Click(object sender, EventArgs e)
        {
            Pedido pedido = new Pedido(this.txtcodigopedido.Text, this.codigoClienteSeleccionado, this.txtfechaentrega.Text, this.txtformaenvio.Text,  int.Parse(this.txtimporte.Text));

            int pedidoInsertado = this.repo.insertarPedido(pedido);
            MessageBox.Show(pedidoInsertado + " pedido insertado");
            this.cargarPedidos();
        }

        private void btneliminarpedido_Click(object sender, EventArgs e)
        {
            int pedidoEliminado = this.repo.eliminarPedido(this.txtcodigopedido.Text);
        }
    }
}
