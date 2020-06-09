namespace MonCollection
{
    partial class FormAssignOrigin
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
            this.comboBoxOrigin = new System.Windows.Forms.ComboBox();
            this.labelOrigin = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxOT = new System.Windows.Forms.TextBox();
            this.labelOT = new System.Windows.Forms.Label();
            this.buttonSet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxOrigin
            // 
            this.comboBoxOrigin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxOrigin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxOrigin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxOrigin.ForeColor = System.Drawing.Color.Black;
            this.comboBoxOrigin.FormattingEnabled = true;
            this.comboBoxOrigin.Location = new System.Drawing.Point(58, 57);
            this.comboBoxOrigin.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxOrigin.Name = "comboBoxOrigin";
            this.comboBoxOrigin.Size = new System.Drawing.Size(180, 21);
            this.comboBoxOrigin.TabIndex = 199;
            // 
            // labelOrigin
            // 
            this.labelOrigin.AutoSize = true;
            this.labelOrigin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOrigin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrigin.ForeColor = System.Drawing.Color.Black;
            this.labelOrigin.Location = new System.Drawing.Point(12, 60);
            this.labelOrigin.Name = "labelOrigin";
            this.labelOrigin.Size = new System.Drawing.Size(44, 13);
            this.labelOrigin.TabIndex = 200;
            this.labelOrigin.Text = "Origin:";
            // 
            // textBoxID
            // 
            this.textBoxID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxID.ForeColor = System.Drawing.Color.Black;
            this.textBoxID.Location = new System.Drawing.Point(172, 33);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(66, 20);
            this.textBoxID.TabIndex = 196;
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.ForeColor = System.Drawing.Color.Black;
            this.labelID.Location = new System.Drawing.Point(142, 35);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(24, 13);
            this.labelID.TabIndex = 198;
            this.labelID.Text = "ID:";
            // 
            // textBoxOT
            // 
            this.textBoxOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxOT.ForeColor = System.Drawing.Color.Black;
            this.textBoxOT.Location = new System.Drawing.Point(46, 32);
            this.textBoxOT.Name = "textBoxOT";
            this.textBoxOT.Size = new System.Drawing.Size(90, 20);
            this.textBoxOT.TabIndex = 195;
            // 
            // labelOT
            // 
            this.labelOT.AutoSize = true;
            this.labelOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOT.ForeColor = System.Drawing.Color.Black;
            this.labelOT.Location = new System.Drawing.Point(12, 33);
            this.labelOT.Name = "labelOT";
            this.labelOT.Size = new System.Drawing.Size(28, 13);
            this.labelOT.TabIndex = 197;
            this.labelOT.Text = "OT:";
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(91, 97);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(75, 23);
            this.buttonSet.TabIndex = 201;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // FormAssignOrigin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(252, 133);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.comboBoxOrigin);
            this.Controls.Add(this.labelOrigin);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.textBoxOT);
            this.Controls.Add(this.labelOT);
            this.Name = "FormAssignOrigin";
            this.Text = "FormAssignOrigin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOrigin;
        private System.Windows.Forms.Label labelOrigin;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxOT;
        private System.Windows.Forms.Label labelOT;
        private System.Windows.Forms.Button buttonSet;
    }
}