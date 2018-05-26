namespace data_process
{
    partial class nlp_process_dlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(nlp_process_dlg));
            this.link_nlp_service = new System.Windows.Forms.LinkLabel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.link_nlp_service_sec = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // link_nlp_service
            // 
            this.link_nlp_service.AutoSize = true;
            this.link_nlp_service.Location = new System.Drawing.Point(32, 155);
            this.link_nlp_service.Name = "link_nlp_service";
            this.link_nlp_service.Size = new System.Drawing.Size(191, 12);
            this.link_nlp_service.TabIndex = 0;
            this.link_nlp_service.TabStop = true;
            this.link_nlp_service.Text = "http://pan.baidu.com/s/1kUQ4ueb";
            this.link_nlp_service.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_nlp_service_LinkClicked);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 6);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(494, 146);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // link_nlp_service_sec
            // 
            this.link_nlp_service_sec.AutoSize = true;
            this.link_nlp_service_sec.Location = new System.Drawing.Point(240, 155);
            this.link_nlp_service_sec.Name = "link_nlp_service_sec";
            this.link_nlp_service_sec.Size = new System.Drawing.Size(251, 12);
            this.link_nlp_service_sec.TabIndex = 2;
            this.link_nlp_service_sec.TabStop = true;
            this.link_nlp_service_sec.Text = "http://pan.baidu.com/share/home?uk=319...";
            this.link_nlp_service_sec.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_nlp_service_sec_LinkClicked);
            // 
            // nlp_process_dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 176);
            this.Controls.Add(this.link_nlp_service_sec);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.link_nlp_service);
            this.Name = "nlp_process_dlg";
            this.Text = "文本自然语言处理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel link_nlp_service;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel link_nlp_service_sec;
    }
}