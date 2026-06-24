using System;
using System.Web.UI.WebControls;

namespace Negocio
{
    public static class Validacion
    {
        public static bool validaTextoVacio(object control)
        {
            if (control is TextBox textBox)
            {
                return string.IsNullOrWhiteSpace(textBox.Text);
            }
            return false;
        }
    }
}
