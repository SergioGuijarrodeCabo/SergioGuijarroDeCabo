using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SergioGuijarroDeCabo.Models;

#region

//CREATE OR ALTER PROCEDURE SP_CARGAR_CLIENTES

//AS

//SELECT * FROM clientes

//GO

//exec SP_CARGAR_CLIENTES


//CREATE OR ALTER PROCEDURE SP_CARGAR_PEDIDOS
//(@CODIGOCLIENTE NVARCHAR(50))

//AS

//SELECT * FROM pedidos WHERE CodigoCliente = @CODIGOCLIENTE

//GO


//CREATE OR ALTER PROCEDURE SP_BUSCAR_CLIENTE
//(@CONTACTO NVARCHAR(50))
//AS
//SELECT * FROM clientes WHERE Contacto =@CONTACTO
//GO




//CREATE OR ALTER PROCEDURE SP_BUSCAR_PEDIDO
//(@FECHA NVARCHAR(50))
//AS
//SELECT * FROM pedidos WHERE FechaEntrega =@FECHA
//GO


//CREATE OR ALTER PROCEDURE SP_MODIFICAR_CLIENTE
//(@CODIGOCLIENTE NVARCHAR(50),
//@EMPRESA NVARCHAR(50),
//@CONTACTO NVARCHAR(50),
//@CARGO NVARCHAR(50),
//@CIUDAD NVARCHAR(50),
//@TELEFONO INT)

//as
//UPDATE clientes SET Empresa=@EMPRESA, Contacto = @CONTACTO, Cargo = @CARGO, Ciudad = @CIUDAD, Telefono = @TELEFONO WHERE CodigoCliente=@CODIGOCLIENTE

//go




//CREATE OR ALTER PROCEDURE SP_NUEVO_PEDIDO
//(@CODIGOPEDIDO NVARCHAR(50),
//@CODIGOCLIENTE NVARCHAR(50),
//@FECHAENTREGA NVARCHAR(50),
//@FORMAENVIO NVARCHAR(50),
//@IMPORTE INT)

//AS

//INSERT INTO pedidos (CodigoPedido, CodigoCliente, FechaEntrega, FormaEnvio, Importe) VALUES(@CODIGOPEDIDO, @CODIGOCLIENTE, @FECHAENTREGA, @FORMAENVIO, @IMPORTE)

//GO




//CREATE OR ALTER PROCEDURE SP_ELIMINAR_PEDIDO
//(@CODIGOPEDIDO NVARCHAR(50))
//AS
//DELETE FROM pedidos WHERE CodigoPedido= @CODIGOPEDIDO

//GO

#endregion


namespace SergioGuijarroDeCabo.Repositories
{
    public class Repository
    {

        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public Repository()
        {

            
            string connectionString = @"Data Source=LOCALHOST\DESARROLLO; Initial Catalog = PRACTICAADO; Integrated Security = True;User ID=SA;Password=MCSD2022";
            // this.cn = new SqlConnection(HelperConfiguartion.GetConnectionString());
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }


        public List<string> cargarClientes()
        {
            List<string> listaClientes = new List<string>();

            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_CARGAR_CLIENTES";

            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())

            {
               
                string Contacto = this.reader["Contacto"].ToString();
                

                
                listaClientes.Add(Contacto);
            }
            this.reader.Close();
            this.cn.Close();


            return listaClientes;

        }

        public Cliente cargarCliente(string contacto)
        {
            SqlParameter pamfecha = new SqlParameter("@CONTACTO", contacto);

            this.com.Parameters.Add(pamfecha);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_BUSCAR_CLIENTE";
            Cliente cliente = null;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())

