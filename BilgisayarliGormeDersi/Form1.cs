using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace BilgisayarliGormeDersi
{
    public partial class Form1 : Form
    {
        Color[,] RGB;

        int[] histDegerleri = new int[256];

        int[,] Matris;
        int[,] AynalanmisMatris;

        int[,] ClustersIntensity;
        float[] KNoktalariIntensity;
        int[,] KMeansMatrisIntensity;
        int[] Intensityhist = new int[256];

        int[] adetler;

        Color[] KNoktalariOklid;
        int[,] ClustersOklid;
        Color[,] KMeansRenklendir;




        public Form1()
        {
            InitializeComponent();
        }


        //Fotoğraf Seç Butonu
        private void ChooseBtn_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog(this);
            SelectedImage.ImageLocation = ofd.FileName;

        }

        //Uygula Butonu
        //Önce Resim Siyah Beyaz Yapılmalıdır. Yoksa Diğer seçeneklerde Ya hata verir ya da önceki fotoğraf için seçimi uygular
        private void ApplyBtn_Click(object sender, EventArgs e)
        {
            string secim = optionsbox.Text;
            try
            {
                if (secim == "Siyah-Beyaz Yap")
                {
                    ConvertToArray();
                    BlackWhite();
                }
                else if (secim == "Histogramını Çıkar")
                {
                    
                    Histogram();
                    gercektensiyahbeyaz();
                }
                else if (secim == "K-Means İntensity")
                {
                    K_Means_Intensity(Convert.ToInt32(textBox1.Text));
                    K_Means_Renklendirme_Intensity(Convert.ToInt32(textBox1.Text));
                    K_Means_resime_cevir_Intensity();
                    richTextBox1.Text = "";
                    for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                    {
                        richTextBox1.Text += "Cluster " + (i + 1) + " Adet Sayısı= " + adetler[i] + "\n";
                        richTextBox1.Text += "Konumu: " + (int)KNoktalariIntensity[i] + "\n\n";
                    }
                    Intensity_Histogram();
                }
                else if (secim == "K-Means Öklid")
                {
                    K_Means_RGB(Convert.ToInt32(textBox1.Text));
                    K_Means_Oklid_Renklendir();
                    K_Means_Oklid_Resime_Cevir();
                    richTextBox1.Text = "";
                    for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                    {
                        richTextBox1.Text += "Cluster " + (i + 1) + " Adet Sayısı= " + adetler[i] + "\n";
                        richTextBox1.Text += "R Değeri: " + KNoktalariOklid[i].R + "\nG Değeri: " + KNoktalariOklid[i].G + "\nB Değeri: " + KNoktalariOklid[i].B + "\n\n";
                    }
                }
                else if (secim == "Kenar Bulma")
                {
                    AdjustMatrisForFilter();
                    DoFiltering();
                }

            }
            catch (Exception x)
            {

                MessageBox.Show("Lütfen Önce Resmi uygulamadan Siyah-Beyaz yapın daha sonra Histogramını Çıkarın. Ardından Yapmak istediğiniz işlemi seçin. KMeans işlemleri için alttaki kutucuğa Bir K sayısı vermeyi unutmayın ", "Tekrar Deneyin");

            }


        }




        void Histogram()
        {

            //Histogramın Tablosunu oluşturma aşamaları
            //Önce Tabloda geçmişte olan veriler temizlenir
            histGrafik.Series.Clear();

            //Yeni veriler Tek tek kayıt edilir.
            Series seri = histGrafik.Series.Add("Histogram");
            for (int i = 0; i < 256; i++)
            {
                seri.Points.Add(histDegerleri[i]);
            }

            //En yüksek Histogram Değeri bulunur
            int x = histDegerleri[0];
            for (int i = 0; i < 256; i++)
            {
                if (x < histDegerleri[i])
                {
                    x = histDegerleri[i];
                }
            }

            //Tabloda Y ordinatının maksimum seviyesi belirlenerek Tablonun daha rahat okunması için ayarlama yapılmış olur.
            histGrafik.ChartAreas[0].AxisY.Maximum = (int)(x * 1.1);
        }
        void gercektensiyahbeyaz(int secim = 125)
        {
            try
            {
                Bitmap siyah = new Bitmap(Matris.GetLength(0), Matris.GetLength(1));
                Bitmap siyah2 = new Bitmap(Matris.GetLength(0), Matris.GetLength(1));
                Bitmap siyah3 = new Bitmap(Matris.GetLength(0), Matris.GetLength(1));
                int[,] resimsıfırbir = new int[Matris.GetLength(0), Matris.GetLength(1)];
                int[,] asama2 = new int[Matris.GetLength(0), Matris.GetLength(1)];
                for (int i = 0; i < Matris.GetLength(0); i++)
                {
                    for (int j = 0; j < Matris.GetLength(1); j++)
                    {
                        if (Matris[i, j] < secim)
                        {
                            siyah.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                            resimsıfırbir[i, j] = 0;
                        }
                        else
                        {
                            siyah.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                            resimsıfırbir[i, j] = 1;
                        }
                    }
                }

                pictureBox1.Image = siyah;

                //Dilation
                for (int i = 1; i < Matris.GetLength(0) - 1; i++)
                {
                    for (int j = 1; j < Matris.GetLength(1) - 1; j++)
                    {
                        if (resimsıfırbir[i - 1, j] == 1 || resimsıfırbir[i, j - 1] == 1 || resimsıfırbir[i, j] == 1 || resimsıfırbir[i, j + 1] == 1 || resimsıfırbir[i + 1, j] == 1)
                        {
                            asama2[i, j] = 1;
                        }
                        else
                        {
                            asama2[i, j] = 0;
                        }
                    }
                }

                //siyah beyaza çevir
                for (int i = 0; i < Matris.GetLength(0); i++)
                {
                    for (int j = 0; j < Matris.GetLength(1); j++)
                    {
                        if (asama2[i, j] == 0)
                        {
                            siyah3.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            siyah3.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
                ChangedImage.Image = siyah3;

                //Erosion
                for (int i = 1; i < Matris.GetLength(0) - 1; i++)
                {
                    for (int j = 1; j < Matris.GetLength(1) - 1; j++)
                    {
                        if (asama2[i - 1, j] == 1 && asama2[i, j - 1] == 1 && asama2[i, j] == 1 && asama2[i, j + 1] == 1 && resimsıfırbir[i + 1, j] == 1)
                        {
                            resimsıfırbir[i, j] = 1;
                        }
                        else
                        {
                            resimsıfırbir[i, j] = 0;
                        }
                    }
                }



                //siyah beyaza çevir
                for (int i = 0; i < Matris.GetLength(0); i++)
                {
                    for (int j = 0; j < Matris.GetLength(1); j++)
                    {
                        if (resimsıfırbir[i, j] == 0)
                        {
                            siyah2.SetPixel(i, j, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            siyah2.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                    }
                }
                AdjustImage.Image = siyah2;
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen Önce Resmi uygulamadan Siyah-Beyaz yapın daha sonra Histogramını Çıkarın. Ardından Yapmak istediğiniz işlemi seçin. KMeans işlemleri için alttaki kutucuğa Bir K sayısı vermeyi unutmayın ", "Tekrar Deneyin");


            }

        }
        void BlackWhite()
        {
            //newresim isimli kullanıcının seçtiği resim ile aynı boyutta yeni bir Bitmap oluşturulur.
            Bitmap newresim = new Bitmap(RGB.GetLength(0), RGB.GetLength(1));

            //Bitmap ile sürekli işlem yapmak çok maliyetli olduğundan ve Bu yeni matrisi Filtrelemede kullanacağımızdan dolayı
            //Global Matris isimli değişkene atamak için önce bu matrisin boyutu belirliyoruz.
            Matris = new int[RGB.GetLength(0), RGB.GetLength(1)];


            for (int i = 0; i < RGB.GetLength(0); i++)
            {
                for (int j = 0; j < RGB.GetLength(1); j++)
                {
                    //R,G,B değerleri toplanarak ortalaması alınır. Bu Resmi siyah-beyaz formata çevirmek için yapılır. 
                    int yenirenk = (RGB[i, j].R + RGB[i, j].G + RGB[i, j].B) / 3;
                    //histogram tablosunu çizmek için HistDeğerleri adlı tabloda bulunan yeni değer bir arttırılarak sayma işlemi yapılır.
                    histDegerleri[yenirenk]++;
                    //Filtre uygulamak için bu değerleri bir matrise sıralı bir şekilde kaydedilir
                    Matris[i, j] = yenirenk;
                    //Ortalaması alınan bu değer Yeni bir pixele dönüştürülerek Newresim adlı Bitmape kaydedelir.
                    Color clr = Color.FromArgb(yenirenk, yenirenk, yenirenk);
                    newresim.SetPixel(i, j, clr);

                }
            }
            //ChangedImage Adlı PictureBox içerisine bu Bitmap Image formatında gönderilir.
            ChangedImage.Image = (Image)newresim;
        }
        void ConvertToArray()
        {
            //Seçilen Resimi Bitmap Formatına dönüştürür.
            Bitmap resim = (Bitmap)(SelectedImage.Image);

            //Global HistDeğerleri matrisini Sıfırlar. Daha sonra yeni bir Resim Seçildiğinde Eski Histogram değerlerini yenisi ile toplamamak için bunu yapar.
            for (int i = 0; i < 256; i++)
            {
                histDegerleri[i] = 0;
            }

            //Global RGB Matrisinin boyutu belirlenir.
            RGB = new Color[resim.Width, resim.Height];

            //bu matrisin içi Seçilen Resimin Pixel değerleri ile doldurulur
            for (int i = 0; i < resim.Width; i++)
            {
                for (int j = 0; j < resim.Height; j++)
                {
                    Color Clr = resim.GetPixel(i, j);
                    RGB[i, j] = Clr;
                }
            }
        }
        void AdjustMatrisForFilter()
        {
            //Aynalama işi yapacağımız için Normal Matrisin Boyutunda artış olacak 
            //Bu artış ile birlikte Global AynalanmisMatris değişkeninin boyutunu belirliyoruz
            AynalanmisMatris = new int[Matris.GetLength(0) + 2, Matris.GetLength(1) + 2];

            //Sütun Aynalama Yaparak Değerleri Yeni matrise yazıyoruz
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < AynalanmisMatris.GetLength(1) - 1; j++)
                {
                    if (i == 0)
                    {
                        AynalanmisMatris[0, j] = Matris[0, j - 1];
                    }
                    else
                    {
                        AynalanmisMatris[AynalanmisMatris.GetLength(0) - 1, j] = Matris[Matris.GetLength(0) - 1, j - 1];
                    }

                }
            }
            //Satir Aynalama yaparak Değerleri Yeni matrise yazıyoruz
            for (int i = 0; i < 2; i++)
            {
                for (int j = 1; j < AynalanmisMatris.GetLength(0) - 1; j++)
                {
                    if (i == 0)
                    {
                        AynalanmisMatris[j, 0] = Matris[j - 1, 0];
                    }
                    else
                    {
                        AynalanmisMatris[j, AynalanmisMatris.GetLength(1) - 1] = Matris[j - 1, Matris.GetLength(1) - 1];
                    }
                }
            }
            //Sütun ve Satırlar aynalandıktan sonra Bu matrisi Aynalanmış Matrisin içine Kopyalıyoruz.
            for (int i = 1; i < AynalanmisMatris.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < AynalanmisMatris.GetLength(1) - 1; j++)
                {
                    AynalanmisMatris[i, j] = Matris[i - 1, j - 1];
                }
            }
            //Yeni Matrisin Köşe kısımlarını da Aynalama yaparak Aynalama işini bitiriyoruz.
            AynalanmisMatris[0, 0] = Matris[0, 0];
            AynalanmisMatris[0, AynalanmisMatris.GetLength(1) - 1] = Matris[0, Matris.GetLength(1) - 1];
            AynalanmisMatris[AynalanmisMatris.GetLength(0) - 1, 0] = Matris[Matris.GetLength(0) - 1, 0];
            AynalanmisMatris[AynalanmisMatris.GetLength(0) - 1, AynalanmisMatris.GetLength(1) - 1] = Matris[Matris.GetLength(0) - 1, Matris.GetLength(1) - 1];
        }
        void DoFiltering()
        {
            int[,] filtre = { { 3, 3, 0 }, { 3, 0, -3 }, { 0, -3, -3 } };
            Bitmap filtresonrasi = new Bitmap(RGB.GetLength(0), RGB.GetLength(1));
            for (int i = 1; i < AynalanmisMatris.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < AynalanmisMatris.GetLength(1) - 1; j++)
                {

                    int hesaplaustsatir = AynalanmisMatris[i - 1, j - 1] * filtre[0, 0] + AynalanmisMatris[i - 1, j] * filtre[0, 1] + AynalanmisMatris[i - 1, j + 1] * filtre[0, 2];
                    int hesaplaortasatir = AynalanmisMatris[i, j - 1] * filtre[1, 0] + AynalanmisMatris[i, j] * filtre[1, 1] + AynalanmisMatris[i, j + 1] * filtre[1, 2];
                    int hesaplaaltsatir = AynalanmisMatris[i + 1, j - 1] * filtre[2, 0] + AynalanmisMatris[i + 1, j] * filtre[2, 1] + AynalanmisMatris[i + 1, j + 1] * filtre[2, 2];
                    int toplam = (hesaplaaltsatir + hesaplaortasatir + hesaplaustsatir);
                    if (toplam > 255)
                        toplam = 255;
                    if (toplam < 0)
                        toplam = 0;

                    //Bulunan sonucu Bitmape aktarma
                    Color clr = Color.FromArgb(toplam, toplam, toplam);
                    filtresonrasi.SetPixel(i - 1, j - 1, clr);
                }
            }
            AdjustImage.Image = (Image)filtresonrasi;



        }

        //Intesity K-means
        void K_Means_Intensity(int adet)
        {


            KNoktalariIntensity = new float[adet];
            float[] Knoktalariyeni = new float[adet];
            ClustersIntensity = new int[adet, 256];

        geridon:
            //Rastgele K Noktaları seçiliyor
            Random rnd = new Random();
            for (int i = 0; i < adet; i++)
            {
                KNoktalariIntensity[i] = rnd.Next(0, 255);
            }

            bool döngü = true;
            while (döngü)
            {
                adetler = new int[adet];

                //Her Noktanın, K Noktasına Uzaklığı hesaplanıp Cluster'a atama işlemi yapılıyor
                for (int i = 0; i < 256; i++)
                {
                    int yakin = 256;
                    int secilen = 0;
                    for (int j = 0; j < adet; j++)
                    {
                        float x = KNoktalariIntensity[j] - i;
                        x = Math.Abs(x);
                        if (x < yakin)
                        {
                            yakin = (int)x;
                            secilen = j;
                        }
                    }
                    adetler[secilen]++;
                    ClustersIntensity[secilen, i] = 1;
                }

                //Herhangi bir Cluster'a Atama işlemi olduğunu kontrol eder. Eğer Boş cluster varsa Başa döner.
                for (int i = 0; i < adet; i++)
                {
                    if (adetler[i] <= 0)
                    {
                        goto geridon;
                    }
                }


                //Atama yapıldıktan sonra Her Clusterı Ortalama konumuna yerleştirir.
                for (int i = 0; i < adet; i++)
                {
                    float toplam = 0;
                    float bolecek = 0;
                    for (int j = 0; j < 256; j++)
                    {
                        if (ClustersIntensity[i, j] == 1)
                        {
                            bolecek += histDegerleri[j];
                            toplam += j * histDegerleri[j];
                        }
                    }

                    Knoktalariyeni[i] = (toplam / bolecek);
                }

                //Oluşan yeni Cluster noktaları ile Bir önceki cluster noktalarının aynı olup olmadığını kontrol eder
                //Konumların aynı olması durumunda Döngüyü sonlandırır.
                döngü = false;
                for (int i = 0; i < adet; i++)
                {
                    if (Knoktalariyeni[i] != KNoktalariIntensity[i])
                    {
                        döngü = true;
                        break;
                    }
                }

                //Global KNoktalarına yeni oluşan K Noktlarını Atar.
                for (int i = 0; i < adet; i++)
                {
                    KNoktalariIntensity[i] = Knoktalariyeni[i];
                }
            }

        }
        void K_Means_Renklendirme_Intensity(int adet)
        {
            for (int i = 0; i < 256; i++)
            {
                Intensityhist[i] = 0;
            }
            //Resmi Siyah-Beyaz halinde oluşturduğumuz Matrisin Her elemanını kontrol eder
            KMeansMatrisIntensity = new int[Matris.GetLength(0), Matris.GetLength(1)];
            //Matrisi dön
            for (int i = 0; i < Matris.GetLength(0); i++)
            {
                for (int j = 0; j < Matris.GetLength(1); j++)
                {
                    //O anki Değer Hangi Clusterda bul
                    int y = Matris[i, j];
                    for (int x = 0; x < adet; x++)
                    {
                        if (ClustersIntensity[x, y] == 1)
                        {
                            //Yeni Matris içine Yeni renk koduyla ata
                            KMeansMatrisIntensity[i, j] = (int)KNoktalariIntensity[x];
                            Intensityhist[(int)KNoktalariIntensity[x]]++;
                            break;
                        }
                    }
                }
            }
        }
        void K_Means_resime_cevir_Intensity()
        {
            Bitmap bmp = new Bitmap(Matris.GetLength(0), Matris.GetLength(1));
            for (int i = 0; i < Matris.GetLength(0); i++)
            {
                for (int j = 0; j < Matris.GetLength(1); j++)
                {
                    Color clr = Color.FromArgb(KMeansMatrisIntensity[i, j], KMeansMatrisIntensity[i, j], KMeansMatrisIntensity[i, j]);
                    bmp.SetPixel(i, j, clr);
                }
            }
            pictureBox1.Image = (Image)bmp;
        }
        void Intensity_Histogram()
        {
            chart1.Series.Clear();

            //Yeni veriler Tek tek kayıt edilir.
            Series seri = chart1.Series.Add("Histogram");
            for (int i = 0; i < 256; i++)
            {
                seri.Points.Add(Intensityhist[i]);
            }

            //En yüksek Histogram Değeri bulunur
            int x = Intensityhist[0];
            for (int i = 0; i < 256; i++)
            {
                if (x < Intensityhist[i])
                {
                    x = Intensityhist[i];
                }
            }

            //Tabloda Y ordinatının maksimum seviyesi belirlenerek Tablonun daha rahat okunması için ayarlama yapılmış olur.
            chart1.ChartAreas[0].AxisY.Maximum = (int)(x * 1.1);
        }

        //RGB Değerleri ile K-Means Öklid
        void K_Means_RGB(int adet)
        {

            bool döngü = true;
            ClustersOklid = new int[RGB.GetLength(0), RGB.GetLength(1)];
            KNoktalariOklid = new Color[adet];
            Random rnd = new Random();
        geridon:
            //Rastgele K Noktalarina, Resmin bir Pixelini(Color'ını) atar 
            for (int i = 0; i < adet; i++)
            {
                KNoktalariOklid[i] = RGB[rnd.Next(0, RGB.GetLength(0)), rnd.Next(0, RGB.GetLength(1))];
            }
            while (döngü)
            {

                adetler = new int[adet];
                double yakin = 50000;

                //Resmin Her Pixeli için Her K noktasinin Öklid Değerini Hesaplar
                //En Küçük değerli olan K Noktasına O anki Pixeli Atar.
                for (int i = 0; i < RGB.GetLength(0); i++)
                {
                    for (int j = 0; j < RGB.GetLength(1); j++)
                    {
                        Color c = RGB[i, j];
                        yakin = 50000;
                        int secilen = 0;
                        for (int y = 0; y < adet; y++)
                        {
                            double hesap = Math.Sqrt(Math.Pow(KNoktalariOklid[y].R - c.R, 2) + Math.Pow(KNoktalariOklid[y].G - c.G, 2) + Math.Pow(KNoktalariOklid[y].B - c.B, 2));
                            if (hesap < yakin)
                            {
                                yakin = hesap;
                                secilen = y;
                            }
                        }

                        ClustersOklid[i, j] = secilen;
                        adetler[secilen]++;
                    }
                }

                //Clusterlar arasında Boş Cluster olup olmadığını kontrol eder
                //Boş Cluster olursa Rastgele K noktası atamaya döner.
                for (int i = 0; i < adet; i++)
                {
                    if (adetler[i] <= 0)
                    {
                        goto geridon;
                    }
                }
                double[,] ortalamahesapla = new double[adet, 3];
                int[] boluneceksayi = new int[adet];

                //Her Clusterın Ortalama RGB değerlerini Hesaplamak için O clustera ait Tüm Pixerllerin RGB değerlerini Toplar
                for (int a = 0; a < RGB.GetLength(0); a++)
                {
                    for (int b = 0; b < RGB.GetLength(1); b++)
                    {
                        int c = ClustersOklid[a, b];
                        boluneceksayi[c]++;
                        ortalamahesapla[c, 0] += RGB[a, b].R;
                        ortalamahesapla[c, 1] += RGB[a, b].G;
                        ortalamahesapla[c, 2] += RGB[a, b].B;

                    }
                }

                //Her Clusterın Değerleri Toplandıktan sonra Bölme işlemi yapılır. Yeni RGB değerleri Bulunmuş olur.
                for (int i = 0; i < adet; i++)
                {
                    ortalamahesapla[i, 0] = ortalamahesapla[i, 0] / boluneceksayi[i];
                    ortalamahesapla[i, 1] = ortalamahesapla[i, 1] / boluneceksayi[i];
                    ortalamahesapla[i, 2] = ortalamahesapla[i, 2] / boluneceksayi[i];
                }

                //Oluşan yeni Cluster noktaları ile Bir önceki cluster noktalarının aynı olup olmadığını kontrol eder
                //Konumların aynı olması durumunda Döngüyü sonlandırır.
                döngü = false;
                for (int i = 0; i < adet; i++)
                {
                    if ((int)ortalamahesapla[i, 0] != KNoktalariOklid[i].R || (int)ortalamahesapla[i, 1] != KNoktalariOklid[i].G || (int)ortalamahesapla[i, 2] != KNoktalariOklid[i].B)
                    {
                        döngü = true;
                        break;
                    }
                }



                for (int i = 0; i < adet; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (ortalamahesapla[i, j] < 0)
                        {
                            ortalamahesapla[i, j] = 0;
                        }
                        if (ortalamahesapla[i, j] > 255)
                        {
                            ortalamahesapla[i, j] = 255;
                        }
                    }
                }

                //Global KNoktalarına yeni oluşan K Noktlarını Atar.
                for (int i = 0; i < adet; i++)
                {
                    Color c = Color.FromArgb((int)ortalamahesapla[i, 0], (int)ortalamahesapla[i, 1], (int)ortalamahesapla[i, 2]);
                    KNoktalariOklid[i] = c;
                }

            }
        }
        void K_Means_Oklid_Renklendir()
        {
            KMeansRenklendir = new Color[RGB.GetLength(0), RGB.GetLength(1)];
            for (int i = 0; i < RGB.GetLength(0); i++)
            {
                for (int j = 0; j < RGB.GetLength(1); j++)
                {
                    KMeansRenklendir[i, j] = KNoktalariOklid[ClustersOklid[i, j]];
                }
            }
        }
        void K_Means_Oklid_Resime_Cevir()
        {
            Bitmap bmp = new Bitmap(RGB.GetLength(0), RGB.GetLength(1));
            for (int i = 0; i < RGB.GetLength(0); i++)
            {
                for (int j = 0; j < RGB.GetLength(1); j++)
                {
                    bmp.SetPixel(i, j, KMeansRenklendir[i, j]);
                }
            }
            pictureBox1.Image = (Image)bmp;
        }
    }
}