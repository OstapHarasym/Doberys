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
    struct traps
    {
        public int x;
        public int y;
        public PictureBox pb;
    }
    public partial class Form1 : Form
    {
        PictureBox Bird;
        PictureBox pictureBox1;
        traps[] Traps = new traps[200];
        traps[] Borders = new traps[64];
        int trapsCount = 0;
        int bordersCount = 0;
        PictureBox[] pictureBoxes;
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
            //Map.gen();
            var Rand = new Random();
            switch (Rand.Next(0, 3))
            {
                case 0:
                    _Map = map1;
                    break;
                case 1:
                    _Map = map2;
                    break;
                case 2:
                    _Map = map3;
                    break;
            }
            Image img = Properties.Resources.Bloonberry_Bush_Icon;
            switch (Rand.Next(0, 3))
            {
                case 0:
                    img = Properties.Resources.Bloonberry_Bush_Icon;
                    this.BackColor = Color.DarkOliveGreen;
                    break;
                case 1:
                    img = Properties.Resources.кактус1;
                    this.BackColor = Color.Khaki;
                    break;
                case 2:
                    img = Properties.Resources.generic_rpg_tree01;
                    this.BackColor = Color.Azure;
                    break;
            }
            for (int j = 0; j < Length; j++)
            {
                for (int i = 0; i < High; i++)
                {
                    if (_Map[i, j] == 1)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Location = new Point(20 + j * f, 18 + i * k);
                        pictureBox.Size = new Size(f, k);
                        pictureBox.Image = img;
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(pictureBox);
                        Traps[trapsCount].x = i;
                        Traps[trapsCount].y = j;
                        Traps[trapsCount].pb = pictureBox;
                        trapsCount++;
                    }
                    else if(_Map[i, j] == 2)
                    {
                        PictureBox pictureBox1 = new PictureBox();
                        xy.X = f * i + 20 + 10;
                        xy.Y = k * j + 18 + 10;
                        pictureBox1.Location = xy;
                        pictureBox1.Size = new System.Drawing.Size(60, 60);
                        pictureBox1.Image = Properties.Resources.ezgif_com_gif_maker;
                        pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        this.Controls.Add(pictureBox1);
                        this.pictureBox1 = pictureBox1;
                        //pictureBox1.TabIndex = 2;
                        //pictureBox1.TabStop = false;
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
                Borders[bordersCount].pb = buttonUp;
                Borders[bordersCount].x = i;
                Borders[bordersCount].y = 0;
                bordersCount++;
                PictureBox buttonDown = new PictureBox();
                buttonDown.Location = new Point(20 + i * f, 1062);
                buttonDown.Size = new Size(f, 18);
                buttonDown.Image = Properties.Resources.горизонтальна_стіна;
                buttonDown.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonDown);
                Borders[bordersCount].pb = buttonDown;
                Borders[bordersCount].x = i;
                Borders[bordersCount].y = 11;
                bordersCount++;
            }
            for (int i = 0; i < High; i++)
            {
                PictureBox buttonLeft = new PictureBox();
                buttonLeft.Location = new Point(0, 18 + i * k);
                buttonLeft.Size = new Size(20, k);
                buttonLeft.Image = Properties.Resources.вертикальна_стіна;
                buttonLeft.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonLeft);
                Borders[bordersCount].pb = buttonLeft;
                Borders[bordersCount].x = 0;
                Borders[bordersCount].y = i;
                bordersCount++;
                PictureBox buttonRight = new PictureBox();
                buttonRight.Location = new Point(1900, 18 + i * k);
                buttonRight.Size = new Size(20, k);
                buttonRight.Image = Properties.Resources.вертикальна_стіна; 
                buttonRight.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Controls.Add(buttonRight);
                Borders[bordersCount].pb = buttonRight;
                Borders[bordersCount].x = 19;
                Borders[bordersCount].y = i;
                bordersCount++;
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
            addItem();
        }



        void whenBounded()
        {
            if (up == true)
            {
                up = false;
                xy.Y += 2;
                pictureBox1.Location = xy;
            }
            else if (down == true)
            {
                down = false;
                xy.Y -= 2;
                pictureBox1.Location = xy;
            }
            else if (right == true)
            {
                right = false;
                xy.X -= 2;
                pictureBox1.Location = xy;
            }
            else if (left == true)
            {
                left = false;
                xy.X += 2;
                pictureBox1.Location = xy;
            }
            lives--;
        }



        private PictureBox item;
        private int itemCount = 0;

        private static int k = 87, f = 94;
        static int Length = 20;

        
        static int High = 12;
        static int[,] _Map;
                


        #region maps
        int[,] map1 = 
                {{ 0,  0,  0,  0,  0,  0 , 0 , 0 , 0 , 0 , 0,  0 , 1 , 0,  0 , 0 , 0 , 0,  0,  1},
                { 1 , 1 , 1,  0 , 1 , 1 , 1,  1 , 1 , 1  ,1 , 1 , 1 , 1  ,1  ,1 , 1 , 0 , 1 , 1},
                { 0,  1 , 0 , 0 , 1,  0,  1 , 0 , 0  ,0 , 0 , 0  ,1 , 0 , 1 , 0 , 0  ,0 , 0,  1},
                { 0 , 1 , 1 , 0,  1 , 0  ,1 , 0,  1 , 0 , 1 , 0 , 1 , 0 , 0 , 0  ,1 , 1 , 1 , 1},
                { 0 , 1 , 0 , 0 , 1 , 0 , 1 , 0 , 1 ,0  ,1,  0,  1 , 0,  1  ,0  ,0 , 0 , 0 , 1},
                { 0 , 1,  1 , 0,  1 , 2 , 0,  0,  1,  0,  1,  0 , 1 , 0 , 1 , 1 , 1 , 0 , 1 , 1},
                { 0  ,1,  0 , 0 , 1 , 1,  1  ,0 , 1,  0 , 1,  0,  1  ,0  ,1 , 0,  0 , 0  ,1 , 0},
                { 0  ,1 , 0 , 0  ,0 , 0 , 0 , 0 , 1 , 0 , 1 , 0  ,1,  0 , 1 , 1 , 1 , 0,  1 , 0},
                { 0 , 1,  0 , 1 , 1 , 1 , 1  ,1 , 1 , 1 , 1 , 0 , 0 , 0,  1 , 0 , 1,  0 , 1 , 0},
                { 0 , 0  ,0 , 1 , 0  ,1 , 0 , 0 , 0 , 0  ,1 , 0 , 1 , 0  ,0  ,0 , 1,  0,  0,  0},
                { 1 , 1 , 0  ,1 , 0 , 1 , 0  ,1 , 1 , 0,  1 , 1 , 1 , 0 , 1 , 1 , 1 , 1 , 1 , 0},
                { 0 , 0,  0 , 0 , 0  ,0 , 0 , 1 , 0 , 0 , 0 , 0 , 1  ,0  ,0 , 0 , 0 , 0 , 1 , 0}};
        int[,] map2 =
                {{0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1},
                { 1, 0, 1, 0, 1, 0, 0, 0, 2, 1, 1, 1, 1, 0, 0, 1, 0, 1, 0, 0},
                { 1, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1},
                { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1},
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0},
                { 1, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 0},
                { 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0},
                { 1, 0, 0, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0},
                { 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
                { 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 1, 0},
                { 0, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 1, 0},
                { 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0} };
        int[,] map3 =
                {{0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1},
                { 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 1, 0},
                { 1, 1, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0},
                { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0},
                { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0},
                { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0},
                { 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1},
                { 0, 1, 0, 1, 0, 1, 0, 1, 2, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1},
                { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 0},
                { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 0},
                { 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 1} };
        #endregion

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
            else if (e.KeyData == Keys.L)
            {
                //addEnemy();
                //addBird();
                //addItem();
                //removeBorder();
            }
        }
        PictureBox enemy;
        int enemycount = 0;
        bool eUp, eDown, eLeft, eRight;
        
        void removeBorder()
        {
            bool stop = false;
            while (stop != true)
            {
                int side, bord;
                
                var Rand = new Random();
                if (Rand.Next(0, 2) == 0)
                {
                    if (Rand.Next(0, 2) == 0)
                    {
                        bord = 0;
                    }
                    else
                    {
                        bord = 19;
                    }
                    int a = Rand.Next(0, 12);
                    if (_Map[a ,bord] == 0)
                    {
                        foreach (var j in Borders)
                        {
                            if (j.y == a && j.x == bord)
                            {
                                //Controls.Remove(j.pb);
                                j.pb.Visible = false;
                                stop = true;
                            }
                        }
                    }
                }
                else
                {
                    if (Rand.Next(0, 2) == 0)
                    {
                        bord = 0;
                    }
                    else
                    {
                        bord = 11;
                    }
                    int a = Rand.Next(0, 20);
                    if (_Map[bord, a] == 0)
                    {
                        foreach (var j in Borders)
                        {
                            if (j.x == a && j.y == bord)
                            {
                                Controls.Remove(j.pb);
                                j.pb.Visible = false;
                                stop = true;
                            }
                        }
                    }
                }
                

            }            
        }

        void removeCertainBorder()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        bool stopRemoving = false;
        Point xx;
