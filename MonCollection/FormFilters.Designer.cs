namespace MonCollection
{
    partial class FormFilters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFilters));
            this.listViewFilters = new System.Windows.Forms.ListView();
            this.columnHeaderItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVisible = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button1 = new System.Windows.Forms.Button();
            this.buttonSelectAll = new System.Windows.Forms.Button();
            this.buttonClearAll = new System.Windows.Forms.Button();
            this.buttonInvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewFilters
            // 
            this.listViewFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.listViewFilters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderItem,
            this.columnHeaderVisible});
            this.listViewFilters.HideSelection = false;
            this.listViewFilters.Location = new System.Drawing.Point(13, 13);
            this.listViewFilters.Name = "listViewFilters";
            this.listViewFilters.Size = new System.Drawing.Size(228, 200);
            this.listViewFilters.TabIndex = 0;
            this.listViewFilters.UseCompatibleStateImageBehavior = false;
            this.listViewFilters.View = System.Windows.Forms.View.Details;
            this.listViewFilters.Click += new System.EventHandler(this.ListViewFilters_Click);
            // 
            // columnHeaderItem
            // 
            this.columnHeaderItem.Text = "Entry";
            this.columnHeaderItem.Width = 163;
            // 
            // columnHeaderVisible
            // 
            this.columnHeaderVisible.Text = "Show?";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(88, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // buttonSelectAll
            // 
            this.buttonSelectAll.Location = new System.Drawing.Point(12, 219);
            this.buttonSelectAll.Name = "buttonSelectAll";
            this.buttonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectAll.TabIndex = 2;
            this.buttonSelectAll.Text = "Select All";
            this.buttonSelectAll.UseVisualStyleBackColor = true;
            this.buttonSelectAll.Click += new System.EventHandler(this.ButtonSelectAll_Click);
            // 
            // buttonClearAll
            // 
            this.buttonClearAll.Location = new System.Drawing.Point(166, 219);
            this.buttonClearAll.Name = "buttonClearAll";
            this.buttonClearAll.Size = new System.Drawing.Size(75, 23);
            this.buttonClearAll.TabIndex = 3;
            this.buttonClearAll.Text = "Clear All";
            this.buttonClearAll.UseVisualStyleBackColor = true;
            this.buttonClearAll.Click += new System.EventHandler(this.ButtonClearAll_Click);
            // 
            // buttonInvert
            // 
            this.buttonInvert.Location = new System.Drawing.Point(88, 237);
            this.buttonInvert.Name = "buttonInvert";
            this.buttonInvert.Size = new System.Drawing.Size(75, 23);
            this.buttonInvert.TabIndex = 4;
            this.buttonInvert.Text = "Invert";
            this.buttonInvert.UseVisualStyleBackColor = true;
            this.buttonInvert.Click += new System.EventHandler(this.ButtonInvert_Click);
            // 
            // FormFilters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(253, 301);
            this.Controls.Add(this.buttonInvert);
            this.Controls.Add(this.buttonClearAll);
            this.Controls.Add(this.buttonSelectAll);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listViewFilters);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFilters";
            this.Text = "FormFilters";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewFilters;
        private System.Windows.Forms.ColumnHeader columnHeaderItem;
        private System.Windows.Forms.ColumnHeader columnHeaderVisible;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonSelectAll;
        private System.Windows.Forms.Button buttonClearAll;
        private System.Windows.Forms.Button buttonInvert;
    }
}