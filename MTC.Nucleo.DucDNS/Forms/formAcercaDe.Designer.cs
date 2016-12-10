namespace MTC.Nucleo.DucDNS
{
    partial class formAcercaDe
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
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelActualizadorDucDNS = new System.Windows.Forms.Label();
            this.labelDesarroladoPor = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.bCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MTC.Nucleo.DucDNS.Properties.Resources._1_1_1_64;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(64, 62);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            // 
            // labelActualizadorDucDNS
            // 
            this.labelActualizadorDucDNS.AutoSize = true;
            this.labelActualizadorDucDNS.Location = new System.Drawing.Point(82, 12);
            this.labelActualizadorDucDNS.Name = "labelActualizadorDucDNS";
            this.labelActualizadorDucDNS.Size = new System.Drawing.Size(111, 13);
            this.labelActualizadorDucDNS.TabIndex = 23;
            this.labelActualizadorDucDNS.Text = "Actualizador DucDNS";
            // 
            // labelDesarroladoPor
            // 
            this.labelDesarroladoPor.AutoSize = true;
            this.labelDesarroladoPor.Location = new System.Drawing.Point(82, 35);
            this.labelDesarroladoPor.Name = "labelDesarroladoPor";
            this.labelDesarroladoPor.Size = new System.Drawing.Size(152, 13);
            this.labelDesarroladoPor.TabIndex = 24;
            this.labelDesarroladoPor.Text = "Desarrollado por MTCSistemas";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(82, 58);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(145, 13);
            this.linkLabel1.TabIndex = 25;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.mtcsistemas.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // bCerrar
            // 
            this.bCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCerrar.Location = new System.Drawing.Point(159, 88);
            this.bCerrar.Name = "bCerrar";
            this.bCerrar.Size = new System.Drawing.Size(75, 23);
            this.bCerrar.TabIndex = 26;
            this.bCerrar.Text = "Cerrar";
            this.bCerrar.UseVisualStyleBackColor = true;
            // 
            // formAcercaDe
            // 
            this.AcceptButton = this.bCerrar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bCerrar;
            this.ClientSize = new System.Drawing.Size(238, 113);
            this.Controls.Add(this.bCerrar);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.labelDesarroladoPor);
            this.Controls.Add(this.labelActualizadorDucDNS);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAcercaDe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acerca de";
            this.Load += new System.EventHandler(this.formAcercaDe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelActualizadorDucDNS;
        private System.Windows.Forms.Label labelDesarroladoPor;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button bCerrar;
    }
}