namespace MCTester.ObjectWorld.ObjectsUserControls
{
    partial class ObjectItemSelectedFrm
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
            this.btnOKPlay = new System.Windows.Forms.Button();
            this.lstObject = new System.Windows.Forms.ListBox();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.btnAnimationStop = new System.Windows.Forms.Button();
            this.chxAnimatedAll = new System.Windows.Forms.CheckBox();
            this.btnSelectByScaninig = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOKPlay
            // 
            this.btnOKPlay.Location = new System.Drawing.Point(331, 263);
            this.btnOKPlay.Name = "btnOKPlay";
            this.btnOKPlay.Size = new System.Drawing.Size(75, 23);
            this.btnOKPlay.TabIndex = 2;
            this.btnOKPlay.Text = "OK";
            this.btnOKPlay.UseVisualStyleBackColor = true;
            this.btnOKPlay.Click += new System.EventHandler(this.btnOKItemList_Click);
            // 
            // lstObject
            // 
            this.lstObject.FormattingEnabled = true;
            this.lstObject.HorizontalScrollbar = true;
            this.lstObject.Location = new System.Drawing.Point(12, 6);
            this.lstObject.Name = "lstObject";
            this.lstObject.Size = new System.Drawing.Size(191, 251);
            this.lstObject.TabIndex = 3;
            this.lstObject.SelectedIndexChanged += new System.EventHandler(this.lstObject_SelectedIndexChanged);
            // 
            // lstItems
            // 
            this.lstItems.FormattingEnabled = true;
            this.lstItems.HorizontalScrollbar = true;
            this.lstItems.Location = new System.Drawing.Point(209, 6);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(197, 251);
            this.lstItems.TabIndex = 4;
            // 
            // btnAnimationStop
            // 
            this.btnAnimationStop.Location = new System.Drawing.Point(250, 263);
            this.btnAnimationStop.Name = "btnAnimationStop";
            this.btnAnimationStop.Size = new System.Drawing.Size(75, 23);
            this.btnAnimationStop.TabIndex = 5;
            this.btnAnimationStop.Text = "Stop";
            this.btnAnimationStop.UseVisualStyleBackColor = true;
            this.btnAnimationStop.Visible = false;
            this.btnAnimationStop.Click += new System.EventHandler(this.btnAnimationStop_Click);
            // 
            // chxAnimatedAll
            // 
            this.chxAnimatedAll.AutoSize = true;
            this.chxAnimatedAll.Location = new System.Drawing.Point(12, 267);
            this.chxAnimatedAll.Name = "chxAnimatedAll";
            this.chxAnimatedAll.Size = new System.Drawing.Size(84, 17);
            this.chxAnimatedAll.TabIndex = 6;
            this.chxAnimatedAll.Text = "Animated All";
            this.chxAnimatedAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectByScaninig
            // 
            this.btnSelectByScaninig.Location = new System.Drawing.Point(209, 263);
            this.btnSelectByScaninig.Name = "btnSelectByScaninig";
            this.btnSelectByScaninig.Size = new System.Drawing.Size(35, 23);
            this.btnSelectByScaninig.TabIndex = 7;
            this.btnSelectByScaninig.Text = "...";
            this.btnSelectByScaninig.UseVisualStyleBackColor = true;
            this.btnSelectByScaninig.Click += new System.EventHandler(this.btnSelectByScaning_Click);
            // 
            // ObjectItemSelectedFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(410, 289);
            this.Controls.Add(this.btnSelectByScaninig);
            this.Controls.Add(this.chxAnimatedAll);
            this.Controls.Add(this.btnAnimationStop);
            this.Controls.Add(this.lstItems);
            this.Controls.Add(this.lstObject);
            this.Controls.Add(this.btnOKPlay);
            this.Name = "ObjectItemSelectedFrm";
            this.Text = "Items List";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstObject;
        private System.Windows.Forms.ListBox lstItems;
        public System.Windows.Forms.Button btnAnimationStop;
        public System.Windows.Forms.Button btnOKPlay;
        public System.Windows.Forms.CheckBox chxAnimatedAll;
        private System.Windows.Forms.Button btnSelectByScaninig;
    }
}