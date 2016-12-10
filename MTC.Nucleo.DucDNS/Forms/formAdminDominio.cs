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
    public partial class formAdminDominio : formBase
    {
        string dominio;
        delegate_comunObjeto agregarDominio;
        public formAdminDominio()
        {
            InitializeComponent();
        }

        public formAdminDominio(string dominio, delegate_comunObjeto agregarDominio)
        {
            InitializeComponent();
            labelNombreDominio.Text = Properties.Resources.labelNombreDominio;
            
            bGuardar.Text = Properties.Resources.bGuardar;
            bGuardarYNuevo.Text = Properties.Resources.bGuardarYNuevo;
            bCerrar.Text = Properties.Resources.bCerrar;
            this.dominio = dominio;
            this.agregarDominio = agregarDominio;
        }

        private void bGuardar_Click(object sender, EventArgs e)
        {
            if (guardar())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void bGuardarYNuevo_Click(object sender, EventArgs e)
        {
            if (guardar())
            {
                dominio = editDominio.Text = "";
                Text = Properties.Resources.nuevoDominio;
                ActiveControl = editDominio;
            }
        }

        private bool guardar()
        {
            bool agrega = string.IsNullOrEmpty(dominio);
            dominio = editDominio.Text.Trim();
            agregarDominio(dominio);
            return true;
        }

        private void formAdminDominio_Load(object sender, EventArgs e)
        {
            Text = string.IsNullOrEmpty(dominio) ? Properties.Resources.nuevoDominio : Properties.Resources.editarDominio;
            editDominio.Text = dominio;
            ActiveControl = editDominio;
        }

        private void verSiHabilito(object sender, EventArgs e)
        {
            bGuardar.Enabled = bGuardarYNuevo.Enabled = editDominio.Text.Trim().Length > 0;
        }
    }
}