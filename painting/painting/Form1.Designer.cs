namespace painting
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonFullRectangle = new System.Windows.Forms.Button();
            this.buttonFullCircle = new System.Windows.Forms.Button();
            this.buttonLine = new System.Windows.Forms.Button();
            this.buttonColors = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCaligraphy = new System.Windows.Forms.Button();
            this.buttonBrush = new System.Windows.Forms.Button();
            this.buttonPencil = new System.Windows.Forms.Button();
            this.buttonRubber = new System.Windows.Forms.Button();
            this.buttonRectangle = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonImage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(-6, 96);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1163, 508);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.trackBar1.Location = new System.Drawing.Point(889, 25);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(152, 69);
            this.trackBar1.TabIndex = 0;
            this.trackBar1.TickFrequency = 5;
            this.toolTip1.SetToolTip(this.trackBar1, "Volba šířky pera");
            this.trackBar1.Value = 1;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(959, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "1";
            // 
            // buttonFullRectangle
            // 
            this.buttonFullRectangle.BackgroundImage = global::painting.Properties.Resources.square_full;
            this.buttonFullRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFullRectangle.Location = new System.Drawing.Point(314, 23);
            this.buttonFullRectangle.Name = "buttonFullRectangle";
            this.buttonFullRectangle.Size = new System.Drawing.Size(52, 51);
            this.buttonFullRectangle.TabIndex = 16;
            this.toolTip1.SetToolTip(this.buttonFullRectangle, "Odélník s výplní (volba velikosti natahováním)");
            this.buttonFullRectangle.UseVisualStyleBackColor = true;
            this.buttonFullRectangle.Click += new System.EventHandler(this.buttonFullRectangle_Click);
            // 
            // buttonFullCircle
            // 
            this.buttonFullCircle.BackgroundImage = global::painting.Properties.Resources.circle1_full;
            this.buttonFullCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonFullCircle.Location = new System.Drawing.Point(384, 23);
            this.buttonFullCircle.Name = "buttonFullCircle";
            this.buttonFullCircle.Size = new System.Drawing.Size(52, 51);
            this.buttonFullCircle.TabIndex = 15;
            this.toolTip1.SetToolTip(this.buttonFullCircle, "Elipsa s výplní (volba velikosti natahováním)");
            this.buttonFullCircle.UseVisualStyleBackColor = true;
            this.buttonFullCircle.Click += new System.EventHandler(this.buttonFullCircle_Click);
            // 
            // buttonLine
            // 
            this.buttonLine.BackgroundImage = global::painting.Properties.Resources.line;
            this.buttonLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLine.Location = new System.Drawing.Point(243, 23);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(52, 51);
            this.buttonLine.TabIndex = 14;
            this.toolTip1.SetToolTip(this.buttonLine, "Úsečka (volba velikosti natahováním)");
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            // 
            // buttonColors
            // 
            this.buttonColors.BackgroundImage = global::painting.Properties.Resources.colorwheel;
            this.buttonColors.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonColors.Location = new System.Drawing.Point(1059, 23);
            this.buttonColors.Name = "buttonColors";
            this.buttonColors.Size = new System.Drawing.Size(52, 51);
            this.buttonColors.TabIndex = 8;
            this.buttonColors.UseVisualStyleBackColor = true;
            this.buttonColors.Click += new System.EventHandler(this.buttonColors_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::painting.Properties.Resources.brushsize;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(809, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(52, 51);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackgroundImage = global::painting.Properties.Resources.add_paper;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonAdd.Location = new System.Drawing.Point(33, 23);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(52, 51);
            this.buttonAdd.TabIndex = 1;
            this.toolTip1.SetToolTip(this.buttonAdd, "Vymazat obrázek");
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCaligraphy
            // 
            this.buttonCaligraphy.BackgroundImage = global::painting.Properties.Resources.caligraphy_pen3;
            this.buttonCaligraphy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonCaligraphy.Location = new System.Drawing.Point(734, 23);
            this.buttonCaligraphy.Name = "buttonCaligraphy";
            this.buttonCaligraphy.Size = new System.Drawing.Size(52, 51);
            this.buttonCaligraphy.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonCaligraphy, "Kaligrafické pero");
            this.buttonCaligraphy.UseVisualStyleBackColor = true;
            this.buttonCaligraphy.Click += new System.EventHandler(this.buttonCaligraphy_Click);
            // 
            // buttonBrush
            // 
            this.buttonBrush.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonBrush.BackgroundImage")));
            this.buttonBrush.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonBrush.Location = new System.Drawing.Point(662, 23);
            this.buttonBrush.Name = "buttonBrush";
            this.buttonBrush.Size = new System.Drawing.Size(52, 51);
            this.buttonBrush.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonBrush, "Pero/štětec");
            this.buttonBrush.UseVisualStyleBackColor = true;
            this.buttonBrush.Click += new System.EventHandler(this.buttonBrush_Click);
            // 
            // buttonPencil
            // 
            this.buttonPencil.BackgroundImage = global::painting.Properties.Resources.pencil1;
            this.buttonPencil.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPencil.Location = new System.Drawing.Point(591, 23);
            this.buttonPencil.Name = "buttonPencil";
            this.buttonPencil.Size = new System.Drawing.Size(52, 51);
            this.buttonPencil.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonPencil, "Tužka");
            this.buttonPencil.UseVisualStyleBackColor = true;
            this.buttonPencil.Click += new System.EventHandler(this.buttonPencil_Click);
            // 
            // buttonRubber
            // 
            this.buttonRubber.BackgroundImage = global::painting.Properties.Resources.rubber;
            this.buttonRubber.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRubber.Location = new System.Drawing.Point(520, 23);
            this.buttonRubber.Name = "buttonRubber";
            this.buttonRubber.Size = new System.Drawing.Size(52, 51);
            this.buttonRubber.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonRubber, "Guma");
            this.buttonRubber.UseVisualStyleBackColor = true;
            this.buttonRubber.Click += new System.EventHandler(this.buttonRubber_Click);
            // 
            // buttonRectangle
            // 
            this.buttonRectangle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRectangle.BackgroundImage")));
            this.buttonRectangle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRectangle.Location = new System.Drawing.Point(172, 23);
            this.buttonRectangle.Name = "buttonRectangle";
            this.buttonRectangle.Size = new System.Drawing.Size(52, 51);
            this.buttonRectangle.TabIndex = 11;
            this.toolTip1.SetToolTip(this.buttonRectangle, "Obdélník bez výplně (volba velikosti natahováním)");
            this.buttonRectangle.UseVisualStyleBackColor = true;
            this.buttonRectangle.Click += new System.EventHandler(this.buttonRectangle_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonCircle.BackgroundImage")));
            this.buttonCircle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonCircle.Location = new System.Drawing.Point(103, 23);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(52, 51);
            this.buttonCircle.TabIndex = 10;
            this.toolTip1.SetToolTip(this.buttonCircle, "Elipsa bez výplně (volba velikosti natahováním)");
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonImage
            // 
            this.buttonImage.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonImage.BackgroundImage")));
            this.buttonImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonImage.Location = new System.Drawing.Point(452, 23);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(52, 51);
            this.buttonImage.TabIndex = 12;
            this.toolTip1.SetToolTip(this.buttonImage, "Vložení obrázku (volba velikosti natahováním)");
            this.buttonImage.UseVisualStyleBackColor = true;
            this.buttonImage.Click += new System.EventHandler(this.buttonImage_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.toolTip1_Popup);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 595);
            this.Controls.Add(this.buttonFullRectangle);
            this.Controls.Add(this.buttonFullCircle);
            this.Controls.Add(this.buttonLine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.buttonColors);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCaligraphy);
            this.Controls.Add(this.buttonBrush);
            this.Controls.Add(this.buttonPencil);
            this.Controls.Add(this.buttonRubber);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonRectangle);
            this.Controls.Add(this.buttonCircle);
            this.Controls.Add(this.buttonImage);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRubber;
        private System.Windows.Forms.Button buttonPencil;
        private System.Windows.Forms.Button buttonBrush;
        private System.Windows.Forms.Button buttonColors;
        private System.Windows.Forms.Button buttonCaligraphy;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonRectangle;
        private System.Windows.Forms.Button buttonImage;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLine;
        private System.Windows.Forms.Button buttonFullCircle;
        private System.Windows.Forms.Button buttonFullRectangle;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

