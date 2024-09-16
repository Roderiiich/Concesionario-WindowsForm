using DemoCV.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class EditarVentaForm : Form
    {
        public EditarVentaForm()
        {
            InitializeComponent();
        }
        public Venta VentaModificada { get; private set; }

        public EditarVentaForm(Venta venta)
        {
            InitializeComponent();

            // Llenar los campos con la información de la venta
            cbClientes.Items.AddRange(GlobalVar.clientes.ToArray());
            cbVehiculos.Items.AddRange(GlobalVar.Inventario.Lista().ToArray());

            cbClientes.SelectedItem = venta.Cliente;
            cbVehiculos.SelectedItem = venta.Vehiculo;
            txtPrecio.Text = venta.PrecioVenta.ToString();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void EditarVentaForm_Load(object sender, EventArgs e)
        {

        }

    
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtener los valores seleccionados/modificados
            Cliente clienteSeleccionado = cbClientes.SelectedItem as Cliente;
            Vehiculo vehiculoSeleccionado = cbVehiculos.SelectedItem as Vehiculo;
            decimal precioVenta;

            if (clienteSeleccionado == null || vehiculoSeleccionado == null || !decimal.TryParse(txtPrecio.Text, out precioVenta))
            {
                MessageBox.Show("Por favor, ingresa valores válidos.");
                return;
            }

            // Actualizar la venta modificada
            VentaModificada = new Venta
            {
                Cliente = clienteSeleccionado,
                Vehiculo = vehiculoSeleccionado,
                PrecioVenta = precioVenta,
                FechaVenta = DateTime.Now // Puedes mantener la fecha original si prefieres
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
