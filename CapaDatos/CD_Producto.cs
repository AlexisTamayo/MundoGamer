using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;

namespace CapaDatos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {

            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select p.IdProducto,p.Nombre,p.Descripcion,");
                    sb.AppendLine("m.IdMarca,m.Descripcion[DesMarca],");
                    sb.AppendLine("c.IdCategoria,c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio,p.Stock,p.RutaImagen,p.NombreImagen,p.Activo");
                    sb.AppendLine("from PRODUCTO p");
                    sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch
            {
                lista = new List<Producto>();

            }
            return lista;
        }



        public List<Producto> ObtenerProductos(int idMarca, int idCategoria, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        {

            List<Producto> lista = new List<Producto>();
            TotalRegistros = 0;
            TotalPaginas = 0;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_ObtenerProductos", oconexion);
                    cmd.Parameters.AddWithValue("idMarca", idMarca);
                    cmd.Parameters.AddWithValue("idCategoria", idCategoria);
                    cmd.Parameters.AddWithValue("nroPagina", nroPagina);
                    cmd.Parameters.AddWithValue("obtenerRegistros", obtenerRegistros);
                    cmd.Parameters.Add("TotalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("TotalPaginas", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }

                    TotalRegistros = Convert.ToInt32(cmd.Parameters["TotalRegistros"].Value);
                    TotalPaginas = Convert.ToInt32(cmd.Parameters["TotalPaginas"].Value);
                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();

            }
            return lista;
        }



        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("IdMarca", obj.oMarca.IdMarca);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Precio", obj.Precio);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }



        public bool GuardarDatosImagen(Producto obj, out string Mensaje)
        {

            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "update producto set RutaImagen = @rutaimagen, NombreImagen = @nombreimagen where IdProducto = @idproducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@rutaimagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("@nombreimagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("@idproducto", obj.IdProducto);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo actualizar imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;

        }

        //aqui empiexzo con carac


        public bool GuardarCaracteristicas(int idProducto, string[] caracteristicas, out string mensaje)
        {
            mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();

                    foreach (var caracteristica in caracteristicas)
                    {
                        using (SqlCommand cmd = new SqlCommand("InsertarCaracteristicasProducto", oconexion))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();  // Limpia los parámetros antes de añadir nuevos
                            cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                            cmd.Parameters.AddWithValue("@Caracteristica", caracteristica);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    oconexion.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                return false;
            }
        }





        public List<string> ObtenerCaracteristicasDelProducto(int idProducto)
        {
            List<string> listaCaracteristicas = new List<string>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cn)) // Reemplaza "tuCadenaDeConexion" con tu cadena de conexión
            {
                string consulta = "SELECT Caracteristica FROM CaracteristicaProducto WHERE IdProducto = @IdProducto";

                using (SqlCommand cmd = new SqlCommand(consulta, oconexion))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    oconexion.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaCaracteristicas.Add(reader["Caracteristica"].ToString());
                        }
                    }
                }
            }

            return listaCaracteristicas;
        }




        //




        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;
            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }
            return resultado;
        }
        public List<Producto> BuscarProducto(string query)
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select p.IdProducto,p.Nombre,p.Descripcion,");
                    sb.AppendLine("m.IdMarca,m.Descripcion[DesMarca],");
                    sb.AppendLine("c.IdCategoria,c.Descripcion[DesCategoria],");
                    sb.AppendLine("p.Precio,p.Stock,p.RutaImagen,p.NombreImagen,p.Activo");
                    sb.AppendLine("from PRODUCTO p");
                    sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca");
                    sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");
                    sb.AppendLine("where p.Nombre LIKE @query");  // Filtrar productos por nombre

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    // Dentro de tu método BuscarProducto
                    cmd.Parameters.AddWithValue("@query", "%" + query + "%");  // Asegura que el parámetro incluye los %

                    // Ajusta tu consulta SQL
                    sb.AppendLine("where p.Nombre LIKE @query");  // Utiliza el parámetro directamente


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Activo = Convert.ToBoolean(dr["Activo"])
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();  // En caso de error, retorna una lista vacía
            }

            return lista;
        }

        public void AgregarAFavoritos(int idCliente, int idProducto)
        {
            using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
            {
                string query = "INSERT INTO Favoritos (IdCliente, IdProducto, FechaAgregado) VALUES (@IdCliente, @IdProducto, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, oconexion);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);
                cmd.Parameters.AddWithValue("@IdProducto", idProducto);
                oconexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Producto> ObtenerProductosFavoritosPorCliente(int idCliente)
        {
            List<Producto> productosFavoritos = new List<Producto>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos WHERE IdProducto IN (SELECT IdProducto FROM Favoritos WHERE IdCliente = @IdCliente)", con);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Producto producto = new Producto()
                        {
                            IdProducto = (int)reader["IdProducto"],
                            Nombre = reader["Nombre"].ToString(),
                            // Añade aquí más propiedades según necesites
                        };
                        productosFavoritos.Add(producto);
                    }
                }
            }
            return productosFavoritos;
        }



        //public List<Producto> ObtenerProductosFavoritosPorCliente(int idCliente)
        //{
        //    List<Producto> productos = new List<Producto>();
        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //    {
        //        var query = @"
        //    SELECT p.* FROM PRODUCTO p
        //    JOIN Favoritos f ON p.IdProducto = f.IdProducto
        //    WHERE f.IdCliente = @IdCliente";
        //        using (var command = new SqlCommand(query, oconexion))
        //        {
        //            command.Parameters.AddWithValue("@IdCliente", idCliente);
        //            oconexion.Open();
        //            using (var reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    var producto = new Producto
        //                    {
        //                        IdProducto = (int)reader["IdProducto"],
        //                        Nombre = reader["Nombre"].ToString(),
        //                        Descripcion = reader["Descripcion"].ToString(),
        //                        Precio = (decimal)reader["Precio"],
        //                        Stock = (int)reader["Stock"],
        //                        RutaImagen = reader["RutaImagen"].ToString(),
        //                        NombreImagen = reader["NombreImagen"].ToString()
        //                        // Set other properties as needed
        //                    };
        //                    productos.Add(producto);
        //                }
        //            }
        //        }
        //    }
        //    return productos;
        //}









    }
}
