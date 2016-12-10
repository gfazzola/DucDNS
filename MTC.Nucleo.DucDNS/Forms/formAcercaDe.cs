using System;
using System.Windows.Forms;

namespace MTC.Nucleo.DucDNS
{
    public partial class formAcercaDe : formBase
    {
        public formAcercaDe()
        {
            InitializeComponent();
            Text = Properties.Resources.acercaDe;
            labelActualizadorDucDNS.Text = Properties.Resources.labelActualizadorDucDNS;
            labelDesarroladoPor.Text = Properties.Resources.labelDesarroladoPor;
            bCerrar.Text = Properties.Resources.bCerrar;
        }

        private void formAcercaDe_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }
    }
}
