using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DemoCV.clases
{
    public class Vehiculo
    {
        public string Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Año { get; set; }
        public decimal Precio { get; set; }
        public int Kilometraje { get; set; }


        public void MostrarDetalles()
        {
            Console.WriteLine($"Marca: {Marca}, Modelo: {Modelo}, Año: {Año},Precio:{Precio},Km:{Kilometraje}");

        }
        public string[] itemView()
        {
            return new string[]
            {
            Año.ToString(),
            Kilometraje.ToString(),
            Precio.ToString("C", new System.Globalization.CultureInfo("es-CL")), // aqui le dimos el formato de moneda chilena
            Marca,
            Modelo, Id
            };
        }
        public Vehiculo()
        {
            Id = Guid.NewGuid().ToString();  // Genera un ID único al crear el vehículo
        }

        public override string ToString()
        {
            return $"Marca: {Marca}, Modelo: {Modelo}, Año: {Año},Precio:{Precio},Km:{Kilometraje}";
        }

    }
}
