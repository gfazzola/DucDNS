using System.Configuration;
using System.ComponentModel;
using System.Xml.Serialization;
namespace MTC.Nucleo.DucDNS
{
    public class ConfiguracionServidorDucDNS : ConfigurationSection
    {
        [ConfigurationProperty("token")]
        public string token
        {
            get { return (string)base["token"]; }
            set { base["token"] = value; }
        }

        [ConfigurationProperty("simple")]
        public bool simple
        {
            get { return (bool)base["simple"]; }
            set { base["simple"] = value; }
        }

        [ConfigurationProperty("verbose")]
        public bool verbose
        {
            get { return (bool)base["verbose"]; }
            set { base["verbose"] = value; }
        }

        [ConfigurationProperty("https")]
        public bool https
        {
            get { return (bool)base["https"]; }
            set { base["https"] = value; }
        }

        [ConfigurationProperty("minutosActualizacion")]
        public int minutosActualizacion
        {
            get { return (int)base["minutosActualizacion"]; }
            set { base["minutosActualizacion"] = value; }
        }

        [ConfigurationProperty("dominios")]
        public string dominios
        {
            get { return (string)base["dominios"]; }
            set { base["dominios"] = value; }
        }

        [ConfigurationProperty("modoDebug")]
        public int modoDebug
        {
            get { return (int)base["modoDebug"]; }
            set { base["modoDebug"] = value; }
        }

        [ConfigurationProperty("adminRequierePassword")]
        public bool adminRequierePassword
        {
            get { return (bool)base["adminRequierePassword"]; }
            set { base["adminRequierePassword"] = value; }
        }

        [ConfigurationProperty("passwordAdmin")]
        public string passwordAdmin
        {
            get { return (string)base["passwordAdmin"]; }
            set { base["passwordAdmin"] = value; }
        }

        #region email
        [ConfigurationProperty("servidorSMTP")]
        public string servidorSMTP
        {
            get { return (string)base["servidorSMTP"]; }
            set { base["servidorSMTP"] = value; }
        }

        [ConfigurationProperty("puertoSMTP")]
        public int puertoSMTP
        {
            get { return (int)base["puertoSMTP"]; }
            set { base["puertoSMTP"] = value; }
        }

        [ConfigurationProperty("usuarioSMTP")]
        public string usuarioSMTP
        {
            get { return (string)base["usuarioSMTP"]; }
            set { base["usuarioSMTP"] = value; }
        }

        [ConfigurationProperty("claveUsuarioSMTP")]
        public string claveUsuarioSMTP
        {
            get { return (string)base["claveUsuarioSMTP"]; }
            set { base["claveUsuarioSMTP"] = value; }
        }

        [ConfigurationProperty("emailEnvio")]
        public string emailEnvio
        {
            get { return (string)base["emailEnvio"]; }
            set { base["emailEnvio"] = value; }
        }

        [ConfigurationProperty("SMTPSSL")]
        public bool SMTPSSL
        {
            get { return (bool)base["SMTPSSL"]; }
            set { base["SMTPSSL"] = value; }
        }

        [XmlIgnore]
        [Browsable(false)]
        public bool servidorCorreoConfigurado
        {
            get
            {
                return !string.IsNullOrEmpty(servidorSMTP) &&
                    puertoSMTP > 0 &&
                    !string.IsNullOrEmpty(usuarioSMTP) &&
                    !string.IsNullOrEmpty(claveUsuarioSMTP)
                    && !string.IsNullOrEmpty(emailEnvio);
            }
        }

        #endregion

        [ConfigurationProperty("emailsNotificaciones")]
        public string emailsNotificaciones
        {
            get { return (string)base["emailsNotificaciones"]; }
            set { base["emailsNotificaciones"] = value; }
        }

        [ConfigurationProperty("mascaraNotificaciones")]
        public int mascaraNotificaciones
        {
            get { return (int)base["mascaraNotificaciones"]; }
            set { base["mascaraNotificaciones"] = value; }
        }

        [ConfigurationProperty("descripcionEquipo")]
        public string descripcionEquipo
        {
            get { return (string)base["descripcionEquipo"]; }
            set { base["descripcionEquipo"] = value; }
        }

        #region que notifica
        [XmlIgnore]
        public bool notificaOtrosErrores { get { return (mascaraNotificaciones & 1) == 1; } }

        [XmlIgnore]
        public bool notificaCambioIp { get { return (mascaraNotificaciones & 2) == 2; } }

        [XmlIgnore]
        public bool notificaErrorCambioIp { get { return (mascaraNotificaciones & 4) == 4; } }

        [XmlIgnore]
        public bool notificaPerdidaConexionInternet { get { return (mascaraNotificaciones & 8) == 8; } }

        [XmlIgnore]
        public bool notificaReestablecimientoConexionInternet { get { return (mascaraNotificaciones & 16) == 16; } }

        [XmlIgnore]
        public bool notificaActualizacionCorrectaIP { get { return (mascaraNotificaciones & 32) == 32; } }
        #endregion
    }
}