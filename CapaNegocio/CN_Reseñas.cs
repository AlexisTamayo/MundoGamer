using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad; // Asegúrate de importar el espacio de nombres adecuado si tu clase Reseña está en CapaEntidad

namespace CapaNegocio
{
    public class CN_Reseñas
    {
        // Método para insertar una reseña
        public bool InsertarReseña(int idProducto, string usuario, string comentario, int puntuacion, out string mensaje)
        {
            if (!UsuarioAutenticado(usuario))
            {
                mensaje = "Debe iniciar sesión para dejar una reseña.";
                return false;
            }

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();
                    string query = "INSERT INTO Reseñas (IdProducto, Usuario, Comentario, Puntuacion, Fecha) VALUES (@IdProducto, @Usuario, @Comentario, @Puntuacion, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(query, oconexion))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Comentario", comentario);
                        cmd.Parameters.AddWithValue("@Puntuacion", puntuacion);

                        cmd.ExecuteNonQuery();
                    }

                    mensaje = "Reseña insertada correctamente.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al insertar la reseña: " + ex.Message;
                return false;
            }
        }

        // Método para obtener las reseñas de un producto por su ID
        public List<Reseña> ObtenerReseñasPorProducto(int idProducto)
        {
            List<Reseña> reseñas = new List<Reseña>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
                oconexion.Open();
                string query = "SELECT Usuario, Comentario, Puntuacion FROM Reseñas WHERE IdProducto = @IdProducto";

                using (SqlCommand cmd = new SqlCommand(query, oconexion))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Reseña reseña = new Reseña
                        {
                            Usuario = reader["Usuario"].ToString(),
                            Comentario = reader["Comentario"].ToString(),
                            Puntuacion = Convert.ToInt32(reader["Puntuacion"])
                        };
                        reseñas.Add(reseña);
                    }
                }
            }

            // Si no hay reseñas, la lista permanecerá vacía
            return reseñas;
        }

        // Método para verificar si el usuario ha iniciado sesión
        private bool UsuarioAutenticado(string usuario)
        {
            // Aquí puedes añadir la lógica que verifique si el usuario está autenticado
            // Esta lógica puede depender de cómo estés manejando las sesiones en tu proyecto
            return true; // Simulación de que el usuario está autenticado
        }
    }
}
