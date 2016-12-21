using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MTC.Nucleo.DucDNS
{
    public partial class formHistorialVersiones : formBase
    {
        public formHistorialVersiones()
        {
            InitializeComponent();
            Text = Properties.Resources.historialDeVersiones;
        }

        private void formHistorialVersiones_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = Properties.Resources.archivoHistorialVersiones;
        }
    }
}