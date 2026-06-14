using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> listarLibreria()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta SQL con INNER JOIN para recuperar el Producto junto con su Marca y Categoría, filtrando por ID de Categoría = 1 (Librería)
                datos.setearConsulta("Select P.IdProducto, P.Nombre, P.StockActual, P.Activo, M.IdMarca, M.Nombre as Marca, C.IdCategoria, C.Nombre as Categoria From Productos P Inner Join Marcas M On P.IdMarca = M.IdMarca Inner Join Categorias C On P.IdCategoria = C.IdCategoria Where P.IdCategoria = 1 And P.Activo = 1");
                
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto aux = new Producto();
                    aux.Id = (int)datos.Lector["IdProducto"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.StockActual = (int)datos.Lector["StockActual"];
                    aux.Activo = (bool)datos.Lector["Activo"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Nombre = (string)datos.Lector["Categoria"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
