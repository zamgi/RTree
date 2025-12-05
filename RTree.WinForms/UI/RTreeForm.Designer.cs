namespace trees.win_forms
{
    partial class RTreeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                components?.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel ctrlPanel;
            System.Windows.Forms.GroupBox gb2;
            System.Windows.Forms.Label l2;
            System.Windows.Forms.Label l6;
            System.Windows.Forms.Label l5;
            System.Windows.Forms.Label l4;
            System.Windows.Forms.Label l3;
            System.Windows.Forms.Label l1;
            System.Windows.Forms.GroupBox gb1;
            System.Windows.Forms.Label l8;
            System.Windows.Forms.Label l7;
            maxEntriesNUD = new System.Windows.Forms.NumericUpDownEx();
            maxWH_NUD = new System.Windows.Forms.NumericUpDownEx();
            minWH_NUD = new System.Windows.Forms.NumericUpDownEx();
            paddingY_NUD = new System.Windows.Forms.NumericUpDownEx();
            paddingX_NUD = new System.Windows.Forms.NumericUpDownEx();
            rebuildTreeButton = new System.Windows.Forms.Button();
            objRectCountNUD = new System.Windows.Forms.NumericUpDownEx();
            selectFigureComboBox = new System.Windows.Forms.ComboBoxFigure();
            selectColorComboBox = new System.Windows.Forms.ComboBoxColor();
            fillNodeListCheckBox = new System.Windows.Forms.CheckBox();
            drawBboxsCheckBox = new System.Windows.Forms.CheckBox();
            darwTextsCheckBox = new System.Windows.Forms.CheckBox();
            rtreeCanvas = new RTreeCanvas();
            rtTreeView = new System.Windows.Forms.TreeView();
            rtListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip( components );
            checkAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            unckeckAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            mainSplitContainer = new System.Windows.Forms.SplitContainer();
            canvasSplitContainer = new System.Windows.Forms.SplitContainer();
            toolTip = new System.Windows.Forms.ToolTip( components );
            repeatSearchButton = new System.Windows.Forms.Button();
            ctrlPanel = new System.Windows.Forms.Panel();
            gb2 = new System.Windows.Forms.GroupBox();
            l2 = new System.Windows.Forms.Label();
            l6 = new System.Windows.Forms.Label();
            l5 = new System.Windows.Forms.Label();
            l4 = new System.Windows.Forms.Label();
            l3 = new System.Windows.Forms.Label();
            l1 = new System.Windows.Forms.Label();
            gb1 = new System.Windows.Forms.GroupBox();
            l8 = new System.Windows.Forms.Label();
            l7 = new System.Windows.Forms.Label();
            ctrlPanel.SuspendLayout();
            gb2.SuspendLayout();
             maxEntriesNUD.BeginInit();
             maxWH_NUD.BeginInit();
             minWH_NUD.BeginInit();
             paddingY_NUD.BeginInit();
             paddingX_NUD.BeginInit();
             objRectCountNUD.BeginInit();
            gb1.SuspendLayout();
            rtListBoxContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) mainSplitContainer).BeginInit();
            mainSplitContainer.Panel1.SuspendLayout();
            mainSplitContainer.Panel2.SuspendLayout();
            mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize) canvasSplitContainer).BeginInit();
            canvasSplitContainer.Panel1.SuspendLayout();
            canvasSplitContainer.Panel2.SuspendLayout();
            canvasSplitContainer.SuspendLayout();
            SuspendLayout();
            // 
            // ctrlPanel
            // 
            ctrlPanel.AutoScroll = true;
            ctrlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ctrlPanel.Controls.Add( gb2 );
            ctrlPanel.Controls.Add( gb1 );
            ctrlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrlPanel.Location = new System.Drawing.Point( 0, 0 );
            ctrlPanel.Name = "ctrlPanel";
            ctrlPanel.Size = new System.Drawing.Size( 1241, 116 );
            ctrlPanel.TabIndex = 2;
            // 
            // gb2
            // 
            gb2.Anchor =  System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            gb2.Controls.Add( repeatSearchButton );
            gb2.Controls.Add( maxEntriesNUD );
            gb2.Controls.Add( l2 );
            gb2.Controls.Add( maxWH_NUD );
            gb2.Controls.Add( minWH_NUD );
            gb2.Controls.Add( l6 );
            gb2.Controls.Add( l5 );
            gb2.Controls.Add( paddingY_NUD );
            gb2.Controls.Add( paddingX_NUD );
            gb2.Controls.Add( l4 );
            gb2.Controls.Add( l3 );
            gb2.Controls.Add( l1 );
            gb2.Controls.Add( rebuildTreeButton );
            gb2.Controls.Add( objRectCountNUD );
            gb2.Location = new System.Drawing.Point( 250, 1 );
            gb2.Name = "gb2";
            gb2.Size = new System.Drawing.Size( 978, 108 );
            gb2.TabIndex = 6;
            gb2.TabStop = false;
            // 
            // maxEntriesNUD
            // 
            maxEntriesNUD.Location = new System.Drawing.Point( 41, 83 );
            maxEntriesNUD.Minimum = new decimal( new int[] { 2, 0, 0, 0 } );
            maxEntriesNUD.Name = "maxEntriesNUD";
            maxEntriesNUD.Size = new System.Drawing.Size( 74, 23 );
            maxEntriesNUD.TabIndex = 4;
            maxEntriesNUD.Value = new decimal( new int[] { 9, 0, 0, 0 } );
            // 
            // l2
            // 
            l2.AutoSize = true;
            l2.Location = new System.Drawing.Point( 6, 66 );
            l2.Name = "l2";
            l2.Size = new System.Drawing.Size( 71, 15 );
            l2.TabIndex = 3;
            l2.Text = "Max Entries:";
            toolTip.SetToolTip( l2, "Max entries/rect per tree-node" );
            // 
            // maxWH_NUD
            // 
            maxWH_NUD.Increment = new decimal( new int[] { 10, 0, 0, 0 } );
            maxWH_NUD.Location = new System.Drawing.Point( 440, 46 );
            maxWH_NUD.Maximum = new decimal( new int[] { 1000, 0, 0, 0 } );
            maxWH_NUD.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
            maxWH_NUD.Name = "maxWH_NUD";
            maxWH_NUD.Size = new System.Drawing.Size( 63, 23 );
            maxWH_NUD.TabIndex = 13;
            maxWH_NUD.ThousandsSeparator = true;
            maxWH_NUD.Value = new decimal( new int[] { 15, 0, 0, 0 } );
            // 
            // minWH_NUD
            // 
            minWH_NUD.Increment = new decimal( new int[] { 10, 0, 0, 0 } );
            minWH_NUD.Location = new System.Drawing.Point( 440, 17 );
            minWH_NUD.Maximum = new decimal( new int[] { 1000, 0, 0, 0 } );
            minWH_NUD.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
            minWH_NUD.Name = "minWH_NUD";
            minWH_NUD.Size = new System.Drawing.Size( 63, 23 );
            minWH_NUD.TabIndex = 12;
            minWH_NUD.ThousandsSeparator = true;
            minWH_NUD.Value = new decimal( new int[] { 5, 0, 0, 0 } );
            // 
            // l6
            // 
            l6.AutoSize = true;
            l6.Location = new System.Drawing.Point( 315, 48 );
            l6.Name = "l6";
            l6.Size = new System.Drawing.Size( 125, 15 );
            l6.TabIndex = 11;
            l6.Text = "Max obj width/height:";
            // 
            // l5
            // 
            l5.AutoSize = true;
            l5.Location = new System.Drawing.Point( 315, 19 );
            l5.Name = "l5";
            l5.Size = new System.Drawing.Size( 123, 15 );
            l5.TabIndex = 10;
            l5.Text = "Min obj width/height:";
            // 
            // paddingY_NUD
            // 
            paddingY_NUD.Increment = new decimal( new int[] { 10, 0, 0, 0 } );
            paddingY_NUD.Location = new System.Drawing.Point( 216, 75 );
            paddingY_NUD.Maximum = new decimal( new int[] { 1000, 0, 0, 0 } );
            paddingY_NUD.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
            paddingY_NUD.Name = "paddingY_NUD";
            paddingY_NUD.Size = new System.Drawing.Size( 63, 23 );
            paddingY_NUD.TabIndex = 9;
            paddingY_NUD.ThousandsSeparator = true;
            paddingY_NUD.Value = new decimal( new int[] { 70, 0, 0, 0 } );
            // 
            // paddingX_NUD
            // 
            paddingX_NUD.Increment = new decimal( new int[] { 10, 0, 0, 0 } );
            paddingX_NUD.Location = new System.Drawing.Point( 216, 46 );
            paddingX_NUD.Maximum = new decimal( new int[] { 1000, 0, 0, 0 } );
            paddingX_NUD.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
            paddingX_NUD.Name = "paddingX_NUD";
            paddingX_NUD.Size = new System.Drawing.Size( 63, 23 );
            paddingX_NUD.TabIndex = 8;
            paddingX_NUD.ThousandsSeparator = true;
            paddingX_NUD.Value = new decimal( new int[] { 200, 0, 0, 0 } );
            // 
            // l4
            // 
            l4.AutoSize = true;
            l4.Location = new System.Drawing.Point( 146, 77 );
            l4.Name = "l4";
            l4.Size = new System.Drawing.Size( 64, 15 );
            l4.TabIndex = 7;
            l4.Text = "Padding Y:";
            // 
            // l3
            // 
            l3.AutoSize = true;
            l3.Location = new System.Drawing.Point( 146, 48 );
            l3.Name = "l3";
            l3.Size = new System.Drawing.Size( 64, 15 );
            l3.TabIndex = 6;
            l3.Text = "Padding X:";
            // 
            // l1
            // 
            l1.AutoSize = true;
            l1.Location = new System.Drawing.Point( 119, 19 );
            l1.Name = "l1";
            l1.Size = new System.Drawing.Size( 91, 15 );
            l1.TabIndex = 5;
            l1.Text = "Obj/Rect count:";
            // 
            // rebuildTreeButton
            // 
            rebuildTreeButton.AutoEllipsis = true;
            rebuildTreeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            rebuildTreeButton.Location = new System.Drawing.Point( 6, 16 );
            rebuildTreeButton.Name = "rebuildTreeButton";
            rebuildTreeButton.Size = new System.Drawing.Size( 88, 44 );
            rebuildTreeButton.TabIndex = 3;
            rebuildTreeButton.Text = "Rebuild tree";
            toolTip.SetToolTip( rebuildTreeButton, "Ctrl+X; Ctrl+Z" );
            rebuildTreeButton.UseVisualStyleBackColor = true;
            rebuildTreeButton.Click += rebuildTreeButton_Click;
            // 
            // objRectCountNUD
            // 
            objRectCountNUD.Increment = new decimal( new int[] { 100, 0, 0, 0 } );
            objRectCountNUD.Location = new System.Drawing.Point( 216, 17 );
            objRectCountNUD.Maximum = new decimal( new int[] { 10000000, 0, 0, 0 } );
            objRectCountNUD.Minimum = new decimal( new int[] { 1, 0, 0, 0 } );
            objRectCountNUD.Name = "objRectCountNUD";
            objRectCountNUD.Size = new System.Drawing.Size( 75, 23 );
            objRectCountNUD.TabIndex = 4;
            objRectCountNUD.ThousandsSeparator = true;
            objRectCountNUD.Value = new decimal( new int[] { 500, 0, 0, 0 } );
            // 
            // gb1
            // 
            gb1.Controls.Add( selectFigureComboBox );
            gb1.Controls.Add( l8 );
            gb1.Controls.Add( l7 );
            gb1.Controls.Add( selectColorComboBox );
            gb1.Controls.Add( fillNodeListCheckBox );
            gb1.Controls.Add( drawBboxsCheckBox );
            gb1.Controls.Add( darwTextsCheckBox );
            gb1.Location = new System.Drawing.Point( 3, 1 );
            gb1.Name = "gb1";
            gb1.Size = new System.Drawing.Size( 241, 108 );
            gb1.TabIndex = 5;
            gb1.TabStop = false;
            gb1.Text = "Draw";
            // 
            // selectFigureComboBox
            // 
            selectFigureComboBox.Location = new System.Drawing.Point( 127, 80 );
            selectFigureComboBox.Name = "selectFigureComboBox";
            selectFigureComboBox.Size = new System.Drawing.Size( 108, 23 );
            selectFigureComboBox.TabIndex = 6;
            selectFigureComboBox.SelectedIndexChanged += selectFigureComboBox_SelectedIndexChanged;
            // 
            // l8
            // 
            l8.AutoSize = true;
            l8.Location = new System.Drawing.Point( 126, 62 );
            l8.Name = "l8";
            l8.Size = new System.Drawing.Size( 75, 15 );
            l8.TabIndex = 5;
            l8.Text = "Select figure:";
            // 
            // l7
            // 
            l7.AutoSize = true;
            l7.Location = new System.Drawing.Point( 126, 15 );
            l7.Name = "l7";
            l7.Size = new System.Drawing.Size( 71, 15 );
            l7.TabIndex = 4;
            l7.Text = "Select color:";
            // 
            // selectColorComboBox
            // 
            selectColorComboBox.Location = new System.Drawing.Point( 126, 33 );
            selectColorComboBox.Name = "selectColorComboBox";
            selectColorComboBox.Size = new System.Drawing.Size( 109, 23 );
            selectColorComboBox.TabIndex = 3;
            selectColorComboBox.SelectedIndexChanged += selectColorComboBox_SelectedIndexChanged;
            // 
            // fillNodeListCheckBox
            // 
            fillNodeListCheckBox.AutoSize = true;
            fillNodeListCheckBox.Location = new System.Drawing.Point( 13, 66 );
            fillNodeListCheckBox.Name = "fillNodeListCheckBox";
            fillNodeListCheckBox.Size = new System.Drawing.Size( 97, 19 );
            fillNodeListCheckBox.TabIndex = 2;
            fillNodeListCheckBox.Text = "Fill node's list";
            fillNodeListCheckBox.UseVisualStyleBackColor = true;
            fillNodeListCheckBox.CheckStateChanged += fillNodeListCheckBox_CheckStateChanged;
            // 
            // drawBboxsCheckBox
            // 
            drawBboxsCheckBox.AutoSize = true;
            drawBboxsCheckBox.Location = new System.Drawing.Point( 13, 16 );
            drawBboxsCheckBox.Name = "drawBboxsCheckBox";
            drawBboxsCheckBox.Size = new System.Drawing.Size( 91, 19 );
            drawBboxsCheckBox.TabIndex = 0;
            drawBboxsCheckBox.Text = "Draw bbox's";
            drawBboxsCheckBox.UseVisualStyleBackColor = true;
            drawBboxsCheckBox.CheckedChanged += drawMode_Changed;
            // 
            // darwTextsCheckBox
            // 
            darwTextsCheckBox.AutoSize = true;
            darwTextsCheckBox.Location = new System.Drawing.Point( 13, 41 );
            darwTextsCheckBox.Name = "darwTextsCheckBox";
            darwTextsCheckBox.Size = new System.Drawing.Size( 84, 19 );
            darwTextsCheckBox.TabIndex = 1;
            darwTextsCheckBox.Text = "Darw text's";
            darwTextsCheckBox.UseVisualStyleBackColor = true;
            darwTextsCheckBox.CheckStateChanged += drawMode_Changed;
            // 
            // rtreeCanvas
            // 
            rtreeCanvas.AutoScroll = true;
            rtreeCanvas.AutoScrollMinSize = new System.Drawing.Size( 100, 100 );
            rtreeCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            rtreeCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            rtreeCanvas.Location = new System.Drawing.Point( 0, 0 );
            rtreeCanvas.Name = "rtreeCanvas";
            rtreeCanvas.Size = new System.Drawing.Size( 1241, 461 );
            rtreeCanvas.TabIndex = 0;
            //
            // rtTreeView
            //
            rtTreeView.BackColor = System.Drawing.Color.FromArgb( 251, 251, 251 );
            rtTreeView.ContextMenuStrip = rtListBoxContextMenu;
            rtTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            rtTreeView.Location = new System.Drawing.Point( 0, 0 );
            rtTreeView.Name = "rtTreeView";
            rtTreeView.Size = new System.Drawing.Size( 166, 581 );
            rtTreeView.TabIndex = 1;
            rtTreeView.CheckBoxes = true;
            rtTreeView.HideSelection = false;
            rtTreeView.AfterCheck += rtTreeView_AfterCheck;
            rtTreeView.AfterSelect += rtTreeView_AfterSelect;
            rtTreeView.MouseUp += rtTreeView_MouseUp;
            // 
            // rtListBoxContextMenu
            // 
            rtListBoxContextMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] { checkAllMenuItem, unckeckAllMenuItem } );
            rtListBoxContextMenu.Name = "rtListBoxContextMenu";
            rtListBoxContextMenu.Size = new System.Drawing.Size( 135, 48 );
            // 
            // checkAllMenuItem
            // 
            checkAllMenuItem.Name = "checkAllMenuItem";
            checkAllMenuItem.Size = new System.Drawing.Size( 134, 22 );
            checkAllMenuItem.Text = "Check all";
            checkAllMenuItem.Click += checkAllMenuItem_Click;
            // 
            // unckeckAllMenuItem
            // 
            unckeckAllMenuItem.Name = "unckeckAllMenuItem";
            unckeckAllMenuItem.Size = new System.Drawing.Size( 134, 22 );
            unckeckAllMenuItem.Text = "Unckeck all";
            unckeckAllMenuItem.Click += unckeckAllMenuItem_Click;
            // 
            // mainSplitContainer
            // 
            mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            mainSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            mainSplitContainer.Name = "mainSplitContainer";
            //---mainSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(mainSplitContainer_SplitterMoved);
            // 
            // mainSplitContainer.Panel1
            // 
            //mainSplitContainer.Panel1.Controls.Add( rtListBox );
            mainSplitContainer.Panel1.Controls.Add( rtTreeView );
            // 
            // mainSplitContainer.Panel2
            // 
            mainSplitContainer.Panel2.Controls.Add( canvasSplitContainer );
            mainSplitContainer.Size = new System.Drawing.Size( 1411, 581 );
            mainSplitContainer.SplitterDistance = 166;
            mainSplitContainer.TabIndex = 5;
            // 
            // canvasSplitContainer
            // 
            canvasSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            canvasSplitContainer.Location = new System.Drawing.Point( 0, 0 );
            canvasSplitContainer.Name = "canvasSplitContainer";
            canvasSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            //---canvasSplitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(canvasSplitContainer_SplitterMoved);
            // 
            // canvasSplitContainer.Panel1
            // 
            canvasSplitContainer.Panel1.Controls.Add( rtreeCanvas );
            // 
            // canvasSplitContainer.Panel2
            // 
            canvasSplitContainer.Panel2.Controls.Add( ctrlPanel );
            canvasSplitContainer.Size = new System.Drawing.Size( 1241, 581 );
            canvasSplitContainer.SplitterDistance = 461;
            canvasSplitContainer.TabIndex = 3;
            // 
            // repeatSearchButton
            // 
            repeatSearchButton.AutoEllipsis = true;
            repeatSearchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            repeatSearchButton.Location = new System.Drawing.Point( 523, 15 );
            repeatSearchButton.Name = "repeatSearchButton";
            repeatSearchButton.Size = new System.Drawing.Size( 88, 44 );
            repeatSearchButton.TabIndex = 14;
            repeatSearchButton.Text = "Repeat search";
            repeatSearchButton.UseVisualStyleBackColor = true;
            repeatSearchButton.Visible = false;
            repeatSearchButton.Click += repeatSearchButton_Click;
            // 
            // RTreeForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF( 7F, 15F );
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size( 1411, 581 );
            Controls.Add( mainSplitContainer );
            KeyPreview = true;
            Name = "RTreeForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "RTree";
            ctrlPanel.ResumeLayout( false );
            gb2.ResumeLayout( false );
            gb2.PerformLayout();
             maxEntriesNUD.EndInit();
             maxWH_NUD.EndInit();
             minWH_NUD.EndInit();
             paddingY_NUD.EndInit();
             paddingX_NUD.EndInit();
             objRectCountNUD.EndInit();
            gb1.ResumeLayout( false );
            gb1.PerformLayout();
            rtListBoxContextMenu.ResumeLayout( false );
            mainSplitContainer.Panel1.ResumeLayout( false );
            mainSplitContainer.Panel2.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize) mainSplitContainer).EndInit();
            mainSplitContainer.ResumeLayout( false );
            canvasSplitContainer.Panel1.ResumeLayout( false );
            canvasSplitContainer.Panel2.ResumeLayout( false );
            ((System.ComponentModel.ISupportInitialize) canvasSplitContainer).EndInit();
            canvasSplitContainer.ResumeLayout( false );
            ResumeLayout( false );
        }
        #endregion

        private System.Windows.Forms.TreeView rtTreeView;
        private trees.win_forms.RTreeCanvas rtreeCanvas;
        private System.Windows.Forms.ContextMenuStrip rtListBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem checkAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unckeckAllMenuItem;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.CheckBox darwTextsCheckBox;
        private System.Windows.Forms.CheckBox drawBboxsCheckBox;
        private System.Windows.Forms.Button rebuildTreeButton;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer canvasSplitContainer;
        private System.Windows.Forms.NumericUpDownEx objRectCountNUD;
        private System.Windows.Forms.CheckBox fillNodeListCheckBox;
        private System.Windows.Forms.NumericUpDownEx maxEntriesNUD;
        private System.Windows.Forms.NumericUpDownEx paddingY_NUD;
        private System.Windows.Forms.NumericUpDownEx paddingX_NUD;
        private System.Windows.Forms.NumericUpDownEx maxWH_NUD;
        private System.Windows.Forms.NumericUpDownEx minWH_NUD;
        private System.Windows.Forms.ComboBoxColor selectColorComboBox;
        private System.Windows.Forms.ComboBoxFigure selectFigureComboBox;
        private System.Windows.Forms.Button repeatSearchButton;
    }
}