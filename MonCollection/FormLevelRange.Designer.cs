namespace MonCollection
{
    partial class FormLevelRange
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
            this.buttonSet = new System.Windows.Forms.Button();
            this.textBoxMax = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxMin = new System.Windows.Forms.TextBox();
            this.labelOT = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSet
            // 
            this.buttonSet.Location = new System.Drawing.Point(140, 115);
            this.buttonSet.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Size = new System.Drawing.Size(112, 35);
            this.buttonSet.TabIndex = 206;
            this.buttonSet.Text = "Set";
            this.buttonSet.UseVisualStyleBackColor = true;
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // textBoxMax
            // 
            this.textBoxMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxMax.ForeColor = System.Drawing.Color.Black;
            this.textBoxMax.Location = new System.Drawing.Point(242, 54);
            this.textBoxMax.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMax.Name = "textBoxMax";
            this.textBoxMax.Size = new System.Drawing.Size(119, 26);
            this.textBoxMax.TabIndex = 203;
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.ForeColor = System.Drawing.Color.Black;
            this.labelID.Location = new System.Drawing.Point(191, 60);
            this.labelID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(43, 20);
            this.labelID.TabIndex = 205;
            this.labelID.Text = "Max";
            // 
            // textBoxMin
            // 
            this.textBoxMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxMin.ForeColor = System.Drawing.Color.Black;
            this.textBoxMin.Location = new System.Drawing.Point(64, 57);
            this.textBoxMin.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxMin.Name = "textBoxMin";
            this.textBoxMin.Size = new System.Drawing.Size(119, 26);
            this.textBoxMin.TabIndex = 202;
            // 
            // labelOT
            // 
            this.labelOT.AutoSize = true;
            this.labelOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOT.ForeColor = System.Drawing.Color.Black;
            this.labelOT.Location = new System.Drawing.Point(17, 60);
            this.labelOT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOT.Name = "labelOT";
            this.labelOT.Size = new System.Drawing.Size(39, 20);
            this.labelOT.TabIndex = 204;
            this.labelOT.Text = "Min";
            // 
            // FormLevelRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(378, 205);
            this.Controls.Add(this.buttonSet);
            this.Controls.Add(this.textBoxMax);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.textBoxMin);
            this.Controls.Add(this.labelOT);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormLevelRange";
            this.Text = "FormAssignOrigin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.TextBox textBoxMax;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxMin;
        private System.Windows.Forms.Label labelOT;
    }
}