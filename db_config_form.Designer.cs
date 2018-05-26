namespace data_process
{
    partial class db_config_form
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
            this.label_table_name = new System.Windows.Forms.Label();
            this.text_table_name = new System.Windows.Forms.TextBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label_user_name = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_db_name = new System.Windows.Forms.Label();
            this.text_password = new System.Windows.Forms.TextBox();
            this.text_user_name = new System.Windows.Forms.TextBox();
            this.text_db_name = new System.Windows.Forms.TextBox();
            this.text_server_name = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.text_start_row = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.text_port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.combo_column = new System.Windows.Forms.ComboBox();
            this.dgDisplay = new System.Windows.Forms.DataGridView();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // label_table_name
            // 
            this.label_table_name.AutoSize = true;
            this.label_table_name.Location = new System.Drawing.Point(24, 168);
            this.label_table_name.Name = "label_table_name";
            this.label_table_name.Size = new System.Drawing.Size(35, 12);
            this.label_table_name.TabIndex = 49;
            this.label_table_name.Text = "表名:";
            // 
            // text_table_name
            // 
            this.text_table_name.Location = new System.Drawing.Point(65, 164);
            this.text_table_name.Name = "text_table_name";
            this.text_table_name.Size = new System.Drawing.Size(186, 21);
            this.text_table_name.TabIndex = 48;
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(176, 191);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(75, 21);
            this.btn_select.TabIndex = 47;
            this.btn_select.Text = "浏览";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 138);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 42;
            this.label4.Text = "密码:";
            // 
            // label_user_name
            // 
            this.label_user_name.AutoSize = true;
            this.label_user_name.Location = new System.Drawing.Point(12, 106);
            this.label_user_name.Name = "label_user_name";
            this.label_user_name.Size = new System.Drawing.Size(47, 12);
            this.label_user_name.TabIndex = 41;
            this.label_user_name.Text = "用户名:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 40;
            this.label5.Text = "服务器:";
            // 
            // label_db_name
            // 
            this.label_db_name.AutoSize = true;
            this.label_db_name.Location = new System.Drawing.Point(12, 76);
            this.label_db_name.Name = "label_db_name";
            this.label_db_name.Size = new System.Drawing.Size(47, 12);
            this.label_db_name.TabIndex = 39;
            this.label_db_name.Text = "数据库:";
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(65, 134);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(186, 21);
            this.text_password.TabIndex = 38;
            // 
            // text_user_name
            // 
            this.text_user_name.Location = new System.Drawing.Point(65, 103);
            this.text_user_name.Name = "text_user_name";
            this.text_user_name.Size = new System.Drawing.Size(186, 21);
            this.text_user_name.TabIndex = 37;
            // 
            // text_db_name
            // 
            this.text_db_name.Location = new System.Drawing.Point(65, 72);
            this.text_db_name.Name = "text_db_name";
            this.text_db_name.Size = new System.Drawing.Size(186, 21);
            this.text_db_name.TabIndex = 36;
            // 
            // text_server_name
            // 
            this.text_server_name.Location = new System.Drawing.Point(65, 15);
            this.text_server_name.Name = "text_server_name";
            this.text_server_name.Size = new System.Drawing.Size(186, 21);
            this.text_server_name.TabIndex = 35;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.text_start_row);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.text_port);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.combo_column);
            this.groupBox1.Controls.Add(this.text_table_name);
            this.groupBox1.Controls.Add(this.text_server_name);
            this.groupBox1.Controls.Add(this.text_db_name);
            this.groupBox1.Controls.Add(this.label_table_name);
            this.groupBox1.Controls.Add(this.text_user_name);
            this.groupBox1.Controls.Add(this.text_password);
            this.groupBox1.Controls.Add(this.btn_select);
            this.groupBox1.Controls.Add(this.label_db_name);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label_user_name);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(1, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(273, 294);
            this.groupBox1.TabIndex = 52;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接参数";
            // 
            // text_start_row
            // 
            this.text_start_row.Location = new System.Drawing.Point(64, 257);
            this.text_start_row.Name = "text_start_row";
            this.text_start_row.Size = new System.Drawing.Size(186, 21);
            this.text_start_row.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 260);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 57;
            this.label3.Text = "起始行:";
            // 
            // text_port
            // 
            this.text_port.Location = new System.Drawing.Point(65, 42);
            this.text_port.Name = "text_port";
            this.text_port.Size = new System.Drawing.Size(186, 21);
            this.text_port.TabIndex = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 51;
            this.label2.Text = "端口:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 56;
            this.label1.Text = "列名:";
            // 
            // combo_column
            // 
            this.combo_column.FormattingEnabled = true;
            this.combo_column.Location = new System.Drawing.Point(64, 228);
            this.combo_column.Name = "combo_column";
            this.combo_column.Size = new System.Drawing.Size(187, 20);
            this.combo_column.TabIndex = 55;
            // 
            // dgDisplay
            // 
            this.dgDisplay.AllowUserToAddRows = false;
            this.dgDisplay.AllowUserToDeleteRows = false;
            this.dgDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDisplay.Location = new System.Drawing.Point(280, 4);
            this.dgDisplay.Name = "dgDisplay";
            this.dgDisplay.ReadOnly = true;
            this.dgDisplay.RowHeadersVisible = false;
            this.dgDisplay.Size = new System.Drawing.Size(354, 351);
            this.dgDisplay.TabIndex = 54;
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(68, 322);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(45, 25);
            this.btn_cancel.TabIndex = 58;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(15, 322);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(45, 25);
            this.btn_ok.TabIndex = 57;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(209, 322);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(65, 25);
            this.btn_load.TabIndex = 63;
            this.btn_load.Text = "加载配置";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(138, 322);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(65, 25);
            this.btn_save.TabIndex = 62;
            this.btn_save.Text = "保存配置";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // db_config_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 357);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.dgDisplay);
            this.Controls.Add(this.groupBox1);
            this.Name = "db_config_form";
            this.Text = "数据库配置";
            this.Load += new System.EventHandler(this.db_config_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_table_name;
        private System.Windows.Forms.TextBox text_table_name;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_user_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_db_name;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.TextBox text_user_name;
        private System.Windows.Forms.TextBox text_db_name;
        private System.Windows.Forms.TextBox text_server_name;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgDisplay;
        private System.Windows.Forms.TextBox text_port;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combo_column;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_start_row;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_save;
    }
}