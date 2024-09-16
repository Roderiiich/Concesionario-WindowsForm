using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoCV.clases
{
    public class Venta
    {
        public Vehiculo Vehiculo { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal PrecioVenta { get; set; }

        public string[] itemView()
        {
            return new string[]
            {
                Vehiculo.ToString(), // Asegúrate de que `ToString` en `Vehiculo` devuelva la representación correcta del vehículo
                Cliente.ToString(),  // Asegúrate de que `ToString` en `Cliente` devuelva la representación correcta del cliente
                FechaVenta.ToString("dd/MM/yyyy"),
                PrecioVenta.ToString("C") // Esto formatea el precio como una moneda
            };
        }
    }
}