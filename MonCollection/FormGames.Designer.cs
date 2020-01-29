namespace MonCollection
{
    partial class FormGames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGames));
            this.comboBoxGame = new System.Windows.Forms.ComboBox();
            this.labelGame = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxOT = new System.Windows.Forms.TextBox();
            this.labelOT = new System.Windows.Forms.Label();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.labelLanguage = new System.Windows.Forms.Label();
            this.labelOrigin = new System.Windows.Forms.Label();
            this.comboBoxVersion = new System.Windows.Forms.ComboBox();
            this.buttonChangeTitle = new System.Windows.Forms.Button();
            this.buttonAddGame = new System.Windows.Forms.Button();
            this.buttonRemoveGame = new System.Windows.Forms.Button();
            this.buttonSaveGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxGame
            // 
            this.comboBoxGame.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxGame.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxGame.ForeColor = System.Drawing.Color.Black;
            this.comboBoxGame.FormattingEnabled = true;
            this.comboBoxGame.Location = new System.Drawing.Point(58, 31);
            this.comboBoxGame.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxGame.Name = "comboBoxGame";
            this.comboBoxGame.Size = new System.Drawing.Size(180, 21);
            this.comboBoxGame.TabIndex = 157;
            this.comboBoxGame.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGame_SelectedIndexChanged);
            // 
            // labelGame
            // 
            this.labelGame.AutoSize = true;
            this.labelGame.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGame.ForeColor = System.Drawing.Color.Black;
            this.labelGame.Location = new System.Drawing.Point(12, 34);
            this.labelGame.Name = "labelGame";
            this.labelGame.Size = new System.Drawing.Size(43, 13);
            this.labelGame.TabIndex = 156;
            this.labelGame.Text = "Game:";
            // 
            // textBoxID
            // 
            this.textBoxID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxID.ForeColor = System.Drawing.Color.Black;
            this.textBoxID.Location = new System.Drawing.Point(172, 126);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(66, 20);
            this.textBoxID.TabIndex = 162;
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelID.ForeColor = System.Drawing.Color.Black;
            this.labelID.Location = new System.Drawing.Point(142, 128);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(24, 13);
            this.labelID.TabIndex = 161;
            this.labelID.Text = "ID:";
            // 
            // textBoxOT
            // 
            this.textBoxOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.textBoxOT.ForeColor = System.Drawing.Color.Black;
            this.textBoxOT.Location = new System.Drawing.Point(46, 125);
            this.textBoxOT.Name = "textBoxOT";
            this.textBoxOT.Size = new System.Drawing.Size(90, 20);
            this.textBoxOT.TabIndex = 160;
            // 
            // labelOT
            // 
            this.labelOT.AutoSize = true;
            this.labelOT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOT.ForeColor = System.Drawing.Color.Black;
            this.labelOT.Location = new System.Drawing.Point(12, 126);
            this.labelOT.Name = "labelOT";
            this.labelOT.Size = new System.Drawing.Size(28, 13);
            this.labelOT.TabIndex = 159;
            this.labelOT.Text = "OT:";
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxLanguage.ForeColor = System.Drawing.Color.Black;
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(82, 162);
            this.comboBoxLanguage.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(99, 21);
            this.comboBoxLanguage.TabIndex = 164;
            // 
            // labelLanguage
            // 
            this.labelLanguage.AutoSize = true;
            this.labelLanguage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLanguage.ForeColor = System.Drawing.Color.Black;
            this.labelLanguage.Location = new System.Drawing.Point(12, 166);
            this.labelLanguage.Name = "labelLanguage";
            this.labelLanguage.Size = new System.Drawing.Size(67, 13);
            this.labelLanguage.TabIndex = 163;
            this.labelLanguage.Text = "Language:";
            // 
            // labelOrigin
            // 
            this.labelOrigin.AutoSize = true;
            this.labelOrigin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.labelOrigin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrigin.ForeColor = System.Drawing.Color.Black;
            this.labelOrigin.Location = new System.Drawing.Point(12, 83);
            this.labelOrigin.Name = "labelOrigin";
            this.labelOrigin.Size = new System.Drawing.Size(53, 13);
            this.labelOrigin.TabIndex = 166;
            this.labelOrigin.Text = "Version:";
            // 
            // comboBoxVersion
            // 
            this.comboBoxVersion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxVersion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.comboBoxVersion.ForeColor = System.Drawing.Color.Black;
            this.comboBoxVersion.FormattingEnabled = true;
            this.comboBoxVersion.Location = new System.Drawing.Point(68, 80);
            this.comboBoxVersion.Margin = new System.Windows.Forms.Padding(0);
            this.comboBoxVersion.Name = "comboBoxVersion";
            this.comboBoxVersion.Size = new System.Drawing.Size(115, 21);
            this.comboBoxVersion.TabIndex = 165;
            // 
            // buttonChangeTitle
            // 
            this.buttonChangeTitle.Location = new System.Drawing.Point(243, 29);
            this.buttonChangeTitle.Name = "buttonChangeTitle";
            this.buttonChangeTitle.Size = new System.Drawing.Size(68, 23);
            this.buttonChangeTitle.TabIndex = 167;
            this.buttonChangeTitle.Text = "Change";
            this.buttonChangeTitle.UseVisualStyleBackColor = true;
            this.buttonChangeTitle.Click += new System.EventHandler(this.ButtonChangeTitle_Click);
            // 
            // buttonAddGame
            // 
            this.buttonAddGame.Location = new System.Drawing.Point(58, 253);
            this.buttonAddGame.Name = "buttonAddGame";
            this.buttonAddGame.Size = new System.Drawing.Size(68, 23);
            this.buttonAddGame.TabIndex = 168;
            this.buttonAddGame.Text = "Add";
            this.buttonAddGame.UseVisualStyleBackColor = true;
            this.buttonAddGame.Click += new System.EventHandler(this.ButtonAddGame_Click);
            // 
            // buttonRemoveGame
            // 
            this.buttonRemoveGame.Location = new System.Drawing.Point(170, 253);
            this.buttonRemoveGame.Name = "buttonRemoveGame";
            this.buttonRemoveGame.Size = new System.Drawing.Size(68, 23);
            this.buttonRemoveGame.TabIndex = 169;
            this.buttonRemoveGame.Text = "Remove";
            this.buttonRemoveGame.UseVisualStyleBackColor = true;
            this.buttonRemoveGame.Click += new System.EventHandler(this.ButtonRemoveGame_Click);
            // 
            // buttonSaveGame
            // 
            this.buttonSaveGame.Location = new System.Drawing.Point(113, 213);
            this.buttonSaveGame.Name = "buttonSaveGame";
            this.buttonSaveGame.Size = new System.Drawing.Size(68, 23);
            this.buttonSaveGame.TabIndex = 170;
            this.buttonSaveGame.Text = "Save";
            this.buttonSaveGame.UseVisualStyleBackColor = true;
            this.buttonSaveGame.Click += new System.EventHandler(this.ButtonSaveGame_Click);
            // 
            // FormGames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(199)))), ((int)(((byte)(251)))));
            this.ClientSize = new System.Drawing.Size(319, 288);
            this.Controls.Add(this.buttonSaveGame);
            this.Controls.Add(this.buttonRemoveGame);
            this.Controls.Add(this.buttonAddGame);
            this.Controls.Add(this.buttonChangeTitle);
            this.Controls.Add(this.labelOrigin);
            this.Controls.Add(this.comboBoxVersion);
            this.Controls.Add(this.comboBoxLanguage);
            this.Controls.Add(this.labelLanguage);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.textBoxOT);
            this.Controls.Add(this.labelOT);
            this.Controls.Add(this.comboBoxGame);
            this.Controls.Add(this.labelGame);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGames";
            this.Text = "FormGames";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxGame;
        private System.Windows.Forms.Label labelGame;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.TextBox textBoxOT;
        private System.Windows.Forms.Label labelOT;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label labelLanguage;
        private System.Windows.Forms.Label labelOrigin;
        private System.Windows.Forms.ComboBox comboBoxVersion;
        private System.Windows.Forms.Button buttonChangeTitle;
        private System.Windows.Forms.Button buttonAddGame;
        private System.Windows.Forms.Button buttonRemoveGame;
        private System.Windows.Forms.Button buttonSaveGame;
    }
}