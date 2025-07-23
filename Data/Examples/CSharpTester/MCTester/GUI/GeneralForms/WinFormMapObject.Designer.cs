namespace MCTester.GUI.Map
{
    partial class WinFormMapObject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WinFormMapObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "WinFormMapObject";
            this.Size = new System.Drawing.Size(300, 300);
            this.Load += new System.EventHandler(this.WinFormMapObject_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WinFormMapObject_Paint);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WinFormMapObject_PreviewKeyDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WinFormMapObject_MouseMove);
            this.Validated += new System.EventHandler(this.WinFormMapObject_Validated);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WinFormMapObject_MouseDoubleClick);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.WinFormMapObject_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WinFormMapObject_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WinFormMapObject_MouseDown);
            this.Resize += new System.EventHandler(this.WinFormMapObject_Resize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WinFormMapObject_MouseUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WinFormMapObject_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
