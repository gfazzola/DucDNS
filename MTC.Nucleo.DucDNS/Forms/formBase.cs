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
        public formBase()
        {
            InitializeComponent();
        }

        public string productVersion
        {
            get
            {
                return InformacionEnsamblado.Instance().ProductVersion;
            }
        }
    }
}
