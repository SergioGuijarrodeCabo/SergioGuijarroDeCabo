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
    }
}
