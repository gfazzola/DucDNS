using System;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.IO;
using MTC.Host.IComun;
namespace MTC.Nucleo.DucDNS
{
    public class Mensajes
    {
        string _mailFrom, _passMailFrom;
        System.Net.Mail.SmtpClient _client;
        public Mensajes(string smtpServer, int puerto, string mailFrom, string usuario, string passMailFrom, bool ssl = false)
        {
            _mailFrom = mailFrom;
            _passMailFrom = passMailFrom;
            _client = new System.Net.Mail.SmtpClient(smtpServer);
            _client.Credentials = new System.Net.NetworkCredential(usuario, _passMailFrom);
            _client.Port = puerto;
            _client.EnableSsl = ssl;
        }

        public Mensajes(string smtpServer, int puerto, string mailFrom, string passMailFrom, bool ssl = false)
        {
            _mailFrom = mailFrom;
            _passMailFrom = passMailFrom;
            _client = new System.Net.Mail.SmtpClient(smtpServer);
            _client.Credentials = new System.Net.NetworkCredential(_mailFrom, _passMailFrom);
            _client.Port = puerto;
            _client.EnableSsl = ssl;
        }

        public string enviarEmail(string mailDestino, string asunto, string msg, bool html)
        {
            return enviarEmail(mailDestino, asunto, msg, html, null);
        }

        public string enviarEmail(string mailDestino, string asunto, string msg, bool html, string[] archivosAdjuntos)
        {
            return enviarEmail(mailDestino, asunto, msg, html, archivosAdjuntos, false);
        }

        public string enviarEmail(string mailDestino, string asunto, string msg, bool html, string[] archivosAdjuntos, bool async)
        {
            return enviarEmail(mailDestino, asunto, msg, html, archivosAdjuntos, async, false, Encoding.UTF8/* .GetEncoding("iso-8859-1")*/);
        }

        public string enviarEmail(string mailDestino, string asunto, string msg, bool html, string[] archivosAdjuntos, bool async, bool borraArchivosAdjuntos, Encoding encoding)
        {
            if (!async)
                return __enviarEmail(mailDestino, asunto, msg, html, archivosAdjuntos, borraArchivosAdjuntos, encoding);
            else
            {
                __mailDestino = mailDestino;
                __asunto = asunto;
                __msg = msg;
                __html = html;
                __archivosAdjuntos = archivosAdjuntos;
                __borraArchivosAdjuntos = borraArchivosAdjuntos;
                __encoding = encoding;
                new Thread(new ThreadStart(run)).Start();
                return "";
            }
        }

        private string __enviarEmail(string mailDestino, string asunto, string msg, bool html, string[] archivosAdjuntos, bool borraArchivosAdjuntos, Encoding encoding)
        {
            string result = "";
            System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(_mailFrom);
            System.Net.Mail.MailAddress to = new System.Net.Mail.MailAddress(mailDestino);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);

            message.SubjectEncoding = message.BodyEncoding = encoding;
            message.IsBodyHtml = html;
            message.Subject = asunto;
            message.Body = msg;

            if (archivosAdjuntos != null && archivosAdjuntos.Length > 0)
                foreach (string archivo in archivosAdjuntos)
                    message.Attachments.Add(new Attachment(archivo));

            try
            {
                _client.Send(message);
                message.Attachments.Dispose();

                if (borraArchivosAdjuntos)
                    try
                    {
                        foreach (string archivo in archivosAdjuntos)
                            File.Delete(archivo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error borrando archivos " + ex.Message);
                    }
            }
            catch (Exception ex)
            {
                result = Utils.armarMensajeErrorExcepcion("__enviarEmail", ex);
            }
            return result;
        }

        string __mailDestino, __asunto, __msg;
        bool __html, __borraArchivosAdjuntos;
        string[] __archivosAdjuntos;
        Encoding __encoding;
        void run()
        {
            try
            {
                __enviarEmail(__mailDestino, __asunto, __msg, __html, __archivosAdjuntos, __borraArchivosAdjuntos, __encoding);
            }
            catch (Exception ex)
            {
            }
        }


    }
}
