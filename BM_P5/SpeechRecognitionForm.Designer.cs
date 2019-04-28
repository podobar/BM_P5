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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeechRecognitionForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.PlayButton_Track2 = new System.Windows.Forms.Button();
            this.PlayButton_Track1 = new System.Windows.Forms.Button();
            this.waveViewer1 = new NAudio.Gui.WaveViewer();
            this.LoadButton_Track2 = new System.Windows.Forms.Button();
            this.LoadButton_Track1 = new System.Windows.Forms.Button();
            this.Label_OfTrack2 = new System.Windows.Forms.Label();
            this.Label_OfTrack1 = new System.Windows.Forms.Label();
            this.waveViewer2 = new NAudio.Gui.WaveViewer();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.PlayButton_Track2);
            this.panel1.Controls.Add(this.PlayButton_Track1);
            this.panel1.Controls.Add(this.waveViewer1);
            this.panel1.Controls.Add(this.LoadButton_Track2);
            this.panel1.Controls.Add(this.LoadButton_Track1);
            this.panel1.Controls.Add(this.Label_OfTrack2);
            this.panel1.Controls.Add(this.Label_OfTrack1);
            this.panel1.Controls.Add(this.waveViewer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 450);
            this.panel1.TabIndex = 0;
            // 
            // PlayButton_Track2
            // 
            this.PlayButton_Track2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PlayButton_Track2.BackgroundImage")));
            this.PlayButton_Track2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayButton_Track2.Location = new System.Drawing.Point(106, 323);
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
            this.PlayButton_Track1.Location = new System.Drawing.Point(104, 198);
            this.PlayButton_Track1.Name = "PlayButton_Track1";
            this.PlayButton_Track1.Size = new System.Drawing.Size(23, 23);
            this.PlayButton_Track1.TabIndex = 7;
            this.PlayButton_Track1.Text = " ";
            this.PlayButton_Track1.UseVisualStyleBackColor = true;
            this.PlayButton_Track1.Click += new System.EventHandler(this.PlayButton_Track1_Click);
            // 
            // waveViewer1
            // 
            this.waveViewer1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.waveViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveViewer1.Location = new System.Drawing.Point(25, 227);
            this.waveViewer1.Name = "waveViewer1";
            this.waveViewer1.SamplesPerPixel = 128;
            this.waveViewer1.Size = new System.Drawing.Size(750, 90);
            this.waveViewer1.StartPosition = ((long)(0));
            this.waveViewer1.TabIndex = 2;
            this.waveViewer1.WaveStream = null;
            // 
            // LoadButton_Track2
            // 
            this.LoadButton_Track2.Location = new System.Drawing.Point(25, 323);
            this.LoadButton_Track2.Name = "LoadButton_Track2";
            this.LoadButton_Track2.Size = new System.Drawing.Size(75, 23);
            this.LoadButton_Track2.TabIndex = 6;
            this.LoadButton_Track2.Text = "Load";
            this.LoadButton_Track2.UseVisualStyleBackColor = true;
            this.LoadButton_Track2.Click += new System.EventHandler(this.LoadButton_Track2_Click);
            // 
            // LoadButton_Track1
            // 
            this.LoadButton_Track1.Location = new System.Drawing.Point(25, 198);
            this.LoadButton_Track1.Name = "LoadButton_Track1";
            this.LoadButton_Track1.Size = new System.Drawing.Size(73, 23);
            this.LoadButton_Track1.TabIndex = 5;
            this.LoadButton_Track1.Text = "Load";
            this.LoadButton_Track1.UseVisualStyleBackColor = true;
            this.LoadButton_Track1.Click += new System.EventHandler(this.LoadButton_Track1_Click);
            // 
            // Label_OfTrack2
            // 
            this.Label_OfTrack2.AutoSize = true;
            this.Label_OfTrack2.Location = new System.Drawing.Point(135, 328);
            this.Label_OfTrack2.Name = "Label_OfTrack2";
            this.Label_OfTrack2.Size = new System.Drawing.Size(44, 13);
            this.Label_OfTrack2.TabIndex = 3;
            this.Label_OfTrack2.Text = "Track 2";
            // 
            // Label_OfTrack1
            // 
            this.Label_OfTrack1.AutoSize = true;
            this.Label_OfTrack1.Location = new System.Drawing.Point(135, 203);
            this.Label_OfTrack1.Name = "Label_OfTrack1";
            this.Label_OfTrack1.Size = new System.Drawing.Size(44, 13);
            this.Label_OfTrack1.TabIndex = 2;
            this.Label_OfTrack1.Text = "Track 1";
            // 
            // waveViewer2
            // 
            this.waveViewer2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.waveViewer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.waveViewer2.Location = new System.Drawing.Point(25, 351);
            this.waveViewer2.Name = "waveViewer2";
            this.waveViewer2.SamplesPerPixel = 128;
            this.waveViewer2.Size = new System.Drawing.Size(750, 90);
            this.waveViewer2.StartPosition = ((long)(0));
            this.waveViewer2.TabIndex = 1;
            this.waveViewer2.WaveStream = null;
            // 
            // SpeechRecognitionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "SpeechRecognitionForm";
            this.Text = "Speech recognition - DTW";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private NAudio.Gui.WaveViewer waveViewer2;
        private System.Windows.Forms.Label Label_OfTrack2;
        private System.Windows.Forms.Label Label_OfTrack1;
        private System.Windows.Forms.Button LoadButton_Track2;
        private System.Windows.Forms.Button LoadButton_Track1;
        private NAudio.Gui.WaveViewer waveViewer1;
        private System.Windows.Forms.Button PlayButton_Track2;
        private System.Windows.Forms.Button PlayButton_Track1;
    }
}