//////////////////////////////////////////////////////////////////////////////
        private void addEnemy()
        {
            var Rand = new Random();
            x = Rand.Next(0, 20);
            y = Rand.Next(0, 12);
            while (_Map[y, x] != 0)
            {
                x = Rand.Next(0, 20);
                y = Rand.Next(0, 12);
            }
            


            PictureBox pictureBox1 = new PictureBox();
            xx.X = f * x + 20 + 5;
            xx.Y = k * y + 18 + 5;
            pictureBox1.Location = xx;
            pictureBox1.Size = new System.Drawing.Size(65, 65);
            pictureBox1.Image = Properties.Resources.Типи_даних_C_;
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pictureBox1);
            enemy = pictureBox1;
            
            
            switch(Rand.Next(0, 4))
            {
                case 0:
                    eUp = true;
                    break;
                case 1:
                    eDown = true;
                    break;
                case 2:
                    eLeft = true;
                    break;
                case 3:
                    eRight = true;
                    break;
            }
        }
///////////////////////////////////////////////////////////        
        private void addBird()
        {
            var Rand = new Random();
            x = Rand.Next(0, 20);
            y = Rand.Next(0, 12);
           
            PictureBox pictureBox3 = new PictureBox();
            bb.X = f * x + 20 + 5;
            bb.Y = k * y + 18 + 5;
            pictureBox3.Location = bb;
            pictureBox3.Size = new System.Drawing.Size(100, 100);
            pictureBox3.Image = Properties.Resources._1529678303_Flying_;
            pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            
            this.Controls.Add(pictureBox3);
            this.Bird = pictureBox3;

            Bird.BringToFront();

            var Rand2 = new Random();
            switch (Rand2.Next(0, 4))
            {
                case 0:
                    birdUp = true;
                    birdDown = false;
                    birdLeft = false;
                    birdRight = false;
                    break;
                case 1:
                    birdDown = true;
                    birdUp = false;
                    birdLeft = false;
                    birdRight = false;
                    break;
                case 2:
                    birdLeft = true;
                    birdDown = false;
                    birdUp = false;
                    birdRight = false;
                    break;
                case 3:
                    birdRight = true;
                    birdDown = false;
                    birdLeft = false;
                    birdUp = false;
                    break;
            }
        }
