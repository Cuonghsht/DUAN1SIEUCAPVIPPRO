namespace DUAN1.Nhân_Viên
{
    partial class ViewMedicine
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewMedicine));
            this.label1 = new System.Windows.Forms.Label();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.TxtTimkiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.IdPro = new System.Windows.Forms.TextBox();
            this.NamePro = new System.Windows.Forms.TextBox();
            this.Date = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.CatePro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.UnitPro = new Guna.UI2.WinForms.Guna2ComboBox();
            this.ChiTiet = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.SLPro = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 39);
            this.label1.TabIndex = 2;
            this.label1.Text = "View Medicine";
            // 
            // guna2Button1
            // 
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.SystemColors.Control;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button1.ForeColor = System.Drawing.SystemColors.Control;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageSize = new System.Drawing.Size(30, 30);
            this.guna2Button1.Location = new System.Drawing.Point(257, 12);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(68, 78);
            this.guna2Button1.TabIndex = 4;
            this.guna2Button1.Text = "guna2Button1";
            // 
            // TxtTimkiem
            // 
            this.TxtTimkiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TxtTimkiem.DefaultText = "";
            this.TxtTimkiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.TxtTimkiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.TxtTimkiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtTimkiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.TxtTimkiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtTimkiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.TxtTimkiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.TxtTimkiem.Location = new System.Drawing.Point(1500, 104);
            this.TxtTimkiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TxtTimkiem.Name = "TxtTimkiem";
            this.TxtTimkiem.PasswordChar = '\0';
            this.TxtTimkiem.PlaceholderText = "Search.......";
            this.TxtTimkiem.SelectedText = "";
            this.TxtTimkiem.Size = new System.Drawing.Size(229, 34);
            this.TxtTimkiem.TabIndex = 8;
            this.TxtTimkiem.TextChanged += new System.EventHandler(this.TxtTimkiem_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1496, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Name medicine:";
            // 
            // guna2Button2
            // 
            this.guna2Button2.BorderRadius = 19;
            this.guna2Button2.BorderThickness = 1;
            this.guna2Button2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button2.ForeColor = System.Drawing.Color.White;
            this.guna2Button2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(225)))));
            this.guna2Button2.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.guna2Button2.HoverState.ForeColor = System.Drawing.Color.Black;
            this.guna2Button2.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button2.Image")));
            this.guna2Button2.Location = new System.Drawing.Point(1500, 297);
            this.guna2Button2.Name = "guna2Button2";
            this.guna2Button2.Size = new System.Drawing.Size(178, 72);
            this.guna2Button2.TabIndex = 22;
            this.guna2Button2.Text = "Remove";
            this.guna2Button2.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 223);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1376, 563);
            this.dataGridView1.TabIndex = 23;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dataGridView1_RowPrePaint);
            // 
            // IdPro
            // 
            this.IdPro.Location = new System.Drawing.Point(103, 68);
            this.IdPro.Name = "IdPro";
            this.IdPro.Size = new System.Drawing.Size(205, 22);
            this.IdPro.TabIndex = 24;
            // 
            // NamePro
            // 
            this.NamePro.Location = new System.Drawing.Point(103, 104);
            this.NamePro.Name = "NamePro";
            this.NamePro.Size = new System.Drawing.Size(205, 22);
            this.NamePro.TabIndex = 24;
            // 
            // Date
            // 
            this.Date.Checked = true;
            this.Date.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Date.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.Date.Location = new System.Drawing.Point(466, 48);
            this.Date.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.Date.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.Date.Name = "Date";
            this.Date.Size = new System.Drawing.Size(271, 42);
            this.Date.TabIndex = 25;
            this.Date.Value = new System.DateTime(2024, 7, 29, 15, 14, 31, 162);
            // 
            // CatePro
            // 
            this.CatePro.BackColor = System.Drawing.Color.Transparent;
            this.CatePro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.CatePro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CatePro.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CatePro.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.CatePro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.CatePro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.CatePro.ItemHeight = 30;
            this.CatePro.Location = new System.Drawing.Point(466, 110);
            this.CatePro.Name = "CatePro";
            this.CatePro.Size = new System.Drawing.Size(271, 36);
            this.CatePro.TabIndex = 26;
            // 
            // UnitPro
            // 
            this.UnitPro.BackColor = System.Drawing.Color.Transparent;
            this.UnitPro.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.UnitPro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UnitPro.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UnitPro.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.UnitPro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.UnitPro.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.UnitPro.ItemHeight = 30;
            this.UnitPro.Location = new System.Drawing.Point(466, 152);
            this.UnitPro.Name = "UnitPro";
            this.UnitPro.Size = new System.Drawing.Size(271, 36);
            this.UnitPro.TabIndex = 26;
            // 
            // ChiTiet
            // 
            this.ChiTiet.BorderRadius = 19;
            this.ChiTiet.BorderThickness = 1;
            this.ChiTiet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.ChiTiet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.ChiTiet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.ChiTiet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.ChiTiet.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ChiTiet.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChiTiet.ForeColor = System.Drawing.Color.White;
            this.ChiTiet.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(225)))));
            this.ChiTiet.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.ChiTiet.HoverState.ForeColor = System.Drawing.Color.Black;
            this.ChiTiet.Image = ((System.Drawing.Image)(resources.GetObject("ChiTiet.Image")));
            this.ChiTiet.Location = new System.Drawing.Point(1500, 405);
            this.ChiTiet.Name = "ChiTiet";
            this.ChiTiet.Size = new System.Drawing.Size(178, 72);
            this.ChiTiet.TabIndex = 22;
            this.ChiTiet.Text = "Thêm";
            this.ChiTiet.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // guna2Button4
            // 
            this.guna2Button4.BorderRadius = 19;
            this.guna2Button4.BorderThickness = 1;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2Button4.ForeColor = System.Drawing.Color.White;
            this.guna2Button4.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(118)))), ((int)(((byte)(225)))));
            this.guna2Button4.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.guna2Button4.HoverState.ForeColor = System.Drawing.Color.Black;
            this.guna2Button4.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button4.Image")));
            this.guna2Button4.Location = new System.Drawing.Point(1500, 527);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Size = new System.Drawing.Size(178, 72);
            this.guna2Button4.TabIndex = 22;
            this.guna2Button4.Text = "Cập Nhật ";
            this.guna2Button4.Click += new System.EventHandler(this.guna2Button2_Click);
            // 
            // SLPro
            // 
            this.SLPro.Location = new System.Drawing.Point(103, 166);
            this.SLPro.Name = "SLPro";
            this.SLPro.Size = new System.Drawing.Size(205, 22);
            this.SLPro.TabIndex = 24;
            // 
            // ViewMedicine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1790, 884);
            this.Controls.Add(this.UnitPro);
            this.Controls.Add(this.CatePro);
            this.Controls.Add(this.Date);
            this.Controls.Add(this.SLPro);
            this.Controls.Add(this.NamePro);
            this.Controls.Add(this.IdPro);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.guna2Button4);
            this.Controls.Add(this.ChiTiet);
            this.Controls.Add(this.guna2Button2);
            this.Controls.Add(this.TxtTimkiem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ViewMedicine";
            this.Text = "ViewMedicine";
            this.Load += new System.EventHandler(this.ViewMedicine_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2TextBox TxtTimkiem;
        private System.Windows.Forms.Label label2;
        private Guna.UI2.WinForms.Guna2Button guna2Button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox IdPro;
        private System.Windows.Forms.TextBox NamePro;
        private Guna.UI2.WinForms.Guna2DateTimePicker Date;
        private Guna.UI2.WinForms.Guna2ComboBox CatePro;
        private Guna.UI2.WinForms.Guna2ComboBox UnitPro;
        private Guna.UI2.WinForms.Guna2Button ChiTiet;
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private System.Windows.Forms.TextBox SLPro;
    }
}