namespace MonCollection
{
    partial class FormDexOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDexOrder));
            this.labelOrder = new System.Windows.Forms.Label();
            this.labelVisible = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxVisible = new System.Windows.Forms.ComboBox();
            this.comboBoxOrder = new System.Windows.Forms.ComboBox();
            this.checkedListBoxDexes = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // labelOrder
            // 
            this.labelOrder.AutoSize = true;
            this.labelOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrder.ForeColor = System.Drawing.Color.Black;
            this.labelOrder.Location = new System.Drawing.Point(18, 52);
            this.labelOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOrder.Name = "labelOrder";
            this.labelOrder.Size = new System.Drawing.Size(63, 20);
            this.labelOrder.TabIndex = 156;
            this.labelOrder.Text = "Order:";
            // 
            // labelVisible
            // 
            this.labelVisible.AutoSize = true;
            this.labelVisible.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelVisible.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVisible.ForeColor = System.Drawing.Color.Black;
            this.labelVisible.Location = new System.Drawing.Point(18, 120);
            this.labelVisible.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelVisible.Name = "labelVisible";
            this.labelVisible.Size = new System.Drawing.Size(72, 20);
            this.labelVisible.TabIndex = 157;
            this.labelVisible.Text = "Visible:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 333);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 158;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxVisible
            // 
            this.comboBoxVisible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxVisible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxVisible.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxVisible.ForeColor = System.Drawing.Color.Black;
            this.comboBoxVisible.FormattingEnabled = true;
            this.comboBoxVisible.Items.AddRange(new object[] {
            "All",
            "Available",
            "Native"});
            this.comboBoxVisible.Location = new System.Drawing.Point(94, 115);
            this.comboBoxVisible.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxVisible.Name = "comboBoxVisible";
            this.comboBoxVisible.Size = new System.Drawing.Size(274, 28);
            this.comboBoxVisible.TabIndex = 166;
            // 
            // comboBoxOrder
            // 
            this.comboBoxOrder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxOrder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxOrder.ForeColor = System.Drawing.Color.Black;
            this.comboBoxOrder.FormattingEnabled = true;
            this.comboBoxOrder.Location = new System.Drawing.Point(94, 48);
            this.comboBoxOrder.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxOrder.Name = "comboBoxOrder";
            this.comboBoxOrder.Size = new System.Drawing.Size(274, 28);
            this.comboBoxOrder.TabIndex = 167;
            this.comboBoxOrder.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrder_SelectedIndexChanged);
            // 
            // checkedListBoxDexes
            // 
            this.checkedListBoxDexes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.checkedListBoxDexes.FormattingEnabled = true;
            this.checkedListBoxDexes.Location = new System.Drawing.Point(22, 169);
            this.checkedListBoxDexes.Name = "checkedListBoxDexes";
            this.checkedListBoxDexes.Size = new System.Drawing.Size(346, 142);
            this.checkedListBoxDexes.TabIndex = 168;
            // 
            // FormDexOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(412, 382);
            this.Controls.Add(this.checkedListBoxDexes);
            this.Controls.Add(this.comboBoxOrder);
            this.Controls.Add(this.comboBoxVisible);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelVisible);
            this.Controls.Add(this.labelOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormDexOrder";
            this.Text = "FormDexOrder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.Label labelVisible;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxVisible;
        private System.Windows.Forms.ComboBox comboBoxOrder;
        private System.Windows.Forms.CheckedListBox checkedListBoxDexes;
    }
}