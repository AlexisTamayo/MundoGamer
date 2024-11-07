using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Favorito
    {
        private CD_Carrito objCapaDato = new CD_Carrito();

        public bool ExisteFavorito(int idcliente, int idproducto)//CREAR
        {
            return objCapaDato.ExisteCarrito(idcliente, idproducto);
        }

        public bool OperacionFavorito(int idcliente, int idproducto, bool sumar, out string Mensaje)//CREAR
        {
            return objCapaDato.OperacionCarrito(idcliente, idproducto, sumar, out Mensaje);
        }
        public List<Carrito> ListarProducto(int idcliente)
        {
            return objCapaDato.ListarProducto(idcliente);
        }
        //public int CantidadEnCarrito(int idcliente)
        //{
        //    return objCapaDato.CantidadEnCarrito(idcliente);
        //}


        //public List<Carrito> ListarProducto(int idcliente)
        //{
        //    return objCapaDato.ListarProducto(idcliente);
        //}

        //public bool EliminarCarrito(int idcliente, int idproducto)
        //{
        //    return objCapaDato.EliminarCarrito(idcliente, idproducto);
        //}
    }
}
