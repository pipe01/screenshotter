namespace Screenshotter
{
    partial class frmMain
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFileFormats = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbScale = new System.Windows.Forms.TrackBar();
            this.lblScale = new System.Windows.Forms.Label();
            this.timerSave = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkEffect = new System.Windows.Forms.CheckBox();
            this.chkWinStart = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.tbScale)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Screenshot format:";
            // 
            // cbFileFormats
            // 
            this.cbFileFormats.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileFormats.FormattingEnabled = true;
            this.cbFileFormats.Location = new System.Drawing.Point(112, 15);
            this.cbFileFormats.Name = "cbFileFormats";
            this.cbFileFormats.Size = new System.Drawing.Size(153, 21);
            this.cbFileFormats.TabIndex = 1;
            this.cbFileFormats.SelectedIndexChanged += new System.EventHandler(this.cbFileFormats_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Image size:";
            // 
            // tbScale
            // 
            this.tbScale.Location = new System.Drawing.Point(105, 51);
            this.tbScale.Maximum = 100;
            this.tbScale.Minimum = 20;
            this.tbScale.Name = "tbScale";
            this.tbScale.Size = new System.Drawing.Size(167, 45);
            this.tbScale.TabIndex = 3;
            this.tbScale.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbScale.Value = 100;
            this.tbScale.Scroll += new System.EventHandler(this.tbScale_Scroll);
            // 
            // lblScale
            // 
            this.lblScale.Location = new System.Drawing.Point(8, 63);
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(100, 14);
            this.lblScale.TabIndex = 4;
            this.lblScale.Text = "(1366, 768)";
            this.lblScale.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timerSave
            // 
            this.timerSave.Interval = 2000;
            this.timerSave.Tick += new System.EventHandler(this.timerSave_Tick);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(12, 100);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Quit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "Screenshotter";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // chkEffect
            // 
            this.chkEffect.AutoSize = true;
            this.chkEffect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkEffect.Location = new System.Drawing.Point(140, 85);
            this.chkEffect.Name = "chkEffect";
            this.chkEffect.Size = new System.Drawing.Size(125, 17);
            this.chkEffect.TabIndex = 6;
            this.chkEffect.Text = "Screen shutter effect";
            this.chkEffect.UseVisualStyleBackColor = true;
            this.chkEffect.CheckedChanged += new System.EventHandler(this.chkEffect_CheckedChanged);
            // 
            // chkWinStart
            // 
            this.chkWinStart.AutoSize = true;
            this.chkWinStart.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkWinStart.Location = new System.Drawing.Point(120, 104);
            this.chkWinStart.Name = "chkWinStart";
            this.chkWinStart.Size = new System.Drawing.Size(145, 17);
            this.chkWinStart.TabIndex = 7;
            this.chkWinStart.Text = "Start on Windows startup";
            this.chkWinStart.UseVisualStyleBackColor = true;
            this.chkWinStart.CheckedChanged += new System.EventHandler(this.chkWinStart_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 135);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkWinStart);
            this.Controls.Add(this.chkEffect);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblScale);
            this.Controls.Add(this.tbScale);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbFileFormats);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Screenshotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFileFormats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbScale;
        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.Timer timerSave;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.CheckBox chkEffect;
        private System.Windows.Forms.CheckBox chkWinStart;
        private System.Windows.Forms.Button button2;
    }
}

