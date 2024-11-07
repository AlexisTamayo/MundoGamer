using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        private CD_Producto objCapaDato = new CD_Producto();
        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }




        public List<Producto> ObtenerProductos(int idMarca, int idCategoria, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {
            return objCapaDato.ObtenerProductos(idMarca, idCategoria, nroPagina, obtenerRegistros, out TotalRegistros, out TotalPaginas);

        }
        // LUEGO ARREGLAR AQUI PARA LA BUSQUEDA
        //public List<Producto> ObtenerProductosPorBusqueda(string query)
        //{
        //    return objCapaDato.BuscarProducto(query);
        //}


        public int Registrar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;


            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del producto no puede ser vacio";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.Precio == 0)
            {

                Mensaje = "Debe ingrear el precio del producto";
            }
            else if (obj.Stock == 0)
            {

                Mensaje = "Debe ingrear el stock del producto";
            }



            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);

            }
            else
            {

                return 0;
            }



        }

        public bool Editar(Producto obj, out string Mensaje)
        {

            Mensaje = string.Empty;



            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {
                Mensaje = "La descripcion del producto no puede ser vacio";
            }
            else if (obj.oMarca.IdMarca == 0)
            {
                Mensaje = "Debe seleccionar una marca";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe seleccionar una categoria";
            }
            else if (obj.Precio == 0)
            {

                Mensaje = "Debe ingrear el precio del producto";
            }
            else if (obj.Stock == 0)
            {

                Mensaje = "Debe ingrear el stock del producto";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Editar(obj, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {

            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);
        }



        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }




        public bool GuardarCaracteristicas(int idProducto, string[] caracteristicas, out string mensaje)
        {
            mensaje = string.Empty;
            bool respuesta = false;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ToString()))
                {
                    string query = "INSERT INTO CaracteristicaProducto (IdProducto, Caracteristica) VALUES (@IdProducto, @Caracteristica)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                        cmd.Parameters.AddWithValue("@Caracteristica", caracteristicas);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error al guardar las características: " + ex.Message;
                respuesta = false;
            }

            return respuesta;
        }



        public bool GuardarCaracteristicasProducto(int idProducto, string caracteristicas)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_InsertarCaracteristicaProducto", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                    cmd.Parameters.AddWithValue("@Caracteristica", caracteristicas);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Manejo del error
                return false;
            }
        }

        public bool AgregarAFavoritos(int idCliente, int idProducto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ToString()))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_AgregarAFavoritos", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo del error
                throw new Exception("Error al agregar a favoritos: " + ex.Message);
            }
        }
        public List<Producto> ObtenerProductosFavoritosPorCliente(int idCliente)
        {
            return objCapaDato.ObtenerProductosFavoritosPorCliente(idCliente);
        }




        //public List<Producto> ObtenerProductosFavoritosPorCliente(int idCliente)
        //{
        //    return objCapaDato.ObtenerProductosFavoritosPorCliente(idCliente);
        //}





    }
}
