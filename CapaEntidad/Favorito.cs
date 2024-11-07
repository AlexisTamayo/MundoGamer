using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Favorito
    {
        public int IdFavorito { get; set; }
        public Cliente oCliente { get; set; }  // Mantiene una referencia a un objeto Cliente
        public Producto oProducto { get; set; }  // Mantiene una referencia a un objeto Producto
        public DateTime FechaAgregado { get; set; }  // Almacena la fecha y hora cuando se agregó el ítem

        // Constructor para inicializar FechaAgregado con la fecha y hora actual
        public Favorito()
        {
            FechaAgregado = DateTime.Now;
        }
    }
}
