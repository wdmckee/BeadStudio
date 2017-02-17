namespace BeadStudio
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox_Colors = new System.Windows.Forms.PictureBox();
            this.print_btn = new System.Windows.Forms.Button();
            this.checkBox_Gridlines = new System.Windows.Forms.CheckBox();
            this.checkBox_Pixelated = new System.Windows.Forms.CheckBox();
            this.Load_Picture = new System.Windows.Forms.Button();
            this.pictureBoxImg = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Colors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox_Colors);
            this.splitContainer1.Panel1.Controls.Add(this.print_btn);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_Gridlines);
            this.splitContainer1.Panel1.Controls.Add(this.checkBox_Pixelated);
            this.splitContainer1.Panel1.Controls.Add(this.Load_Picture);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.splitContainer1.Panel2.Controls.Add(this.pictureBoxImg);
            this.splitContainer1.Size = new System.Drawing.Size(1794, 1767);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;
            // 
            // pictureBox_Colors
            // 
            this.pictureBox_Colors.Location = new System.Drawing.Point(26, 300);
            this.pictureBox_Colors.Name = "pictureBox_Colors";
            this.pictureBox_Colors.Size = new System.Drawing.Size(180, 600);
            this.pictureBox_Colors.TabIndex = 10;
            this.pictureBox_Colors.TabStop = false;
            this.pictureBox_Colors.Click += new System.EventHandler(this.pictureBox_Colors_Click);
            this.pictureBox_Colors.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Colors_MouseDoubleClick);
            this.pictureBox_Colors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Colors_MouseDown);
            // 
            // print_btn
            // 
            this.print_btn.Location = new System.Drawing.Point(26, 231);
            this.print_btn.Name = "print_btn";
            this.print_btn.Size = new System.Drawing.Size(174, 45);
            this.print_btn.TabIndex = 9;
            this.print_btn.Text = "Print";
            this.print_btn.UseVisualStyleBackColor = true;
            this.print_btn.Click += new System.EventHandler(this.print_btn_Click);
            // 
            // checkBox_Gridlines
            // 
            this.checkBox_Gridlines.AutoSize = true;
            this.checkBox_Gridlines.Location = new System.Drawing.Point(26, 148);
            this.checkBox_Gridlines.Name = "checkBox_Gridlines";
            this.checkBox_Gridlines.Size = new System.Drawing.Size(170, 29);
            this.checkBox_Gridlines.TabIndex = 8;
            this.checkBox_Gridlines.Text = "Map to Pallet";
            this.checkBox_Gridlines.UseVisualStyleBackColor = true;
            this.checkBox_Gridlines.CheckedChanged += new System.EventHandler(this.checkBox_Gridlines_CheckedChanged);
            // 
            // checkBox_Pixelated
            // 
            this.checkBox_Pixelated.AutoSize = true;
            this.checkBox_Pixelated.Location = new System.Drawing.Point(26, 103);
            this.checkBox_Pixelated.Name = "checkBox_Pixelated";
            this.checkBox_Pixelated.Size = new System.Drawing.Size(192, 29);
            this.checkBox_Pixelated.TabIndex = 6;
            this.checkBox_Pixelated.Text = "Show Pixelated";
            this.checkBox_Pixelated.UseVisualStyleBackColor = true;
            this.checkBox_Pixelated.CheckedChanged += new System.EventHandler(this.checkBox_Pixelated_CheckedChanged);
            // 
            // Load_Picture
            // 
            this.Load_Picture.Location = new System.Drawing.Point(26, 40);
            this.Load_Picture.Name = "Load_Picture";
            this.Load_Picture.Size = new System.Drawing.Size(174, 45);
            this.Load_Picture.TabIndex = 0;
            this.Load_Picture.Text = "Picture";
            this.Load_Picture.UseVisualStyleBackColor = true;
            this.Load_Picture.Click += new System.EventHandler(this.Load_Picture_Click);
            // 
            // pictureBoxImg
            // 
            this.pictureBoxImg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBoxImg.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxImg.Name = "pictureBoxImg";
            this.pictureBoxImg.Size = new System.Drawing.Size(1120, 1120);
            this.pictureBoxImg.TabIndex = 0;
            this.pictureBoxImg.TabStop = false;
            this.pictureBoxImg.Click += new System.EventHandler(this.pictureBoxImg_Click);
            this.pictureBoxImg.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBoxImg_DragDrop);
            this.pictureBoxImg.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBoxImg_DragEnter);
            this.pictureBoxImg.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxImg_Paint);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1794, 1767);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = " Bead Studio";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Colors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxImg;
        private System.Windows.Forms.Button Load_Picture;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.CheckBox checkBox_Pixelated;
        private System.Windows.Forms.CheckBox checkBox_Gridlines;
        private System.Windows.Forms.Button print_btn;
        private System.Windows.Forms.PictureBox pictureBox_Colors;
    }
}

