namespace MonCollection
{
    partial class FormEditDexes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditDexes));
            this.labelOrder = new System.Windows.Forms.Label();
            this.comboBoxOrder = new System.Windows.Forms.ComboBox();
            this.buttonAddDex = new System.Windows.Forms.Button();
            this.buttonRemoveDex = new System.Windows.Forms.Button();
            this.buttonChangeTitle = new System.Windows.Forms.Button();
            this.comboBoxDexes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonRenameDex = new System.Windows.Forms.Button();
            this.textBoxSpecies = new System.Windows.Forms.TextBox();
            this.buttonCreateOrder = new System.Windows.Forms.Button();
            this.buttonDeleteOrder = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonAutoFill = new System.Windows.Forms.Button();
            this.listSpecies = new System.Windows.Forms.ListBox();
            this.buttonAddLine = new System.Windows.Forms.Button();
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
            // buttonAddDex
            // 
            this.buttonAddDex.Location = new System.Drawing.Point(485, 99);
            this.buttonAddDex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAddDex.Name = "buttonAddDex";
            this.buttonAddDex.Size = new System.Drawing.Size(129, 35);
            this.buttonAddDex.TabIndex = 169;
            this.buttonAddDex.Text = "Add Dex";
            this.buttonAddDex.UseVisualStyleBackColor = true;
            this.buttonAddDex.Click += new System.EventHandler(this.buttonAddDex_Click);
            // 
            // buttonRemoveDex
            // 
            this.buttonRemoveDex.Location = new System.Drawing.Point(622, 100);
            this.buttonRemoveDex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRemoveDex.Name = "buttonRemoveDex";
            this.buttonRemoveDex.Size = new System.Drawing.Size(124, 35);
            this.buttonRemoveDex.TabIndex = 170;
            this.buttonRemoveDex.Text = "Remove Dex";
            this.buttonRemoveDex.UseVisualStyleBackColor = true;
            // 
            // buttonChangeTitle
            // 
            this.buttonChangeTitle.Location = new System.Drawing.Point(375, 45);
            this.buttonChangeTitle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonChangeTitle.Name = "buttonChangeTitle";
            this.buttonChangeTitle.Size = new System.Drawing.Size(102, 35);
            this.buttonChangeTitle.TabIndex = 171;
            this.buttonChangeTitle.Text = "Rename";
            this.buttonChangeTitle.UseVisualStyleBackColor = true;
            // 
            // comboBoxDexes
            // 
            this.comboBoxDexes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxDexes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxDexes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxDexes.ForeColor = System.Drawing.Color.Black;
            this.comboBoxDexes.FormattingEnabled = true;
            this.comboBoxDexes.Location = new System.Drawing.Point(94, 103);
            this.comboBoxDexes.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxDexes.Name = "comboBoxDexes";
            this.comboBoxDexes.Size = new System.Drawing.Size(274, 28);
            this.comboBoxDexes.TabIndex = 173;
            this.comboBoxDexes.SelectedIndexChanged += new System.EventHandler(this.comboBoxDexes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(18, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 172;
            this.label1.Text = "Dex:";
            // 
            // buttonRenameDex
            // 
            this.buttonRenameDex.Location = new System.Drawing.Point(375, 100);
            this.buttonRenameDex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRenameDex.Name = "buttonRenameDex";
            this.buttonRenameDex.Size = new System.Drawing.Size(102, 35);
            this.buttonRenameDex.TabIndex = 174;
            this.buttonRenameDex.Text = "Rename";
            this.buttonRenameDex.UseVisualStyleBackColor = true;
            // 
            // textBoxSpecies
            // 
            this.textBoxSpecies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxSpecies.Location = new System.Drawing.Point(94, 154);
            this.textBoxSpecies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxSpecies.Multiline = true;
            this.textBoxSpecies.Name = "textBoxSpecies";
            this.textBoxSpecies.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSpecies.Size = new System.Drawing.Size(274, 153);
            this.textBoxSpecies.TabIndex = 175;
            // 
            // buttonCreateOrder
            // 
            this.buttonCreateOrder.Location = new System.Drawing.Point(485, 45);
            this.buttonCreateOrder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCreateOrder.Name = "buttonCreateOrder";
            this.buttonCreateOrder.Size = new System.Drawing.Size(129, 35);
            this.buttonCreateOrder.TabIndex = 176;
            this.buttonCreateOrder.Text = "Create Order";
            this.buttonCreateOrder.UseVisualStyleBackColor = true;
            this.buttonCreateOrder.Click += new System.EventHandler(this.buttonCreateOrder_Click);
            // 
            // buttonDeleteOrder
            // 
            this.buttonDeleteOrder.Location = new System.Drawing.Point(622, 45);
            this.buttonDeleteOrder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDeleteOrder.Name = "buttonDeleteOrder";
            this.buttonDeleteOrder.Size = new System.Drawing.Size(129, 35);
            this.buttonDeleteOrder.TabIndex = 177;
            this.buttonDeleteOrder.Text = "Delete Order";
            this.buttonDeleteOrder.UseVisualStyleBackColor = true;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(322, 318);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(129, 35);
            this.buttonSave.TabIndex = 178;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonAutoFill
            // 
            this.buttonAutoFill.Location = new System.Drawing.Point(380, 363);
            this.buttonAutoFill.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAutoFill.Name = "buttonAutoFill";
            this.buttonAutoFill.Size = new System.Drawing.Size(129, 35);
            this.buttonAutoFill.TabIndex = 179;
            this.buttonAutoFill.Text = "Auto-Fill";
            this.buttonAutoFill.UseVisualStyleBackColor = true;
            this.buttonAutoFill.Click += new System.EventHandler(this.buttonAutoFill_Click);
            // 
            // listSpecies
            // 
            this.listSpecies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.listSpecies.FormattingEnabled = true;
            this.listSpecies.ItemHeight = 20;
            this.listSpecies.Location = new System.Drawing.Point(380, 154);
            this.listSpecies.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listSpecies.Name = "listSpecies";
            this.listSpecies.ScrollAlwaysVisible = true;
            this.listSpecies.Size = new System.Drawing.Size(366, 144);
            this.listSpecies.TabIndex = 180;
            // 
            // buttonAddLine
            // 
            this.buttonAddLine.Location = new System.Drawing.Point(242, 363);
            this.buttonAddLine.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAddLine.Name = "buttonAddLine";
            this.buttonAddLine.Size = new System.Drawing.Size(129, 35);
            this.buttonAddLine.TabIndex = 181;
            this.buttonAddLine.Text = "Add";
            this.buttonAddLine.UseVisualStyleBackColor = true;
            this.buttonAddLine.Click += new System.EventHandler(this.buttonAddLine_Click);
            // 
            // FormEditDexes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(776, 455);
            this.Controls.Add(this.buttonAddLine);
            this.Controls.Add(this.listSpecies);
            this.Controls.Add(this.buttonAutoFill);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDeleteOrder);
            this.Controls.Add(this.buttonCreateOrder);
            this.Controls.Add(this.textBoxSpecies);
            this.Controls.Add(this.buttonRenameDex);
            this.Controls.Add(this.comboBoxDexes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonChangeTitle);
            this.Controls.Add(this.buttonRemoveDex);
            this.Controls.Add(this.buttonAddDex);
            this.Controls.Add(this.comboBoxOrder);
            this.Controls.Add(this.labelOrder);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormEditDexes";
            this.Text = "FormEditDexes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelOrder;
        private System.Windows.Forms.ComboBox comboBoxOrder;
        private System.Windows.Forms.Button buttonAddDex;
        private System.Windows.Forms.Button buttonRemoveDex;
        private System.Windows.Forms.Button buttonChangeTitle;
        private System.Windows.Forms.ComboBox comboBoxDexes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRenameDex;
        private System.Windows.Forms.TextBox textBoxSpecies;
        private System.Windows.Forms.Button buttonCreateOrder;
        private System.Windows.Forms.Button buttonDeleteOrder;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAutoFill;
        private System.Windows.Forms.ListBox listSpecies;
        private System.Windows.Forms.Button buttonAddLine;
    }
}