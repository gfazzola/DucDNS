namespace MTC.Nucleo.DucDNS
{
    partial class formCredenciales
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelClaveCredenciales = new System.Windows.Forms.Label();
            this.editClave = new System.Windows.Forms.TextBox();
            this.bCerrar = new System.Windows.Forms.Button();
            this.bAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelClaveCredenciales
            // 
            this.labelClaveCredenciales.AutoSize = true;
            this.labelClaveCredenciales.Location = new System.Drawing.Point(12, 9);
            this.labelClaveCredenciales.Name = "labelClaveCredenciales";
            this.labelClaveCredenciales.Size = new System.Drawing.Size(37, 13);
            this.labelClaveCredenciales.TabIndex = 0;
            this.labelClaveCredenciales.Text = "Clave:";
            // 
            // editClave
            // 
            this.editClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editClave.Location = new System.Drawing.Point(55, 6);
            this.editClave.Name = "editClave";
            this.editClave.PasswordChar = '●';
            this.editClave.Size = new System.Drawing.Size(203, 20);
            this.editClave.TabIndex = 0;
            this.editClave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editClave_KeyDown);
            this.editClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.suprimirBeep);
            // 
            // bCerrar
            // 
            this.bCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCerrar.Location = new System.Drawing.Point(183, 32);
            this.bCerrar.Name = "bCerrar";
            this.bCerrar.Size = new System.Drawing.Size(75, 23);
            this.bCerrar.TabIndex = 2;
            this.bCerrar.Text = "Cerrar";
            this.bCerrar.UseVisualStyleBackColor = true;
            // 
            // bAceptar
            // 
            this.bAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAceptar.Location = new System.Drawing.Point(102, 32);
            this.bAceptar.Name = "bAceptar";
            this.bAceptar.Size = new System.Drawing.Size(75, 23);
            this.bAceptar.TabIndex = 1;
            this.bAceptar.Text = "Aceptar";
            this.bAceptar.UseVisualStyleBackColor = true;
            this.bAceptar.Click += new System.EventHandler(this.bAceptar_Click);
            // 
            // formCredenciales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCerrar;
            this.ClientSize = new System.Drawing.Size(266, 61);
            this.Controls.Add(this.bAceptar);
            this.Controls.Add(this.bCerrar);
            this.Controls.Add(this.editClave);
            this.Controls.Add(this.labelClaveCredenciales);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formCredenciales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTC.Nucleo.DucDNS Admin";
            this.Load += new System.EventHandler(this.formCredenciales_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelClaveCredenciales;
        private System.Windows.Forms.TextBox editClave;
        private System.Windows.Forms.Button bCerrar;
        private System.Windows.Forms.Button bAceptar;
    }
}