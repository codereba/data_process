namespace data_process
{
    partial class html_process_form
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
            this.btn_browser = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.text_file_path = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.combo_type = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.text_xpath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.text_name = new System.Windows.Forms.TextBox();
            this.list_data_info = new System.Windows.Forms.ListView();
            this.column_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_xpath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.column_type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_xpath = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_browser
            // 
            this.btn_browser.Location = new System.Drawing.Point(105, 402);
            this.btn_browser.Name = "btn_browser";
            this.btn_browser.Size = new System.Drawing.Size(40, 23);
            this.btn_browser.TabIndex = 52;
            this.btn_browser.Text = "浏览";
            this.btn_browser.UseVisualStyleBackColor = true;
            // 
            // btn_load
            // 
            this.btn_load.Location = new System.Drawing.Point(59, 402);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(40, 23);
            this.btn_load.TabIndex = 51;
            this.btn_load.Text = "加载";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.btn_load_Click);
            // 
            // text_file_path
            // 
            this.text_file_path.Location = new System.Drawing.Point(57, 16);
            this.text_file_path.Name = "text_file_path";
            this.text_file_path.Size = new System.Drawing.Size(443, 21);
            this.text_file_path.TabIndex = 50;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(14, 402);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(40, 23);
            this.btn_save.TabIndex = 49;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(194, 119);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(40, 23);
            this.btn_clear.TabIndex = 48;
            this.btn_clear.Text = "清空";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(105, 119);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(40, 23);
            this.btn_remove.TabIndex = 47;
            this.btn_remove.Text = "删除";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(59, 119);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(39, 23);
            this.btn_add.TabIndex = 46;
            this.btn_add.Text = "添加";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // combo_type
            // 
            this.combo_type.AllowDrop = true;
            this.combo_type.FormattingEnabled = true;
            this.combo_type.Items.AddRange(new object[] {
            "int",
            "text"});
            this.combo_type.Location = new System.Drawing.Point(52, -32);
            this.combo_type.Name = "combo_type";
            this.combo_type.Size = new System.Drawing.Size(224, 20);
            this.combo_type.Sorted = true;
            this.combo_type.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, -27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "类型:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, -54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "xpath:";
            // 
            // text_xpath
            // 
            this.text_xpath.Location = new System.Drawing.Point(52, -59);
            this.text_xpath.Multiline = true;
            this.text_xpath.Name = "text_xpath";
            this.text_xpath.Size = new System.Drawing.Size(224, 21);
            this.text_xpath.TabIndex = 42;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, -81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 41;
            this.label1.Text = "名称:";
            // 
            // text_name
            // 
            this.text_name.Location = new System.Drawing.Point(52, -86);
            this.text_name.Multiline = true;
            this.text_name.Name = "text_name";
            this.text_name.Size = new System.Drawing.Size(223, 21);
            this.text_name.TabIndex = 40;
            // 
            // list_data_info
            // 
            this.list_data_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.column_name,
            this.column_xpath,
            this.column_type});
            this.list_data_info.FullRowSelect = true;
            this.list_data_info.LabelEdit = true;
            this.list_data_info.Location = new System.Drawing.Point(2, 148);
            this.list_data_info.Name = "list_data_info";
            this.list_data_info.ShowItemToolTips = true;
            this.list_data_info.Size = new System.Drawing.Size(531, 248);
            this.list_data_info.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.list_data_info.TabIndex = 39;
            this.list_data_info.UseCompatibleStateImageBehavior = false;
            this.list_data_info.View = System.Windows.Forms.View.Details;
            // 
            // column_name
            // 
            this.column_name.Text = "名称";
            // 
            // column_xpath
            // 
            this.column_xpath.Text = "xpath";
            this.column_xpath.Width = 137;
            // 
            // column_type
            // 
            this.column_type.Text = "类型";
            // 
            // label_xpath
            // 
            this.label_xpath.AutoSize = true;
            this.label_xpath.Location = new System.Drawing.Point(11, 22);
            this.label_xpath.Name = "label_xpath";
            this.label_xpath.Size = new System.Drawing.Size(41, 12);
            this.label_xpath.TabIndex = 54;
            this.label_xpath.Text = "XPATH:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 56;
            this.label4.Text = "名称:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(57, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(175, 21);
            this.textBox1.TabIndex = 55;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 58;
            this.label5.Text = "类型:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_xpath);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.text_file_path);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 101);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "HTML元素属性:";
            // 
            // html_process_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 427);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_browser);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.combo_type);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_xpath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_name);
            this.Controls.Add(this.list_data_info);
            this.Name = "html_process_form";
            this.Text = "html_process_form";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_browser;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.TextBox text_file_path;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox text_xpath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_name;
        private System.Windows.Forms.ListView list_data_info;
        private System.Windows.Forms.ColumnHeader column_name;
        private System.Windows.Forms.ColumnHeader column_xpath;
        private System.Windows.Forms.ColumnHeader column_type;
        private System.Windows.Forms.Label label_xpath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox combo_type;
    }
}