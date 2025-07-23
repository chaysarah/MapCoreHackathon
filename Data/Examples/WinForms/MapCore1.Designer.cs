namespace MapCoreWinFormExample
{
    partial class MapCore1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapCore1));
            this.toolStrip = new System.Windows.Forms.ToolStripContainer();
            this.statusToolStrip = new System.Windows.Forms.StatusStrip();
            this.lblCoords = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.menuToolStrip = new System.Windows.Forms.ToolStrip();
            this.btnOpenMap = new System.Windows.Forms.ToolStripButton();
            this.btnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.btnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.btnDrag = new System.Windows.Forms.ToolStripButton();
            this.btnMeasure = new System.Windows.Forms.ToolStripButton();
            this.btnLine = new System.Windows.Forms.ToolStripButton();
            this.btnAddRectangle = new System.Windows.Forms.ToolStripButton();
            this.toolEllipseButton1 = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnBrightness = new System.Windows.Forms.ToolStripButton();
            this.btnOffsetIcon = new System.Windows.Forms.ToolStripButton();
            this.renderTimer = new System.Windows.Forms.Timer(this.components);
            this.lblScale = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip.BottomToolStripPanel.SuspendLayout();
            this.toolStrip.ContentPanel.SuspendLayout();
            this.toolStrip.TopToolStripPanel.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.menuToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            // 
            // toolStrip.BottomToolStripPanel
            // 
            this.toolStrip.BottomToolStripPanel.Controls.Add(this.statusToolStrip);
            // 
            // toolStrip.ContentPanel
            // 
            this.toolStrip.ContentPanel.Controls.Add(this.splitContainer);
            this.toolStrip.ContentPanel.Size = new System.Drawing.Size(933, 446);
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(933, 493);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStripContainer1";
            // 
            // toolStrip.TopToolStripPanel
            // 
            this.toolStrip.TopToolStripPanel.Controls.Add(this.menuToolStrip);
            // 
            // statusToolStrip
            // 
            this.statusToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.statusToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCoords,
            this.lblScale});
            this.statusToolStrip.Location = new System.Drawing.Point(0, 0);
            this.statusToolStrip.Name = "statusToolStrip";
            this.statusToolStrip.Size = new System.Drawing.Size(933, 22);
            this.statusToolStrip.TabIndex = 0;
            // 
            // lblCoords
            // 
            this.lblCoords.Name = "lblCoords";
            this.lblCoords.Size = new System.Drawing.Size(10, 17);
            this.lblCoords.Text = " ";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel1_MouseDoubleClick);
            this.splitContainer.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseDown);
            this.splitContainer.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseMove);
            this.splitContainer.Panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel1_MouseUp);
            this.splitContainer.Panel1.Resize += new System.EventHandler(this.Panel1_Resize);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer1_Panel2_MouseDoubleClick);
            this.splitContainer.Panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseDown);
            this.splitContainer.Panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseMove);
            this.splitContainer.Panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel2_MouseUp);
            this.splitContainer.Panel2.Resize += new System.EventHandler(this.Panel2_Resize);
            this.splitContainer.Size = new System.Drawing.Size(933, 446);
            this.splitContainer.SplitterDistance = 458;
            this.splitContainer.TabIndex = 0;
            // 
            // menuToolStrip
            // 
            this.menuToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.menuToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenMap,
            this.btnZoomIn,
            this.btnZoomOut,
            this.btnDrag,
            this.btnMeasure,
            this.btnLine,
            this.btnAddRectangle,
            this.toolEllipseButton1,
            this.btnEdit,
            this.btnBrightness,
            this.btnOffsetIcon});
            this.menuToolStrip.Location = new System.Drawing.Point(3, 0);
            this.menuToolStrip.Name = "menuToolStrip";
            this.menuToolStrip.Size = new System.Drawing.Size(715, 25);
            this.menuToolStrip.TabIndex = 0;
            // 
            // btnOpenMap
            // 
            this.btnOpenMap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpenMap.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenMap.Image")));
            this.btnOpenMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenMap.Name = "btnOpenMap";
            this.btnOpenMap.Size = new System.Drawing.Size(67, 22);
            this.btnOpenMap.Text = "Open Map";
            this.btnOpenMap.Click += new System.EventHandler(this.btnOpenMap_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(56, 22);
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("btnZoomOut.Image")));
            this.btnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(66, 22);
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnDrag
            // 
            this.btnDrag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDrag.Image = ((System.Drawing.Image)(resources.GetObject("btnDrag.Image")));
            this.btnDrag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrag.Name = "btnDrag";
            this.btnDrag.Size = new System.Drawing.Size(63, 22);
            this.btnDrag.Text = "Drag Map";
            this.btnDrag.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // btnMeasure
            // 
            this.btnMeasure.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMeasure.Image = ((System.Drawing.Image)(resources.GetObject("btnMeasure.Image")));
            this.btnMeasure.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(104, 22);
            this.btnMeasure.Text = "Azimuth Distance";
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // btnLine
            // 
            this.btnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLine.Image = ((System.Drawing.Image)(resources.GetObject("btnLine.Image")));
            this.btnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLine.Name = "btnLine";
            this.btnLine.Size = new System.Drawing.Size(33, 22);
            this.btnLine.Text = "Line";
            this.btnLine.Click += new System.EventHandler(this.btnLine_Click);
            // 
            // btnAddRectangle
            // 
            this.btnAddRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnAddRectangle.Image")));
            this.btnAddRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddRectangle.Name = "btnAddRectangle";
            this.btnAddRectangle.Size = new System.Drawing.Size(63, 22);
            this.btnAddRectangle.Text = "Rectangle";
            this.btnAddRectangle.Click += new System.EventHandler(this.btnAddRectangle_Click);
            // 
            // toolEllipseButton1
            // 
            this.toolEllipseButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolEllipseButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolEllipseButton1.Image")));
            this.toolEllipseButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolEllipseButton1.Name = "toolEllipseButton1";
            this.toolEllipseButton1.Size = new System.Drawing.Size(74, 22);
            this.toolEllipseButton1.Text = "Sight Ellipse";
            this.toolEllipseButton1.Click += new System.EventHandler(this.toolEllipseButton1_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnEdit.Image")));
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(31, 22);
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnBrightness
            // 
            this.btnBrightness.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBrightness.Image = ((System.Drawing.Image)(resources.GetObject("btnBrightness.Image")));
            this.btnBrightness.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBrightness.Name = "btnBrightness";
            this.btnBrightness.Size = new System.Drawing.Size(74, 22);
            this.btnBrightness.Text = "Brightness+";
            this.btnBrightness.Click += new System.EventHandler(this.btnBrightness_Click);
            // 
            // btnOffsetIcon
            // 
            this.btnOffsetIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOffsetIcon.Image = ((System.Drawing.Image)(resources.GetObject("btnOffsetIcon.Image")));
            this.btnOffsetIcon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOffsetIcon.Name = "btnOffsetIcon";
            this.btnOffsetIcon.Size = new System.Drawing.Size(72, 22);
            this.btnOffsetIcon.Text = "Icon & Offset";
            this.btnOffsetIcon.Click += new System.EventHandler(this.btnOffsetIcon_Click);
            // 
            // renderTimer
            // 
            this.renderTimer.Interval = 50;
            this.renderTimer.Tick += new System.EventHandler(this.renderTimer_Tick);
            // 
            // lblScale
            // 
            this.lblScale.Name = "lblScale";
            this.lblScale.Size = new System.Drawing.Size(0, 17);
            // 
            // MapCore1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 493);
            this.Controls.Add(this.toolStrip);
            this.Name = "MapCore1";
            this.Text = "MapCore1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip.BottomToolStripPanel.ResumeLayout(false);
            this.toolStrip.BottomToolStripPanel.PerformLayout();
            this.toolStrip.ContentPanel.ResumeLayout(false);
            this.toolStrip.TopToolStripPanel.ResumeLayout(false);
            this.toolStrip.TopToolStripPanel.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusToolStrip.ResumeLayout(false);
            this.statusToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.menuToolStrip.ResumeLayout(false);
            this.menuToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStrip;
        private System.Windows.Forms.StatusStrip statusToolStrip;
        private System.Windows.Forms.ToolStrip menuToolStrip;
        private System.Windows.Forms.ToolStripButton btnZoomIn;
        private System.Windows.Forms.ToolStripStatusLabel lblCoords;
        private System.Windows.Forms.ToolStripButton btnOpenMap;
        private System.Windows.Forms.ToolStripButton btnZoomOut;
        private System.Windows.Forms.ToolStripButton btnDrag;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripButton btnAddRectangle;
        private System.Windows.Forms.Timer renderTimer;
        private System.Windows.Forms.ToolStripButton toolEllipseButton1;
        private System.Windows.Forms.ToolStripButton btnLine;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnMeasure;
        private System.Windows.Forms.ToolStripButton btnBrightness;
        private System.Windows.Forms.ToolStripButton btnOffsetIcon;
        private System.Windows.Forms.ToolStripStatusLabel lblScale;
    }
}

