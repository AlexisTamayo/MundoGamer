using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public Marca oMarca { get; set; }
        public Categoria oCategoria { get; set; }
        public decimal Precio { get; set; }

        public string PrecioTexto { get; set; }
        public int Stock { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }

        public string Base64 { get; set; }

        public string Extension { get; set; }



        // Agregar la propiedad de reseñas
        public List<Reseña> Reseñas { get; set; } = new List<Reseña>();

        public List<string> Caracteristicas { get; set; }

    }

    public class Reseña
    {
        public string Usuario { get; set; }
        public string Comentario { get; set; }
        public int Puntuacion { get; set; }
    }


}
