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
    public partial class Ventas : Form
    {
        public Ventas()
        {
            InitializeComponent();
        }
        void ListarVentas()
        {
            listView1.Items.Clear();
            foreach (Venta venta in GlobalVar.ventas)
            {
                listView1.Items.Add(new ListViewItem(venta.itemView()));
            }
        }


        private void Ventas_Load(object sender, EventArgs e)
        {
            cargaClientes();
            cargaVehiculos();
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Columns.Add("Vehiculo Vendido");
            listView1.Columns.Add("Cliente");
            listView1.Columns.Add("Fecha de Venta");
            listView1.Columns.Add("Precio de Venta");


            // Cargar las ventas al abrir el formulario
            ListarVentas();

        }


        void cargaClientes()
        {
            cb_clientes.Items.AddRange(GlobalVar.clientes.ToArray());

        }

        void cargaVehiculos()
        {
            cb_vehiculo.Items.AddRange(GlobalVar.Inventario.Lista().ToArray());

        }

        private void cb_vehiculo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ActualizarVehiculos()
        {
            // Actualiza la lista de vehículos en el formulario de vehículos si está abierto
            if (Application.OpenForms.OfType<VehiculosForms>().Any())
            {
                var vehiculosForm = Application.OpenForms.OfType<VehiculosForms>().FirstOrDefault();
                vehiculosForm.Listar();
            }
        }
        public event Action OnVentaRealizada;
        private void button1_Click(object sender, EventArgs e)
        {
            Cliente clienteSeleccionado = cb_clientes.SelectedItem as Cliente;
            Vehiculo vehiculoSeleccionado = cb_vehiculo.SelectedItem as Vehiculo;


            if (clienteSeleccionado == null)
            {
                MessageBox.Show("Selecciona un cliente.");
                return;
            }

            if (vehiculoSeleccionado == null)
            {
                MessageBox.Show("Selecciona un vehículo.");
                return;
            }

            decimal precioVenta;
            if (!decimal.TryParse(tx_precio.Text, out precioVenta))
            {
                MessageBox.Show("Ingresa un precio válido.");
                return;
            }

            if (precioVenta > clienteSeleccionado.DineroDisponible)
            {
                MessageBox.Show("El cliente no tiene suficiente dinero.");
                return;
            }

            // Registrar la venta
            Venta nuevaVenta = new Venta
            {
                Vehiculo = vehiculoSeleccionado,
                Cliente = clienteSeleccionado,
                FechaVenta = DateTime.Now,
                PrecioVenta = precioVenta
            };


            clienteSeleccionado.DineroDisponible -= precioVenta;
            GlobalVar.concesionario.RegistrarVenta(vehiculoSeleccionado, clienteSeleccionado, precioVenta);
            // Actualizar el ListView
            ListarVentas();

            ActualizarVehiculos();
        }


        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tx_precio_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count > 0)
            {
                // Obtener la venta seleccionada
                ListViewItem itemSeleccionado = listView1.SelectedItems[0];
                Venta ventaSeleccionada = GlobalVar.ventas.FirstOrDefault(v => v.itemView().SequenceEqual(itemSeleccionado.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(s => s.Text).ToList()));

                if (ventaSeleccionada != null)
                {
                    // Eliminar la venta de la lista global
                    GlobalVar.ventas.Remove(ventaSeleccionada);

                    // Actualizar la lista visual
                    ListarVentas();

                }

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void ModificarStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Obtener la venta seleccionada
                ListViewItem itemSeleccionado = listView1.SelectedItems[0];
                Venta ventaSeleccionada = GlobalVar.ventas.FirstOrDefault(v =>
                    v.itemView().SequenceEqual(itemSeleccionado.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(s => s.Text).ToList()));

                if (ventaSeleccionada != null)
                {
                    // Mostrar el formulario de edición con los detalles de la venta
                    EditarVentaForm editarForm = new EditarVentaForm(ventaSeleccionada);
                    if (editarForm.ShowDialog() == DialogResult.OK)
                    {
                        // Obtener la venta modificada del formulario
                        Venta ventaModificada = editarForm.VentaModificada;

                        // Actualizar la venta en la lista global
                        int indiceVenta = GlobalVar.ventas.IndexOf(ventaSeleccionada);
                        if (indiceVenta >= 0)
                        {
                            GlobalVar.ventas[indiceVenta] = ventaModificada;
                        }

                        // Actualizar el ListView con la venta modificada
                        ListarVentas();

                        // Actualizar la lista de vehículos disponibles si es necesario
                        ActualizarVehiculos();
                    }
                }
            }
        }
    }
}