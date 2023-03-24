
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel ctrlPanel;
            System.Windows.Forms.GroupBox gb3;
            System.Windows.Forms.Label l2;
            System.Windows.Forms.GroupBox gb2;
            System.Windows.Forms.Label l6;
            System.Windows.Forms.Label l5;
            System.Windows.Forms.Label l4;
            System.Windows.Forms.Label l3;
            System.Windows.Forms.Label l1;
            System.Windows.Forms.GroupBox gb1;
            System.Windows.Forms.Label l7;
            System.Windows.Forms.Label l8;
            this.maxEntries_1_NUD = new System.Windows.Forms.NumericUpDownEx();
            this.rebuildTree_1_Button = new System.Windows.Forms.Button();
            this.maxWH_NUD = new System.Windows.Forms.NumericUpDownEx();
            this.minWH_NUD = new System.Windows.Forms.NumericUpDownEx();
            this.paddingY_NUD = new System.Windows.Forms.NumericUpDownEx();
            this.paddingX_NUD = new System.Windows.Forms.NumericUpDownEx();
            this.rebuildTree_2_Button = new System.Windows.Forms.Button();
            this.objRectCountNUD = new System.Windows.Forms.NumericUpDownEx();
            this.selectColorComboBox = new System.Windows.Forms.ComboBoxColor();
            this.fillNodeListCheckBox = new System.Windows.Forms.CheckBox();
            this.drawBboxsCheckBox = new System.Windows.Forms.CheckBox();
            this.darwTextsCheckBox = new System.Windows.Forms.CheckBox();
            this.rtreeCanvas = new trees.win_forms.RTreeCanvas();
            this.rtListBox = new System.Windows.Forms.CheckedListBox();
            this.rtListBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unckeckAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.canvasSplitContainer = new System.Windows.Forms.SplitContainer();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.selectFigureComboBox = new System.Windows.Forms.ComboBoxFigure();
            ctrlPanel = new System.Windows.Forms.Panel();
            gb3 = new System.Windows.Forms.GroupBox();
            l2 = new System.Windows.Forms.Label();
            gb2 = new System.Windows.Forms.GroupBox();
            l6 = new System.Windows.Forms.Label();
            l5 = new System.Windows.Forms.Label();
            l4 = new System.Windows.Forms.Label();
            l3 = new System.Windows.Forms.Label();
            l1 = new System.Windows.Forms.Label();
            gb1 = new System.Windows.Forms.GroupBox();
            l7 = new System.Windows.Forms.Label();
            l8 = new System.Windows.Forms.Label();
            ctrlPanel.SuspendLayout();
            gb3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxEntries_1_NUD)).BeginInit();
            gb2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxWH_NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWH_NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingY_NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingX_NUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objRectCountNUD)).BeginInit();
            gb1.SuspendLayout();
            this.rtListBoxContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasSplitContainer)).BeginInit();
            this.canvasSplitContainer.Panel1.SuspendLayout();
            this.canvasSplitContainer.Panel2.SuspendLayout();
            this.canvasSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPanel
            // 
            ctrlPanel.AutoScroll = true;
            ctrlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ctrlPanel.Controls.Add(gb3);
            ctrlPanel.Controls.Add(gb2);
            ctrlPanel.Controls.Add(gb1);
            ctrlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ctrlPanel.Location = new System.Drawing.Point(0, 0);
            ctrlPanel.Name = "ctrlPanel";
            ctrlPanel.Size = new System.Drawing.Size(1241, 116);
            ctrlPanel.TabIndex = 2;
            // 
            // gb3
            // 
            gb3.Controls.Add(this.maxEntries_1_NUD);
            gb3.Controls.Add(l2);
            gb3.Controls.Add(this.rebuildTree_1_Button);
            gb3.Location = new System.Drawing.Point(250, 1);
            gb3.Name = "gb3";
            gb3.Size = new System.Drawing.Size(121, 108);
            gb3.TabIndex = 7;
            gb3.TabStop = false;
            // 
            // maxEntries_1_NUD
            // 
            this.maxEntries_1_NUD.Location = new System.Drawing.Point(41, 83);
            this.maxEntries_1_NUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.maxEntries_1_NUD.Name = "maxEntries_1_NUD";
            this.maxEntries_1_NUD.Size = new System.Drawing.Size(74, 23);
            this.maxEntries_1_NUD.TabIndex = 4;
            this.maxEntries_1_NUD.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // l2
            // 
            l2.AutoSize = true;
            l2.Location = new System.Drawing.Point(6, 66);
            l2.Name = "l2";
            l2.Size = new System.Drawing.Size(71, 15);
            l2.TabIndex = 3;
            l2.Text = "Max Entries:";
            // 
            // rebuildTree_1_Button
            // 
            this.rebuildTree_1_Button.AutoEllipsis = true;
            this.rebuildTree_1_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rebuildTree_1_Button.Location = new System.Drawing.Point(6, 16);
            this.rebuildTree_1_Button.Name = "rebuildTree_1_Button";
            this.rebuildTree_1_Button.Size = new System.Drawing.Size(84, 44);
            this.rebuildTree_1_Button.TabIndex = 2;
            this.rebuildTree_1_Button.Text = "Rebuild tree (Standard)";
            this.rebuildTree_1_Button.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip.SetToolTip(this.rebuildTree_1_Button, "Ctrl+Z");
            this.rebuildTree_1_Button.UseVisualStyleBackColor = true;
            this.rebuildTree_1_Button.Click += new System.EventHandler(this.rebuildTree_1_Button_Click);
            // 
            // gb2
            // 
            gb2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            gb2.Controls.Add(this.maxWH_NUD);
            gb2.Controls.Add(this.minWH_NUD);
            gb2.Controls.Add(l6);
            gb2.Controls.Add(l5);
            gb2.Controls.Add(this.paddingY_NUD);
            gb2.Controls.Add(this.paddingX_NUD);
            gb2.Controls.Add(l4);
            gb2.Controls.Add(l3);
            gb2.Controls.Add(l1);
            gb2.Controls.Add(this.rebuildTree_2_Button);
            gb2.Controls.Add(this.objRectCountNUD);
            gb2.Location = new System.Drawing.Point(377, 1);
            gb2.Name = "gb2";
            gb2.Size = new System.Drawing.Size(857, 108);
            gb2.TabIndex = 6;
            gb2.TabStop = false;
            // 
            // maxWH_NUD
            // 
            this.maxWH_NUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxWH_NUD.Location = new System.Drawing.Point(440, 46);
            this.maxWH_NUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.maxWH_NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxWH_NUD.Name = "maxWH_NUD";
            this.maxWH_NUD.Size = new System.Drawing.Size(63, 23);
            this.maxWH_NUD.TabIndex = 13;
            this.maxWH_NUD.ThousandsSeparator = true;
            this.maxWH_NUD.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // minWH_NUD
            // 
            this.minWH_NUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.minWH_NUD.Location = new System.Drawing.Point(440, 17);
            this.minWH_NUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.minWH_NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minWH_NUD.Name = "minWH_NUD";
            this.minWH_NUD.Size = new System.Drawing.Size(63, 23);
            this.minWH_NUD.TabIndex = 12;
            this.minWH_NUD.ThousandsSeparator = true;
            this.minWH_NUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // l6
            // 
            l6.AutoSize = true;
            l6.Location = new System.Drawing.Point(315, 48);
            l6.Name = "l6";
            l6.Size = new System.Drawing.Size(125, 15);
            l6.TabIndex = 11;
            l6.Text = "Max obj width/height:";
            // 
            // l5
            // 
            l5.AutoSize = true;
            l5.Location = new System.Drawing.Point(315, 19);
            l5.Name = "l5";
            l5.Size = new System.Drawing.Size(123, 15);
            l5.TabIndex = 10;
            l5.Text = "Min obj width/height:";
            // 
            // paddingY_NUD
            // 
            this.paddingY_NUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.paddingY_NUD.Location = new System.Drawing.Point(216, 75);
            this.paddingY_NUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.paddingY_NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.paddingY_NUD.Name = "paddingY_NUD";
            this.paddingY_NUD.Size = new System.Drawing.Size(63, 23);
            this.paddingY_NUD.TabIndex = 9;
            this.paddingY_NUD.ThousandsSeparator = true;
            this.paddingY_NUD.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // paddingX_NUD
            // 
            this.paddingX_NUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.paddingX_NUD.Location = new System.Drawing.Point(216, 46);
            this.paddingX_NUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.paddingX_NUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.paddingX_NUD.Name = "paddingX_NUD";
            this.paddingX_NUD.Size = new System.Drawing.Size(63, 23);
            this.paddingX_NUD.TabIndex = 8;
            this.paddingX_NUD.ThousandsSeparator = true;
            this.paddingX_NUD.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            // 
            // l4
            // 
            l4.AutoSize = true;
            l4.Location = new System.Drawing.Point(146, 77);
            l4.Name = "l4";
            l4.Size = new System.Drawing.Size(64, 15);
            l4.TabIndex = 7;
            l4.Text = "Padding Y:";
            // 
            // l3
            // 
            l3.AutoSize = true;
            l3.Location = new System.Drawing.Point(146, 48);
            l3.Name = "l3";
            l3.Size = new System.Drawing.Size(64, 15);
            l3.TabIndex = 6;
            l3.Text = "Padding X:";
            // 
            // l1
            // 
            l1.AutoSize = true;
            l1.Location = new System.Drawing.Point(119, 19);
            l1.Name = "l1";
            l1.Size = new System.Drawing.Size(91, 15);
            l1.TabIndex = 5;
            l1.Text = "Obj/Rect count:";
            // 
            // rebuildTree_2_Button
            // 
            this.rebuildTree_2_Button.AutoEllipsis = true;
            this.rebuildTree_2_Button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rebuildTree_2_Button.Location = new System.Drawing.Point(6, 16);
            this.rebuildTree_2_Button.Name = "rebuildTree_2_Button";
            this.rebuildTree_2_Button.Size = new System.Drawing.Size(88, 44);
            this.rebuildTree_2_Button.TabIndex = 3;
            this.rebuildTree_2_Button.Text = "Rebuild tree (Variable)";
            this.toolTip.SetToolTip(this.rebuildTree_2_Button, "Ctrl+X");
            this.rebuildTree_2_Button.UseVisualStyleBackColor = true;
            this.rebuildTree_2_Button.Click += new System.EventHandler(this.rebuildTree_2_Button_Click);
            // 
            // objRectCountNUD
            // 
            this.objRectCountNUD.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.objRectCountNUD.Location = new System.Drawing.Point(216, 17);
            this.objRectCountNUD.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.objRectCountNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objRectCountNUD.Name = "objRectCountNUD";
            this.objRectCountNUD.Size = new System.Drawing.Size(75, 23);
            this.objRectCountNUD.TabIndex = 4;
            this.objRectCountNUD.ThousandsSeparator = true;
            this.objRectCountNUD.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // gb1
            // 
            gb1.Controls.Add(this.selectFigureComboBox);
            gb1.Controls.Add(l8);
            gb1.Controls.Add(l7);
            gb1.Controls.Add(this.selectColorComboBox);
            gb1.Controls.Add(this.fillNodeListCheckBox);
            gb1.Controls.Add(this.drawBboxsCheckBox);
            gb1.Controls.Add(this.darwTextsCheckBox);
            gb1.Location = new System.Drawing.Point(3, 1);
            gb1.Name = "gb1";
            gb1.Size = new System.Drawing.Size(241, 108);
            gb1.TabIndex = 5;
            gb1.TabStop = false;
            gb1.Text = "Draw";
            // 
            // l7
            // 
            l7.AutoSize = true;
            l7.Location = new System.Drawing.Point(126, 15);
            l7.Name = "l7";
            l7.Size = new System.Drawing.Size(71, 15);
            l7.TabIndex = 4;
            l7.Text = "Select color:";
            // 
            // selectColorComboBox
            // 
            this.selectColorComboBox.Location = new System.Drawing.Point(126, 33);
            this.selectColorComboBox.Name = "selectColorComboBox";
            this.selectColorComboBox.Size = new System.Drawing.Size(109, 23);
            this.selectColorComboBox.TabIndex = 3;
            this.selectColorComboBox.SelectedIndexChanged += new System.EventHandler(this.selectColorComboBox_SelectedIndexChanged);
            // 
            // fillNodeListCheckBox
            // 
            this.fillNodeListCheckBox.AutoSize = true;
            this.fillNodeListCheckBox.Location = new System.Drawing.Point(13, 66);
            this.fillNodeListCheckBox.Name = "fillNodeListCheckBox";
            this.fillNodeListCheckBox.Size = new System.Drawing.Size(97, 19);
            this.fillNodeListCheckBox.TabIndex = 2;
            this.fillNodeListCheckBox.Text = "Fill node\'s list";
            this.fillNodeListCheckBox.UseVisualStyleBackColor = true;
            this.fillNodeListCheckBox.CheckStateChanged += new System.EventHandler(this.fillNodeListCheckBox_CheckStateChanged);
            // 
            // drawBboxsCheckBox
            // 
            this.drawBboxsCheckBox.AutoSize = true;
            this.drawBboxsCheckBox.Location = new System.Drawing.Point(13, 16);
            this.drawBboxsCheckBox.Name = "drawBboxsCheckBox";
            this.drawBboxsCheckBox.Size = new System.Drawing.Size(91, 19);
            this.drawBboxsCheckBox.TabIndex = 0;
            this.drawBboxsCheckBox.Text = "Draw bbox\'s";
            this.drawBboxsCheckBox.UseVisualStyleBackColor = true;
            this.drawBboxsCheckBox.CheckedChanged += new System.EventHandler(this.drawMode_Changed);
            // 
            // darwTextsCheckBox
            // 
            this.darwTextsCheckBox.AutoSize = true;
            this.darwTextsCheckBox.Location = new System.Drawing.Point(13, 41);
            this.darwTextsCheckBox.Name = "darwTextsCheckBox";
            this.darwTextsCheckBox.Size = new System.Drawing.Size(84, 19);
            this.darwTextsCheckBox.TabIndex = 1;
            this.darwTextsCheckBox.Text = "Darw text\'s";
            this.darwTextsCheckBox.UseVisualStyleBackColor = true;
            this.darwTextsCheckBox.CheckStateChanged += new System.EventHandler(this.drawMode_Changed);
            // 
            // rtreeCanvas
            // 
            this.rtreeCanvas.AutoScroll = true;
            this.rtreeCanvas.AutoScrollMinSize = new System.Drawing.Size(100, 100);
            this.rtreeCanvas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rtreeCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtreeCanvas.Location = new System.Drawing.Point(0, 0);
            this.rtreeCanvas.Name = "rtreeCanvas";
            this.rtreeCanvas.Size = new System.Drawing.Size(1241, 461);
            this.rtreeCanvas.TabIndex = 0;
            // 
            // rtListBox
            // 
            this.rtListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(251)))), ((int)(((byte)(251)))));
            this.rtListBox.ContextMenuStrip = this.rtListBoxContextMenu;
            this.rtListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtListBox.FormattingEnabled = true;
            this.rtListBox.HorizontalScrollbar = true;
            this.rtListBox.Location = new System.Drawing.Point(0, 0);
            this.rtListBox.Name = "rtListBox";
            this.rtListBox.Size = new System.Drawing.Size(166, 581);
            this.rtListBox.TabIndex = 1;
            this.rtListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.rtListBox_ItemCheck);
            this.rtListBox.SelectedIndexChanged += new System.EventHandler(this.rtListBox_SelectedIndexChanged);
            // 
            // rtListBoxContextMenu
            // 
            this.rtListBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkAllMenuItem,
            this.unckeckAllMenuItem});
            this.rtListBoxContextMenu.Name = "rtListBoxContextMenu";
            this.rtListBoxContextMenu.Size = new System.Drawing.Size(135, 48);
            // 
            // checkAllMenuItem
            // 
            this.checkAllMenuItem.Name = "checkAllMenuItem";
            this.checkAllMenuItem.Size = new System.Drawing.Size(134, 22);
            this.checkAllMenuItem.Text = "Check all";
            this.checkAllMenuItem.Click += new System.EventHandler(this.checkAllMenuItem_Click);
            // 
            // unckeckAllMenuItem
            // 
            this.unckeckAllMenuItem.Name = "unckeckAllMenuItem";
            this.unckeckAllMenuItem.Size = new System.Drawing.Size(134, 22);
            this.unckeckAllMenuItem.Text = "Unckeck all";
            this.unckeckAllMenuItem.Click += new System.EventHandler(this.unckeckAllMenuItem_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.rtListBox);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.canvasSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1411, 581);
            this.mainSplitContainer.SplitterDistance = 166;
            this.mainSplitContainer.TabIndex = 5;
            // 
            // canvasSplitContainer
            // 
            this.canvasSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvasSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.canvasSplitContainer.Name = "canvasSplitContainer";
            this.canvasSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // canvasSplitContainer.Panel1
            // 
            this.canvasSplitContainer.Panel1.Controls.Add(this.rtreeCanvas);
            // 
            // canvasSplitContainer.Panel2
            // 
            this.canvasSplitContainer.Panel2.Controls.Add(ctrlPanel);
            this.canvasSplitContainer.Size = new System.Drawing.Size(1241, 581);
            this.canvasSplitContainer.SplitterDistance = 461;
            this.canvasSplitContainer.TabIndex = 3;
            // 
            // l8
            // 
            l8.AutoSize = true;
            l8.Location = new System.Drawing.Point(126, 62);
            l8.Name = "l8";
            l8.Size = new System.Drawing.Size(75, 15);
            l8.TabIndex = 5;
            l8.Text = "Select figure:";
            // 
            // selectFigureComboBox
            // 
            this.selectFigureComboBox.Location = new System.Drawing.Point(127, 80);
            this.selectFigureComboBox.Name = "selectFigureComboBox";
            this.selectFigureComboBox.Size = new System.Drawing.Size(108, 23);
            this.selectFigureComboBox.TabIndex = 6;
            this.selectFigureComboBox.SelectedIndexChanged += new System.EventHandler(this.selectFigureComboBox_SelectedIndexChanged);
            // 
            // RTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1411, 581);
            this.Controls.Add(this.mainSplitContainer);
            this.KeyPreview = true;
            this.Name = "RTreeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTree";
            ctrlPanel.ResumeLayout(false);
            gb3.ResumeLayout(false);
            gb3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxEntries_1_NUD)).EndInit();
            gb2.ResumeLayout(false);
            gb2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxWH_NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minWH_NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingY_NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paddingX_NUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objRectCountNUD)).EndInit();
            gb1.ResumeLayout(false);
            gb1.PerformLayout();
            this.rtListBoxContextMenu.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.canvasSplitContainer.Panel1.ResumeLayout(false);
            this.canvasSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvasSplitContainer)).EndInit();
            this.canvasSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.CheckedListBox rtListBox;
        private trees.win_forms.RTreeCanvas rtreeCanvas;
        private System.Windows.Forms.ContextMenuStrip rtListBoxContextMenu;
        private System.Windows.Forms.ToolStripMenuItem checkAllMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unckeckAllMenuItem;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.CheckBox darwTextsCheckBox;
        private System.Windows.Forms.CheckBox drawBboxsCheckBox;
        private System.Windows.Forms.Button rebuildTree_1_Button;
        private System.Windows.Forms.Button rebuildTree_2_Button;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.SplitContainer canvasSplitContainer;
        private System.Windows.Forms.NumericUpDownEx objRectCountNUD;
        private System.Windows.Forms.CheckBox fillNodeListCheckBox;
        private System.Windows.Forms.NumericUpDownEx maxEntries_1_NUD;
        private System.Windows.Forms.NumericUpDownEx paddingY_NUD;
        private System.Windows.Forms.NumericUpDownEx paddingX_NUD;
        private System.Windows.Forms.NumericUpDownEx maxWH_NUD;
        private System.Windows.Forms.NumericUpDownEx minWH_NUD;
        private System.Windows.Forms.ComboBoxColor selectColorComboBox;
        private System.Windows.Forms.ComboBoxFigure selectFigureComboBox;
    }
}