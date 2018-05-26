namespace data_process
{
    partial class output_log_form
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
            this.text_output = new System.Windows.Forms.TextBox();
            this.text_input = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // text_output
            // 
            this.text_output.Location = new System.Drawing.Point(434, 6);
            this.text_output.Multiline = true;
            this.text_output.Name = "text_output";
            this.text_output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_output.Size = new System.Drawing.Size(432, 468);
            this.text_output.TabIndex = 3;
            // 
            // text_input
            // 
            this.text_input.Location = new System.Drawing.Point(5, 6);
            this.text_input.Multiline = true;
            this.text_input.Name = "text_input";
            this.text_input.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.text_input.Size = new System.Drawing.Size(425, 468);
            this.text_input.TabIndex = 2;
            // 
            // output_log_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 479);
            this.Controls.Add(this.text_output);
            this.Controls.Add(this.text_input);
            this.Name = "output_log_form";
            this.Text = "输入/输出分析";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox text_output;
        public System.Windows.Forms.TextBox text_input;
    }
}