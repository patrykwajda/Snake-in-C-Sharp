using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace snake1
{
    public partial class Snake : Form
    {
        private static readonly Stopwatch watch = new Stopwatch();
        private bool czy_gra_aktywna;
        private Waz snake;
        private Owoc owoc;
        public int wynik;
        public int[] tabela = new int[1000];
        public int i = 0;
        public int k = 0;
        public int l = 0;
        bool posortowane;
        int bufor;
        public Snake()
        {
            InitializeComponent();
            czy_gra_aktywna = false;
            timer1.Enabled = true;
            pauzaToolStripMenuItem.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (czy_gra_aktywna)
            {
                pole_gry.CreateGraphics().Clear(Color.Black);
                snake.move();
                snake.rysuj(pole_gry.CreateGraphics(), new SolidBrush(Color.Yellow));
                owoc.rysuj_owoc(pole_gry.CreateGraphics(), new SolidBrush(Color.Red));
                label3.Text = wynik.ToString();

                if (owoc.czy_nowy_owoc(snake.x[0], snake.y[0]))
                {
                    snake.dodaj();
                    wynik += 10;

                }
                if (snake.czy_waz_zyje() == false)
                {
                    czy_gra_aktywna = false;
                    pauzaToolStripMenuItem.Enabled = false;
                    restartToolStripMenuItem.Enabled = false;
                    startToolStripMenuItem.Enabled = true;
                    pole_gry.CreateGraphics().Clear(Color.Black);
                    watch.Reset();
                    zapis_wyniku(wynik);
                    wypisanie_wynikow();
                    sortowanie();
                    label3.Text = "0";
                }
            }
            else
            {
                FontFamily fontFamily1 = new FontFamily("Arial");
                Font f = new Font(fontFamily1, 14);
                Brush b = new SolidBrush(Color.White);
                pole_gry.CreateGraphics().DrawString("Aby zagrać naciśnij Start", f, b, 25, 180);
                watch.Stop();
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            czy_gra_aktywna = true;
            snake = new Waz(pole_gry.Width, pole_gry.Height);
            owoc = new Owoc(snake.segment);
            pauzaToolStripMenuItem.Enabled = true;
            startToolStripMenuItem.Enabled = false;
            timer2.Enabled = true;
            labelStoper.Text = GetTimeString(watch.Elapsed);
            watch.Reset();
            watch.Start();
            wynik = 0;
        }

        private string GetTimeString(TimeSpan elapsed)
        {
            string result = string.Empty;
            result = string.Format("{0}:{1}:{2}.{3}",
            elapsed.Hours,
            elapsed.Minutes,
            elapsed.Seconds,
            elapsed.Milliseconds);

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) snake.ruch = "gora";
            if (e.KeyCode == Keys.Down) snake.ruch = "dol";
            if (e.KeyCode == Keys.Right) snake.ruch = "prawo";
            if (e.KeyCode == Keys.Left) snake.ruch = "lewo";
        }

        private void pauzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (czy_gra_aktywna)
            {
                czy_gra_aktywna = false;
                pauzaToolStripMenuItem.Text = "Wznów";
                restartToolStripMenuItem.Enabled = true;
            }
            else
            {
                czy_gra_aktywna = true;
                pauzaToolStripMenuItem.Text = "Pauza";
                watch.Start();
                startToolStripMenuItem.Enabled = false;
            }
        }

        private void wypisanie_wynikow()
        {
            FontFamily fontFamily1 = new FontFamily("Arial");
            Font f = new Font(fontFamily1, 14);
            Brush b = new SolidBrush(Color.White);
            pole_gry.CreateGraphics().DrawString("Game over", f, b, 70, 105);
            pole_gry.CreateGraphics().DrawString("Twój wynik: ", f, b, 70, 130);
            pole_gry.CreateGraphics().DrawString(label3.Text, f, b, 175, 130);
            pole_gry.CreateGraphics().DrawString("Czas:", f, b, 70, 155);
            pole_gry.CreateGraphics().DrawString(labelStoper.Text, f, b, 125, 155);

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            snake = new Waz(pole_gry.Width, pole_gry.Height);
            owoc = new Owoc(snake.segment);
            watch.Reset();
            zapis_wyniku(wynik);
            sortowanie();
            watch.Start();
            wynik = 0;
        }

        private void szybciejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Interval > 10) { timer1.Interval -= 10; }
        }
        private void wolniejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval += 10;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            labelStoper.Text = GetTimeString(watch.Elapsed);
            timer2.Enabled = true;
        }

        private void zapis_wyniku(int wynik)
        {
                tabela[i] = wynik;
                label4.Text = "";
                for (k = 0; k < 10; k++)
                label4.Text += tabela[k] + Environment.NewLine;
                i++;

        }
        private void sortowanie()
        {
            for (int k = 0; k < i; k++)
            {
                posortowane = true;
                for (int l = 0; l < i; l++)
                {
                    if (tabela[l + 1] > tabela[l])
                    {
                        bufor = tabela[l];
                        tabela[l] = tabela[l + 1];
                        tabela[l + 1] = bufor;
                        posortowane = false;
                    }
                }
                if (posortowane) break;
               
            }
            label4.Text = "";
            for (k = 0; k < 10; k++)
                label4.Text += tabela[k] + Environment.NewLine;
        }
    }
}