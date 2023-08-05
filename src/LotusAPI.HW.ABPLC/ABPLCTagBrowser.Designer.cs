namespace LotusAPI.HW {
    partial class ABPLCTagBrowser {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv = new Abeo.Controls.FlatDataGridView();
            this.flatPanel1 = new Abeo.Controls.FlatPanel();
            this.bt_ReadAllTags = new Abeo.Controls.FlatButton();
            this.tb_Filter = new Abeo.Controls.Roundable.RoundTextbox();
            this.flatLabel1 = new Abeo.Controls.FlatLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.flatPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.dgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dgv.CellColorMode = true;
            this.dgv.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgv.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(88)))), ((int)(((byte)(99)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Consolas", 14F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.DateTimeFormat = "yyyy/MM/dd HH:mm:ss";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Consolas", 14F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv.EnableHeadersVisualStyles = false;
            this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.dgv.Location = new System.Drawing.Point(0, 32);
            this.dgv.Margin = new System.Windows.Forms.Padding(0);
            this.dgv.MultiSelect = false;
            this.dgv.Name = "dgv";
            this.dgv.NumericFormat = "F3";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgv.RowHeadersVisible = false;
            this.dgv.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(55)))), ((int)(((byte)(66)))));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(212)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.dgv.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv.RowTemplate.ReadOnly = true;
            this.dgv.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(212)))));
            this.dgv.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(43)))), ((int)(((byte)(55)))));
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.ShowCellErrors = false;
            this.dgv.ShowCellToolTips = false;
            this.dgv.ShowEditingIcon = false;
            this.dgv.ShowRowErrors = false;
            this.dgv.Size = new System.Drawing.Size(956, 575);
            this.dgv.TabIndex = 0;
            this.dgv.TabStop = false;
            this.dgv.VirtualMode = true;
            // 
            // flatPanel1
            // 
            this.flatPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flatPanel1.Controls.Add(this.tb_Filter);
            this.flatPanel1.Controls.Add(this.flatLabel1);
            this.flatPanel1.Controls.Add(this.bt_ReadAllTags);
            this.flatPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flatPanel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flatPanel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.flatPanel1.Location = new System.Drawing.Point(0, 0);
            this.flatPanel1.Name = "flatPanel1";
            this.flatPanel1.Size = new System.Drawing.Size(956, 32);
            this.flatPanel1.TabIndex = 1;
            // 
            // bt_ReadAllTags
            // 
            this.bt_ReadAllTags.BackColor = System.Drawing.Color.Transparent;
            this.bt_ReadAllTags.BorderColor = System.Drawing.Color.Empty;
            this.bt_ReadAllTags.BorderSize = 0;
            this.bt_ReadAllTags.Dock = System.Windows.Forms.DockStyle.Left;
            this.bt_ReadAllTags.FlatAppearance.BorderSize = 0;
            this.bt_ReadAllTags.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.bt_ReadAllTags.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(212)))));
            this.bt_ReadAllTags.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_ReadAllTags.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.bt_ReadAllTags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(231)))), ((int)(((byte)(212)))));
            this.bt_ReadAllTags.Location = new System.Drawing.Point(0, 0);
            this.bt_ReadAllTags.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bt_ReadAllTags.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(44)))));
            this.bt_ReadAllTags.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(142)))), ((int)(((byte)(212)))));
            this.bt_ReadAllTags.Name = "bt_ReadAllTags";
            this.bt_ReadAllTags.Size = new System.Drawing.Size(136, 32);
            this.bt_ReadAllTags.TabIndex = 0;
            this.bt_ReadAllTags.Text = "Read all tags";
            this.bt_ReadAllTags.UseVisualStyleBackColor = false;
            this.bt_ReadAllTags.Click += new System.EventHandler(this.bt_ReadAllTags_Click);
            // 
            // tb_Filter
            // 
            this.tb_Filter.BackColor = System.Drawing.Color.Transparent;
            this.tb_Filter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(110)))), ((int)(((byte)(118)))));
            this.tb_Filter.BorderCorners = Abeo.Controls.Roundable.Corners.All;
            this.tb_Filter.BorderRadius = 3;
            this.tb_Filter.BorderThickness = 1;
            this.tb_Filter.ContentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_Filter.ContentBackColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(88)))));
            this.tb_Filter.ContentBackColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_Filter.ContentBackColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(110)))));
            this.tb_Filter.DebugMode = false;
            this.tb_Filter.Dock = System.Windows.Forms.DockStyle.Left;
            this.tb_Filter.EnforceContent = Abeo.Controls.Roundable.RoundTextbox.EnforceType.Text;
            this.tb_Filter.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.tb_Filter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_Filter.ForeColorChecked = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(44)))), ((int)(((byte)(55)))));
            this.tb_Filter.ForeColorMouseDown = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_Filter.ForeColorMouseOver = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(161)))), ((int)(((byte)(161)))));
            this.tb_Filter.Icon = null;
            this.tb_Filter.IconPortion = 0.3F;
            this.tb_Filter.IconSize = 24;
            this.tb_Filter.IconVisible = false;
            this.tb_Filter.Image = null;
            this.tb_Filter.ImageSizeMode = Abeo.Controls.Roundable.SizeMode.Stretch;
            this.tb_Filter.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.tb_Filter.Location = new System.Drawing.Point(206, 0);
            this.tb_Filter.Name = "tb_Filter";
            this.tb_Filter.Padding = new System.Windows.Forms.Padding(6);
            this.tb_Filter.PasswordChar = '\0';
            this.tb_Filter.ReadOnly = false;
            this.tb_Filter.Size = new System.Drawing.Size(321, 34);
            this.tb_Filter.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.tb_Filter.TabIndex = 1;
            this.tb_Filter.TagColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(66)))), ((int)(((byte)(129)))));
            this.tb_Filter.TagLocation = Abeo.Controls.Roundable.TagLocation.Left;
            this.tb_Filter.TagOffset = 5;
            this.tb_Filter.TagVisible = false;
            this.tb_Filter.TagWidth = 5;
            this.tb_Filter.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.tb_Filter.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tb_Filter.TextIconRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            // 
            // flatLabel1
            // 
            this.flatLabel1.BackColor = System.Drawing.Color.Transparent;
            this.flatLabel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flatLabel1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.flatLabel1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.flatLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(123)))), ((int)(((byte)(131)))));
            this.flatLabel1.Location = new System.Drawing.Point(136, 0);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(70, 32);
            this.flatLabel1.TabIndex = 2;
            this.flatLabel1.Text = "Filter";
            this.flatLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ABPLCTagBrowser
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(956, 607);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.flatPanel1);
            this.Font = new System.Drawing.Font("Consolas", 14F);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ABPLCTagBrowser";
            this.Text = "ABPLCTagBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.flatPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Abeo.Controls.FlatDataGridView dgv;
        private Abeo.Controls.FlatPanel flatPanel1;
        private Abeo.Controls.FlatButton bt_ReadAllTags;
        private Abeo.Controls.Roundable.RoundTextbox tb_Filter;
        private Abeo.Controls.FlatLabel flatLabel1;
    }
}