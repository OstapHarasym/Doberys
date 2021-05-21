using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinForm
{
    public partial class Form1 : Form
    {
        private static bool up, down, left, right;
        private static int x, y;
        private static int lives = 5;
        Point xy = new Point(90, 90);
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.Controls.Remove(button1);
        }









        private void button2_Click(object sender, EventArgs e)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Controls.Remove(button2);
            Map.gen();
            for (int i = 0; i < Length; i++)
            {
                for (int j = 0; j < High; j++)
                {
                    if (_Map[i, j] == 1)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Location = new Point(20 + i * f, 18 + j * k);
                        pictureBox.Size = new Size(f, k);
                        pictureBox.Image = Properties.Resources.кущ;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(pictureBox);
                    }
                    else if(_Map[i, j] == 2)
                    {
                        xy.X = f * i;
                        xy.Y = k * j;
                        pictureBox1.Location = xy;
                    }
                }
            }
            for (int i = 0; i < Length; i++)
            {
                PictureBox buttonUp = new PictureBox();
                buttonUp.Location = new Point(20 + i * f);
                buttonUp.Size = new Size(f, 18);
                buttonUp.Image = Properties.Resources.горизонтальна_стіна;
                buttonUp.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonUp);
                PictureBox buttonDown = new PictureBox();
                buttonDown.Location = new Point(20 + i * f, 1062);
                buttonDown.Size = new Size(f, 18);
                buttonDown.Image = Properties.Resources.горизонтальна_стіна;
                buttonDown.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonDown);
            }
            for (int i = 0; i < High; i++)
            {
                PictureBox buttonLeft = new PictureBox();
                buttonLeft.Location = new Point(0, 18 + i * k);
                buttonLeft.Size = new Size(20, k);
                buttonLeft.Image = Properties.Resources.вертикальна_стіна;
                buttonLeft.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonLeft);
                PictureBox buttonRight = new PictureBox();
                buttonRight.Location = new Point(1900, 18 + i * k);
                buttonRight.Size = new Size(20, k);
                buttonRight.Image = Properties.Resources.вертикальна_стіна; 
                buttonRight.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonRight);
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    PictureBox buttonLeft = new PictureBox();
                    buttonLeft.Location = new Point(i * 1900, j * 1062);
                    buttonLeft.Size = new Size(20, 18);
                    buttonLeft.Image = Properties.Resources.Кутова_стіна;
                    buttonLeft.SizeMode = PictureBoxSizeMode.StretchImage;
                    this.Controls.Add(buttonLeft);
                }
            }
        }










        private static int k = 87, f = 94;
        static int Length = 20;

        

        static int High = 12;
        static int[,] _Map = new int[Length, High];

        class Map
        {
            public static void gen()
            {
                var Rand = new Random();
                for (int i = 0; i < Length; i++)
                {
                    for (int j = 0; j < High; j++)
                    {
                        _Map[i, j] = Rand.Next(0, 2);
                    }
                }
                _Map[Rand.Next(0, Length), Rand.Next(0, High)] = 2;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.W)
            {
                up = true;
                left = false;
                right = false;
                down = false;
            }
            else if (e.KeyData == Keys.A)
            {
                left = true;
                up = false;
                right = false;
                down = false;
            }
            else if (e.KeyData == Keys.S)
            {
                down = true;
                up = false;
                left = false;
                right = false;
            }
            else if (e.KeyData == Keys.D)
            {
                right = true;
                up = false;
                left = false;
                down = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (up == true)
            {
                xy.Y -= 2;
                pictureBox1.Location = xy;
            }
            else if (left == true)
            {
                xy.X -= 2;
                pictureBox1.Location = xy;
            }
            else if (down == true)
            {
                xy.Y += 2;
                pictureBox1.Location = xy;
            }
            else if (right == true)
            {
                xy.X += 2;
                pictureBox1.Location = xy;
            }

            foreach (Control x in this.Controls)
            {
                if (x is Button && x.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    if(up == true)
                    {
                        up = false;
                        xy.Y += 2;
                        pictureBox1.Location = xy;
                    }
                    else if(down == true)
                    {
                        down = false;
                        xy.Y -= 2;
                        pictureBox1.Location = xy;
                    }
                    else if(right == true)
                    {
                        right = false;
                        xy.X -= 2;
                        pictureBox1.Location = xy;
                    }
                    else if(left == true)
                    {
                        left = false;
                        xy.X += 2;
                        pictureBox1.Location = xy;
                    }
                    lives--;
                }
            }

            if(lives <= 0)
            {
                this.Controls.Remove(pictureBox1);
            }
        }
    }

    #region Window
    abstract class Window
    {
        // хз чи так варто робити, може краще через простий клас і об'єкт на нього
        void SetInterface()
        {

        }
    }
    class Menu: Window
    {

    }
    class Settings: Window
    {

    }
    class GameArea: Window
    {

    }
    #endregion
    #region Sprite
    abstract class Sprite
    {

    }
    class Hero: Sprite
    {

    }
    class Enemy: Sprite
    {

    }
    #endregion
    class Map
    {

    }
}