///////////////////////////////////////////////////////////////
        void addItem()
        {
            var Rand = new Random();
            x = Rand.Next(0, 20);
            y = Rand.Next(0, 12);
            while (_Map[y, x] != 0)
            {
                x = Rand.Next(0, 20);
                y = Rand.Next(0, 12);
            }
            PictureBox item = new PictureBox();
            yy.X = x * f + 20 + 30;
            yy.Y = y * k + 18 + 30;
            item.Location = yy;
            item.Size = new System.Drawing.Size(30, 30);

            switch (Rand.Next(0, 1))
            {
                case 0:
                    item.Image = Properties.Resources.item1;
                    break;
            }

            item.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Controls.Add(item);
            this.item = item;
           
        }

        
        Point bb;
        bool birdUp, birdDown, birdLeft, birdRight;

        Point yy;
        bool bounded = false;
        int timer = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                bounded = false;
            }
            
            if (up == true)
            {
                xy.Y -= 2;
                pictureBox1.Location = xy;
            }
            else if (left == true)
            {
                pictureBox1.Image = Properties.Resources.ліво;
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
                pictureBox1.Image = Properties.Resources.ezgif_com_gif_maker;
                xy.X += 2;
                pictureBox1.Location = xy;
            }

            foreach (traps x in Traps)
            {
                if (x.pb != null && pictureBox1 != null && x.pb.Bounds.IntersectsWith(pictureBox1.Bounds))
                {
                    whenBounded();
                }
            }

            foreach (traps x in Borders)
            {
                if (x.pb != null && pictureBox1 != null && x.pb.Bounds.IntersectsWith(pictureBox1.Bounds) && x.pb.Visible == true)
                {
                    whenBounded();
                }
            }

            if (lives <= 0)
            {
                this.Controls.Remove(pictureBox1);
            }

            var Rand2 = new Random();

            if (enemy != null)
            {
                foreach (traps x in Traps)
                {
                    
                    {
                        if (x.pb != null && enemy != null && x.pb.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            var Rand = new Random();
                            if (eUp == true)
                            {
                                eUp = false;
                                xx.Y += 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }

                            }
                            else if (eDown == true)
                            {
                                eDown = false;
                                xx.Y -= 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eUp = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }
                            }
                            else if (eRight == true)
                            {
                                eRight = false;
                                xx.X -= 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eUp = true;
                                        break;
                                }
                            }
                            else if (eLeft == true)
                            {
                                eLeft = false;
                                xx.X += 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eUp = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }
                            }
                            bounded = true;
                            timer += 10;

                        }
                    }
                }
            }
            
            if (stopRemoving != true && pictureBox1 != null && item != null && pictureBox1.Bounds.IntersectsWith(item.Bounds))
            {
                itemCount++;
                if (itemCount == 1)
                {
                    addEnemy();
                }
                else if (itemCount == 2)
                {
                    addBird();
                }
                //item.Location =
                Controls.Remove(item);
                if (itemCount > 4)
                {
                    removeBorder();
                    stopRemoving = true;
                    itemCount = 0;
                }
                else if (stopRemoving == false)
                {
                    addItem();
                }
                
            }
            if (enemy != null)
            {
                foreach (traps x in Borders)
                {
                    
                    {
                        if (x.pb != null && enemy != null && x.pb.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            var Rand = new Random();
                            if (eUp == true)
                            {
                                eUp = false;
                                xx.Y += 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }

                            }
                            else if (eDown == true)
                            {
                                eDown = false;
                                xx.Y -= 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eUp = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }
                            }
                            else if (eRight == true)
                            {
                                eRight = false;
                                xx.X -= 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eLeft = true;
                                        break;
                                    case 2:
                                        eUp = true;
                                        break;
                                }
                            }
                            else if (eLeft == true)
                            {
                                eLeft = false;
                                xx.X += 7;
                                enemy.Location = xx;
                                switch (Rand.Next(0, 3))
                                {
                                    case 0:
                                        eDown = true;
                                        break;
                                    case 1:
                                        eUp = true;
                                        break;
                                    case 2:
                                        eRight = true;
                                        break;
                                }
                            }
                            bounded = true;
                            timer += 10;
                        }
                    }
                }
            }
            ///////////////////////////////////////////////////////////////////////////
            if (Bird != null)
            {
                //birdUp = false;
                //birdRight = true;
                if (birdUp == true)
                {
                    bb.Y -= 2;
                    Bird.Location = bb;
                }
                else if (birdLeft == true)
                {
                    bb.X -= 2;
                    Bird.Location = bb;
                }
                else if (birdDown == true)
                {
                    bb.Y += 2;
                    Bird.Location = bb;
                }
                else if (birdRight == true)
                {
                    bb.X += 2;
                    Bird.Location = bb;
                }
            
                
                if (Rand2.Next(0, 70) == 3)
                {
                    if (birdRight == true || birdLeft == true)
                    {
                        switch (Rand2.Next(0, 2))
                        {
                            case 0:
                                birdUp = true;
                                birdDown = false;
                                birdLeft = false;
                                birdRight = false;
                                break;
                            case 1:
                                birdDown = true;
                                birdUp = false;
                                birdLeft = false;
                                birdRight = false;
                                break;
                        }
                    }
                    else
                    {
                        switch (Rand2.Next(0, 2))
                        {
                            case 0:
                                birdRight = true;
                                birdDown = false;
                                birdLeft = false;
                                birdUp = false;
                                break;
                            case 1:
                                birdLeft = true;
                                birdDown = false;
                                birdUp = false;
                                birdRight = false;
                                break;
                        }
                    }
                }

            
                foreach (var item in Borders)
                {
                    if (item.pb.Bounds.IntersectsWith(Bird.Bounds))
                    {
                        if (birdLeft)
                        {
                            birdLeft = false;
                            birdRight = true;
                            bb.X += 5;
                            Bird.Location = bb;
                        }
                        else if (birdRight)
                        {
                            birdRight = false;
                            birdLeft = true;
                            bb.X -= 5;
                            Bird.Location = bb;
                        }
                        else if (birdUp)
                        {
                            birdUp = false;
                            birdDown = true;
                            bb.Y += 5;
                            Bird.Location = bb;
                        }
                        else if (birdDown)
                        {
                            birdDown = false;
                            birdUp = true;
                            bb.Y -= 5;
                            Bird.Location = bb;
                        }
                    }
                }
            }
            



            if (enemy != null)
            {
                    
                        if (eUp == true)
                        {
                            xx.Y -= 2;
                            enemy.Location = xx;
                        }
                        else if (eLeft == true)
                        {
                            xx.X -= 2;
                            enemy.Location = xx;
                        }
                        else if (eDown == true)
                        {
                            xx.Y += 2;
                            enemy.Location = xx;
                        }
                        else if (eRight == true)
                        {
                            xx.X += 2;
                            enemy.Location = xx;
                        }
                    
                
            }


            if (bounded != true && Rand2.Next(0, 50) == 9)
            {
                if (eRight == true || eLeft == true)
                {
                    eLeft = false;
                    eRight = false;
                    switch (Rand2.Next(0, 2))
                    {
                        case 0:
                            eUp = true;
                            break;
                        case 1:
                            eDown = true;
                            break;
                    }
                }
                else
                {
                    eDown = false;
                    eUp = false;
                    switch (Rand2.Next(0, 2))
                    {
                        case 0:
                            eRight = true;
                            break;
                        case 1:
                            eLeft = true;
                            break;
                    }
                }
                
            }
            if (pictureBox1 != null && (pictureBox1.Location.X > 1920 || pictureBox1.Location.X < 0 || pictureBox1.Location.Y > 1080 || pictureBox1.Location.Y < 0))
            {
                addItem();
            }
            if (pictureBox1 != null)
            {
                if (enemy != null && pictureBox1.Bounds.IntersectsWith(enemy.Bounds))
                {
                    lives--;
                }
                else if (Bird != null && pictureBox1.Bounds.IntersectsWith(Bird.Bounds))
                {
                    lives--;
                }
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
