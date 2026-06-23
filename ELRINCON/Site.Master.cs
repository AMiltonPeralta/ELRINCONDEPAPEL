using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;
using Newtonsoft.Json;

namespace ELRINCON
{
    public partial class SiteMaster : MasterPage
    {
        public string ProductosJson { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> lista = negocio.listar();

                // Convertir la lista a una estructura ligera libre de referencias circulares
                var listaLigera = lista.Select(p => new
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    ImagenUrl = p.ImagenUrl,
                    StockActual = p.StockActual,
                    Marca = p.Marca.Nombre,
                    Categoria = p.Categoria.Nombre,
                    IdCategoria = p.Categoria.Id
                }).ToList();

                ProductosJson = JsonConvert.SerializeObject(listaLigera);
            }
            catch (Exception)
            {
                ProductosJson = "[]";
            }
        }
    }
}
