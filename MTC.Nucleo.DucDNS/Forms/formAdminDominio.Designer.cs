namespace MTC.Nucleo.DucDNS
{
    partial class formAdminDominio
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
            this.labelNombreDominio = new System.Windows.Forms.Label();
            this.editDominio = new System.Windows.Forms.TextBox();
            this.bGuardar = new System.Windows.Forms.Button();
            this.bCerrar = new System.Windows.Forms.Button();
            this.bGuardarYNuevo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelNombreDominio
            // 
            this.labelNombreDominio.AutoSize = true;
            this.labelNombreDominio.Location = new System.Drawing.Point(5, 9);
            this.labelNombreDominio.Name = "labelNombreDominio";
            this.labelNombreDominio.Size = new System.Drawing.Size(47, 13);
            this.labelNombreDominio.TabIndex = 0;
            this.labelNombreDominio.Text = "Nombre:";
            // 
            // editDominio
            // 
            this.editDominio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editDominio.Location = new System.Drawing.Point(8, 25);
            this.editDominio.Name = "editDominio";
            this.editDominio.Size = new System.Drawing.Size(276, 20);
            this.editDominio.TabIndex = 1;
            this.editDominio.TextChanged += new System.EventHandler(this.verSiHabilito);
            // 
            // bGuardar
            // 
            this.bGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGuardar.Location = new System.Drawing.Point(8, 51);
            this.bGuardar.Name = "bGuardar";
            this.bGuardar.Size = new System.Drawing.Size(75, 23);
            this.bGuardar.TabIndex = 2;
            this.bGuardar.Text = "Guardar";
            this.bGuardar.UseVisualStyleBackColor = true;
            this.bGuardar.Click += new System.EventHandler(this.bGuardar_Click);
            // 
            // bCerrar
            // 
            this.bCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCerrar.Location = new System.Drawing.Point(216, 51);
            this.bCerrar.Name = "bCerrar";
            this.bCerrar.Size = new System.Drawing.Size(75, 23);
            this.bCerrar.TabIndex = 4;
            this.bCerrar.Text = "Cerrar";
            this.bCerrar.UseVisualStyleBackColor = true;
            // 
            // bGuardarYNuevo
            // 
            this.bGuardarYNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bGuardarYNuevo.Location = new System.Drawing.Point(89, 51);
            this.bGuardarYNuevo.Name = "bGuardarYNuevo";
            this.bGuardarYNuevo.Size = new System.Drawing.Size(121, 23);
            this.bGuardarYNuevo.TabIndex = 3;
            this.bGuardarYNuevo.Text = "Guardar y nuevo";
            this.bGuardarYNuevo.UseVisualStyleBackColor = true;
            this.bGuardarYNuevo.Click += new System.EventHandler(this.bGuardarYNuevo_Click);
            // 
            // formAdminDominio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCerrar;
            this.ClientSize = new System.Drawing.Size(298, 80);
            this.Controls.Add(this.bGuardarYNuevo);
            this.Controls.Add(this.bCerrar);
            this.Controls.Add(this.bGuardar);
            this.Controls.Add(this.editDominio);
            this.Controls.Add(this.labelNombreDominio);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAdminDominio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "formAdminDominio";
            this.Load += new System.EventHandler(this.formAdminDominio_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelNombreDominio;
        private System.Windows.Forms.TextBox editDominio;
        private System.Windows.Forms.Button bGuardar;
        private System.Windows.Forms.Button bCerrar;
        private System.Windows.Forms.Button bGuardarYNuevo;
    }
}