namespace Screenshotter
{
    partial class frmImageEditor
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
            this.cmsRightClick = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetZoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.imageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cropToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.taskDialog = new Ookii.Dialogs.TaskDialog(this.components);
            this.taskDialogButton1 = new Ookii.Dialogs.TaskDialogButton(this.components);
            this.taskDialogButton2 = new Ookii.Dialogs.TaskDialogButton(this.components);
            this.taskDialogButton3 = new Ookii.Dialogs.TaskDialogButton(this.components);
            this.timerSelectionBlink = new System.Windows.Forms.Timer(this.components);
            this.cmsRightClick.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsRightClick
            // 
            this.cmsRightClick.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetZoomToolStripMenuItem,
            this.toolStripSeparator1,
            this.imageToolStripMenuItem});
            this.cmsRightClick.Name = "contextMenuStrip1";
            this.cmsRightClick.Size = new System.Drawing.Size(136, 54);
            // 
            // resetZoomToolStripMenuItem
            // 
            this.resetZoomToolStripMenuItem.Name = "resetZoomToolStripMenuItem";
            this.resetZoomToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.resetZoomToolStripMenuItem.Text = "Reset zoom";
            this.resetZoomToolStripMenuItem.Click += new System.EventHandler(this.resetZoomToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // imageToolStripMenuItem
            // 
            this.imageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.toolStripSeparator2,
            this.cropToolStripMenuItem1});
            this.imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            this.imageToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.imageToolStripMenuItem.Text = "Selection";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(99, 6);
            // 
            // cropToolStripMenuItem1
            // 
            this.cropToolStripMenuItem1.Name = "cropToolStripMenuItem1";
            this.cropToolStripMenuItem1.Size = new System.Drawing.Size(102, 22);
            this.cropToolStripMenuItem1.Text = "Crop";
            this.cropToolStripMenuItem1.Click += new System.EventHandler(this.cropToolStripMenuItem1_Click);
            // 
            // taskDialog
            // 
            this.taskDialog.Buttons.Add(this.taskDialogButton1);
            this.taskDialog.Buttons.Add(this.taskDialogButton2);
            this.taskDialog.Buttons.Add(this.taskDialogButton3);
            this.taskDialog.ButtonStyle = Ookii.Dialogs.TaskDialogButtonStyle.CommandLinks;
            this.taskDialog.Content = "Do you want to continue and save the image, or discard the changes?";
            this.taskDialog.MainIcon = Ookii.Dialogs.TaskDialogIcon.Warning;
            this.taskDialog.MainInstruction = "The image changed";
            // 
            // taskDialogButton1
            // 
            this.taskDialogButton1.Text = "Continue with changes";
            // 
            // taskDialogButton2
            // 
            this.taskDialogButton2.Text = "Discard changes";
            // 
            // taskDialogButton3
            // 
            this.taskDialogButton3.Default = true;
            this.taskDialogButton3.Text = "Keep editing";
            // 
            // timerSelectionBlink
            // 
            this.timerSelectionBlink.Enabled = true;
            this.timerSelectionBlink.Interval = 500;
            this.timerSelectionBlink.Tick += new System.EventHandler(this.timerSelectionBlink_Tick);
            // 
            // frmImageEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "frmImageEditor";
            this.ShowIcon = false;
            this.Text = "Image preview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmImagePreview_FormClosing);
            this.Load += new System.EventHandler(this.frmImagePreview_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmImagePreview_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImageEditor_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmImagePreview_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmImagePreview_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmImagePreview_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmImagePreview_MouseUp);
            this.cmsRightClick.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip cmsRightClick;
        private System.Windows.Forms.ToolStripMenuItem resetZoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem imageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cropToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private Ookii.Dialogs.TaskDialog taskDialog;
        private Ookii.Dialogs.TaskDialogButton taskDialogButton1;
        private Ookii.Dialogs.TaskDialogButton taskDialogButton2;
        private Ookii.Dialogs.TaskDialogButton taskDialogButton3;
        private System.Windows.Forms.Timer timerSelectionBlink;
    }
}