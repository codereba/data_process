using MySql.Data; 

namespace data_process
{
    partial class text_process_form
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
            this.btn_convert = new System.Windows.Forms.Button();
            this.dg_data_out = new System.Windows.Forms.DataGridView();
            this.text_input = new System.Windows.Forms.TextBox();
            this.btn_csv_file = new System.Windows.Forms.Button();
            this.btn_process = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_data_out)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_convert
            // 
            this.btn_convert.Location = new System.Drawing.Point(1, 413);
            this.btn_convert.Name = "btn_convert";
            this.btn_convert.Size = new System.Drawing.Size(85, 23);
            this.btn_convert.TabIndex = 28;
            this.btn_convert.Text = "转换数字";
            this.btn_convert.UseVisualStyleBackColor = true;
            this.btn_convert.Click += new System.EventHandler(this.btn_convert_Click);
            // 
            // dg_data_out
            // 
            this.dg_data_out.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_data_out.Location = new System.Drawing.Point(1, -1);
            this.dg_data_out.Name = "dg_data_out";
            this.dg_data_out.RowTemplate.Height = 23;
            this.dg_data_out.Size = new System.Drawing.Size(285, 408);
            this.dg_data_out.TabIndex = 29;
            // 
            // text_input
            // 
            this.text_input.Location = new System.Drawing.Point(149, 26);
            this.text_input.MaxLength = 0;
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.Size = new System.Drawing.Size(312, 381);
            this.text_input.TabIndex = 30;
            // 
            // btn_csv_file
            // 
            this.btn_csv_file.Location = new System.Drawing.Point(201, 413);
            this.btn_csv_file.Name = "btn_csv_file";
            this.btn_csv_file.Size = new System.Drawing.Size(85, 23);
            this.btn_csv_file.TabIndex = 39;
            this.btn_csv_file.Text = "处理CSV文件";
            this.btn_csv_file.UseVisualStyleBackColor = true;
            // 
            // btn_process
            // 
            this.btn_process.Location = new System.Drawing.Point(120, 413);
            this.btn_process.Name = "btn_process";
            this.btn_process.Size = new System.Drawing.Size(75, 23);
            this.btn_process.TabIndex = 38;
            this.btn_process.Text = "处理文本";
            this.btn_process.UseVisualStyleBackColor = true;
            // 
            // text_process_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 438);
            this.Controls.Add(this.text_input);
            this.Controls.Add(this.dg_data_out);
            this.Controls.Add(this.btn_convert);
            this.Controls.Add(this.btn_csv_file);
            this.Controls.Add(this.btn_process);
            this.Name = "text_process_form";
            this.Text = "HTML数据处理";
            ((System.ComponentModel.ISupportInitialize)(this.dg_data_out)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_convert;
        private System.Windows.Forms.DataGridView dg_data_out;
        private System.Windows.Forms.TextBox text_input;
        private System.Windows.Forms.Button btn_csv_file;
        private System.Windows.Forms.Button btn_process; 
    }
}

