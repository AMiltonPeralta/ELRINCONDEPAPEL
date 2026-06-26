using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace Negocio
{
    public class EmailService
    {
        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            string host = ConfigurationManager.AppSettings["mailtrapHost"] ?? "sandbox.smtp.mailtrap.io";
            int port = 2525;
            if (int.TryParse(ConfigurationManager.AppSettings["mailtrapPort"], out int p))
            {
                port = p;
            }

            string user = ConfigurationManager.AppSettings["mailtrapUser"] ?? "";
            string pass = ConfigurationManager.AppSettings["mailtrapPass"] ?? "";

            server = new SmtpClient();
            server.Host = host;
            server.Port = port;
            server.EnableSsl = true;

            if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pass))
            {
                server.Credentials = new NetworkCredential(user, pass);
            }
        }

        public void armarCorreo(string emailDestino, string asunto, string cuerpo)
        {
            email = new MailMessage();
            email.From = new MailAddress("no-reply@elrincondelpapel.com", "El Rincón del Papel");
            email.To.Add(emailDestino);
            email.Subject = asunto;
            email.IsBodyHtml = true;
            email.Body = cuerpo;
        }

        public void enviarEmail()
        {
            try
            {
                // Solo intentar enviar si las credenciales están configuradas
                if (server.Credentials != null)
                {
                    server.Send(email);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
