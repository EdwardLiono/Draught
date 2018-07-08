using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int turn;
        int coordinatex;
        int coordinatey;
        int[,] map = new int[8, 8];
        int[,] heu = new int[8, 8];
        Button[,] board = new Button[8, 8];
        Bitmap cPurple = new Bitmap( WindowsFormsApp2.Properties.Resources.circle, 40, 40);
        Bitmap cBlue = new Bitmap(WindowsFormsApp2.Properties.Resources.circle2, 40, 40);
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Button();
                    board[i, j].Location = new Point(i * 50, j * 50);
                    board[i, j].Size = new Size(50, 50);
                    board[i, j].Name = "b" + i.ToString() + j.ToString();
                    board[i, j].Click += new EventHandler(buttonpress);
                    board[i, j].BackColor = Color.White;
                    if ((i + j) % 2 == 1)
                    {
                        board[i, j].BackColor = Color.Black;
                        board[i, j].Tag = "Nothing";
                        if (j <= 2)
                        {
                            board[i, j].Tag = "Purple";
                            board[i, j].Image = cPurple;
                        }
                        else if (j >= 5)
                        {
                            board[i, j].Tag = "Blue";
                            board[i, j].Image = cBlue;
                        }

                    }
                    this.Controls.Add(board[i, j]);
                }

            }
            //MessageBox.Show(board[1, 1].Tag.ToString());
            turn = 1;
            

            checker(turn);
            movecheck();

        }

        void buttonpress(object sender, EventArgs e)
        {
           
            Button now = (Button)sender;
            int i, j;
            i = Convert.ToInt32(now.Name[1]);
            j = Convert.ToInt32(now.Name[2]);
            i -= 48;
            j -= 48;
           //MessageBox.Show(i.ToString()+"  "+j.ToString());
            move(i, j);


            
        }
        void move(int i,int j)
        {
            int dsa = 0;
            if(board[i,j].BackColor == Color.Green)
            {
                clear();
                movecheck();
                board[i, j].BackColor = Color.LimeGreen;
                coordinatex = i;
                coordinatey = j;
                if (turn == 1 && map[i, j] == 2)
                {
                    if (i > 1 && j>1 && board[i - 1, j - 1].Tag== "Purple" && board [i-2,j-2].Tag=="Nothing")
                    {
                        board[i - 2, j - 2].BackColor = Color.Yellow;
                    }
                    if (i < 6 && j>1 && board[i + 1, j - 1].Tag == "Purple" && board[i + 2, j - 2].Tag == "Nothing")
                    {
                        board[i + 2, j - 2].BackColor = Color.Yellow;
                    }
                    if (board[i, j].Text == "King")
                    {
                        if (i > 1 && j < 6 && board[i - 1, j + 1].Tag == "Purple" && board[i - 2, j + 2].Tag == "Nothing")
                        {
                            board[i - 2, j + 2].BackColor = Color.Yellow;
                        }
                        if (i < 6 && j < 6 && board[i + 1, j + 1].Tag == "Purple" && board[i + 2, j + 2].Tag == "Nothing")
                        {
                            board[i + 2, j + 2].BackColor = Color.Yellow;
                        }
                    }
                }
                else if(turn == 1 && map[i,j] == 1)
                {
                    if (i != 0 && j != 0 && board[i - 1,j - 1].Tag == "Nothing")
                    {
                        board[i - 1, j - 1].BackColor = Color.Yellow;
                    }
                    if (i != 7 && j != 0 && board[i + 1, j - 1].Tag == "Nothing")
                    {
                        board[i + 1, j - 1].BackColor = Color.Yellow;
                    }
                    if (board[i, j].Text == "King")
                    {
                        if (i != 0 && j != 7 && board[i - 1, j + 1].Tag == "Nothing")
                        {
                            board[i - 1, j + 1].BackColor = Color.Yellow;
                        }
                        if (i != 7 && j != 7 && board[i + 1, j + 1].Tag == "Nothing")
                        {
                            board[i + 1, j + 1].BackColor = Color.Yellow;
                        }
                    }
                }
                else if (turn == 0 && map[i, j] == 2)
                {
                    if (i > 1 && j<6 && board[i - 1, j + 1].Tag == "Blue" && board[i - 2, j + 2].Tag == "Nothing")
                    {
                        board[i - 2, j + 2].BackColor = Color.Yellow;
                    }
                    if (i < 6 && j<6 && board[i + 1, j + 1].Tag == "Blue" && board[i + 2, j + 2].Tag == "Nothing")
                    {
                        board[i + 2, j + 2].BackColor = Color.Yellow;
                    }
                    if (board[i, j].Text == "King")
                    {
                        if (i > 1 && j > 1 && board[i - 1, j - 1].Tag == "Blue" && board[i - 2, j - 2].Tag == "Nothing")
                        {
                            board[i - 2, j - 2].BackColor = Color.Yellow;
                        }
                        if (i < 6 && j > 1 && board[i + 1, j - 1].Tag == "Blue" && board[i + 2, j - 2].Tag == "Nothing")
                        {
                            board[i + 2, j - 2].BackColor = Color.Yellow;
                        }
                    }
                }
                else if (turn == 0 && map[i, j] == 1)
                {
                    if (i != 0 && j!=7 && board[i - 1, j + 1].Tag == "Nothing")
                    {
                        board[i - 1, j + 1].BackColor = Color.Yellow;
                    }
                    if (i != 7 && j!=7 && board[i + 1, j + 1].Tag == "Nothing")
                    {
                        board[i + 1, j + 1].BackColor = Color.Yellow;
                    }
                    if (board[i, j].Text == "King")
                    {
                        if (i != 0 && j != 0 && board[i - 1, j - 1].Tag == "Nothing")
                        {
                            board[i - 1, j - 1].BackColor = Color.Yellow;
                        }
                        if (i != 7 && j != 0 && board[i + 1, j - 1].Tag == "Nothing")
                        {
                            board[i + 1, j - 1].BackColor = Color.Yellow;
                        }
                    }
                }
            }

            if(board[i,j].BackColor == Color.Yellow)
            {
                clear();
                board[i, j].Image = board[coordinatex, coordinatey].Image;
                board[i, j].Tag = board[coordinatex, coordinatey].Tag;
                board[i, j].Text = board[coordinatex, coordinatey].Text;
                board[coordinatex, coordinatey].Image = null;
                board[coordinatex, coordinatey].Tag = "Nothing";
                board[coordinatex, coordinatey].Text = null;
                if(j==0 && board[i, j].Tag == "Blue")
                {
                    board[i, j].Text = "King";
                }
                else if(j == 7 && board[i, j].Tag == "Purple")
                {
                    board[i, j].Text = "King";
                }
                int fgh, jkl = 0;
                if(map[coordinatex,coordinatey] == 2)
                {
                    board[coordinatex - ((coordinatex - i) / 2), coordinatey - ((coordinatey - j) / 2)].Image = null;
                    board[coordinatex - ((coordinatex - i) / 2), coordinatey - ((coordinatey - j) / 2)].Tag = "Nothing";
                    board[coordinatex - ((coordinatex - i) / 2), coordinatey - ((coordinatey - j) / 2)].Text = null;
                    checker(turn);

                    fgh = map[i, j];
                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            map[x, y] = 0;
                        }
                    }
                    map[i, j] = fgh;

                    if(map[i,j] == 2)
                    {
                        movecheck();
                        jkl = 1;
                    }
                }
                if (turn == 0 && jkl == 0)
                {
                    turn = 1;
                    checker(turn);
                    movecheck();
                    //MessageBox.Show("test");
                }
                else if(turn == 0 && jkl == 1)
                {
                    turn = 0;
                    checker(turn);
                    movecheck();
                    //MessageBox.Show("TEST");
                    AI();
                }
                else if (turn == 1 && jkl == 0)
                {
                    turn = 0;
                    checker(turn);
                    movecheck();
                    //MessageBox.Show("test");
                    AI();
                }
                else if (turn == 1 && jkl == 1)
                {
                    turn = 1;
                    checker(turn);
                    movecheck();
                    //MessageBox.Show("TEST");
                    
                }
            }

        }
        void clear()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        board[i, j].BackColor = Color.Black;
                    }
                }
            }
        }
        
        void checker(int color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    map[i, j] = 0;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(color == 0 && board[i, j].Tag == "Purple")
                    {
                       
                        if(i!=0&&j!=7&&board[i-1, j + 1].Tag == "Nothing")
                        {
                            if (map[i, j] != 2)
                            {
                                map[i, j] = 1;
                            }
                        }
                        else if(i != 0 && i != 1  && j != 7 && j!=6 && board[i-1, j + 1].Tag == "Blue" && board[i-2, j+2].Tag == "Nothing")
                        {
                            map[i, j] = 2;
                        }
                        
                       
                        if(j != 7 && i != 7 &&board[i + 1, j + 1].Tag == "Nothing")
                        {
                            if (map[i, j] != 2)
                            {
                                map[i, j] = 1;
                            }
                        }
                        else if (j != 6 && i != 7 && j != 7 && i!=6 && board[i + 1, j + 1].Tag == "Blue" && board[i + 2, j + 2].Tag == "Nothing")
                        {
                            map[i, j] = 2;
                        }
                        
                        if(board[i, j].Text == "King")
                        {
                            if (i != 0 && j != 0 && board[i - 1, j - 1].Tag == "Nothing")
                            {
                                if (map[i, j] != 2)
                                {
                                    map[i, j] = 1;
                                }
                            }

                            else if (i != 0 && j != 0 && i != 1 && j != 1 && board[i - 1, j - 1].Tag == "Blue" && board[i - 2, j - 2].Tag == "Nothing")
                            {
                                map[i, j] = 2;
                            }



                            if (i != 7 && j != 0 && board[i + 1, j - 1].Tag == "Nothing")
                            {
                                if (map[i, j] != 2)
                                {
                                    map[i, j] = 1;
                                }
                            }
                            else if (i != 7 && j != 0 && i != 6 && j != 1 && board[i + 1, j - 1].Tag == "Blue" && board[i + 2, j - 2].Tag == "Nothing")
                            {
                                map[i, j] = 2;
                            }


                        }

                    }
                    if (color == 1 && board[i, j].Tag == "Blue")
                    {

                        if (i!=0&&j!=0&&board[i - 1, j - 1].Tag == "Nothing")
                        {
                            if (map[i, j] != 2)
                            {
                                map[i, j] = 1;
                            }
                        }
                             
                        else if (i != 0 && j != 0 && i != 1 && j!= 1&&board[i - 1, j - 1].Tag == "Purple" && board[i - 2, j - 2].Tag == "Nothing")
                        {
                            map[i, j] = 2;
                        }
                               
                            

                        if (i != 7 && j != 0 && board[i + 1, j - 1].Tag == "Nothing")
                        {
                            if (map[i, j] != 2)
                            {
                                map[i, j] = 1;
                            }
                        }
                        else if (i != 7 && j != 0 && i != 6 && j != 1 && board[i + 1, j - 1].Tag == "Purple" && board[i + 2, j - 2].Tag == "Nothing")
                        {
                            map[i, j] = 2;
                        }
                        
                        if(board[i ,j].Text == "King")
                        {
                            if (i != 0 && j != 7 && board[i - 1, j + 1].Tag == "Nothing")
                            {
                                if (map[i, j] != 2)
                                {
                                    map[i, j] = 1;
                                }
                            }
                            else if (i != 0 && i != 1 && j != 7 && j != 6 && board[i - 1, j + 1].Tag == "Purple" && board[i - 2, j + 2].Tag == "Nothing")
                            {
                                map[i, j] = 2;
                            }


                            if (j != 7 && i != 7 && board[i + 1, j + 1].Tag == "Nothing")
                            {
                                if (map[i, j] != 2)
                                {
                                    map[i, j] = 1;
                                }
                            }
                            else if (j != 6 && i != 7 && j != 7 && i != 6 && board[i + 1, j + 1].Tag == "Purple" && board[i + 2, j + 2].Tag == "Nothing")
                            {
                                map[i, j] = 2;
                            }
                        }
                        
                    }
                }
            }
        }
        void movecheck()
        {
            int big=0;
            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(map[i,j] > big)
                    {
                        big = map[i, j];
                    }
                }
            }
            //MessageBox.Show(big.ToString());
            if(big == 0)
            {
                int a=0, b=0;
                for(int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if(board[i,j].Image == cPurple)
                        {
                            a = a+1;
                        }
                        if(board[i,j].Image == cBlue)
                        {
                            b = b+1;
                        }
                    }
                }
                if(a > b)
                {
                    MessageBox.Show("purple win");
                }
                if(a < b)
                {
                    MessageBox.Show("blue win");
                }
               
                this.Close();
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] == big)
                    {
                        board[i, j].BackColor = Color.Green;
                    }
                }
            }
        }
        void AI()
        {
            checker(0);
            int heu2=0;
            int big=0;
            int xmove=0;
            int ymove=0;
            int xmove2=0;
            int ymove2=0;
            int x2=0;
            int y2=0;
            int x3=0;
            int y3=0;
            //MessageBox.Show("test");
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    heu[x, y] = 0;
                    if(board[x,y].BackColor== Color.Green)
                    {
                        heu[x, y] = 3;
                        if (y == 0)
                        {
                            heu[x, y] = 1;
                        }
                        else if (y == 2 && y >= 2 && y <= 5)
                        {
                            heu[x, y] = 2;
                        }
                        else if (y == 6 && map[x, y] == 1 && board[x, y].Text != "King")
                        {
                            heu[x, y] = 5;
                        }
                        else if (y == 5 && map[x, y] == 2&& board[x,y].Text!="King")
                        {
                            heu[x, y] = 5;
                        }
                        else if (y >= 3 && y <= 4 && x >= 2 && x <= 5)
                        {
                            heu[x, y] = 4;
                        }

                        if (map[x, y] == 2)
                        {
                            if (x > 1 && y < 6 && board[x - 1, y + 1].Tag == "Blue" && board[x - 2, y + 2].Tag == "Nothing")
                            {
                                heu2 = heu_check(x-2, y+2,x,y);
                                xmove = x - 2;
                                ymove = y + 2;
                                x2 = x;
                                y2 = y;
                            }
                            if (x < 6 && y < 6 && board[x + 1, y + 1].Tag == "Blue" && board[x + 2, y + 2].Tag == "Nothing")
                            {
                                if (heu_check(x + 2, y + 2, x, y) > heu2)
                                {
                                    heu2 = heu_check(x + 2, y + 2, x, y);
                                    xmove = x + 2;
                                    ymove = y + 2;
                                    x2 = x;
                                    y2 = y;
                                }
                            }
                            if (board[x, y].Text == "King")
                            {
                                if (x > 1 && y > 1 && board[x - 1, y - 1].Tag == "Blue" && board[x - 2, y - 2].Tag == "Nothing")
                                {
                                    if (heu_check(x - 2, y - 2, x, y) > heu2)
                                    {
                                        heu2 = heu_check(x - 2, y - 2, x, y);
                                        xmove = x - 2;
                                        ymove = y - 2;
                                        x2 = x;
                                        y2 = y;
                                    }
                                }
                                if (x < 6 && y > 1 && board[x + 1, y - 1].Tag == "Blue" && board[x + 2, y - 2].Tag == "Nothing")
                                {
                                    if (heu_check(x + 2, y - 2, x, y) > heu2)
                                    {
                                        heu2 = heu_check(x + 2, y - 2, x, y);
                                        xmove = x + 2;
                                        ymove = y - 2;
                                        x2 = x;
                                        y2 = y;
                                    }
                                }
                            }
                        }
                        else if (map[x, y] == 1)
                        {
                            if (x != 0 && y != 7 && board[x - 1, y + 1].Tag == "Nothing")
                            {
                                if (heu_check(x - 1, y + 1, x, y) > heu2)
                                {
                                    heu2 = heu_check(x - 1, y + 1, x, y);
                                    xmove = x - 1;
                                    ymove = y + 1;
                                    x2 = x;
                                    y2 = y;
                                }
                            }
                            if (x != 7 && y != 7 && board[x + 1, y + 1].Tag == "Nothing")
                            {
                                if (heu_check(x + 1, y + 1, x, y) > heu2)
                                {
                                    heu2 = heu_check(x + 1, y + 1, x, y);
                                    xmove = x + 1;
                                    ymove = y + 1;
                                    x2 = x;
                                    y2 = y;
                                }
                            }
                            if (board[x, y].Text == "King")
                            {
                                if (x != 0 && y != 0 && board[x - 1, y - 1].Tag == "Nothing")
                                {
                                    if (heu_check(x - 1, y - 1, x, y) > heu2)
                                    {
                                        heu2 = heu_check(x - 1, y - 1, x, y);
                                        xmove = x - 1;
                                        ymove = y - 1;
                                        x2 = x;
                                        y2 = y;
                                    }
                                }
                                if (x != 7 && y != 0 && board[x + 1, y - 1].Tag == "Nothing")
                                {
                                    if (heu_check(x + 1, y - 1, x, y) > heu2)
                                    {
                                        heu2 = heu_check(x + 1, y - 1, x, y);
                                        xmove = x + 1;
                                        ymove = y - 1;
                                        x2 = x;
                                        y2 = y;
                                    }
                                }
                            }
                        }
                        heu[x, y] = heu[x, y] * heu2;
                        if (heu[x, y] > big)
                        {
                            big = heu[x2, y2];
                            xmove2 = xmove;
                            ymove2 = ymove;
                            x3 = x2;
                            y3 = y2;
                        }
                    }
                }
            }
            board[x3, y3].PerformClick();
            board[xmove2, ymove2].PerformClick();
        }

        int heu_check(int xto,int yto,int x,int y)
        {
            int heu2=2;
            if (xto >= 2 && xto <= 5 && yto == 2)
            {
                heu2 = 4;
            }
            else if (xto == 0 || xto == 7)
            {
                heu2 = 4;
            }

            if (xto != 0 && xto != 7 && yto != 7 && yto != 0) 
            {
                if (board[xto - 1, yto + 1].Tag != "Nothing" && board[xto + 1, yto - 1].Tag == "Blue" && board[xto + 1, yto - 1].Text != "King")
                {
                    heu2--;
                }
                else if (board[xto + 1, yto - 1].Tag != "Nothing" && board[xto - 1, yto + 1].Tag == "Blue")
                {
                    heu2--;
                }

                else if (board[xto + 1, yto + 1].Tag != "Nothing" && board[xto - 1, yto - 1].Tag == "Blue" && board[xto - 1, yto - 1].Text != "King")
                { 
                    heu2--;
                }
                else if (board[xto - 1, yto - 1].Tag != "Nothing" && board[xto - 1, yto - 1].Tag == "Blue")
                {
                    heu2--;
                }
            }
            else if(xto<=5&&yto<=5&&board[xto+1,yto+1].Tag=="Blue"&& board[xto + 2, yto + 2].Tag == "Nothing")
            {
                heu2++;
            }
            else if(xto >= 2 && yto <= 5 && board[xto - 1, yto + 1].Tag == "Blue" && board[xto - 2, yto + 2].Tag == "Nothing")
            {
                heu2++;
            }
            else if (xto <= 5 && yto >= 2 && board[xto + 1, yto - 1].Tag == "Blue" && board[xto + 2, yto - 2].Tag == "Nothing" && board[x, y].Text == "King")
            {
                heu2++;
            }
            else if (xto >= 2 && yto >= 2 && board[xto - 1, yto - 1].Tag == "Blue" && board[xto - 2, yto - 2].Tag == "Nothing" && board[x, y].Text == "King")
            {
                heu2++;
            }
            else if(xto <= 6 && yto <= 6&&board[xto + 1, yto + 1].Image == cPurple || xto <= 6 && yto >= 1 && board[xto + 1, yto - 1].Image == cPurple || xto >= 1 && yto <= 6 && board[xto - 1, yto + 1].Image == cPurple || xto >= 1 && yto >= 1 && board[xto - 1, yto - 1].Image == cPurple)
            {
                heu2++;
            }
            
            return heu2;
        }
    }
}
