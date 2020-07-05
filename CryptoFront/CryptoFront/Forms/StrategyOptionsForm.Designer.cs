namespace CryptoFront.Forms
{
    partial class StrategyOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StrategyOptionsForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtFees = new DevExpress.XtraEditors.SpinEdit();
            this.txtMaxOpenOrders = new DevExpress.XtraEditors.SpinEdit();
            this.txtMinsInbetweenOrders = new DevExpress.XtraEditors.SpinEdit();
            this.txtBalance = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFees.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxOpenOrders.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinsInbetweenOrders.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.txtFees);
            this.groupControl1.Controls.Add(this.txtMaxOpenOrders);
            this.groupControl1.Controls.Add(this.txtMinsInbetweenOrders);
            this.groupControl1.Controls.Add(this.txtBalance);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(359, 226);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Strategy Options";
            // 
            // txtFees
            // 
            this.txtFees.EditValue = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.txtFees.Location = new System.Drawing.Point(212, 163);
            this.txtFees.Name = "txtFees";
            this.txtFees.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtFees.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtFees.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtFees.Size = new System.Drawing.Size(100, 20);
            this.txtFees.TabIndex = 7;
            // 
            // txtMaxOpenOrders
            // 
            this.txtMaxOpenOrders.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtMaxOpenOrders.Location = new System.Drawing.Point(212, 119);
            this.txtMaxOpenOrders.Name = "txtMaxOpenOrders";
            this.txtMaxOpenOrders.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMaxOpenOrders.Properties.IsFloatValue = false;
            this.txtMaxOpenOrders.Properties.Mask.EditMask = "N00";
            this.txtMaxOpenOrders.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtMaxOpenOrders.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMaxOpenOrders.Size = new System.Drawing.Size(100, 20);
            this.txtMaxOpenOrders.TabIndex = 6;
            // 
            // txtMinsInbetweenOrders
            // 
            this.txtMinsInbetweenOrders.EditValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.txtMinsInbetweenOrders.Location = new System.Drawing.Point(212, 81);
            this.txtMinsInbetweenOrders.Name = "txtMinsInbetweenOrders";
            this.txtMinsInbetweenOrders.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtMinsInbetweenOrders.Properties.IsFloatValue = false;
            this.txtMinsInbetweenOrders.Properties.Mask.EditMask = "N00";
            this.txtMinsInbetweenOrders.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtMinsInbetweenOrders.Size = new System.Drawing.Size(100, 20);
            this.txtMinsInbetweenOrders.TabIndex = 5;
            // 
            // txtBalance
            // 
            this.txtBalance.EditValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtBalance.Location = new System.Drawing.Point(212, 43);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtBalance.Properties.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.txtBalance.Properties.IsFloatValue = false;
            this.txtBalance.Properties.Mask.EditMask = "N00";
            this.txtBalance.Properties.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.txtBalance.Properties.MinValue = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.txtBalance.Size = new System.Drawing.Size(100, 20);
            this.txtBalance.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(25, 166);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(125, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Fees per transaction (%):";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(25, 122);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(85, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "Max open orders:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(25, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(132, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Minutes in-between orders:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(99, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Starting Balance ($):";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(296, 258);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // StrategyOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 293);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StrategyOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Current Strategy Options";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFees.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaxOpenOrders.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMinsInbetweenOrders.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalance.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit txtMaxOpenOrders;
        private DevExpress.XtraEditors.SpinEdit txtMinsInbetweenOrders;
        private DevExpress.XtraEditors.SpinEdit txtBalance;
        private DevExpress.XtraEditors.SpinEdit txtFees;
    }
}