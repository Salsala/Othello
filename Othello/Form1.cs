using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Othello
{
    public partial class Form_Othello : Form
    {

        public Board Othello_Board = new Board();

        public Form_Othello()
        {
            InitializeComponent();
            Controls.Add(Othello_Board.Panel_Board);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Othello_Board.Reset();
        }
    }

    public class Board
    {
        public Piece[,] pieces = new Piece[8, 8];
        public Panel Panel_Board = new Panel();
        public int Turn = 1;
        public int possible_num = 4;
        bool is_Passed = false;

        public Board()
        {
            Panel_Board.AutoScroll = true;
            Panel_Board.AutoSize = true;
            Panel_Board.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Panel_Board.BorderStyle = BorderStyle.FixedSingle;
            Panel_Board.Location = new Point(3, 3);
            Panel_Board.Name = "Panel_Board";
            Panel_Board.Size = new Size(2, 2);
            Panel_Board.TabIndex = 0;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece a_Piece = new Piece();
                    Panel_Board.Controls.Add(a_Piece.Piece_Image);
                    a_Piece.Piece_Image.Name = "Piece_Box_" + x.ToString() + "_" + y.ToString();
                    if ((x == 3 && y == 3) || (x == 4 && y == 4))
                        a_Piece.Set_Status(Piece.Status.black);
                    else if ((x == 4 && y == 3) || (x == 3 && y == 4))
                        a_Piece.Set_Status(Piece.Status.white);
                    else
                        a_Piece.Set_Status(Piece.Status.empty);
                    a_Piece.Set_Position(x, y);
                    a_Piece.Piece_Image.Click += new EventHandler(Piece_Click);
                    pieces[x, y] = a_Piece;
                }
            }
        }

        public void Reset()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Piece a_Piece = pieces[x, y];
                    if ((x == 3 && y == 3) || (x == 4 && y == 4))
                        a_Piece.Set_Status(Piece.Status.black);
                    else if ((x == 4 && y == 3) || (x == 3 && y == 4))
                        a_Piece.Set_Status(Piece.Status.white);
                    else
                        a_Piece.Set_Status(Piece.Status.empty);
                }
            }

            Turn = 1;
            possible_num = 4;
            is_Passed = false;
    }

        public void Piece_Click(object sender, EventArgs e)
        {
            PictureBox a_Piece_Image = (PictureBox)sender;
            string[] str = a_Piece_Image.Name.Split('_');
            int x = Convert.ToInt32(str[2]);
            int y = Convert.ToInt32(str[3]);

            Scan(x, y, 0);

            if (Turn == 1 && pieces[x, y].isPossible) 
            {
                Reburse();
                Turn = 2;
            }
            else if (Turn == 2 && pieces[x, y].isPossible)
            {
                Reburse();
                Turn = 1;
            }

            Scan_all();
            if (Turn == 0) ;
            else{
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        pieces[i, j].isPossible = false;
                    }
                }
            }

        }

        public void Reburse()
        {
            Piece.Status stat = new Piece.Status();
            if (Turn == 1) stat = Piece.Status.black;
            else if (Turn == 2) stat = Piece.Status.white;

            for (int y = 0; y < 8; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    if (pieces[x, y].isPossible)
                    {
                        pieces[x, y].Set_Status(stat);
                        pieces[x, y].isPossible = false;
                    }
                }
            }
        }

        public void Scan_all()
        {
            possible_num = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Scan(x, y,0);
                    if (pieces[x, y].isPossible) possible_num++;
                }
            }
            if (possible_num == 0)
            {
                if (is_Passed)
                {
                    Turn = 0;
                    is_Passed = false;
                }
                else
                {
                    if (Turn == 1) Turn = 2;
                    else if (Turn == 2) Turn = 1;
                    is_Passed = true;
                    Scan_all();
                }
            }
        }

        public void Scan(int x, int y, int index)
        {
            pieces[x, y].isPossible = false;

            Piece.Status stat = new Piece.Status();
            if (Turn == 1) stat = Piece.Status.white;
            else if (Turn == 2) stat = Piece.Status.black;

            Piece[] piece_Array = new Piece[9];
            for(int i = 0; i < 9; i++)
            {
                switch (i)
                {
                    case 0:
                        piece_Array[i] = pieces[x, y];
                        break;
                    case 1:
                        if (x == 0 || y == 0) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x - 1, y - 1];
                        break;
                    case 2:
                        if (y == 0) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x, y - 1];
                        break;
                    case 3:
                        if (x == 7 || y == 0) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x + 1, y - 1];
                        break;
                    case 4:
                        if (x == 0) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x - 1, y];
                        break;
                    case 5:
                        if (x == 7) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x + 1, y];
                        break;
                    case 6:
                        if (x == 0 || y == 7) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x - 1, y + 1];
                        break;
                    case 7:
                        if (y == 7) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x, y + 1];
                        break;
                    case 8:
                        if (x == 7 || y == 7) piece_Array[i] = null;
                        else piece_Array[i] = pieces[x + 1, y + 1];
                        break;
                }
            }
            if (index == 0)
            {
                if (piece_Array[0].status == Piece.Status.empty)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        if (piece_Array[j] != null)
                        {
                            if (piece_Array[j].status == stat)
                            {
                                switch (j)
                                {
                                    case 1: Scan(x - 1, y - 1, j); break;
                                    case 2: Scan(x, y - 1, j); break;
                                    case 3: Scan(x + 1, y - 1, j); break;
                                    case 4: Scan(x - 1, y, j); break;
                                    case 5: Scan(x + 1, y, j); break;
                                    case 6: Scan(x - 1, y + 1, j); break;
                                    case 7: Scan(x, y + 1, j); break;
                                    case 8: Scan(x + 1, y + 1, j); break;
                                }
                            }
                            else piece_Array[j].isPossible = false;
                            piece_Array[0].isPossible |= piece_Array[j].isPossible;
                        }
                    }
                }
                else piece_Array[0].isPossible = false;
            }
            else
            {
                if (piece_Array[index] != null)
                {
                    if (piece_Array[index].status == stat)
                    {
                        switch (index)
                        {
                            case 1: Scan(x - 1, y - 1, index); break;
                            case 2: Scan(x, y - 1, index); break;
                            case 3: Scan(x + 1, y - 1, index); break;
                            case 4: Scan(x - 1, y, index); break;
                            case 5: Scan(x + 1, y, index); break;
                            case 6: Scan(x - 1, y + 1, index); break;
                            case 7: Scan(x, y + 1, index); break;
                            case 8: Scan(x + 1, y + 1, index); break;
                        }
                    }
                    else if (piece_Array[index].status == Piece.Status.empty)
                    {
                        piece_Array[0].isPossible = false;
                        piece_Array[index].isPossible = false;
                    }
                    else  piece_Array[index].isPossible = true;
                    piece_Array[0].isPossible |= piece_Array[index].isPossible;
                }
                else piece_Array[0].isPossible = false;
            }
        }
    }

    public class Piece
    {
        public PictureBox Piece_Image;
        public Status status;
        public Point position;
        public bool isPossible = false;

        public Piece()
        {
            Piece_Image = new PictureBox();
            Piece_Image.Size = new Size(70, 70);
            Piece_Image.Margin = new Padding(1);
            Piece_Image.SizeMode = PictureBoxSizeMode.StretchImage;
            status = Status.empty;
            position = new Point(0, 0);
            Set_Status(status);
        }
        
        public void Set_Status(Status stat)
        {
            status = stat;
            switch (stat)
            {
                case Status.black:
                    Piece_Image.Image = Properties.Resources.BlackPiece;
                    break;
                case Status.white:
                    Piece_Image.Image = Properties.Resources.WhitePiece;
                    break;
                case Status.empty:
                    Piece_Image.Image = Properties.Resources.Empty;
                    break;
            }
        }
        
        public void Set_Position(int x, int y)
        {
            position = new Point(x,y);
            Piece_Image.Location = new Point(1 + x * 70, 1 + y * 70);
        }
        
        public enum Status
        {
            empty = 2,
            black = 0,
            white = 1
        }
    }

}
