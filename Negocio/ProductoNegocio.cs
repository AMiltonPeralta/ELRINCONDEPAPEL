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

        public List<Producto> listar(string id = "")
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "Select P.IdProducto, P.Nombre, P.StockActual, P.Activo, M.IdMarca, M.Nombre as Marca, C.IdCategoria, C.Nombre as Categoria From Productos P Inner Join Marcas M On P.IdMarca = M.IdMarca Inner Join Categorias C On P.IdCategoria = C.IdCategoria Where P.Activo = 1";
                if (id != "")
                {
                    consulta += " And P.IdProducto = " + id;
                }
                
                datos.setearConsulta(consulta);
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

        public void modificar(Producto prod)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Consulta SQL parametrizada para actualizar los datos de un producto
                datos.setearConsulta("Update Productos set Nombre = @nombre, IdMarca = @idMarca, IdCategoria = @idCategoria, StockActual = @stock Where IdProducto = @id");
                datos.setearParametro("@nombre", prod.Nombre);
                datos.setearParametro("@idMarca", prod.Marca.Id);
                datos.setearParametro("@idCategoria", prod.Categoria.Id);
                datos.setearParametro("@stock", prod.StockActual);
                datos.setearParametro("@id", prod.Id);

                datos.ejecutarAccion();
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

        public void agregar(Producto nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Consulta SQL parametrizada para insertar un nuevo producto de forma segura
                datos.setearConsulta("Insert into Productos (Nombre, IdMarca, IdCategoria, StockActual, Activo) values (@nombre, @idMarca, @idCategoria, @stock, 1)");
                
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@stock", nuevo.StockActual);

                datos.ejecutarAccion();
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
