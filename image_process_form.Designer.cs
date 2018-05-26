namespace data_process
{
    partial class image_process_form
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
            this.iamge_box = new System.Windows.Forms.PictureBox();
            this.btn_image_process = new System.Windows.Forms.Button();
            this.btn_source = new System.Windows.Forms.Button();
            this.btn_dest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.iamge_box)).BeginInit();
            this.SuspendLayout();
            // 
            // iamge_box
            // 
            this.iamge_box.Location = new System.Drawing.Point(2, 1);
            this.iamge_box.Name = "iamge_box";
            this.iamge_box.Size = new System.Drawing.Size(438, 370);
            this.iamge_box.TabIndex = 0;
            this.iamge_box.TabStop = false;
            // 
            // btn_image_process
            // 
            this.btn_image_process.Location = new System.Drawing.Point(164, 377);
            this.btn_image_process.Name = "btn_image_process";
            this.btn_image_process.Size = new System.Drawing.Size(75, 23);
            this.btn_image_process.TabIndex = 1;
            this.btn_image_process.Text = "处理";
            this.btn_image_process.UseVisualStyleBackColor = true;
            this.btn_image_process.Click += new System.EventHandler(this.btn_image_process_Click);
            // 
            // btn_source
            // 
            this.btn_source.Location = new System.Drawing.Point(2, 377);
            this.btn_source.Name = "btn_source";
            this.btn_source.Size = new System.Drawing.Size(75, 23);
            this.btn_source.TabIndex = 2;
            this.btn_source.Text = "输入源";
            this.btn_source.UseVisualStyleBackColor = true;
            this.btn_source.Click += new System.EventHandler(this.btn_source_Click);
            // 
            // btn_dest
            // 
            this.btn_dest.Location = new System.Drawing.Point(83, 377);
            this.btn_dest.Name = "btn_dest";
            this.btn_dest.Size = new System.Drawing.Size(75, 23);
            this.btn_dest.TabIndex = 3;
            this.btn_dest.Text = "输出目标";
            this.btn_dest.UseVisualStyleBackColor = true;
            this.btn_dest.Click += new System.EventHandler(this.btn_dest_Click);
            // 
            // image_process_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 404);
            this.Controls.Add(this.btn_dest);
            this.Controls.Add(this.btn_source);
            this.Controls.Add(this.btn_image_process);
            this.Controls.Add(this.iamge_box);
            this.Name = "image_process_form";
            this.Text = "图像处理";
            ((System.ComponentModel.ISupportInitialize)(this.iamge_box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox iamge_box;
        private System.Windows.Forms.Button btn_image_process;
        private System.Windows.Forms.Button btn_source;
        private System.Windows.Forms.Button btn_dest;
    }
}