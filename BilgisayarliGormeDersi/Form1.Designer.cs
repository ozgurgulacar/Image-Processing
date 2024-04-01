namespace BilgisayarliGormeDersi
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.SelectedImage = new System.Windows.Forms.PictureBox();
            this.ChangedImage = new System.Windows.Forms.PictureBox();
            this.ChooseBtn = new System.Windows.Forms.Button();
            this.ApplyBtn = new System.Windows.Forms.Button();
            this.histGrafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.AdjustImage = new System.Windows.Forms.PictureBox();
            this.optionsbox = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.SelectedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChangedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.histGrafik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdjustImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectedImage
            // 
            this.SelectedImage.Location = new System.Drawing.Point(12, 12);
            this.SelectedImage.Name = "SelectedImage";
            this.SelectedImage.Size = new System.Drawing.Size(232, 266);
            this.SelectedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.SelectedImage.TabIndex = 0;
            this.SelectedImage.TabStop = false;
            // 
            // ChangedImage
            // 
            this.ChangedImage.Location = new System.Drawing.Point(979, 12);
            this.ChangedImage.Name = "ChangedImage";
            this.ChangedImage.Size = new System.Drawing.Size(321, 301);
            this.ChangedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ChangedImage.TabIndex = 1;
            this.ChangedImage.TabStop = false;
            // 
            // ChooseBtn
            // 
            this.ChooseBtn.Location = new System.Drawing.Point(75, 284);
            this.ChooseBtn.Name = "ChooseBtn";
            this.ChooseBtn.Size = new System.Drawing.Size(106, 40);
            this.ChooseBtn.TabIndex = 2;
            this.ChooseBtn.Text = "Fotoğraf Seç";
            this.ChooseBtn.UseVisualStyleBackColor = true;
            this.ChooseBtn.Click += new System.EventHandler(this.ChooseBtn_Click);
            // 
            // ApplyBtn
            // 
            this.ApplyBtn.Location = new System.Drawing.Point(598, 76);
            this.ApplyBtn.Name = "ApplyBtn";
            this.ApplyBtn.Size = new System.Drawing.Size(106, 40);
            this.ApplyBtn.TabIndex = 3;
            this.ApplyBtn.Text = "Uygula";
            this.ApplyBtn.UseVisualStyleBackColor = true;
            this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtn_Click);
            // 
            // histGrafik
            // 
            chartArea3.Name = "ChartArea1";
            this.histGrafik.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.histGrafik.Legends.Add(legend3);
            this.histGrafik.Location = new System.Drawing.Point(390, 185);
            this.histGrafik.Name = "histGrafik";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Histogram";
            this.histGrafik.Series.Add(series3);
            this.histGrafik.Size = new System.Drawing.Size(559, 316);
            this.histGrafik.TabIndex = 4;
            this.histGrafik.Text = "------------------";
            // 
            // AdjustImage
            // 
            this.AdjustImage.Location = new System.Drawing.Point(12, 507);
            this.AdjustImage.Name = "AdjustImage";
            this.AdjustImage.Size = new System.Drawing.Size(328, 316);
            this.AdjustImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AdjustImage.TabIndex = 5;
            this.AdjustImage.TabStop = false;
            // 
            // optionsbox
            // 
            this.optionsbox.FormattingEnabled = true;
            this.optionsbox.Items.AddRange(new object[] {
            "Seçiniz",
            "Siyah-Beyaz Yap",
            "Histogramını Çıkar",
            "K-Means İntensity",
            "K-Means Öklid",
            "Kenar Bulma"});
            this.optionsbox.Location = new System.Drawing.Point(583, 12);
            this.optionsbox.Name = "optionsbox";
            this.optionsbox.Size = new System.Drawing.Size(135, 24);
            this.optionsbox.TabIndex = 7;
            this.optionsbox.Text = "Seçiniz";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(1002, 507);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(328, 316);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(598, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(106, 22);
            this.textBox1.TabIndex = 9;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1178, 319);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(152, 182);
            this.richTextBox1.TabIndex = 10;
            this.richTextBox1.Text = "";
            // 
            // chart1
            // 
            this.chart1.BorderlineColor = System.Drawing.Color.LightCoral;
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(390, 507);
            this.chart1.Name = "chart1";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Legend = "Legend1";
            series4.Name = "Histogram";
            this.chart1.Series.Add(series4);
            this.chart1.Size = new System.Drawing.Size(559, 316);
            this.chart1.TabIndex = 11;
            this.chart1.Text = "------------------";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 835);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.optionsbox);
            this.Controls.Add(this.AdjustImage);
            this.Controls.Add(this.histGrafik);
            this.Controls.Add(this.ApplyBtn);
            this.Controls.Add(this.ChooseBtn);
            this.Controls.Add(this.ChangedImage);
            this.Controls.Add(this.SelectedImage);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.SelectedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChangedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.histGrafik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AdjustImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox SelectedImage;
        private System.Windows.Forms.PictureBox ChangedImage;
        private System.Windows.Forms.Button ChooseBtn;
        private System.Windows.Forms.Button ApplyBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart histGrafik;
        private System.Windows.Forms.PictureBox AdjustImage;
        private System.Windows.Forms.ComboBox optionsbox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

