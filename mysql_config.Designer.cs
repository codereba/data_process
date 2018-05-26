namespace ConnectCsharpToMysql
{
    partial class Form1
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
            this.bInsert = new System.Windows.Forms.Button();
            this.bUpdate = new System.Windows.Forms.Button();
            this.bSelect = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.bCount = new System.Windows.Forms.Button();
            this.bBackup = new System.Windows.Forms.Button();
            this.bRestore = new System.Windows.Forms.Button();
            this.dgDisplay = new System.Windows.Forms.DataGridView();
            this.ciD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.text_server_name = new System.Windows.Forms.TextBox();
            this.text_db_name = new System.Windows.Forms.TextBox();
            this.text_user_name = new System.Windows.Forms.TextBox();
            this.text_password = new System.Windows.Forms.TextBox();
            this.label_db_name = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_user_name = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_disconnect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // bInsert
            // 
            this.bInsert.Location = new System.Drawing.Point(435, 36);
            this.bInsert.Name = "bInsert";
            this.bInsert.Size = new System.Drawing.Size(75, 21);
            this.bInsert.TabIndex = 0;
            this.bInsert.Text = "Insert";
            this.bInsert.UseVisualStyleBackColor = true;
            this.bInsert.Click += new System.EventHandler(this.bInsert_Click);
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(273, 36);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(75, 21);
            this.bUpdate.TabIndex = 1;
            this.bUpdate.Text = "Update";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // bSelect
            // 
            this.bSelect.Location = new System.Drawing.Point(273, 67);
            this.bSelect.Name = "bSelect";
            this.bSelect.Size = new System.Drawing.Size(75, 21);
            this.bSelect.TabIndex = 2;
            this.bSelect.Text = "Select";
            this.bSelect.UseVisualStyleBackColor = true;
            this.bSelect.Click += new System.EventHandler(this.bSelect_Click);
            // 
            // bDelete
            // 
            this.bDelete.Location = new System.Drawing.Point(354, 36);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(75, 21);
            this.bDelete.TabIndex = 3;
            this.bDelete.Text = "Delete";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bCount
            // 
            this.bCount.Location = new System.Drawing.Point(354, 67);
            this.bCount.Name = "bCount";
            this.bCount.Size = new System.Drawing.Size(75, 21);
            this.bCount.TabIndex = 4;
            this.bCount.Text = "Count";
            this.bCount.UseVisualStyleBackColor = true;
            this.bCount.Click += new System.EventHandler(this.bCount_Click);
            // 
            // bBackup
            // 
            this.bBackup.Location = new System.Drawing.Point(273, 93);
            this.bBackup.Name = "bBackup";
            this.bBackup.Size = new System.Drawing.Size(75, 21);
            this.bBackup.TabIndex = 5;
            this.bBackup.Text = "Backup";
            this.bBackup.UseVisualStyleBackColor = true;
            this.bBackup.Click += new System.EventHandler(this.bBackup_Click);
            // 
            // bRestore
            // 
            this.bRestore.Location = new System.Drawing.Point(354, 94);
            this.bRestore.Name = "bRestore";
            this.bRestore.Size = new System.Drawing.Size(75, 21);
            this.bRestore.TabIndex = 6;
            this.bRestore.Text = "Restore";
            this.bRestore.UseVisualStyleBackColor = true;
            this.bRestore.Click += new System.EventHandler(this.bRestore_Click);
            // 
            // dgDisplay
            // 
            this.dgDisplay.AllowUserToAddRows = false;
            this.dgDisplay.AllowUserToDeleteRows = false;
            this.dgDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDisplay.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ciD,
            this.cName,
            this.cAge});
            this.dgDisplay.Location = new System.Drawing.Point(-16, 142);
            this.dgDisplay.Name = "dgDisplay";
            this.dgDisplay.ReadOnly = true;
            this.dgDisplay.RowHeadersVisible = false;
            this.dgDisplay.Size = new System.Drawing.Size(507, 129);
            this.dgDisplay.TabIndex = 7;
            // 
            // ciD
            // 
            this.ciD.HeaderText = "id";
            this.ciD.Name = "ciD";
            this.ciD.ReadOnly = true;
            // 
            // cName
            // 
            this.cName.HeaderText = "Name";
            this.cName.Name = "cName";
            this.cName.ReadOnly = true;
            // 
            // cAge
            // 
            this.cAge.HeaderText = "Age";
            this.cAge.Name = "cAge";
            this.cAge.ReadOnly = true;
            // 
            // text_server_name
            // 
            this.text_server_name.Location = new System.Drawing.Point(76, 4);
            this.text_server_name.Name = "text_server_name";
            this.text_server_name.Size = new System.Drawing.Size(172, 21);
            this.text_server_name.TabIndex = 8;
            // 
            // text_db_name
            // 
            this.text_db_name.Location = new System.Drawing.Point(76, 32);
            this.text_db_name.Name = "text_db_name";
            this.text_db_name.Size = new System.Drawing.Size(172, 21);
            this.text_db_name.TabIndex = 9;
            // 
            // text_user_name
            // 
            this.text_user_name.Location = new System.Drawing.Point(76, 63);
            this.text_user_name.Name = "text_user_name";
            this.text_user_name.Size = new System.Drawing.Size(172, 21);
            this.text_user_name.TabIndex = 10;
            // 
            // text_password
            // 
            this.text_password.Location = new System.Drawing.Point(76, 90);
            this.text_password.Name = "text_password";
            this.text_password.Size = new System.Drawing.Size(172, 21);
            this.text_password.TabIndex = 11;
            // 
            // label_db_name
            // 
            this.label_db_name.AutoSize = true;
            this.label_db_name.Location = new System.Drawing.Point(23, 36);
            this.label_db_name.Name = "label_db_name";
            this.label_db_name.Size = new System.Drawing.Size(47, 12);
            this.label_db_name.TabIndex = 12;
            this.label_db_name.Text = "数据库:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "服务器:";
            // 
            // label_user_name
            // 
            this.label_user_name.AutoSize = true;
            this.label_user_name.Location = new System.Drawing.Point(23, 66);
            this.label_user_name.Name = "label_user_name";
            this.label_user_name.Size = new System.Drawing.Size(47, 12);
            this.label_user_name.TabIndex = 14;
            this.label_user_name.Text = "用户名:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "密码:";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(273, 9);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 21);
            this.btn_connect.TabIndex = 16;
            this.btn_connect.Text = "连接";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_disconnect
            // 
            this.btn_disconnect.Location = new System.Drawing.Point(354, 9);
            this.btn_disconnect.Name = "btn_disconnect";
            this.btn_disconnect.Size = new System.Drawing.Size(75, 21);
            this.btn_disconnect.TabIndex = 17;
            this.btn_disconnect.Text = "断开";
            this.btn_disconnect.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 241);
            this.Controls.Add(this.btn_disconnect);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_user_name);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_db_name);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_user_name);
            this.Controls.Add(this.text_db_name);
            this.Controls.Add(this.text_server_name);
            this.Controls.Add(this.dgDisplay);
            this.Controls.Add(this.bRestore);
            this.Controls.Add(this.bBackup);
            this.Controls.Add(this.bCount);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bSelect);
            this.Controls.Add(this.bUpdate);
            this.Controls.Add(this.bInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect Csharp To Mysql";
            ((System.ComponentModel.ISupportInitialize)(this.dgDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bInsert;
        private System.Windows.Forms.Button bSelect;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Button bCount;
        private System.Windows.Forms.Button bBackup;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Button bRestore;
        private System.Windows.Forms.DataGridView dgDisplay;
        private System.Windows.Forms.DataGridViewTextBoxColumn ciD;
        private System.Windows.Forms.DataGridViewTextBoxColumn cName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cAge;
        private System.Windows.Forms.TextBox text_server_name;
        private System.Windows.Forms.TextBox text_db_name;
        private System.Windows.Forms.TextBox text_user_name;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.Label label_db_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_user_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_disconnect;
    }
}

