using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {

        // here we define variables
        int xWins = 0;
        int yWins = 0;
        int d = 0;
        Random rnd = new Random();
        int temp = 0;
        Button[] b = new Button[9];

        public Form1()
        {
            InitializeComponent();
        }

        //Here form will be loaded like a program load in to memory
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                b[i] = new Button();
                b[i].Width = 100;
                b[i].Height = 100;
                b[i].Click += new EventHandler(Form1_Click);

                flowLayoutPanel1.Controls.Add(b[i]);
            }

        }

        bool flag = false;
        int draw = 0;
        /// <summary>
        /// Reset is function who clear the board.
        /// </summary>
        void reset()
        {
            for (int i = 0; i < 9; i++)
            {
                b[i].Enabled = true;
                b[i].Text = "";
                draw = 0;
            }
        }

        void Form1_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if (radioButton1.Checked)
            {
                if (!flag)
                {
                    bt.Text = "X";
                    label10.Text = "O";
                    flag = true;

                }
                else
                {
                    bt.Text = "O";
                    label10.Text = "X";
                    flag = false;
                }
                bt.Enabled = false;

                draw++;

                //call check funtion for conditions
                check();
                if (draw == 9)
                {
                    MessageBox.Show("Game Draw");
                    d++;
                    label6.Text = d.ToString();
                    reset();
                }
            }
            if (radioButton2.Checked)
            {
                //playe's turn
                bt.Text = "X";
                bt.Enabled = false;
                label10.Text = "X";
                flag = true;

                draw++;

                //call check funtion for conditions
                check();
                if (draw == 9)
                {
                    MessageBox.Show("Game Draw");
                    d++;
                    label6.Text = d.ToString();
                    reset();
                }
                //////////////////////computer's turn

                if (were_win("O") != -1)
                    put_in(were_win("O"));
                else if (were_win("X") != -1)
                    put_in(were_win("X"));
                else if (arrows() != -1)
                    put_in(arrows());
                else
                {
                    while (!b[temp].Enabled)
                        temp = rnd.Next(0, 9);
                    put_in(temp);
                }


                draw++;

                //call check funtion for conditions
                check();
                if (draw == 9)
                {
                    MessageBox.Show("Game Draw");
                    d++;
                    label6.Text = d.ToString();
                    reset();
                }
            }
        }
        void put_in(int i)
        {
            b [i].Text = "O";
            b[i].Enabled = false;
        }
        int arrows()
        {
            if (draw <= 3)
            {
                if (b[4].Enabled)//middle
                    return 4;
                if (b[4].Text == "X")//middle
                    return 0;
                for (int i = 1; i < 8; i += 2)
                    if (b[i].Enabled)
                        return i;
            }
            return -1;
        }
        int were_win(string str)
        {
            int win = 0;
            int empty = -1;
            //check rows
            for (int i = 0; i < 7; i += 3)
            {
                win = 0; empty = -1;
                for (int j = 0; j < 3; j++)
                {
                    if (b[i+j].Text == str)
                        win++;
                    if (b[i + j].Enabled)
                        empty = i+j;
                }
                if (win == 2 && empty != -1)
                    return empty;
            }
            //check columns
            for (int i = 0; i < 3; i ++)
            {
                win = 0; empty = -1;
                for (int j = 0; j < 7; j+=3)
                {
                    if (b[i + j].Text == str)
                        win++;
                    if (b[i + j].Enabled)
                        empty = i + j;
                }
                if (win == 2 && empty != -1)
                    return empty;
            }
            win = 0; empty = -1;
            for (int i = 0; i < 9; i+=4)
            {
                if (b[i].Text == str)
                    win++;
                if (b[i].Enabled)
                    empty = i;
            }
            if (win == 2 && empty != -1)
                return empty;
            win = 0; empty = -1;
            for (int i = 2; i < 7; i += 2)
            {
                if (b[i].Text == str)
                    win++;
                if (b[i].Enabled)
                    empty = i;
            }
            if (win == 2 && empty != -1)
                return empty;
            return -1;
        }
        void Form1_Click1(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            if (!flag)
            {
                bt.Text = "X";
                label10.Text = "O";
                flag = true;

            }
            else
            {
                bt.Text = "O";
                label10.Text = "X";
                flag = false;
            }
            bt.Enabled = false;

            draw++;

            //call check funtion for conditions
            check();
            if (draw == 9)
            {
                MessageBox.Show("Game Draw");
                d++;
                label6.Text = d.ToString();
                reset();
            }
        }

        void check() // Here is the check() function in which conditions will full fill.
        {
            //For Rows
            for (int i = 0; i < 7; i += 3)
            {
                if (b[i].Text == b[i + 1].Text && b[i + 1].Text == b[i + 2].Text && b[i].Text != "")
                    check_print(i);
            }
            //For Coloums
            for (int i = 0; i < 3; i ++)
            {
                if (b[i].Text == b[i+3].Text && b[i+3].Text == b[i+6].Text && b[i].Text != "")
                    check_print(i);
            }
            //For Diagnols
            if (b[0].Text == b[4].Text && b[4].Text == b[8].Text && b[0].Text != "")
                check_print(0);
            if (b[2].Text == b[4].Text && b[4].Text == b[6].Text && b[2].Text != "")
                check_print(2);

        }
        void check_print(int i)
        {
            MessageBox.Show(b[i].Text + " Wins");
            if (b[i].Text == "X")
            {
                xWins++;
                label3.Text = xWins.ToString();
            }
            else
            {
                yWins++;
                label4.Text = yWins.ToString();
            }
            reset();
        }

    }

}
