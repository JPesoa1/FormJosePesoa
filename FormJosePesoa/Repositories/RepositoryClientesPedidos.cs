using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormJosePesoa.Helpers;
using FormJosePesoa.Models;

#region

//RECOGEMOS TODOS LOS CLIENTES 

//ALTER PROCEDURE SP_CLIENTES
//AS
//	SELECT * FROM CLIENTES
//GO





//RECOGEMOS TODOS LOS PEDIDOS POR CLIENTE SELECCIONADO

//CREATE PROCEDURE SP_PEDIDOS_CLIENTE
//(@CODIGOCLIENTE NVARCHAR(20))
//AS
//	SELECT * FROM PEDIDOS WHERE CodigoCliente = @CODIGOCLIENTE
//GO

#endregion



namespace FormJosePesoa.Repositories
{
    public  class RepositoryClientesPedidos
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryClientesPedidos()
        {
            string connectionString = HelperConfiguration.GetConnectionString();
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;

        }



        public List<Cliente> GetClientes() {

            List<Cliente>clientes = new List<Cliente>();
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_CLIENTES";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            while (this.reader.Read())
            {
                string codigoCliente = this.reader["CodigoCliente"].ToString();
                string empresa = this.reader["Empresa"].ToString();
                string contacto = this.reader["Contacto"].ToString();
                string cargo = this.reader["Cargo"].ToString();
                string ciudad = this.reader["Ciudad"].ToString();
                int telefono = int.Parse(this.reader["Telefono"].ToString());
                clientes.Add(new Cliente(codigoCliente,empresa,contacto,cargo,ciudad,telefono));
            }
            this.reader.Close();
            this.cn.Close();

            return clientes;

        }


        public List<Pedidos> GetPedidosCliente(string codigo) {


            SqlParameter pamcodigo = new SqlParameter("@CODIGOCLIENTE", codigo );
            this.com.Parameters.Add(pamcodigo);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_PEDIDOS_CLIENTE";
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Pedidos> pedidos = new List<Pedidos>();
            while (this.reader.Read())
            {
                string codigoPedido = this.reader["CodigoPedido"].ToString();
                string codigoCliente = this.reader["CodigoCliente"].ToString();
                string fechaentrega = this.reader["FechaEntrega"].ToString();
                string formaEnvio = this.reader["FormaEnvio"].ToString();
                int importe = int.Parse(this.reader["Importe"].ToString());
                pedidos.Add(new Pedidos(codigoPedido, codigoCliente, fechaentrega, formaEnvio, importe));
            }
            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();
            return pedidos;

        }



        public void EliminarPedido(string codigo) {



            string sql = "DELETE FROM PEDIDOS WHERE CodigoPedido = @CODIGOPEDIDO";
            SqlParameter pamCodigo = new SqlParameter("@CODIGOPEDIDO", codigo);
           
            this.com.Parameters.Add(pamCodigo);
          
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
          


        }


        public void ModificarCliente(Cliente cliente) {


            string sql = "UPDATE CLIENTES SET Empresa=@EMPRESA , Contacto=@CONTACTO,Cargo=@CARGO, Ciudad=@CIUDAD, Telefono=@TELEFONO WHERE CodigoCliente=@CODIGO";
            SqlParameter pamEmpresa = new SqlParameter("@EMPRESA", cliente.Empresa);
            SqlParameter pamContacto = new SqlParameter("@CONTACTO", cliente.Contacto);
            SqlParameter pamCargo = new SqlParameter("@CARGO", cliente.Cargo);
            SqlParameter pamCiudad = new SqlParameter("@CIUDAD", cliente.Ciudad);
            SqlParameter pamTelefono = new SqlParameter("@TELEFONO", cliente.Telefono);
            SqlParameter pamCodigo = new SqlParameter("@CODIGO", cliente.CodigoCliente);


            this.com.Parameters.Add(pamEmpresa);
            this.com.Parameters.Add(pamContacto);
            this.com.Parameters.Add(pamCargo);
            this.com.Parameters.Add(pamCiudad);
            this.com.Parameters.Add(pamTelefono);
            this.com.Parameters.Add(pamCodigo);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

        }
    }
}
