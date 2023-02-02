﻿using System;
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


        public List<Cliente> cargarClientes()
        {
            List<Cliente> listaClientes = new List<Cliente>();

            this.com.CommandType = System.Data.CommandType.StoredProcedure;
            this.com.CommandText = "SP_CARGAR_CLIENTES";

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

                Cliente cliente = new Cliente(CodigoCliente, Empresa, Contacto, Cargo, Ciudad, Telefono);
                listaClientes.Add(cliente);
            }
            this.reader.Close();
            this.cn.Close();


            return listaClientes;

        }

    }
}
