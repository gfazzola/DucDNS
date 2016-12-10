namespace MTC.Nucleo.DucDNS
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuDominios = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemAgregar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.paginas = new System.Windows.Forms.TabControl();
            this.tabPrincipal = new System.Windows.Forms.TabPage();
            this.comboLogs = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuLogs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemActualizarLogs = new System.Windows.Forms.ToolStripMenuItem();
            this.bOpciones = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.editModoDebug = new System.Windows.Forms.NumericUpDown();
            this.labelModoDebug = new System.Windows.Forms.Label();
            this.checkHttps = new System.Windows.Forms.CheckBox();
            this.labelIntervaloActualizacionMinutos = new System.Windows.Forms.Label();
            this.comboMinutosActualizacion = new System.Windows.Forms.ComboBox();
            this.checkVerbose = new System.Windows.Forms.CheckBox();
            this.editToken = new System.Windows.Forms.TextBox();
            this.labelToken = new System.Windows.Forms.Label();
            this.checkSimple = new System.Windows.Forms.CheckBox();
            this.tabDominios = new System.Windows.Forms.TabPage();
            this.lDominios = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabSeguridad = new System.Windows.Forms.TabPage();
            this.editClave = new System.Windows.Forms.TextBox();
            this.checkRequierePassword = new System.Windows.Forms.CheckBox();
            this.tabNotificaciones = new System.Windows.Forms.TabPage();
            this.grupoNotificaciones = new System.Windows.Forms.GroupBox();
            this.labelNotificar = new System.Windows.Forms.Label();
            this.checkLNotificaciones = new System.Windows.Forms.CheckedListBox();
            this.editEmailDestinatarios = new System.Windows.Forms.TextBox();
            this.labelEmailsANotificar = new System.Windows.Forms.Label();
            this.grupoSMTP = new System.Windows.Forms.GroupBox();
            this.editClaveSMTP = new System.Windows.Forms.TextBox();
            this.labelClaveSMTP = new System.Windows.Forms.Label();
            this.bProbarEmail = new System.Windows.Forms.Button();
            this.checkSSL = new System.Windows.Forms.CheckBox();
            this.editEmail = new System.Windows.Forms.TextBox();
            this.labelEmail = new System.Windows.Forms.Label();
            this.editUsuarioSMTP = new System.Windows.Forms.TextBox();
            this.labelUsuarioSMTP = new System.Windows.Forms.Label();
            this.labelPuertoSMTP = new System.Windows.Forms.Label();
            this.editPuertoSMTP = new System.Windows.Forms.NumericUpDown();
            this.editServidorSMTP = new System.Windows.Forms.TextBox();
            this.labelServidorSMTP = new System.Windows.Forms.Label();
            this.barraEstado = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelEstadoConexion = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.itemSistema = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGuardarYSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemIniciarDetenerActualizacion = new System.Windows.Forms.ToolStripMenuItem();
            this.itemActualizarAhora = new System.Windows.Forms.ToolStripMenuItem();
            this.editDescripcionEquipo = new System.Windows.Forms.TextBox();
            this.labelDescripcionEquipo = new System.Windows.Forms.Label();
            this.menuDominios.SuspendLayout();
            this.paginas.SuspendLayout();
            this.tabPrincipal.SuspendLayout();
            this.menuLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editModoDebug)).BeginInit();
            this.tabDominios.SuspendLayout();
            this.tabSeguridad.SuspendLayout();
            this.tabNotificaciones.SuspendLayout();
            this.grupoNotificaciones.SuspendLayout();
            this.grupoSMTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPuertoSMTP)).BeginInit();
            this.barraEstado.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.menuOpciones.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuDominios
            // 
            this.menuDominios.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAgregar,
            this.itemEditar,
            this.toolStripMenuItem1,
            this.itemEliminar});
            this.menuDominios.Name = "contextMenuStrip1";
            this.menuDominios.Size = new System.Drawing.Size(114, 76);
            // 
            // itemAgregar
            // 
            this.itemAgregar.Name = "itemAgregar";
            this.itemAgregar.Size = new System.Drawing.Size(113, 22);
            this.itemAgregar.Text = "Agregar";
            this.itemAgregar.Click += new System.EventHandler(this.itemAgregar_Click);
            // 
            // itemEditar
            // 
            this.itemEditar.Name = "itemEditar";
            this.itemEditar.Size = new System.Drawing.Size(113, 22);
            this.itemEditar.Text = "Editar";
            this.itemEditar.Click += new System.EventHandler(this.itemEditar_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(110, 6);
            // 
            // itemEliminar
            // 
            this.itemEliminar.Name = "itemEliminar";
            this.itemEliminar.Size = new System.Drawing.Size(113, 22);
            this.itemEliminar.Text = "Eliminar";
            this.itemEliminar.Click += new System.EventHandler(this.itemEliminar_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "desconectado.png");
            this.imageList1.Images.SetKeyName(1, "conectado.png");
            // 
            // paginas
            // 
            this.paginas.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.paginas.Controls.Add(this.tabPrincipal);
            this.paginas.Controls.Add(this.tabDominios);
            this.paginas.Controls.Add(this.tabSeguridad);
            this.paginas.Controls.Add(this.tabNotificaciones);
            this.paginas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paginas.Location = new System.Drawing.Point(0, 24);
            this.paginas.Name = "paginas";
            this.paginas.SelectedIndex = 0;
            this.paginas.Size = new System.Drawing.Size(634, 248);
            this.paginas.TabIndex = 0;
            // 
            // tabPrincipal
            // 
            this.tabPrincipal.Controls.Add(this.editDescripcionEquipo);
            this.tabPrincipal.Controls.Add(this.labelDescripcionEquipo);
            this.tabPrincipal.Controls.Add(this.comboLogs);
            this.tabPrincipal.Controls.Add(this.richTextBox1);
            this.tabPrincipal.Controls.Add(this.bOpciones);
            this.tabPrincipal.Controls.Add(this.linkLabel1);
            this.tabPrincipal.Controls.Add(this.editModoDebug);
            this.tabPrincipal.Controls.Add(this.labelModoDebug);
            this.tabPrincipal.Controls.Add(this.checkHttps);
            this.tabPrincipal.Controls.Add(this.labelIntervaloActualizacionMinutos);
            this.tabPrincipal.Controls.Add(this.comboMinutosActualizacion);
            this.tabPrincipal.Controls.Add(this.checkVerbose);
            this.tabPrincipal.Controls.Add(this.editToken);
            this.tabPrincipal.Controls.Add(this.labelToken);
            this.tabPrincipal.Controls.Add(this.checkSimple);
            this.tabPrincipal.Location = new System.Drawing.Point(4, 25);
            this.tabPrincipal.Name = "tabPrincipal";
            this.tabPrincipal.Padding = new System.Windows.Forms.Padding(3);
            this.tabPrincipal.Size = new System.Drawing.Size(626, 219);
            this.tabPrincipal.TabIndex = 0;
            this.tabPrincipal.Text = "Principal";
            this.tabPrincipal.UseVisualStyleBackColor = true;
            // 
            // comboLogs
            // 
            this.comboLogs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboLogs.FormattingEnabled = true;
            this.comboLogs.Items.AddRange(new object[] {
            "Informacion",
            "Errores",
            "Depuracion"});
            this.comboLogs.Location = new System.Drawing.Point(288, 8);
            this.comboLogs.Name = "comboLogs";
            this.comboLogs.Size = new System.Drawing.Size(239, 21);
            this.comboLogs.TabIndex = 8;
            this.comboLogs.SelectedIndexChanged += new System.EventHandler(this.comboLogs_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox1.ContextMenuStrip = this.menuLogs;
            this.richTextBox1.Location = new System.Drawing.Point(288, 35);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(330, 178);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // menuLogs
            // 
            this.menuLogs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemActualizarLogs});
            this.menuLogs.Name = "menuLogs";
            this.menuLogs.Size = new System.Drawing.Size(122, 26);
            // 
            // itemActualizarLogs
            // 
            this.itemActualizarLogs.Name = "itemActualizarLogs";
            this.itemActualizarLogs.Size = new System.Drawing.Size(121, 22);
            this.itemActualizarLogs.Text = "Actualizar";
            this.itemActualizarLogs.Click += new System.EventHandler(this.itemActualizarLogs_Click);
            // 
            // bOpciones
            // 
            this.bOpciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpciones.Location = new System.Drawing.Point(8, 168);
            this.bOpciones.Name = "bOpciones";
            this.bOpciones.Size = new System.Drawing.Size(75, 23);
            this.bOpciones.TabIndex = 4;
            this.bOpciones.Text = "Opciones";
            this.bOpciones.UseVisualStyleBackColor = true;
            this.bOpciones.Click += new System.EventHandler(this.bOpciones_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(153, 198);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(129, 13);
            this.linkLabel1.TabIndex = 26;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.duckdns.org";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // editModoDebug
            // 
            this.editModoDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editModoDebug.Location = new System.Drawing.Point(8, 142);
            this.editModoDebug.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.editModoDebug.Name = "editModoDebug";
            this.editModoDebug.Size = new System.Drawing.Size(51, 20);
            this.editModoDebug.TabIndex = 3;
            // 
            // labelModoDebug
            // 
            this.labelModoDebug.AutoSize = true;
            this.labelModoDebug.Location = new System.Drawing.Point(5, 126);
            this.labelModoDebug.Name = "labelModoDebug";
            this.labelModoDebug.Size = new System.Drawing.Size(70, 13);
            this.labelModoDebug.TabIndex = 7;
            this.labelModoDebug.Text = "Modo debug:";
            this.labelModoDebug.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // checkHttps
            // 
            this.checkHttps.AutoSize = true;
            this.checkHttps.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkHttps.Location = new System.Drawing.Point(135, 145);
            this.checkHttps.Name = "checkHttps";
            this.checkHttps.Size = new System.Drawing.Size(80, 17);
            this.checkHttps.TabIndex = 7;
            this.checkHttps.Text = "Utilizar https";
            this.checkHttps.UseVisualStyleBackColor = true;
            // 
            // labelIntervaloActualizacionMinutos
            // 
            this.labelIntervaloActualizacionMinutos.AutoSize = true;
            this.labelIntervaloActualizacionMinutos.Location = new System.Drawing.Point(5, 83);
            this.labelIntervaloActualizacionMinutos.Name = "labelIntervaloActualizacionMinutos";
            this.labelIntervaloActualizacionMinutos.Size = new System.Drawing.Size(117, 13);
            this.labelIntervaloActualizacionMinutos.TabIndex = 5;
            this.labelIntervaloActualizacionMinutos.Text = "Intervalo act. (minutos):";
            this.labelIntervaloActualizacionMinutos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // comboMinutosActualizacion
            // 
            this.comboMinutosActualizacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMinutosActualizacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboMinutosActualizacion.FormattingEnabled = true;
            this.comboMinutosActualizacion.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "30",
            "60"});
            this.comboMinutosActualizacion.Location = new System.Drawing.Point(8, 97);
            this.comboMinutosActualizacion.Name = "comboMinutosActualizacion";
            this.comboMinutosActualizacion.Size = new System.Drawing.Size(121, 21);
            this.comboMinutosActualizacion.TabIndex = 2;
            // 
            // checkVerbose
            // 
            this.checkVerbose.AutoSize = true;
            this.checkVerbose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkVerbose.Location = new System.Drawing.Point(135, 122);
            this.checkVerbose.Name = "checkVerbose";
            this.checkVerbose.Size = new System.Drawing.Size(129, 17);
            this.checkVerbose.TabIndex = 6;
            this.checkVerbose.Text = "Actualizar con detalles";
            this.checkVerbose.UseVisualStyleBackColor = true;
            // 
            // editToken
            // 
            this.editToken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editToken.Location = new System.Drawing.Point(8, 60);
            this.editToken.Name = "editToken";
            this.editToken.Size = new System.Drawing.Size(202, 20);
            this.editToken.TabIndex = 1;
            // 
            // labelToken
            // 
            this.labelToken.AutoSize = true;
            this.labelToken.Location = new System.Drawing.Point(5, 44);
            this.labelToken.Name = "labelToken";
            this.labelToken.Size = new System.Drawing.Size(41, 13);
            this.labelToken.TabIndex = 1;
            this.labelToken.Text = "Token:";
            // 
            // checkSimple
            // 
            this.checkSimple.AutoSize = true;
            this.checkSimple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkSimple.Location = new System.Drawing.Point(135, 98);
            this.checkSimple.Name = "checkSimple";
            this.checkSimple.Size = new System.Drawing.Size(54, 17);
            this.checkSimple.TabIndex = 5;
            this.checkSimple.Text = "Simple";
            this.checkSimple.UseVisualStyleBackColor = true;
            // 
            // tabDominios
            // 
            this.tabDominios.Controls.Add(this.lDominios);
            this.tabDominios.Location = new System.Drawing.Point(4, 25);
            this.tabDominios.Name = "tabDominios";
            this.tabDominios.Padding = new System.Windows.Forms.Padding(3);
            this.tabDominios.Size = new System.Drawing.Size(626, 219);
            this.tabDominios.TabIndex = 1;
            this.tabDominios.Text = "Dominios";
            this.tabDominios.UseVisualStyleBackColor = true;
            // 
            // lDominios
            // 
            this.lDominios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lDominios.CheckBoxes = true;
            this.lDominios.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lDominios.ContextMenuStrip = this.menuDominios;
            this.lDominios.FullRowSelect = true;
            this.lDominios.Location = new System.Drawing.Point(8, 3);
            this.lDominios.Name = "lDominios";
            this.lDominios.Size = new System.Drawing.Size(610, 210);
            this.lDominios.TabIndex = 3;
            this.lDominios.UseCompatibleStateImageBehavior = false;
            this.lDominios.View = System.Windows.Forms.View.Details;
            this.lDominios.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lDominios_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Nombre";
            this.columnHeader1.Width = 610;
            // 
            // tabSeguridad
            // 
            this.tabSeguridad.Controls.Add(this.editClave);
            this.tabSeguridad.Controls.Add(this.checkRequierePassword);
            this.tabSeguridad.Location = new System.Drawing.Point(4, 25);
            this.tabSeguridad.Name = "tabSeguridad";
            this.tabSeguridad.Padding = new System.Windows.Forms.Padding(3);
            this.tabSeguridad.Size = new System.Drawing.Size(626, 219);
            this.tabSeguridad.TabIndex = 2;
            this.tabSeguridad.Text = "Seguridad";
            this.tabSeguridad.UseVisualStyleBackColor = true;
            // 
            // editClave
            // 
            this.editClave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editClave.Enabled = false;
            this.editClave.Location = new System.Drawing.Point(26, 29);
            this.editClave.Name = "editClave";
            this.editClave.PasswordChar = '●';
            this.editClave.Size = new System.Drawing.Size(203, 20);
            this.editClave.TabIndex = 5;
            // 
            // checkRequierePassword
            // 
            this.checkRequierePassword.AutoSize = true;
            this.checkRequierePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkRequierePassword.Location = new System.Drawing.Point(8, 6);
            this.checkRequierePassword.Name = "checkRequierePassword";
            this.checkRequierePassword.Size = new System.Drawing.Size(221, 17);
            this.checkRequierePassword.TabIndex = 4;
            this.checkRequierePassword.Text = "Requiere clave para acceder a la consola";
            this.checkRequierePassword.UseVisualStyleBackColor = true;
            this.checkRequierePassword.CheckedChanged += new System.EventHandler(this.checkRequierePassword_CheckedChanged);
            // 
            // tabNotificaciones
            // 
            this.tabNotificaciones.Controls.Add(this.grupoNotificaciones);
            this.tabNotificaciones.Controls.Add(this.grupoSMTP);
            this.tabNotificaciones.Location = new System.Drawing.Point(4, 25);
            this.tabNotificaciones.Name = "tabNotificaciones";
            this.tabNotificaciones.Padding = new System.Windows.Forms.Padding(3);
            this.tabNotificaciones.Size = new System.Drawing.Size(626, 219);
            this.tabNotificaciones.TabIndex = 3;
            this.tabNotificaciones.Text = "Notificaciones";
            this.tabNotificaciones.UseVisualStyleBackColor = true;
            // 
            // grupoNotificaciones
            // 
            this.grupoNotificaciones.Controls.Add(this.labelNotificar);
            this.grupoNotificaciones.Controls.Add(this.checkLNotificaciones);
            this.grupoNotificaciones.Controls.Add(this.editEmailDestinatarios);
            this.grupoNotificaciones.Controls.Add(this.labelEmailsANotificar);
            this.grupoNotificaciones.Location = new System.Drawing.Point(294, 8);
            this.grupoNotificaciones.Name = "grupoNotificaciones";
            this.grupoNotificaciones.Size = new System.Drawing.Size(321, 205);
            this.grupoNotificaciones.TabIndex = 1;
            this.grupoNotificaciones.TabStop = false;
            this.grupoNotificaciones.Text = "Notificaciones";
            // 
            // labelNotificar
            // 
            this.labelNotificar.AutoSize = true;
            this.labelNotificar.Location = new System.Drawing.Point(12, 60);
            this.labelNotificar.Name = "labelNotificar";
            this.labelNotificar.Size = new System.Drawing.Size(49, 13);
            this.labelNotificar.TabIndex = 13;
            this.labelNotificar.Text = "Notificar:";
            // 
            // checkLNotificaciones
            // 
            this.checkLNotificaciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.checkLNotificaciones.CheckOnClick = true;
            this.checkLNotificaciones.FormattingEnabled = true;
            this.checkLNotificaciones.Items.AddRange(new object[] {
            "Otros errores",
            "Cambio de IP",
            "Errores en intento de cambio de IP",
            "Perdida de conexion a internet",
            "Reestablecimiento de conexion a internet",
            "Actualizacion correcta direccion IP"});
            this.checkLNotificaciones.Location = new System.Drawing.Point(11, 76);
            this.checkLNotificaciones.Name = "checkLNotificaciones";
            this.checkLNotificaciones.Size = new System.Drawing.Size(304, 122);
            this.checkLNotificaciones.TabIndex = 1;
            // 
            // editEmailDestinatarios
            // 
            this.editEmailDestinatarios.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editEmailDestinatarios.Location = new System.Drawing.Point(11, 37);
            this.editEmailDestinatarios.Name = "editEmailDestinatarios";
            this.editEmailDestinatarios.Size = new System.Drawing.Size(304, 20);
            this.editEmailDestinatarios.TabIndex = 0;
            this.editEmailDestinatarios.TextChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // labelEmailsANotificar
            // 
            this.labelEmailsANotificar.AutoSize = true;
            this.labelEmailsANotificar.Location = new System.Drawing.Point(8, 21);
            this.labelEmailsANotificar.Name = "labelEmailsANotificar";
            this.labelEmailsANotificar.Size = new System.Drawing.Size(191, 13);
            this.labelEmailsANotificar.TabIndex = 11;
            this.labelEmailsANotificar.Text = "Emails a notificar (separados por coma)";
            this.labelEmailsANotificar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // grupoSMTP
            // 
            this.grupoSMTP.Controls.Add(this.editClaveSMTP);
            this.grupoSMTP.Controls.Add(this.labelClaveSMTP);
            this.grupoSMTP.Controls.Add(this.bProbarEmail);
            this.grupoSMTP.Controls.Add(this.checkSSL);
            this.grupoSMTP.Controls.Add(this.editEmail);
            this.grupoSMTP.Controls.Add(this.labelEmail);
            this.grupoSMTP.Controls.Add(this.editUsuarioSMTP);
            this.grupoSMTP.Controls.Add(this.labelUsuarioSMTP);
            this.grupoSMTP.Controls.Add(this.labelPuertoSMTP);
            this.grupoSMTP.Controls.Add(this.editPuertoSMTP);
            this.grupoSMTP.Controls.Add(this.editServidorSMTP);
            this.grupoSMTP.Controls.Add(this.labelServidorSMTP);
            this.grupoSMTP.Location = new System.Drawing.Point(8, 8);
            this.grupoSMTP.Name = "grupoSMTP";
            this.grupoSMTP.Size = new System.Drawing.Size(280, 205);
            this.grupoSMTP.TabIndex = 0;
            this.grupoSMTP.TabStop = false;
            this.grupoSMTP.Text = "Servidor de correo";
            // 
            // editClaveSMTP
            // 
            this.editClaveSMTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editClaveSMTP.Location = new System.Drawing.Point(9, 111);
            this.editClaveSMTP.Name = "editClaveSMTP";
            this.editClaveSMTP.PasswordChar = '●';
            this.editClaveSMTP.Size = new System.Drawing.Size(202, 20);
            this.editClaveSMTP.TabIndex = 4;
            this.editClaveSMTP.TextChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // labelClaveSMTP
            // 
            this.labelClaveSMTP.AutoSize = true;
            this.labelClaveSMTP.Location = new System.Drawing.Point(6, 95);
            this.labelClaveSMTP.Name = "labelClaveSMTP";
            this.labelClaveSMTP.Size = new System.Drawing.Size(37, 13);
            this.labelClaveSMTP.TabIndex = 16;
            this.labelClaveSMTP.Text = "Clave:";
            // 
            // bProbarEmail
            // 
            this.bProbarEmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bProbarEmail.Location = new System.Drawing.Point(100, 176);
            this.bProbarEmail.Name = "bProbarEmail";
            this.bProbarEmail.Size = new System.Drawing.Size(174, 23);
            this.bProbarEmail.TabIndex = 6;
            this.bProbarEmail.Text = "Enviar correo de prueba";
            this.bProbarEmail.UseVisualStyleBackColor = true;
            this.bProbarEmail.Click += new System.EventHandler(this.bProbarEmail_Click);
            // 
            // checkSSL
            // 
            this.checkSSL.AutoSize = true;
            this.checkSSL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkSSL.Location = new System.Drawing.Point(225, 69);
            this.checkSSL.Name = "checkSSL";
            this.checkSSL.Size = new System.Drawing.Size(43, 17);
            this.checkSSL.TabIndex = 3;
            this.checkSSL.Text = "SSL";
            this.checkSSL.UseVisualStyleBackColor = true;
            // 
            // editEmail
            // 
            this.editEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editEmail.Location = new System.Drawing.Point(9, 150);
            this.editEmail.Name = "editEmail";
            this.editEmail.Size = new System.Drawing.Size(202, 20);
            this.editEmail.TabIndex = 5;
            this.editEmail.TextChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(6, 134);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(35, 13);
            this.labelEmail.TabIndex = 9;
            this.labelEmail.Text = "Email:";
            // 
            // editUsuarioSMTP
            // 
            this.editUsuarioSMTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editUsuarioSMTP.Location = new System.Drawing.Point(9, 69);
            this.editUsuarioSMTP.Name = "editUsuarioSMTP";
            this.editUsuarioSMTP.Size = new System.Drawing.Size(202, 20);
            this.editUsuarioSMTP.TabIndex = 2;
            this.editUsuarioSMTP.TextChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // labelUsuarioSMTP
            // 
            this.labelUsuarioSMTP.AutoSize = true;
            this.labelUsuarioSMTP.Location = new System.Drawing.Point(6, 53);
            this.labelUsuarioSMTP.Name = "labelUsuarioSMTP";
            this.labelUsuarioSMTP.Size = new System.Drawing.Size(46, 13);
            this.labelUsuarioSMTP.TabIndex = 7;
            this.labelUsuarioSMTP.Text = "Usuario:";
            // 
            // labelPuertoSMTP
            // 
            this.labelPuertoSMTP.AutoSize = true;
            this.labelPuertoSMTP.Location = new System.Drawing.Point(214, 16);
            this.labelPuertoSMTP.Name = "labelPuertoSMTP";
            this.labelPuertoSMTP.Size = new System.Drawing.Size(41, 13);
            this.labelPuertoSMTP.TabIndex = 5;
            this.labelPuertoSMTP.Text = "Puerto:";
            // 
            // editPuertoSMTP
            // 
            this.editPuertoSMTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editPuertoSMTP.Location = new System.Drawing.Point(217, 32);
            this.editPuertoSMTP.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.editPuertoSMTP.Name = "editPuertoSMTP";
            this.editPuertoSMTP.Size = new System.Drawing.Size(51, 20);
            this.editPuertoSMTP.TabIndex = 1;
            this.editPuertoSMTP.ValueChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // editServidorSMTP
            // 
            this.editServidorSMTP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editServidorSMTP.Location = new System.Drawing.Point(9, 32);
            this.editServidorSMTP.Name = "editServidorSMTP";
            this.editServidorSMTP.Size = new System.Drawing.Size(202, 20);
            this.editServidorSMTP.TabIndex = 0;
            this.editServidorSMTP.TextChanged += new System.EventHandler(this.verSiHabilitoEnvioCorreo);
            // 
            // labelServidorSMTP
            // 
            this.labelServidorSMTP.AutoSize = true;
            this.labelServidorSMTP.Location = new System.Drawing.Point(6, 16);
            this.labelServidorSMTP.Name = "labelServidorSMTP";
            this.labelServidorSMTP.Size = new System.Drawing.Size(82, 13);
            this.labelServidorSMTP.TabIndex = 3;
            this.labelServidorSMTP.Text = "Servidor SMTP:";
            this.labelServidorSMTP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // barraEstado
            // 
            this.barraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.toolStripStatusLabel1,
            this.labelEstadoConexion});
            this.barraEstado.Location = new System.Drawing.Point(0, 272);
            this.barraEstado.Name = "barraEstado";
            this.barraEstado.Size = new System.Drawing.Size(634, 22);
            this.barraEstado.SizingGrip = false;
            this.barraEstado.TabIndex = 0;
            this.barraEstado.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 17);
            this.labelStatus.Text = "labelStatus";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(452, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // labelEstadoConexion
            // 
            this.labelEstadoConexion.Name = "labelEstadoConexion";
            this.labelEstadoConexion.Size = new System.Drawing.Size(107, 17);
            this.labelEstadoConexion.Text = "labelEstadoConexion";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemSistema,
            this.itemAyuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // itemSistema
            // 
            this.itemSistema.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemGuardar,
            this.itemGuardarYSalir,
            this.toolStripMenuItem2,
            this.itemSalir});
            this.itemSistema.Name = "itemSistema";
            this.itemSistema.Size = new System.Drawing.Size(56, 20);
            this.itemSistema.Text = "Sistema";
            // 
            // itemGuardar
            // 
            this.itemGuardar.Name = "itemGuardar";
            this.itemGuardar.Size = new System.Drawing.Size(144, 22);
            this.itemGuardar.Text = "Guardar";
            this.itemGuardar.Click += new System.EventHandler(this.itemGuardar_Click);
            // 
            // itemGuardarYSalir
            // 
            this.itemGuardarYSalir.Name = "itemGuardarYSalir";
            this.itemGuardarYSalir.Size = new System.Drawing.Size(144, 22);
            this.itemGuardarYSalir.Text = "Guardar y salir";
            this.itemGuardarYSalir.Click += new System.EventHandler(this.itemGuardarYSalir_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(141, 6);
            // 
            // itemSalir
            // 
            this.itemSalir.Name = "itemSalir";
            this.itemSalir.Size = new System.Drawing.Size(144, 22);
            this.itemSalir.Text = "Salir";
            this.itemSalir.Click += new System.EventHandler(this.itemSalir_Click);
            // 
            // itemAyuda
            // 
            this.itemAyuda.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.itemAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemAcercaDe});
            this.itemAyuda.Name = "itemAyuda";
            this.itemAyuda.Size = new System.Drawing.Size(50, 20);
            this.itemAyuda.Text = "Ayuda";
            // 
            // itemAcercaDe
            // 
            this.itemAcercaDe.Name = "itemAcercaDe";
            this.itemAcercaDe.Size = new System.Drawing.Size(122, 22);
            this.itemAcercaDe.Text = "Acerca de";
            this.itemAcercaDe.Click += new System.EventHandler(this.itemAcercaDe_Click);
            // 
            // menuOpciones
            // 
            this.menuOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemIniciarDetenerActualizacion,
            this.itemActualizarAhora});
            this.menuOpciones.Name = "menuOpciones";
            this.menuOpciones.Size = new System.Drawing.Size(178, 48);
            // 
            // itemIniciarDetenerActualizacion
            // 
            this.itemIniciarDetenerActualizacion.Name = "itemIniciarDetenerActualizacion";
            this.itemIniciarDetenerActualizacion.Size = new System.Drawing.Size(177, 22);
            this.itemIniciarDetenerActualizacion.Text = "Detener actualizacion";
            this.itemIniciarDetenerActualizacion.Click += new System.EventHandler(this.itemPausarActualizacion_Click);
            // 
            // itemActualizarAhora
            // 
            this.itemActualizarAhora.Name = "itemActualizarAhora";
            this.itemActualizarAhora.Size = new System.Drawing.Size(177, 22);
            this.itemActualizarAhora.Text = "Actualizar ahora";
            this.itemActualizarAhora.Click += new System.EventHandler(this.itemActualizarAhora_Click);
            // 
            // editDescripcionEquipo
            // 
            this.editDescripcionEquipo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editDescripcionEquipo.Location = new System.Drawing.Point(9, 21);
            this.editDescripcionEquipo.Name = "editDescripcionEquipo";
            this.editDescripcionEquipo.Size = new System.Drawing.Size(202, 20);
            this.editDescripcionEquipo.TabIndex = 0;
            // 
            // labelDescripcionEquipo
            // 
            this.labelDescripcionEquipo.AutoSize = true;
            this.labelDescripcionEquipo.Location = new System.Drawing.Point(8, 5);
            this.labelDescripcionEquipo.Name = "labelDescripcionEquipo";
            this.labelDescripcionEquipo.Size = new System.Drawing.Size(136, 13);
            this.labelDescripcionEquipo.TabIndex = 31;
            this.labelDescripcionEquipo.Text = "Descripcion de este equipo";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 294);
            this.Controls.Add(this.paginas);
            this.Controls.Add(this.barraEstado);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTC.Nucleo.DucDNS Admin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.menuDominios.ResumeLayout(false);
            this.paginas.ResumeLayout(false);
            this.tabPrincipal.ResumeLayout(false);
            this.tabPrincipal.PerformLayout();
            this.menuLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.editModoDebug)).EndInit();
            this.tabDominios.ResumeLayout(false);
            this.tabSeguridad.ResumeLayout(false);
            this.tabSeguridad.PerformLayout();
            this.tabNotificaciones.ResumeLayout(false);
            this.grupoNotificaciones.ResumeLayout(false);
            this.grupoNotificaciones.PerformLayout();
            this.grupoSMTP.ResumeLayout(false);
            this.grupoSMTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.editPuertoSMTP)).EndInit();
            this.barraEstado.ResumeLayout(false);
            this.barraEstado.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuOpciones.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip barraEstado;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem itemSistema;
        private System.Windows.Forms.ToolStripMenuItem itemSalir;
        private System.Windows.Forms.ToolStripMenuItem itemAyuda;
        private System.Windows.Forms.ToolStripMenuItem itemAcercaDe;
        private System.Windows.Forms.TabControl paginas;
        private System.Windows.Forms.TabPage tabPrincipal;
        private System.Windows.Forms.Label labelIntervaloActualizacionMinutos;
        private System.Windows.Forms.ComboBox comboMinutosActualizacion;
        private System.Windows.Forms.CheckBox checkVerbose;
        private System.Windows.Forms.TextBox editToken;
        private System.Windows.Forms.Label labelToken;
        private System.Windows.Forms.CheckBox checkSimple;
        private System.Windows.Forms.TabPage tabDominios;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ListView lDominios;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip menuDominios;
        private System.Windows.Forms.ToolStripMenuItem itemAgregar;
        private System.Windows.Forms.ToolStripMenuItem itemEditar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem itemEliminar;
        private System.Windows.Forms.ToolStripMenuItem itemGuardar;
        private System.Windows.Forms.ToolStripMenuItem itemGuardarYSalir;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.CheckBox checkHttps;
        private System.Windows.Forms.NumericUpDown editModoDebug;
        private System.Windows.Forms.Label labelModoDebug;
        private System.Windows.Forms.TabPage tabSeguridad;
        private System.Windows.Forms.CheckBox checkRequierePassword;
        private System.Windows.Forms.TextBox editClave;
        private System.Windows.Forms.TabPage tabNotificaciones;
        private System.Windows.Forms.GroupBox grupoNotificaciones;
        private System.Windows.Forms.Label labelNotificar;
        private System.Windows.Forms.CheckedListBox checkLNotificaciones;
        private System.Windows.Forms.TextBox editEmailDestinatarios;
        private System.Windows.Forms.Label labelEmailsANotificar;
        private System.Windows.Forms.GroupBox grupoSMTP;
        private System.Windows.Forms.CheckBox checkSSL;
        private System.Windows.Forms.TextBox editEmail;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.TextBox editUsuarioSMTP;
        private System.Windows.Forms.Label labelUsuarioSMTP;
        private System.Windows.Forms.Label labelPuertoSMTP;
        private System.Windows.Forms.NumericUpDown editPuertoSMTP;
        private System.Windows.Forms.TextBox editServidorSMTP;
        private System.Windows.Forms.Label labelServidorSMTP;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel labelEstadoConexion;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button bProbarEmail;
        private System.Windows.Forms.TextBox editClaveSMTP;
        private System.Windows.Forms.Label labelClaveSMTP;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button bOpciones;
        private System.Windows.Forms.ContextMenuStrip menuOpciones;
        private System.Windows.Forms.ToolStripMenuItem itemIniciarDetenerActualizacion;
        private System.Windows.Forms.ToolStripMenuItem itemActualizarAhora;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboLogs;
        private System.Windows.Forms.ContextMenuStrip menuLogs;
        private System.Windows.Forms.ToolStripMenuItem itemActualizarLogs;
        private System.Windows.Forms.TextBox editDescripcionEquipo;
        private System.Windows.Forms.Label labelDescripcionEquipo;
    }
}