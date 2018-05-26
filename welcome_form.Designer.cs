namespace data_process
{
    partial class welcome_form
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
            this.rich_text_welcome = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rich_text_welcome
            // 
            this.rich_text_welcome.BackColor = System.Drawing.SystemColors.Info;
            this.rich_text_welcome.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rich_text_welcome.Location = new System.Drawing.Point(3, 1);
            this.rich_text_welcome.Name = "rich_text_welcome";
            this.rich_text_welcome.Size = new System.Drawing.Size(416, 483);
            this.rich_text_welcome.TabIndex = 0;
            this.rich_text_welcome.Text = "";
            // 
            // welcome_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(421, 491);
            this.Controls.Add(this.rich_text_welcome);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "welcome_form";
            this.Text = "欢迎";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rich_text_welcome;
    }
}