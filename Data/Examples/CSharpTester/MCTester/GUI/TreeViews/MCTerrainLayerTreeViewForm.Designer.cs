namespace MCTester.GUI.Trees
{
    partial class MCTerrainLayerTreeViewForm
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
            this.menuStripTerrain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadTerrainFromAFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadTerrainFromABuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateNewTerrain = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadLayerFromAFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadLayerFromABuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.miCreateNewLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.miBackgroungThreadIndex = new System.Windows.Forms.ToolStripMenuItem();
            this.serverLayersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkValidityBeforeRenderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raw3DModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildIndexingDataFor3DModelToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.rawVector3DExtrusionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTerrainOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddAndRemoveVectorLayers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveTerrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveTerrainToAFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveTerrainToABuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.removeTerrainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miRename = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteName = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsLayerOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.saveLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveLayerToAFile = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveLayerToABuffer = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLayerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.miMoveToCenterLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.miRenameLayer = new System.Windows.Forms.ToolStripMenuItem();
            this.miDeleteLayer = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.m_splitter)).BeginInit();
            this.m_splitter.Panel1.SuspendLayout();
            this.m_splitter.SuspendLayout();
            this.menuStripTerrain.SuspendLayout();
            this.cmsTerrainOptions.SuspendLayout();
            this.cmsLayerOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_TreeView
            // 
            this.m_TreeView.LineColor = System.Drawing.Color.Black;
            this.m_TreeView.Margin = new System.Windows.Forms.Padding(4);
            this.m_TreeView.Size = new System.Drawing.Size(400, 722);
            this.m_TreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.m_TreeView_AfterCollapse);
            // 
            // m_splitter
            // 
            this.m_splitter.Location = new System.Drawing.Point(0, 24);
            this.m_splitter.Margin = new System.Windows.Forms.Padding(4);
            // 
            // m_splitter.Panel2
            // 
            this.m_splitter.Panel2.AutoScroll = true;
            this.m_splitter.Size = new System.Drawing.Size(1251, 722);
            this.m_splitter.SplitterDistance = 400;
            this.m_splitter.SplitterWidth = 5;
            // 
            // menuStripTerrain
            // 
            this.menuStripTerrain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripTerrain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStripTerrain.Location = new System.Drawing.Point(0, 0);
            this.menuStripTerrain.Name = "menuStripTerrain";
            this.menuStripTerrain.Size = new System.Drawing.Size(1251, 24);
            this.menuStripTerrain.TabIndex = 1;
            this.menuStripTerrain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terrainToolStripMenuItem,
            this.layerToolStripMenuItem,
            this.serverLayersToolStripMenuItem,
            this.raw3DModelToolStripMenuItem,
            this.rawVector3DExtrusionToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // terrainToolStripMenuItem
            // 
            this.terrainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadTerrainFromAFile,
            this.miLoadTerrainFromABuffer,
            this.miCreateNewTerrain});
            this.terrainToolStripMenuItem.Name = "terrainToolStripMenuItem";
            this.terrainToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.terrainToolStripMenuItem.Text = "Terrain";
            // 
            // miLoadTerrainFromAFile
            // 
            this.miLoadTerrainFromAFile.Name = "miLoadTerrainFromAFile";
            this.miLoadTerrainFromAFile.Size = new System.Drawing.Size(173, 22);
            this.miLoadTerrainFromAFile.Text = "Load from a File";
            this.miLoadTerrainFromAFile.Click += new System.EventHandler(this.miLoadTerrainFromAFile_Click);
            // 
            // miLoadTerrainFromABuffer
            // 
            this.miLoadTerrainFromABuffer.Name = "miLoadTerrainFromABuffer";
            this.miLoadTerrainFromABuffer.Size = new System.Drawing.Size(173, 22);
            this.miLoadTerrainFromABuffer.Text = "Load from a Buffer";
            this.miLoadTerrainFromABuffer.Click += new System.EventHandler(this.miLoadTerrainFromABuffer_Click);
            // 
            // miCreateNewTerrain
            // 
            this.miCreateNewTerrain.Name = "miCreateNewTerrain";
            this.miCreateNewTerrain.Size = new System.Drawing.Size(173, 22);
            this.miCreateNewTerrain.Text = "Create New";
            this.miCreateNewTerrain.Click += new System.EventHandler(this.miCreateNewTerrain_Click);
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLoadLayerFromAFile,
            this.miLoadLayerFromABuffer,
            this.miCreateNewLayer,
            this.miBackgroungThreadIndex});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // miLoadLayerFromAFile
            // 
            this.miLoadLayerFromAFile.Name = "miLoadLayerFromAFile";
            this.miLoadLayerFromAFile.Size = new System.Drawing.Size(209, 22);
            this.miLoadLayerFromAFile.Text = "Load from a File";
            this.miLoadLayerFromAFile.Click += new System.EventHandler(this.miLoadLayerFromAFile_Click);
            // 
            // miLoadLayerFromABuffer
            // 
            this.miLoadLayerFromABuffer.Name = "miLoadLayerFromABuffer";
            this.miLoadLayerFromABuffer.Size = new System.Drawing.Size(209, 22);
            this.miLoadLayerFromABuffer.Text = "Load from a Buffer";
            this.miLoadLayerFromABuffer.Click += new System.EventHandler(this.miLoadLayerFromABuffer_Click);
            // 
            // miCreateNewLayer
            // 
            this.miCreateNewLayer.Name = "miCreateNewLayer";
            this.miCreateNewLayer.Size = new System.Drawing.Size(209, 22);
            this.miCreateNewLayer.Text = "Create New";
            this.miCreateNewLayer.Click += new System.EventHandler(this.miCreateNewLayer_Click);
            // 
            // miBackgroungThreadIndex
            // 
            this.miBackgroungThreadIndex.Name = "miBackgroungThreadIndex";
            this.miBackgroungThreadIndex.Size = new System.Drawing.Size(209, 22);
            this.miBackgroungThreadIndex.Text = "Backgroung Thread Index";
            this.miBackgroungThreadIndex.Click += new System.EventHandler(this.miBackgroungThreadIndex_Click);
            // 
            // serverLayersToolStripMenuItem
            // 
            this.serverLayersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem,
            this.checkValidityBeforeRenderToolStripMenuItem});
            this.serverLayersToolStripMenuItem.Name = "serverLayersToolStripMenuItem";
            this.serverLayersToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.serverLayersToolStripMenuItem.Text = "Server Layers";
            // 
            // checkAllNativeServerLayersValidityAsyncToolStripMenuItem
            // 
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem.Name = "checkAllNativeServerLayersValidityAsyncToolStripMenuItem";
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem.Text = "Check All Native Server Layers Validity Async";
            this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem.Click += new System.EventHandler(this.checkAllNativeServerLayersValidityAsyncToolStripMenuItem_Click);
            // 
            // checkValidityBeforeRenderToolStripMenuItem
            // 
            this.checkValidityBeforeRenderToolStripMenuItem.Name = "checkValidityBeforeRenderToolStripMenuItem";
            this.checkValidityBeforeRenderToolStripMenuItem.Size = new System.Drawing.Size(308, 22);
            this.checkValidityBeforeRenderToolStripMenuItem.Text = "Check Validity Before Each Render";
            this.checkValidityBeforeRenderToolStripMenuItem.Click += new System.EventHandler(this.checkValidityBeforeRenderToolStripMenuItem_Click);
            // 
            // raw3DModelToolStripMenuItem
            // 
            this.raw3DModelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildIndexingDataFor3DModelToolStripMenuItem2,
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2});
            this.raw3DModelToolStripMenuItem.Name = "raw3DModelToolStripMenuItem";
            this.raw3DModelToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.raw3DModelToolStripMenuItem.Text = "Raw 3D Model";
            // 
            // buildIndexingDataFor3DModelToolStripMenuItem2
            // 
            this.buildIndexingDataFor3DModelToolStripMenuItem2.Name = "buildIndexingDataFor3DModelToolStripMenuItem2";
            this.buildIndexingDataFor3DModelToolStripMenuItem2.Size = new System.Drawing.Size(281, 22);
            this.buildIndexingDataFor3DModelToolStripMenuItem2.Text = "Build Indexing Data For Raw 3D Model";
            this.buildIndexingDataFor3DModelToolStripMenuItem2.Click += new System.EventHandler(this.buildIndexingDataFor3DModelToolStripMenuItem2_Click);
            // 
            // deleteIndexingDataForRaw3DModelToolStripMenuItem2
            // 
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2.Name = "deleteIndexingDataForRaw3DModelToolStripMenuItem2";
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2.Size = new System.Drawing.Size(281, 22);
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2.Text = "Delete Indexing Data For Raw 3D Model";
            this.deleteIndexingDataForRaw3DModelToolStripMenuItem2.Click += new System.EventHandler(this.deleteIndexingDataForRaw3DModelToolStripMenuItem2_Click);
            // 
            // rawVector3DExtrusionToolStripMenuItem
            // 
            this.rawVector3DExtrusionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2,
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2});
            this.rawVector3DExtrusionToolStripMenuItem.Name = "rawVector3DExtrusionToolStripMenuItem";
            this.rawVector3DExtrusionToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.rawVector3DExtrusionToolStripMenuItem.Text = "Raw Vector 3D Extrusion";
            // 
            // buildIndexingDataForVector3DExtrusionToolStripMenuItem2
            // 
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2.Name = "buildIndexingDataForVector3DExtrusionToolStripMenuItem2";
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2.Size = new System.Drawing.Size(331, 22);
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2.Text = "Build Indexing Data For Raw Vector 3D Extrusion";
            this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2.Click += new System.EventHandler(this.buildIndexingDataForVector3DExtrusionToolStripMenuItem2_Click);
            // 
            // deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2
            // 
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2.Name = "deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2";
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2.Size = new System.Drawing.Size(331, 22);
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2.Text = "Delete Indexing Data For Raw Vector 3D Extrusion";
            this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2.Click += new System.EventHandler(this.deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2_Click);
            // 
            // cmsTerrainOptions
            // 
            this.cmsTerrainOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsTerrainOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLayers,
            this.toolStripSeparator2,
            this.saveTerrainToolStripMenuItem,
            this.removeTerrainToolStripMenuItem,
            this.toolStripSeparator1,
            this.miRename,
            this.miDeleteName});
            this.cmsTerrainOptions.Name = "contextMenuStrip2";
            this.cmsTerrainOptions.Size = new System.Drawing.Size(147, 126);
            // 
            // miLayers
            // 
            this.miLayers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAddLayer,
            this.miRemoveLayer,
            this.miAddAndRemoveVectorLayers});
            this.miLayers.Name = "miLayers";
            this.miLayers.Size = new System.Drawing.Size(146, 22);
            this.miLayers.Text = "Layers";
            // 
            // miAddLayer
            // 
            this.miAddLayer.Name = "miAddLayer";
            this.miAddLayer.Size = new System.Drawing.Size(237, 22);
            this.miAddLayer.Text = "Add Layer";
            this.miAddLayer.Click += new System.EventHandler(this.miAddLayer_Click);
            // 
            // miRemoveLayer
            // 
            this.miRemoveLayer.Name = "miRemoveLayer";
            this.miRemoveLayer.Size = new System.Drawing.Size(237, 22);
            this.miRemoveLayer.Text = "Remove Layer";
            this.miRemoveLayer.Click += new System.EventHandler(this.miRemoveLayer_Click);
            // 
            // miAddAndRemoveVectorLayers
            // 
            this.miAddAndRemoveVectorLayers.Name = "miAddAndRemoveVectorLayers";
            this.miAddAndRemoveVectorLayers.Size = new System.Drawing.Size(237, 22);
            this.miAddAndRemoveVectorLayers.Text = "Add and Remove Vector Layers";
            this.miAddAndRemoveVectorLayers.Click += new System.EventHandler(this.miAddAndRemoveVectorLayers_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // saveTerrainToolStripMenuItem
            // 
            this.saveTerrainToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSaveTerrainToAFile,
            this.miSaveTerrainToABuffer});
            this.saveTerrainToolStripMenuItem.Name = "saveTerrainToolStripMenuItem";
            this.saveTerrainToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveTerrainToolStripMenuItem.Text = "Save Terrain";
            // 
            // miSaveTerrainToAFile
            // 
            this.miSaveTerrainToAFile.Name = "miSaveTerrainToAFile";
            this.miSaveTerrainToAFile.Size = new System.Drawing.Size(156, 22);
            this.miSaveTerrainToAFile.Text = "Save to a File";
            this.miSaveTerrainToAFile.Click += new System.EventHandler(this.miSaveTerrainToAFile_Click);
            // 
            // miSaveTerrainToABuffer
            // 
            this.miSaveTerrainToABuffer.Name = "miSaveTerrainToABuffer";
            this.miSaveTerrainToABuffer.Size = new System.Drawing.Size(156, 22);
            this.miSaveTerrainToABuffer.Text = "Save to a Buffer";
            this.miSaveTerrainToABuffer.Click += new System.EventHandler(this.miSaveTerrainToABuffer_Click);
            // 
            // removeTerrainToolStripMenuItem
            // 
            this.removeTerrainToolStripMenuItem.Name = "removeTerrainToolStripMenuItem";
            this.removeTerrainToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.removeTerrainToolStripMenuItem.Text = "Delete Terrain";
            this.removeTerrainToolStripMenuItem.Click += new System.EventHandler(this.removeTerrainToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // miRename
            // 
            this.miRename.Name = "miRename";
            this.miRename.Size = new System.Drawing.Size(146, 22);
            this.miRename.Text = "Rename";
            this.miRename.Click += new System.EventHandler(this.miRename_Click);
            // 
            // miDeleteName
            // 
            this.miDeleteName.Name = "miDeleteName";
            this.miDeleteName.Size = new System.Drawing.Size(146, 22);
            this.miDeleteName.Text = "Delete Name";
            this.miDeleteName.Click += new System.EventHandler(this.miDeleteName_Click);
            // 
            // cmsLayerOptions
            // 
            this.cmsLayerOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsLayerOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLayerToolStripMenuItem,
            this.removeLayerToolStripMenuItem,
            this.toolStripSeparator3,
            this.miMoveToCenterLayer,
            this.toolStripSeparator4,
            this.miRenameLayer,
            this.miDeleteLayer});
            this.cmsLayerOptions.Name = "cmsLayerOptions";
            this.cmsLayerOptions.Size = new System.Drawing.Size(162, 126);
            this.cmsLayerOptions.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLayerOptions_Opening);
            // 
            // saveLayerToolStripMenuItem
            // 
            this.saveLayerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSaveLayerToAFile,
            this.miSaveLayerToABuffer});
            this.saveLayerToolStripMenuItem.Name = "saveLayerToolStripMenuItem";
            this.saveLayerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.saveLayerToolStripMenuItem.Text = "Save Layer";
            // 
            // miSaveLayerToAFile
            // 
            this.miSaveLayerToAFile.Name = "miSaveLayerToAFile";
            this.miSaveLayerToAFile.Size = new System.Drawing.Size(156, 22);
            this.miSaveLayerToAFile.Text = "Save to a File";
            this.miSaveLayerToAFile.Click += new System.EventHandler(this.miSaveLayerToAFile_Click);
            // 
            // miSaveLayerToABuffer
            // 
            this.miSaveLayerToABuffer.Name = "miSaveLayerToABuffer";
            this.miSaveLayerToABuffer.Size = new System.Drawing.Size(156, 22);
            this.miSaveLayerToABuffer.Text = "Save to a Buffer";
            this.miSaveLayerToABuffer.Click += new System.EventHandler(this.miSaveLayerToABuffer_Click);
            // 
            // removeLayerToolStripMenuItem
            // 
            this.removeLayerToolStripMenuItem.Name = "removeLayerToolStripMenuItem";
            this.removeLayerToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.removeLayerToolStripMenuItem.Text = "Remove Layer";
            this.removeLayerToolStripMenuItem.Click += new System.EventHandler(this.removeLayerToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(158, 6);
            // 
            // miMoveToCenterLayer
            // 
            this.miMoveToCenterLayer.Name = "miMoveToCenterLayer";
            this.miMoveToCenterLayer.Size = new System.Drawing.Size(161, 22);
            this.miMoveToCenterLayer.Text = "Move To Center ";
            this.miMoveToCenterLayer.Click += new System.EventHandler(this.miMoveToCenterLayer_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(158, 6);
            // 
            // miRenameLayer
            // 
            this.miRenameLayer.Name = "miRenameLayer";
            this.miRenameLayer.Size = new System.Drawing.Size(161, 22);
            this.miRenameLayer.Text = "Rename";
            this.miRenameLayer.Click += new System.EventHandler(this.miRenameLayer_Click);
            // 
            // miDeleteLayer
            // 
            this.miDeleteLayer.Name = "miDeleteLayer";
            this.miDeleteLayer.Size = new System.Drawing.Size(161, 22);
            this.miDeleteLayer.Text = "Delete Name";
            this.miDeleteLayer.Click += new System.EventHandler(this.miDeleteLayer_Click);
            // 
            // MCTerrainLayerTreeViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1251, 746);
            this.Controls.Add(this.menuStripTerrain);
            this.MainMenuStrip = this.menuStripTerrain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MCTerrainLayerTreeViewForm";
            this.Text = "MCTerrainLayerTreeViewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MCTerrainLayerTreeViewForm_FormClosing);
            this.Load += new System.EventHandler(this.MCTerrainLayerTreeViewForm_Load);
            this.Controls.SetChildIndex(this.menuStripTerrain, 0);
            this.Controls.SetChildIndex(this.m_splitter, 0);
            this.m_splitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_splitter)).EndInit();
            this.m_splitter.ResumeLayout(false);
            this.menuStripTerrain.ResumeLayout(false);
            this.menuStripTerrain.PerformLayout();
            this.cmsTerrainOptions.ResumeLayout(false);
            this.cmsLayerOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripTerrain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miLoadTerrainFromAFile;
        private System.Windows.Forms.ToolStripMenuItem miLoadTerrainFromABuffer;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miLoadLayerFromAFile;
        private System.Windows.Forms.ToolStripMenuItem miLoadLayerFromABuffer;
        private System.Windows.Forms.ContextMenuStrip cmsTerrainOptions;
        private System.Windows.Forms.ToolStripMenuItem miLayers;
        private System.Windows.Forms.ToolStripMenuItem miAddLayer;
        private System.Windows.Forms.ToolStripMenuItem miRemoveLayer;
        private System.Windows.Forms.ToolStripMenuItem saveTerrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSaveTerrainToAFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveTerrainToABuffer;
        private System.Windows.Forms.ContextMenuStrip cmsLayerOptions;
        private System.Windows.Forms.ToolStripMenuItem saveLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miSaveLayerToAFile;
        private System.Windows.Forms.ToolStripMenuItem miSaveLayerToABuffer;
        private System.Windows.Forms.ToolStripMenuItem miCreateNewLayer;
        private System.Windows.Forms.ToolStripMenuItem miCreateNewTerrain;
        private System.Windows.Forms.ToolStripMenuItem miAddAndRemoveVectorLayers;
        private System.Windows.Forms.ToolStripMenuItem miRename;
        private System.Windows.Forms.ToolStripMenuItem miDeleteName;
        private System.Windows.Forms.ToolStripMenuItem miRenameLayer;
        private System.Windows.Forms.ToolStripMenuItem miDeleteLayer;
        private System.Windows.Forms.ToolStripMenuItem removeTerrainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeLayerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miBackgroungThreadIndex;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem checkAllNativeServerLayersValidityAsyncToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkValidityBeforeRenderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miMoveToCenterLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem serverLayersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raw3DModelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rawVector3DExtrusionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildIndexingDataFor3DModelToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteIndexingDataForRaw3DModelToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem buildIndexingDataForVector3DExtrusionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteIndexingDataForRawVector3DExtrusionToolStripMenuItem2;
    }
}