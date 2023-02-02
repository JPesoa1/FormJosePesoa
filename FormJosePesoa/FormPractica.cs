using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormJosePesoa.Repositories;
using FormJosePesoa.Models;

namespace FormJosePesoa
{
    public partial class FormPractica : Form
    {
        RepositoryClientesPedidos repository;

        List<Cliente> clientes;
        List<Pedidos> pedidos;
        int indexCliente;
        int indexPedido;
        public FormPractica()
        {
            this.repository = new RepositoryClientesPedidos();
            this.clientes = new List<Cliente>();
            this.pedidos= new List<Pedidos>();
            
            InitializeComponent();

            this.LoadCliente();

        }
        private void LoadCliente() {
            

            this.clientes=this.repository.GetClientes();

            for (int i = 0; i<this.clientes.Count;i++) {
                this.cmbclientes.Items.Add(this.clientes[i].Empresa);
            }
            

        
        }

        private void cmbclientes_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.txtcodigopedido.Text = "";
            this.txtfechaentrega.Text = "";
            this.txtformaenvio.Text = "";
            this.txtimporte.Text = "";


            this.lstpedidos.Items.Clear();
            //CARGANDO LOS DATOS DEL CLIENTE
            this.indexCliente = this.cmbclientes.SelectedIndex;
            this.txtcargo.Text = this.clientes[this.indexCliente].Cargo;
            this.txtciudad.Text = this.clientes[this.indexCliente].Ciudad;
            this.txtcontacto.Text = this.clientes[this.indexCliente].Contacto;
            this.txttelefono.Text = this.clientes[this.indexCliente].Telefono.ToString();
            this.txtempresa.Text = this.clientes[this.indexCliente].Empresa;


            //CARGANDO LOS PEDIDOS DEL CLIENTE SELECCIONADO

            this.LoadPedidos();
           
        }

        private void LoadPedidos() {
            this.lstpedidos.Items.Clear();

            string codigo = this.clientes[this.indexCliente].CodigoCliente;
            this.pedidos = this.repository.GetPedidosCliente(codigo);

            for (int i = 0; i < this.pedidos.Count; i++)
            {
                this.lstpedidos.Items.Add(this.pedidos[i].CodigoPedido);

            }

        }

        private void lstpedidos_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.indexPedido = this.lstpedidos.SelectedIndex;

            this.txtcodigopedido.Text = this.pedidos[this.indexPedido].CodigoPedido;
            this.txtfechaentrega.Text = this.pedidos[this.indexPedido].FechaEntrega;
            this.txtformaenvio.Text = this.pedidos[this.indexPedido].FormaEnvio;
            this.txtimporte.Text = this.pedidos[this.indexPedido].Importe.ToString();
        }

        private void btneliminarpedido_Click(object sender, EventArgs e)
        {
            if (this.lstpedidos.SelectedIndex != -1) {

                this.indexPedido = this.lstpedidos.SelectedIndex;
                string codigo = this.pedidos[this.indexPedido].CodigoPedido;
                this.repository.EliminarPedido(codigo);


                //ESTO DEBERIA ESTAR EN UN METODO PARA CARGAR LOS PEDIDOS 

                this.LoadPedidos();
            }
        }

        private void btnmodificarcliente_Click(object sender, EventArgs e)

        {
            string codigo = this.clientes[this.indexCliente].CodigoCliente;
            string cargo = this.txtcargo.Text;
            string ciudad = this.txtciudad.Text;
            string contacto = this.txtcontacto.Text;
            int telefono = int.Parse(this.txttelefono.Text);
            string empresa =this.txtempresa.Text;  


            Cliente cliente = new Cliente(codigo,empresa,contacto,cargo,ciudad,telefono);
            this.repository.ModificarCliente(cliente);


            
        }
    }
}
