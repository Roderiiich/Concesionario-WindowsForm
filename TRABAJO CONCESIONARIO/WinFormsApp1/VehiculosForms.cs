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
    public partial class VehiculosForms : Form
    {
        string IdGlobal = "";
        public VehiculosForms()
        {
            InitializeComponent();
        }


        private void VehiculosForms_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.Columns.Add("Año");
            listView1.Columns.Add("Kilometraje");
            listView1.Columns.Add("Precio");
            listView1.Columns.Add("Marca");
            listView1.Columns.Add("Modelo");
            

            foreach (ColumnHeader column in listView1.Columns)
            {
                column.Width = 100;
            }

            Listar();

        }

        public void Listar()
        {
            listView1.Items.Clear();
            foreach (Vehiculo vehiculo in GlobalVar.Inventario.Lista())
            {
                listView1.Items.Add(new ListViewItem(vehiculo.itemView()));
            }
        }

        private void bt_guardar_Click(object sender, EventArgs e)
        {
            //Validar
            Vehiculo v = new Vehiculo();
            v.Año = Convert.ToInt16(tx_año.Text);
            v.Kilometraje = Convert.ToInt16(tx_km.Text);
            v.Precio = Convert.ToInt16(tx_precio.Text);
            v.Marca = tx_marca.Text;
            v.Modelo = tx_modelo.Text;

            if (String.IsNullOrEmpty(tx_año.Text))
            {
                MessageBox.Show("Debes ingresar el año");
                tx_año.Focus();
                return;
            }
            if (String.IsNullOrEmpty(tx_km.Text))
            {
                MessageBox.Show("Debes ingresar el kilometraje");
                tx_km.Focus();
                return;
            }
            if (String.IsNullOrEmpty(tx_precio.Text))
            {
                MessageBox.Show("Debes ingresar el precio");
                tx_precio.Focus();
                return;
            }
            if (String.IsNullOrEmpty(tx_marca.Text))
            {
                MessageBox.Show("Debes ingresar la marca");
                tx_marca.Focus();
                return;
            }
            if (String.IsNullOrEmpty(tx_modelo.Text))
            {
                MessageBox.Show("Debes ingresar el modelo");
                tx_modelo.Focus();
                return;
            }

            // Validación del tipo de dato
            int año, kilometraje;
            decimal precio;

            if (!int.TryParse(tx_año.Text, out año))
            {
                MessageBox.Show("Ingresa un año válido");
                tx_año.Focus();
                return;
            }

            if (!int.TryParse(tx_km.Text, out kilometraje))
            {
                MessageBox.Show("Ingresa un kilometraje válido");
                tx_km.Focus();
                return;
            }

            if (!decimal.TryParse(tx_precio.Text, out precio))
            {
                MessageBox.Show("Ingresa un precio válido");
                tx_precio.Focus();
                return;
            }

            if (String.IsNullOrEmpty(IdGlobal))
            {
                // Añadir nuevo vehículo
                GlobalVar.Inventario.AgregarVehiculo(v);
                MessageBox.Show("Vehículo almacenado");
            }
            else
            {
                // Modificar vehículo existente
                Vehiculo vehiculo_modificar = GlobalVar.Inventario.Lista().Where(x => x.Id == IdGlobal).FirstOrDefault()!;
                vehiculo_modificar.Año = v.Año;
                vehiculo_modificar.Kilometraje = v.Kilometraje;
                vehiculo_modificar.Precio = v.Precio;
                vehiculo_modificar.Marca = v.Marca;
                vehiculo_modificar.Modelo = v.Modelo;

                MessageBox.Show("Vehículo modificado");
            }

            // Refrescar la lista
            Listar();
        }
        public void ActualizarListaVehiculos()
        {
            Listar();
        }
        private void OpenVentasForm()
        {
            Ventas ventasForm = new Ventas();
            ventasForm.OnVentaRealizada += ActualizarListaVehiculos;
            ventasForm.ShowDialog();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Obtener los datos del vehículo a eliminar
                string vehiculoMarca = listView1.SelectedItems[0].SubItems[3].Text; // Marca
                string vehiculoModelo = listView1.SelectedItems[0].SubItems[4].Text; // Modelo

                // Buscar y eliminar el vehículo del inventario
                Vehiculo vehiculo_eliminar = GlobalVar.Inventario.Lista()
                    .Where(v => v.Marca == vehiculoMarca && v.Modelo == vehiculoModelo)
                    .FirstOrDefault();

                if (vehiculo_eliminar != null)
                {
                    // Eliminar el vehículo del inventario
                    GlobalVar.Inventario.EliminarVehiculo(vehiculo_eliminar);
                    // Actualizar la lista en el ListView
                    Listar();
                    MessageBox.Show("Vehículo eliminado.");
                }
                else
                {
                    MessageBox.Show("No se encontró el vehículo.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un vehículo para eliminar.");
            }
        }
        private Vehiculo vehiculoSeleccionado = null;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string vehiculoMarca = listView1.SelectedItems[0].SubItems[3].Text; // Marca
                string vehiculoModelo = listView1.SelectedItems[0].SubItems[4].Text; // Modelo

                vehiculoSeleccionado = GlobalVar.Inventario.Lista()
                    .Where(v => v.Marca == vehiculoMarca && v.Modelo == vehiculoModelo)
                    .FirstOrDefault();
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Obtener el ID del vehículo a modificar
                string vehiculoId = listView1.SelectedItems[0].SubItems[5].Text; // ID

                Vehiculo vehiculo_modificar = GlobalVar.Inventario.Lista()
                    .Where(v => v.Id == vehiculoId)
                    .FirstOrDefault();

                if (vehiculo_modificar != null)
                {
                    // Llenar los campos del formulario para editar el vehículo
                    tx_año.Text = vehiculo_modificar.Año.ToString();
                    tx_km.Text = vehiculo_modificar.Kilometraje.ToString();
                    tx_precio.Text = vehiculo_modificar.Precio.ToString();
                    tx_marca.Text = vehiculo_modificar.Marca;
                    tx_modelo.Text = vehiculo_modificar.Modelo;

                    // Guardar el ID global para modificarlo luego al guardar
                    IdGlobal = vehiculo_modificar.Id;
                }
                else
                {
                    MessageBox.Show("No se encontró el vehículo.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un vehículo para modificar.");
            }
        }
    }
}