namespace PokerTournament
{
    partial class newPlayerForm
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
            this.firstNameBox = new System.Windows.Forms.TextBox();
            this.firstNameLbl = new System.Windows.Forms.Label();
            this.titleLbl = new System.Windows.Forms.Label();
            this.lastNameLbl = new System.Windows.Forms.Label();
            this.lastNameBox = new System.Windows.Forms.TextBox();
            this.ssnLbl = new System.Windows.Forms.Label();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.ssnBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // firstNameBox
            // 
            this.firstNameBox.Location = new System.Drawing.Point(144, 84);
            this.firstNameBox.Name = "firstNameBox";
            this.firstNameBox.Size = new System.Drawing.Size(149, 29);
            this.firstNameBox.TabIndex = 0;
            // 
            // firstNameLbl
            // 
            this.firstNameLbl.AutoSize = true;
            this.firstNameLbl.ForeColor = System.Drawing.Color.MistyRose;
            this.firstNameLbl.Location = new System.Drawing.Point(12, 87);
            this.firstNameLbl.Name = "firstNameLbl";
            this.firstNameLbl.Size = new System.Drawing.Size(86, 21);
            this.firstNameLbl.TabIndex = 1;
            this.firstNameLbl.Text = "First Name";
            // 
            // titleLbl
            // 
            this.titleLbl.AutoSize = true;
            this.titleLbl.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLbl.ForeColor = System.Drawing.Color.MistyRose;
            this.titleLbl.Location = new System.Drawing.Point(38, 9);
            this.titleLbl.Name = "titleLbl";
            this.titleLbl.Size = new System.Drawing.Size(235, 38);
            this.titleLbl.TabIndex = 2;
            this.titleLbl.Text = "New Player Entry";
            // 
            // lastNameLbl
            // 
            this.lastNameLbl.AutoSize = true;
            this.lastNameLbl.ForeColor = System.Drawing.Color.MistyRose;
            this.lastNameLbl.Location = new System.Drawing.Point(12, 148);
            this.lastNameLbl.Name = "lastNameLbl";
            this.lastNameLbl.Size = new System.Drawing.Size(84, 21);
            this.lastNameLbl.TabIndex = 3;
            this.lastNameLbl.Text = "Last Name";
            // 
            // lastNameBox
            // 
            this.lastNameBox.Location = new System.Drawing.Point(144, 148);
            this.lastNameBox.Name = "lastNameBox";
            this.lastNameBox.Size = new System.Drawing.Size(149, 29);
            this.lastNameBox.TabIndex = 4;
            // 
            // ssnLbl
            // 
            this.ssnLbl.AutoSize = true;
            this.ssnLbl.ForeColor = System.Drawing.Color.MistyRose;
            this.ssnLbl.Location = new System.Drawing.Point(16, 217);
            this.ssnLbl.Name = "ssnLbl";
            this.ssnLbl.Size = new System.Drawing.Size(53, 21);
            this.ssnLbl.TabIndex = 5;
            this.ssnLbl.Text = "SSN #";
            // 
            // cancelBtn
            // 
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancelBtn.Location = new System.Drawing.Point(20, 289);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(78, 26);
            this.cancelBtn.TabIndex = 7;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.saveBtn.Location = new System.Drawing.Point(144, 289);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(141, 26);
            this.saveBtn.TabIndex = 8;
            this.saveBtn.Text = "Save and Close";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // ssnBox
            // 
            this.ssnBox.Location = new System.Drawing.Point(144, 217);
            this.ssnBox.Mask = "000-00-0000";
            this.ssnBox.Name = "ssnBox";
            this.ssnBox.Size = new System.Drawing.Size(149, 29);
            this.ssnBox.TabIndex = 9;
            // 
            // newPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGreen;
            this.ClientSize = new System.Drawing.Size(310, 339);
            this.Controls.Add(this.ssnBox);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.ssnLbl);
            this.Controls.Add(this.lastNameBox);
            this.Controls.Add(this.lastNameLbl);
            this.Controls.Add(this.titleLbl);
            this.Controls.Add(this.firstNameLbl);
            this.Controls.Add(this.firstNameBox);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "newPlayerForm";
            this.Text = "New Player Entry";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox firstNameBox;
        private System.Windows.Forms.Label firstNameLbl;
        private System.Windows.Forms.Label titleLbl;
        private System.Windows.Forms.Label lastNameLbl;
        private System.Windows.Forms.TextBox lastNameBox;
        private System.Windows.Forms.Label ssnLbl;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.MaskedTextBox ssnBox;
    }
}