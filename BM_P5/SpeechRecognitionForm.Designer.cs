namespace BM_P5
{
    partial class SpeechRecognitionForm
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
            System.Windows.Forms.Button MagicButton;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeechRecognitionForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.CostLabel = new System.Windows.Forms.Label();
            this.CostTextBox = new System.Windows.Forms.TextBox();
            this.DirectoryDatabaseComparisonButton = new System.Windows.Forms.Button();
            this.LocalMatrixPBox = new System.Windows.Forms.PictureBox();
            this.GlobalMatrixPBox = new System.Windows.Forms.PictureBox();
            this.PlayButton_Track2 = new System.Windows.Forms.Button();
            this.PlayButton_Track1 = new System.Windows.Forms.Button();
            this.LoadButton_Track2 = new System.Windows.Forms.Button();
            this.LoadButton_Track1 = new System.Windows.Forms.Button();
            this.Label_OfTrack2 = new System.Windows.Forms.Label();
            this.Label_OfTrack1 = new System.Windows.Forms.Label();
            this.SaveToFileCheckBox = new System.Windows.Forms.CheckBox();
            MagicButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalMatrixPBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalMatrixPBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SaveToFileCheckBox);
            this.panel1.Controls.Add(this.CostLabel);
            this.panel1.Controls.Add(this.CostTextBox);
            this.panel1.Controls.Add(this.DirectoryDatabaseComparisonButton);
            this.panel1.Controls.Add(this.LocalMatrixPBox);
            this.panel1.Controls.Add(this.GlobalMatrixPBox);
            this.panel1.Controls.Add(MagicButton);
            this.panel1.Controls.Add(this.PlayButton_Track2);
            this.panel1.Controls.Add(this.PlayButton_Track1);
            this.panel1.Controls.Add(this.LoadButton_Track2);
            this.panel1.Controls.Add(this.LoadButton_Track1);
            this.panel1.Controls.Add(this.Label_OfTrack2);
            this.panel1.Controls.Add(this.Label_OfTrack1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(887, 558);
            this.panel1.TabIndex = 0;
            // 
            // CostLabel
            // 
            this.CostLabel.AutoSize = true;
            this.CostLabel.Location = new System.Drawing.Point(183, 30);
            this.CostLabel.Name = "CostLabel";
            this.CostLabel.Size = new System.Drawing.Size(31, 13);
            this.CostLabel.TabIndex = 18;
            this.CostLabel.Text = "Cost:";
            // 
            // CostTextBox
            // 
            this.CostTextBox.Location = new System.Drawing.Point(220, 28);
            this.CostTextBox.Name = "CostTextBox";
            this.CostTextBox.Size = new System.Drawing.Size(100, 20);
            this.CostTextBox.TabIndex = 17;
            // 
            // DirectoryDatabaseComparisonButton
            // 
            this.DirectoryDatabaseComparisonButton.Location = new System.Drawing.Point(356, 21);
            this.DirectoryDatabaseComparisonButton.Name = "DirectoryDatabaseComparisonButton";
            this.DirectoryDatabaseComparisonButton.Size = new System.Drawing.Size(150, 32);
            this.DirectoryDatabaseComparisonButton.TabIndex = 15;
            this.DirectoryDatabaseComparisonButton.Text = "Compare with collected data";
            this.DirectoryDatabaseComparisonButton.UseVisualStyleBackColor = true;
            this.DirectoryDatabaseComparisonButton.Click += new System.EventHandler(this.CompareWithDatabaseData_Click);
            // 
            // LocalMatrixPBox
            // 
            this.LocalMatrixPBox.Location = new System.Drawing.Point(25, 142);
            this.LocalMatrixPBox.Name = "LocalMatrixPBox";
            this.LocalMatrixPBox.Size = new System.Drawing.Size(400, 400);
            this.LocalMatrixPBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.LocalMatrixPBox.TabIndex = 13;
            this.LocalMatrixPBox.TabStop = false;
            this.LocalMatrixPBox.Click += new System.EventHandler(this.CopyToClipboard_LocalCost);
            // 
            // GlobalMatrixPBox
            // 
            this.GlobalMatrixPBox.Location = new System.Drawing.Point(458, 142);
            this.GlobalMatrixPBox.Name = "GlobalMatrixPBox";
            this.GlobalMatrixPBox.Size = new System.Drawing.Size(400, 400);
            this.GlobalMatrixPBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.GlobalMatrixPBox.TabIndex = 12;
            this.GlobalMatrixPBox.TabStop = false;
            this.GlobalMatrixPBox.Click += new System.EventHandler(this.CopyToClipboard_GlobalCost);
            // 
            // MagicButton
            // 
            MagicButton.Location = new System.Drawing.Point(27, 21);
            MagicButton.Name = "MagicButton";
            MagicButton.Size = new System.Drawing.Size(150, 32);
            MagicButton.TabIndex = 9;
            MagicButton.Text = "Compare loaded tracks";
            MagicButton.UseVisualStyleBackColor = true;
            MagicButton.Click += new System.EventHandler(this.CompareTracks_1_2_Click);
            // 
            // PlayButton_Track2
            // 
            this.PlayButton_Track2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayButton_Track2.BackgroundImage")));
            this.PlayButton_Track2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayButton_Track2.Location = new System.Drawing.Point(108, 113);
            this.PlayButton_Track2.Name = "PlayButton_Track2";
            this.PlayButton_Track2.Size = new System.Drawing.Size(23, 23);
            this.PlayButton_Track2.TabIndex = 8;
            this.PlayButton_Track2.Text = " ";
            this.PlayButton_Track2.UseVisualStyleBackColor = true;
            this.PlayButton_Track2.Click += new System.EventHandler(this.PlayButton_Track2_Click);
            // 
            // PlayButton_Track1
            // 
            this.PlayButton_Track1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayButton_Track1.BackgroundImage")));
            this.PlayButton_Track1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayButton_Track1.Location = new System.Drawing.Point(108, 73);
            this.PlayButton_Track1.Name = "PlayButton_Track1";
            this.PlayButton_Track1.Size = new System.Drawing.Size(23, 23);
            this.PlayButton_Track1.TabIndex = 7;
            this.PlayButton_Track1.Text = " ";
            this.PlayButton_Track1.UseVisualStyleBackColor = true;
            this.PlayButton_Track1.Click += new System.EventHandler(this.PlayButton_Track1_Click);
            // 
            // LoadButton_Track2
            // 
            this.LoadButton_Track2.Location = new System.Drawing.Point(27, 113);
            this.LoadButton_Track2.Name = "LoadButton_Track2";
            this.LoadButton_Track2.Size = new System.Drawing.Size(75, 23);
            this.LoadButton_Track2.TabIndex = 6;
            this.LoadButton_Track2.Text = "Load";
            this.LoadButton_Track2.UseVisualStyleBackColor = true;
            this.LoadButton_Track2.Click += new System.EventHandler(this.LoadButton_Track2_Click);
            // 
            // LoadButton_Track1
            // 
            this.LoadButton_Track1.Location = new System.Drawing.Point(27, 73);
            this.LoadButton_Track1.Name = "LoadButton_Track1";
            this.LoadButton_Track1.Size = new System.Drawing.Size(75, 23);
            this.LoadButton_Track1.TabIndex = 5;
            this.LoadButton_Track1.Text = "Load";
            this.LoadButton_Track1.UseVisualStyleBackColor = true;
            this.LoadButton_Track1.Click += new System.EventHandler(this.LoadButton_Track1_Click);
            // 
            // Label_OfTrack2
            // 
            this.Label_OfTrack2.AutoSize = true;
            this.Label_OfTrack2.Location = new System.Drawing.Point(137, 118);
            this.Label_OfTrack2.Name = "Label_OfTrack2";
            this.Label_OfTrack2.Size = new System.Drawing.Size(44, 13);
            this.Label_OfTrack2.TabIndex = 3;
            this.Label_OfTrack2.Text = "Track 2";
            // 
            // Label_OfTrack1
            // 
            this.Label_OfTrack1.AutoSize = true;
            this.Label_OfTrack1.Location = new System.Drawing.Point(137, 78);
            this.Label_OfTrack1.Name = "Label_OfTrack1";
            this.Label_OfTrack1.Size = new System.Drawing.Size(44, 13);
            this.Label_OfTrack1.TabIndex = 2;
            this.Label_OfTrack1.Text = "Track 1";
            // 
            // SaveToFileCheckBox
            // 
            this.SaveToFileCheckBox.AutoSize = true;
            this.SaveToFileCheckBox.Location = new System.Drawing.Point(512, 29);
            this.SaveToFileCheckBox.Name = "SaveToFileCheckBox";
            this.SaveToFileCheckBox.Size = new System.Drawing.Size(112, 17);
            this.SaveToFileCheckBox.TabIndex = 19;
            this.SaveToFileCheckBox.Text = "Save results to file";
            this.SaveToFileCheckBox.UseVisualStyleBackColor = true;
            // 
            // SpeechRecognitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 558);
            this.Controls.Add(this.panel1);
            this.Name = "SpeechRecognitionForm";
            this.Text = "Speech recognition - DTW";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LocalMatrixPBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GlobalMatrixPBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Label_OfTrack2;
        private System.Windows.Forms.Label Label_OfTrack1;
        private System.Windows.Forms.Button LoadButton_Track2;
        private System.Windows.Forms.Button LoadButton_Track1;
        private System.Windows.Forms.Button PlayButton_Track2;
        private System.Windows.Forms.Button PlayButton_Track1;
        private System.Windows.Forms.PictureBox LocalMatrixPBox;
        private System.Windows.Forms.PictureBox GlobalMatrixPBox;
        private System.Windows.Forms.Button DirectoryDatabaseComparisonButton;
        private System.Windows.Forms.Label CostLabel;
        private System.Windows.Forms.TextBox CostTextBox;
        private System.Windows.Forms.CheckBox SaveToFileCheckBox;
    }
}

