using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1;

namespace DemoCV.clases
{
    public class Concesionario
    {
        public string Nombre { get; set; }
        public Inventario Inventario { get; set; }
        public List<Venta> VentasRealizadas = new List<Venta>();
        public List<Venta> Ventas { get; set; } = new List<Venta>();


        public Concesionario() {
            Inventario = new Inventario();
        }
        public void RegistrarVenta(Vehiculo vehiculo, Cliente cliente, decimal precioVenta)
        {
            Venta venta = new Venta
            {
                Vehiculo = vehiculo,
                Cliente = cliente,
                FechaVenta = DateTime.Now,
                PrecioVenta = precioVenta
            };

            Ventas.Add(venta);
            // Agregar la venta a la lista de ventas
            GlobalVar.ventas.Add(venta);

            // Eliminar el vehículo del inventario
            GlobalVar.Inventario.EliminarVehiculo(vehiculo);
        }

   
        }
    }

