namespace data_process
{
    partial class process_mode_config
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
            this.button1 = new System.Windows.Forms.Button();
            this.btn_learn = new System.Windows.Forms.Button();
            this.btn_process_mysql = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 23);
            this.button1.TabIndex = 42;
            this.button1.Text = "语义树解析";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_learn
            // 
            this.btn_learn.Location = new System.Drawing.Point(12, 76);
            this.btn_learn.Name = "btn_learn";
            this.btn_learn.Size = new System.Drawing.Size(85, 21);
            this.btn_learn.TabIndex = 41;
            this.btn_learn.Text = "数据学习";
            this.btn_learn.UseVisualStyleBackColor = true;
            // 
            // btn_process_mysql
            // 
            this.btn_process_mysql.Location = new System.Drawing.Point(12, 12);
            this.btn_process_mysql.Name = "btn_process_mysql";
            this.btn_process_mysql.Size = new System.Drawing.Size(85, 23);
            this.btn_process_mysql.TabIndex = 40;
            this.btn_process_mysql.Text = "HTML文本处理";
            this.btn_process_mysql.UseVisualStyleBackColor = true;
            // 
            // process_mode_config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_learn);
            this.Controls.Add(this.btn_process_mysql);
            this.Name = "process_mode_config";
            this.Text = "process_mode_config";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_learn;
        private System.Windows.Forms.Button btn_process_mysql;
    }
}