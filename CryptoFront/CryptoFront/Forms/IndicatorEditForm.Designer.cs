namespace CryptoFront.Forms
{
    partial class IndicatorEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IndicatorEditForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeProperties = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.repoInt = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repoFloat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repoColor = new DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit();
            this.comboInputValue = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblInputValue = new DevExpress.XtraEditors.LabelControl();
            this.comboIndicator = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.chkVisible = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoInt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoFloat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboInputValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboIndicator.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisible.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(47, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Indicator:";
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.treeProperties);
            this.groupControl1.Controls.Add(this.comboInputValue);
            this.groupControl1.Controls.Add(this.lblInputValue);
            this.groupControl1.Controls.Add(this.comboIndicator);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(303, 308);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Edit Indicator";
            // 
            // treeProperties
            // 
            this.treeProperties.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1,
            this.treeListColumn2});
            this.treeProperties.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeProperties.DataSource = null;
            this.treeProperties.Location = new System.Drawing.Point(21, 80);
            this.treeProperties.Name = "treeProperties";
            this.treeProperties.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeProperties.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.treeProperties.OptionsView.ShowIndicator = false;
            this.treeProperties.OptionsView.ShowRoot = false;
            this.treeProperties.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoInt,
            this.repoFloat,
            this.repoColor});
            this.treeProperties.Size = new System.Drawing.Size(259, 177);
            this.treeProperties.TabIndex = 10;
            this.treeProperties.CustomNodeCellEdit += new DevExpress.XtraTreeList.GetCustomNodeCellEditEventHandler(this.TreeProperties_CustomNodeCellEdit);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Property";
            this.treeListColumn1.FieldName = "Property";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.AllowEdit = false;
            this.treeListColumn1.OptionsColumn.ReadOnly = true;
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // treeListColumn2
            // 
            this.treeListColumn2.Caption = "Value";
            this.treeListColumn2.FieldName = "Value";
            this.treeListColumn2.Name = "treeListColumn2";
            this.treeListColumn2.Visible = true;
            this.treeListColumn2.VisibleIndex = 1;
            // 
            // repoInt
            // 
            this.repoInt.AutoHeight = false;
            this.repoInt.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoInt.IsFloatValue = false;
            this.repoInt.Mask.EditMask = "N00";
            this.repoInt.Name = "repoInt";
            // 
            // repoFloat
            // 
            this.repoFloat.AutoHeight = false;
            this.repoFloat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoFloat.Name = "repoFloat";
            // 
            // repoColor
            // 
            this.repoColor.AutoHeight = false;
            this.repoColor.AutomaticColor = System.Drawing.Color.Black;
            this.repoColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoColor.Name = "repoColor";
            // 
            // comboInputValue
            // 
            this.comboInputValue.EditValue = "Close";
            this.comboInputValue.Location = new System.Drawing.Point(92, 270);
            this.comboInputValue.Name = "comboInputValue";
            this.comboInputValue.Properties.AllowFocused = false;
            this.comboInputValue.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboInputValue.Properties.Items.AddRange(new object[] {
            "Open",
            "High",
            "Low",
            "Close"});
            this.comboInputValue.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboInputValue.Size = new System.Drawing.Size(186, 20);
            this.comboInputValue.TabIndex = 7;
            // 
            // lblInputValue
            // 
            this.lblInputValue.Location = new System.Drawing.Point(21, 273);
            this.lblInputValue.Name = "lblInputValue";
            this.lblInputValue.Size = new System.Drawing.Size(59, 13);
            this.lblInputValue.TabIndex = 6;
            this.lblInputValue.Text = "Input Value:";
            // 
            // comboIndicator
            // 
            this.comboIndicator.Location = new System.Drawing.Point(94, 39);
            this.comboIndicator.Name = "comboIndicator";
            this.comboIndicator.Properties.AllowFocused = false;
            this.comboIndicator.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboIndicator.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboIndicator.Size = new System.Drawing.Size(186, 20);
            this.comboIndicator.TabIndex = 1;
            this.comboIndicator.SelectedIndexChanged += new System.EventHandler(this.ComboIndicator_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(235, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkVisible
            // 
            this.chkVisible.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkVisible.Location = new System.Drawing.Point(19, 337);
            this.chkVisible.Name = "chkVisible";
            this.chkVisible.Properties.Caption = "Visible";
            this.chkVisible.Size = new System.Drawing.Size(75, 19);
            this.chkVisible.TabIndex = 6;
            // 
            // IndicatorEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 370);
            this.Controls.Add(this.chkVisible);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndicatorEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Indicator";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoInt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoFloat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboInputValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboIndicator.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVisible.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.ComboBoxEdit comboIndicator;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.ComboBoxEdit comboInputValue;
        private DevExpress.XtraEditors.LabelControl lblInputValue;
        private DevExpress.XtraEditors.CheckEdit chkVisible;
        private DevExpress.XtraTreeList.TreeList treeProperties;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoInt;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoFloat;
        private DevExpress.XtraEditors.Repository.RepositoryItemColorPickEdit repoColor;
    }
}