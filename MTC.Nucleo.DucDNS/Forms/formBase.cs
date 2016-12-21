using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MTC.Host.IComun;
namespace MTC.Nucleo.DucDNS
{
    public partial class formBase : Form
    {
        //1.0.0.1
        private static NucleoDucDNS _nucleo;

        public formBase()
        {
            InitializeComponent();

            if (_nucleo != null)
                this.Icon = _nucleo.icono;
        }

        //1.0.0.1
        public formBase(NucleoDucDNS nucleo)
        {
            InitializeComponent();
            _nucleo = nucleo;
            this.Icon = _nucleo.icono;
        }

        public string productVersion
        {
            get
            {
                return InformacionEnsamblado.Instance().ProductVersion;
            }
        }

        //1.0.0.1
        public NucleoDucDNS nucleo
        {
            get
            {
                return _nucleo;
            }
        }

    }
}