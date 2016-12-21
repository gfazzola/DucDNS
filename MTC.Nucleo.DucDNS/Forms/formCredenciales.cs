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
    public partial class formCredenciales : formBase
    {
        public int cantIntentos = 0;
        delegate_comunObjeto validarCredenciales;

        public formCredenciales()
        {
            InitializeComponent();
        }

        public formCredenciales(delegate_comunObjeto validarCredenciales)
        {
            InitializeComponent();
            this.validarCredenciales = validarCredenciales;
            labelClaveCredenciales.Text = Properties.Resources.labelClaveCredenciales;
            bAceptar.Text = Properties.Resources.bAceptar;
            bCerrar.Text = Properties.Resources.bCerrar;
        }


        private void formCredenciales_Load(object sender, EventArgs e)
        {
            verSiHabilito(null, null);
        }

        private void bAceptar_Click(object sender, EventArgs e)
        {
            validar();
        }

        private void validar()
        {
            bool credencialesValidas = (bool)validarCredenciales(editClave.Text);
            if (credencialesValidas)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                cantIntentos++;
                if (cantIntentos > 3)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
                else
                {
                    //1.0.0.1
                    MessageBox.Show(Properties.Resources.errorClaveIncorrecta, Properties.Resources.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    editClave.Text = "";
                }
            }
        }

        private void suprimirBeep(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void editClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || editClave.Text.Trim().Length==0)
                return;

            validar();
        }

        private void verSiHabilito(object sender, EventArgs e)
        {
            bAceptar.Enabled = editClave.Text.Length > 0;
        }
    }
}