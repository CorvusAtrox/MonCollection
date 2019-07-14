namespace MonCollection
{
    partial class FormGameTally
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
            this.listGames = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listGames
            // 
            this.listGames.AutoSize = true;
            this.listGames.Location = new System.Drawing.Point(12, 9);
            this.listGames.Name = "listGames";
            this.listGames.Size = new System.Drawing.Size(0, 13);
            this.listGames.TabIndex = 0;
            this.listGames.DoubleClick += new System.EventHandler(this.ListGames_DoubleClick);
            // 
            // FormGameTally
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(218)))), ((int)(((byte)(113)))));
            this.ClientSize = new System.Drawing.Size(253, 301);
            this.Controls.Add(this.listGames);
            this.Name = "FormGameTally";
            this.Text = "FormGameTally";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label listGames;
    }
}