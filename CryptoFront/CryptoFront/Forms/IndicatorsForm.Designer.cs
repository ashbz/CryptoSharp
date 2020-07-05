namespace CryptoFront.Forms
{
    partial class IndicatorsForm
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
            this.treeIndicators = new DevExpress.XtraTreeList.TreeList();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.btnAddIndicator = new DevExpress.XtraEditors.SimpleButton();
            this.btnRemoveIndicator = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.treeIndicators)).BeginInit();
            this.SuspendLayout();
            // 
            // treeIndicators
            // 
            this.treeIndicators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeIndicators.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.treeListColumn1});
            this.treeIndicators.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeIndicators.DataSource = null;
            this.treeIndicators.Location = new System.Drawing.Point(12, 12);
            this.treeIndicators.Name = "treeIndicators";
            this.treeIndicators.OptionsBehavior.Editable = false;
            this.treeIndicators.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.treeIndicators.OptionsView.FocusRectStyle = DevExpress.XtraTreeList.DrawFocusRectStyle.None;
            this.treeIndicators.OptionsView.ShowIndicator = false;
            this.treeIndicators.OptionsView.ShowRoot = false;
            this.treeIndicators.Size = new System.Drawing.Size(352, 350);
            this.treeIndicators.TabIndex = 0;
            this.treeIndicators.CustomDrawNodeCell += new DevExpress.XtraTreeList.CustomDrawNodeCellEventHandler(this.TreeIndicators_CustomDrawNodeCell);
            this.treeIndicators.DoubleClick += new System.EventHandler(this.TreeIndicators_DoubleClick);
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "Indicator";
            this.treeListColumn1.FieldName = "Indicator";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.Visible = true;
            this.treeListColumn1.VisibleIndex = 0;
            // 
            // btnAddIndicator
            // 
            this.btnAddIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddIndicator.Location = new System.Drawing.Point(370, 12);
            this.btnAddIndicator.Name = "btnAddIndicator";
            this.btnAddIndicator.Size = new System.Drawing.Size(104, 23);
            this.btnAddIndicator.TabIndex = 1;
            this.btnAddIndicator.Text = "Add Indicator";
            this.btnAddIndicator.Click += new System.EventHandler(this.BtnAddIndicator_Click);
            // 
            // btnRemoveIndicator
            // 
            this.btnRemoveIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveIndicator.Location = new System.Drawing.Point(370, 41);
            this.btnRemoveIndicator.Name = "btnRemoveIndicator";
            this.btnRemoveIndicator.Size = new System.Drawing.Size(104, 23);
            this.btnRemoveIndicator.TabIndex = 2;
            this.btnRemoveIndicator.Text = "Remove Indicator";
            this.btnRemoveIndicator.Click += new System.EventHandler(this.BtnRemoveIndicator_Click);
            // 
            // IndicatorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 374);
            this.Controls.Add(this.btnRemoveIndicator);
            this.Controls.Add(this.btnAddIndicator);
            this.Controls.Add(this.treeIndicators);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IndicatorsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Indicators";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IndicatorsForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.treeIndicators)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList treeIndicators;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
        private DevExpress.XtraEditors.SimpleButton btnAddIndicator;
        private DevExpress.XtraEditors.SimpleButton btnRemoveIndicator;
    }
}