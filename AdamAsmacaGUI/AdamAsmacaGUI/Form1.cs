using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdamAsmacaGUI
{
    public partial class Form1 : Form
    {

        List<string> turkiyeIlleri = new List<string>() { "Adana", "Adıyaman", "Afyonkarahisar", /* Diğer iller... */ };
        string secilenIl;
        char[] kelimeDurumu;
        int yapilanHata = 0;
        int maxHata = 6;
        List<char> yanlisTahminler = new List<char>();
        public Form1()
        {
            InitializeComponent();
            OyunBaslat(); // Oyun başlatıldığında yeni bir kelime seç ve arayüzü hazırla
            button1.Click += new EventHandler(button1_Click); // Butona tıklama olayını bağla
        }
        // Oyunu başlatan fonksiyon
        private void OyunBaslat()
        {
            Random random = new Random();
            secilenIl = turkiyeIlleri[random.Next(turkiyeIlleri.Count)].ToUpper(); // Rastgele bir il seç
            kelimeDurumu = new string('_', secilenIl.Length).ToCharArray(); // Kelimenin durumunu "_" ile başlat
            label1.Text = new string(kelimeDurumu);  // Kelime durumu ekrana yazılıyor
            yanlisTahminler.Clear(); //yanlış tahmınleri sıfırla
            label2.Text = "Yanlış Tahminler: ";  // Yanlış tahminler sıfırlanıyor

            //hataları sıfırla
            yapilanHata = 0;
            panel1.Invalidate();  // Paneli temizle, adam çizimi sıfırlanacak
        }

        // "Tahmin Et" butonuna tıklanıldığında çalışacak
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1) // Kullanıcının sadece bir harf girmesi gerekiyor
            {
                char tahmin = char.ToUpper(textBox1.Text[0]);
                textBox1.Clear();

                if (secilenIl.Contains(tahmin))
                {
                    // Doğru tahmin edilirse kelimeyi güncelle
                    for (int i = 0; i < secilenIl.Length; i++)
                    {
                        if (secilenIl[i] == tahmin)
                        {
                            kelimeDurumu[i] = tahmin;
                        }
                    }
                    label1.Text = new string(kelimeDurumu);  // Güncel kelime durumu ekrana yazılıyor
                }
                else
                {
                    // Yanlış tahmin edilirse
                    if (!yanlisTahminler.Contains(tahmin))
                    {
                        yanlisTahminler.Add(tahmin);
                        yapilanHata++;
                        label2.Text = "Yanlış Tahminler: " + string.Join(", ", yanlisTahminler);
                        panel1.Invalidate();  // Adam çizimini güncelle
                    }
                }

                // Kazanma kontrolü
                if (new string(kelimeDurumu) == secilenIl)
                {
                    MessageBox.Show("Tebrikler, kazandınız!");
                    OyunBaslat();
                }

                // Kaybetme kontrolü
                if (yapilanHata >= maxHata)
                {
                    MessageBox.Show("Kaybettiniz! Doğru cevap: " + secilenIl);
                    OyunBaslat();
                }
            }
        }

        // Adam çizimini yapan fonksiyon
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            Font font = new Font("Arial", 12);  // Gözler ve ağız için font

            // Direği ve ipi çiz
            g.DrawLine(pen, 10, 150, 90, 150);  // Zemini çiz
            g.DrawLine(pen, 50, 20, 50, 150);   // Direği çiz
            g.DrawLine(pen, 50, 20, 100, 20);   // Üst çubuğu çiz
            g.DrawLine(pen, 85, 20, 85, 50);    // İpi biraz sağa kaydırarak çiz

            // Adamı çiz
            if (yapilanHata >= 1) g.DrawEllipse(pen, 70, 50, 30, 30);  // Kafa (ipi ortalayacak şekilde hizalandı)
            if (yapilanHata >= 2) g.DrawLine(pen, 85, 80, 85, 130);    // Gövde
            if (yapilanHata >= 3) g.DrawLine(pen, 85, 90, 65, 110);    // Sol kol
            if (yapilanHata >= 4) g.DrawLine(pen, 85, 90, 105, 110);   // Sağ kol
            if (yapilanHata >= 5) g.DrawLine(pen, 85, 130, 65, 160);   // Sol bacak
            if (yapilanHata >= 6) g.DrawLine(pen, 85, 130, 105, 160);  // Sağ bacak
        }

     
            private void button_Click_1(object sender, EventArgs e)
            {
                if (textBox1.Text.Length == 1) // Kullanıcının sadece bir harf girmesi gerekiyor
                {
                    char tahmin = char.ToUpper(textBox1.Text[0]); // Girilen harfi büyük harfe çevir
                    textBox1.Clear(); // TextBox'ı temizle

                    if (secilenIl.Contains(tahmin)) // Eğer doğru harfse
                    {
                        // Doğru tahmin edilirse kelimeyi güncelle
                        for (int i = 0; i < secilenIl.Length; i++)
                        {
                            if (secilenIl[i] == tahmin)
                            {
                                kelimeDurumu[i] = tahmin;
                            }
                        }
                        label1.Text = new string(kelimeDurumu);  // Güncel kelime durumu ekrana yazılıyor
                    }
                    else
                    {
                        // Yanlış tahmin edilirse
                        if (!yanlisTahminler.Contains(tahmin))
                        {
                            yanlisTahminler.Add(tahmin);
                            yapilanHata++;
                            label2.Text = "Yanlış Tahminler: " + string.Join(", ", yanlisTahminler);
                            panel1.Invalidate();  // Adam çizimini güncelle
                        }
                    }

                    // Oyuncu kazandı mı kontrol et
                    if (new string(kelimeDurumu) == secilenIl)
                    {
                        MessageBox.Show("Tebrikler, kazandınız!");
                        OyunBaslat(); // Yeni oyun başlat
                    }

                    // Oyuncu kaybetti mi kontrol et
                    if (yapilanHata >= maxHata)
                    {
                        MessageBox.Show("Kaybettiniz! Doğru cevap: " + secilenIl);
                        OyunBaslat(); // Yeni oyun başlat
                    }
                }
            }

        }
    }

