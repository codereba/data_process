using MySql.Data; 

namespace data_process
{
    partial class data_process
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
            this.btn_input_db = new System.Windows.Forms.Button();
            this.btn_process_html = new System.Windows.Forms.Button();
            this.text_process_mode = new System.Windows.Forms.TextBox();
            this.text_input_param = new System.Windows.Forms.TextBox();
            this.group_input = new System.Windows.Forms.GroupBox();
            this.btn_text = new System.Windows.Forms.Button();
            this.btn_input_csv = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_process_nlp = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.group_input.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_input_db
            // 
            this.btn_input_db.Location = new System.Drawing.Point(70, 18);
            this.btn_input_db.Name = "btn_input_db";
            this.btn_input_db.Size = new System.Drawing.Size(62, 23);
            this.btn_input_db.TabIndex = 1;
            this.btn_input_db.Text = "数据库";
            this.btn_input_db.UseVisualStyleBackColor = true;
            this.btn_input_db.Click += new System.EventHandler(this.btn_input_db_Click);
            // 
            // btn_process_html
            // 
            this.btn_process_html.Enabled = false;
            this.btn_process_html.Location = new System.Drawing.Point(87, 18);
            this.btn_process_html.Name = "btn_process_html";
            this.btn_process_html.Size = new System.Drawing.Size(86, 23);
            this.btn_process_html.TabIndex = 2;
            this.btn_process_html.Text = "HTML字段解析";
            this.btn_process_html.UseVisualStyleBackColor = true;
            this.btn_process_html.Visible = false;
            this.btn_process_html.Click += new System.EventHandler(this.btn_process_html_Click);
            // 
            // text_process_mode
            // 
            this.text_process_mode.Location = new System.Drawing.Point(6, 45);
            this.text_process_mode.Multiline = true;
            this.text_process_mode.Name = "text_process_mode";
            this.text_process_mode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_process_mode.Size = new System.Drawing.Size(299, 420);
            this.text_process_mode.TabIndex = 3;
            // 
            // text_input_param
            // 
            this.text_input_param.Location = new System.Drawing.Point(2, 45);
            this.text_input_param.Multiline = true;
            this.text_input_param.Name = "text_input_param";
            this.text_input_param.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_input_param.Size = new System.Drawing.Size(295, 420);
            this.text_input_param.TabIndex = 1;
            // 
            // group_input
            // 
            this.group_input.Controls.Add(this.btn_text);
            this.group_input.Controls.Add(this.btn_input_csv);
            this.group_input.Controls.Add(this.text_input_param);
            this.group_input.Controls.Add(this.btn_input_db);
            this.group_input.Location = new System.Drawing.Point(1, 12);
            this.group_input.Name = "group_input";
            this.group_input.Size = new System.Drawing.Size(300, 472);
            this.group_input.TabIndex = 6;
            this.group_input.TabStop = false;
            this.group_input.Text = "输入配置";
            // 
            // btn_text
            // 
            this.btn_text.Location = new System.Drawing.Point(2, 18);
            this.btn_text.Name = "btn_text";
            this.btn_text.Size = new System.Drawing.Size(61, 23);
            this.btn_text.TabIndex = 0;
            this.btn_text.Text = "文本";
            this.btn_text.UseVisualStyleBackColor = true;
            this.btn_text.Click += new System.EventHandler(this.btn_text_Click);
            // 
            // btn_input_csv
            // 
            this.btn_input_csv.Enabled = false;
            this.btn_input_csv.Location = new System.Drawing.Point(141, 18);
            this.btn_input_csv.Name = "btn_input_csv";
            this.btn_input_csv.Size = new System.Drawing.Size(61, 23);
            this.btn_input_csv.TabIndex = 2;
            this.btn_input_csv.Text = "CSV文件";
            this.btn_input_csv.UseVisualStyleBackColor = true;
            this.btn_input_csv.Visible = false;
            this.btn_input_csv.Click += new System.EventHandler(this.btn_input_csv_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_process_nlp);
            this.groupBox1.Controls.Add(this.text_process_mode);
            this.groupBox1.Controls.Add(this.btn_process_html);
            this.groupBox1.Location = new System.Drawing.Point(307, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 472);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "";
            this.groupBox1.Text = "数据处理配置";
            // 
            // btn_process_nlp
            // 
            this.btn_process_nlp.Location = new System.Drawing.Point(6, 18);
            this.btn_process_nlp.Name = "btn_process_nlp";
            this.btn_process_nlp.Size = new System.Drawing.Size(75, 23);
            this.btn_process_nlp.TabIndex = 4;
            this.btn_process_nlp.Text = "语义树解析";
            this.btn_process_nlp.UseVisualStyleBackColor = true;
            this.btn_process_nlp.Click += new System.EventHandler(this.btn_process_nlp_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(458, 484);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 8;
            this.btn_start.Text = "开始";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Location = new System.Drawing.Point(539, 484);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 23);
            this.btn_stop.TabIndex = 9;
            this.btn_stop.Text = "停止";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_load
            // 
            this.btn_load.Enabled = false;
            this.btn_load.Location = new System.Drawing.Point(91, 486);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(75, 23);
            this.btn_load.TabIndex = 61;
            this.btn_load.Text = "加载";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Visible = false;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // btn_save
            // 
            this.btn_save.Enabled = false;
            this.btn_save.Location = new System.Drawing.Point(12, 486);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(73, 23);
            this.btn_save.TabIndex = 60;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Visible = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // data_process
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(624, 511);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group_input);
            this.MaximizeBox = false;
            this.Name = "data_process";
            this.Text = "数据处理";
            this.group_input.ResumeLayout(false);
            this.group_input.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public DBConnect mysql_db;
        public System.Diagnostics.Process diag_process; 
        public MySql.Data.MySqlClient.MySqlConnection db_conn; 
        private System.Windows.Forms.Button btn_input_db;
        private System.Windows.Forms.Button btn_process_html;
        private System.Windows.Forms.TextBox text_process_mode;
        private System.Windows.Forms.TextBox text_input_param;
        private System.Windows.Forms.GroupBox group_input;
        private System.Windows.Forms.Button btn_input_csv;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_process_nlp;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_text;
    }
}

