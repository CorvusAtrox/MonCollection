namespace MonCollection
{
    partial class FormMovepools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMovepools));
            button1 = new System.Windows.Forms.Button();
            buttonAdd = new System.Windows.Forms.Button();
            buttonRemove = new System.Windows.Forms.Button();
            comboBoxMove4 = new System.Windows.Forms.ComboBox();
            comboBoxMove3 = new System.Windows.Forms.ComboBox();
            comboBoxMove2 = new System.Windows.Forms.ComboBox();
            comboBoxMove1 = new System.Windows.Forms.ComboBox();
            labelMoves = new System.Windows.Forms.Label();
            comboBoxCategory = new System.Windows.Forms.ComboBox();
            labelCategory = new System.Windows.Forms.Label();
            buttonSetCategory = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            listBoxMoves = new System.Windows.Forms.ListBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(207, 315);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(100, 35);
            button1.TabIndex = 2;
            button1.Text = "OK";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Location = new System.Drawing.Point(207, 251);
            buttonAdd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new System.Drawing.Size(100, 35);
            buttonAdd.TabIndex = 4;
            buttonAdd.Text = "Add";
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Visible = false;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // buttonRemove
            // 
            buttonRemove.Location = new System.Drawing.Point(355, 251);
            buttonRemove.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonRemove.Name = "buttonRemove";
            buttonRemove.Size = new System.Drawing.Size(100, 35);
            buttonRemove.TabIndex = 5;
            buttonRemove.Text = "Remove";
            buttonRemove.UseVisualStyleBackColor = true;
            buttonRemove.Visible = false;
            buttonRemove.Click += buttonRemove_Click;
            // 
            // comboBoxMove4
            // 
            comboBoxMove4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            comboBoxMove4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxMove4.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            comboBoxMove4.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            comboBoxMove4.ForeColor = System.Drawing.Color.Black;
            comboBoxMove4.FormattingEnabled = true;
            comboBoxMove4.Location = new System.Drawing.Point(14, 213);
            comboBoxMove4.Margin = new System.Windows.Forms.Padding(0);
            comboBoxMove4.Name = "comboBoxMove4";
            comboBoxMove4.Size = new System.Drawing.Size(153, 28);
            comboBoxMove4.TabIndex = 33;
            // 
            // comboBoxMove3
            // 
            comboBoxMove3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            comboBoxMove3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxMove3.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            comboBoxMove3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            comboBoxMove3.ForeColor = System.Drawing.Color.Black;
            comboBoxMove3.FormattingEnabled = true;
            comboBoxMove3.Location = new System.Drawing.Point(14, 181);
            comboBoxMove3.Margin = new System.Windows.Forms.Padding(0);
            comboBoxMove3.Name = "comboBoxMove3";
            comboBoxMove3.Size = new System.Drawing.Size(153, 28);
            comboBoxMove3.TabIndex = 32;
            // 
            // comboBoxMove2
            // 
            comboBoxMove2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            comboBoxMove2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxMove2.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            comboBoxMove2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            comboBoxMove2.ForeColor = System.Drawing.Color.Black;
            comboBoxMove2.FormattingEnabled = true;
            comboBoxMove2.Location = new System.Drawing.Point(14, 147);
            comboBoxMove2.Margin = new System.Windows.Forms.Padding(0);
            comboBoxMove2.Name = "comboBoxMove2";
            comboBoxMove2.Size = new System.Drawing.Size(153, 28);
            comboBoxMove2.TabIndex = 31;
            // 
            // comboBoxMove1
            // 
            comboBoxMove1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            comboBoxMove1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxMove1.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            comboBoxMove1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            comboBoxMove1.ForeColor = System.Drawing.Color.Black;
            comboBoxMove1.FormattingEnabled = true;
            comboBoxMove1.Location = new System.Drawing.Point(14, 115);
            comboBoxMove1.Margin = new System.Windows.Forms.Padding(0);
            comboBoxMove1.Name = "comboBoxMove1";
            comboBoxMove1.Size = new System.Drawing.Size(153, 28);
            comboBoxMove1.TabIndex = 30;
            // 
            // labelMoves
            // 
            labelMoves.AutoSize = true;
            labelMoves.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            labelMoves.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            labelMoves.ForeColor = System.Drawing.Color.Black;
            labelMoves.Location = new System.Drawing.Point(14, 88);
            labelMoves.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelMoves.Name = "labelMoves";
            labelMoves.Size = new System.Drawing.Size(59, 17);
            labelMoves.TabIndex = 34;
            labelMoves.Text = "Moves:";
            // 
            // comboBoxCategory
            // 
            comboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            comboBoxCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            comboBoxCategory.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            comboBoxCategory.ForeColor = System.Drawing.Color.Black;
            comboBoxCategory.FormattingEnabled = true;
            comboBoxCategory.Location = new System.Drawing.Point(92, 27);
            comboBoxCategory.Margin = new System.Windows.Forms.Padding(0);
            comboBoxCategory.Name = "comboBoxCategory";
            comboBoxCategory.Size = new System.Drawing.Size(244, 28);
            comboBoxCategory.TabIndex = 182;
            comboBoxCategory.SelectedIndexChanged += comboBoxCategory_SelectedIndexChanged;
            // 
            // labelCategory
            // 
            labelCategory.AutoSize = true;
            labelCategory.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            labelCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            labelCategory.ForeColor = System.Drawing.Color.Black;
            labelCategory.Location = new System.Drawing.Point(10, 32);
            labelCategory.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelCategory.Name = "labelCategory";
            labelCategory.Size = new System.Drawing.Size(78, 17);
            labelCategory.TabIndex = 183;
            labelCategory.Text = "Category:";
            // 
            // buttonSetCategory
            // 
            buttonSetCategory.Location = new System.Drawing.Point(340, 23);
            buttonSetCategory.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonSetCategory.Name = "buttonSetCategory";
            buttonSetCategory.Size = new System.Drawing.Size(115, 35);
            buttonSetCategory.TabIndex = 184;
            buttonSetCategory.Text = "Set Category";
            buttonSetCategory.UseVisualStyleBackColor = true;
            buttonSetCategory.Click += buttonSetCategory_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new System.Drawing.Point(30, 251);
            buttonSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(115, 35);
            buttonSave.TabIndex = 185;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // listBoxMoves
            // 
            listBoxMoves.BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            listBoxMoves.FormattingEnabled = true;
            listBoxMoves.ItemHeight = 20;
            listBoxMoves.Location = new System.Drawing.Point(207, 115);
            listBoxMoves.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            listBoxMoves.MultiColumn = true;
            listBoxMoves.Name = "listBoxMoves";
            listBoxMoves.Size = new System.Drawing.Size(248, 124);
            listBoxMoves.Sorted = true;
            listBoxMoves.TabIndex = 186;
            // 
            // FormMovepools
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(223, 199, 251);
            ClientSize = new System.Drawing.Size(479, 364);
            Controls.Add(listBoxMoves);
            Controls.Add(buttonSave);
            Controls.Add(buttonSetCategory);
            Controls.Add(labelCategory);
            Controls.Add(comboBoxCategory);
            Controls.Add(labelMoves);
            Controls.Add(comboBoxMove4);
            Controls.Add(comboBoxMove3);
            Controls.Add(comboBoxMove2);
            Controls.Add(comboBoxMove1);
            Controls.Add(buttonRemove);
            Controls.Add(buttonAdd);
            Controls.Add(button1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "FormMovepools";
            Text = "Movepools";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ComboBox comboBoxMove4;
        private System.Windows.Forms.ComboBox comboBoxMove3;
        private System.Windows.Forms.ComboBox comboBoxMove2;
        private System.Windows.Forms.ComboBox comboBoxMove1;
        private System.Windows.Forms.Label labelMoves;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCategory;
        private System.Windows.Forms.Button buttonSetCategory;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ListBox listBoxMoves;
    }
}