            {
                string CodigoCliente = this.reader["CodigoCliente"].ToString();
                string Empresa = this.reader["Empresa"].ToString();
                string Contacto = this.reader["Contacto"].ToString();
                string Cargo = this.reader["Cargo"].ToString();
                string Ciudad = this.reader["Ciudad"].ToString();
                int Telefono = int.Parse(this.reader["Telefono"].ToString());

                 cliente = new Cliente(CodigoCliente, Empresa, Contacto, Cargo, Ciudad, Telefono);


            }
            this.com.Parameters.Clear();
            this.reader.Close();
            this.cn.Close();
            return cliente;

        }

        public Pedido cargarPedido(string fecha)
        {

            SqlParameter pamfecha = new SqlParameter("@FECHA", fecha);
            
            this.com.Parameters.Add(pamfecha);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_BUSCAR_PEDIDO";
            Pedido pedido = null;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())

            {
                string CodigoPedido = this.reader["CodigoPedido"].ToString();
                string CodigoCliente = this.reader["CodigoCliente"].ToString();
                string FechaEntrega = this.reader["FechaEntrega"].ToString();
                string FormaEnvio = this.reader["FormaEnvio"].ToString();
                int Importe = int.Parse(this.reader["Importe"].ToString());

                 pedido = new Pedido(CodigoPedido, CodigoCliente, FechaEntrega, FormaEnvio, Importe);
             

            }
            this.com.Parameters.Clear();
            this.reader.Close();
            this.cn.Close();
            return pedido;

        }


        public List<Pedido> cargarPedidos(string codigocliente)
        {
            List<Pedido> listaPedidos = new List<Pedido>();

            SqlParameter pamcodigocliente = new SqlParameter("@CODIGOCLIENTE", codigocliente);
         
            this.com.Parameters.Add(pamcodigocliente);
            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_CARGAR_PEDIDOS";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())

            {
                string CodigoPedido = this.reader["CodigoPedido"].ToString();
                string CodigoCliente = this.reader["CodigoCliente"].ToString();
                string FechaEntrega = this.reader["FechaEntrega"].ToString();
                string FormaEnvio = this.reader["FormaEnvio"].ToString();            
                int Importe = int.Parse(this.reader["Importe"].ToString());

                Pedido pedido = new Pedido(CodigoPedido, CodigoCliente, FechaEntrega, FormaEnvio, Importe);
                listaPedidos.Add(pedido);
            }
            this.com.Parameters.Clear();
            this.reader.Close();
            this.cn.Close();

            return listaPedidos;
        }


        public int modificarCliente(Cliente cliente)
        {
            int clienteModificado = 0;

            SqlParameter pamcodigocliente = new SqlParameter("@CODIGOCLIENTE", cliente.CodigoCliente);
            this.com.Parameters.Add(pamcodigocliente);
            SqlParameter pamempresa = new SqlParameter("@EMPRESA", cliente.Empresa);
            this.com.Parameters.Add(pamempresa);
            SqlParameter pamcontacto = new SqlParameter("@CONTACTO", cliente.Contacto);
            this.com.Parameters.Add(pamcontacto);
            SqlParameter pamcargo = new SqlParameter("@CARGO", cliente.Cargo);
            this.com.Parameters.Add(pamcargo);
            SqlParameter pamciudad = new SqlParameter("@CIUDAD", cliente.Ciudad);
            this.com.Parameters.Add(pamciudad);
            SqlParameter pamtelefono = new SqlParameter("@TELEFONO", cliente.Telefono);
            this.com.Parameters.Add(pamtelefono);


            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_MODIFICAR_CLIENTE";

            this.cn.Open();
            clienteModificado = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();




            

            return clienteModificado;
        }
      

        public int insertarPedido(Pedido pedido)
        {
            int pedidoInsertado = 0;


            SqlParameter pamcodigopedido = new SqlParameter("@CODIGOPEDIDO", pedido.CodigoPedido);
            this.com.Parameters.Add(pamcodigopedido);
            SqlParameter pamcodigocliente = new SqlParameter("@CODIGOCLIENTE", pedido.CodigoCliente);
            this.com.Parameters.Add(pamcodigocliente);
            SqlParameter pamfechaentrega = new SqlParameter("@FECHAENTREGA", pedido.FechaEntrega);
            this.com.Parameters.Add(pamfechaentrega);
            SqlParameter pamformaenvio = new SqlParameter("@FORMAENVIO", pedido.FormaEnvio);
            this.com.Parameters.Add(pamformaenvio);
            SqlParameter pamimporte = new SqlParameter("@IMPORTE", pedido.Importe);
            this.com.Parameters.Add(pamimporte);
          

            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_NUEVO_PEDIDO";

            this.cn.Open();
            pedidoInsertado = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();


            return pedidoInsertado;
        }

        public int eliminarPedido(string codigoPedido)
        {
            int pedidoEliminado = 0;

            SqlParameter pamcodigopedido = new SqlParameter("@CODIGOPEDIDO", codigoPedido);
            this.com.Parameters.Add(pamcodigopedido);


            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_ELIMINAR_PEDIDO";

            this.cn.Open();
            pedidoEliminado = this.com.ExecuteNonQuery();
            this.com.Parameters.Clear();
            this.cn.Close();

            return pedidoEliminado;
        }
    }
}
