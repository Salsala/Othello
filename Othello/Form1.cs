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
    partial class Form_Othello : Form
    {
        public Board Othello_Board = new Board();

        public Form_Othello()
        {
            InitializeComponent();
            Controls.Add(Othello_Board.Panel_Board);
            Othello_Board.btn_Reset.Click += new EventHandler(btn_Reset_Click);
            Othello_Board.Turn_Box.Location = new Point(Othello_Board.Panel_Board.Right + 2, Othello_Board.Panel_Board.Top);
            Controls.Add(Othello_Board.Turn_Box);
        }

        public void btn_Reset_Click(object sender, EventArgs e)
        {
            Othello_Board.Reset();
        }
    }

    public partial class Board
    {
        public Board()
        {
            Initialize_Board();
            Initialize_TurnBox();

            
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

            Count_Piece();
            Turn_Change();
            turn = Turn.black;
            possible_num = 4;
            is_Passed = false;
            WINNER.Visible = false;
            Count_Piece();
            Turn_Change();
        }

        public void Piece_Click(object sender, EventArgs e)
        {
            PictureBox a_Piece_Image = (PictureBox)sender;
            string[] str = a_Piece_Image.Name.Split('_');
            int x = Convert.ToInt32(str[2]);
            int y = Convert.ToInt32(str[3]);

            Scan(x, y, 0);

            if (turn == Turn.black && pieces[x, y].isPossible) 
            {
                Reburse();
                turn = Turn.white;
            }
            else if (turn == Turn.white && pieces[x, y].isPossible)
            {
                Reburse();
                turn = Turn.black;
            }

            Scan_all();

            if (turn == Turn.game_over)
            {
                if (num_Black > num_White)
                {
                    WINNER.Text = "Winner is Black!!";
                    WINNER.Visible = true;
                }
                else if (num_Black < num_White)
                {
                    WINNER.Text = "Winner is White!!";
                    WINNER.Visible = true;
                }
                else if(num_Black < num_White)
                {
                    WINNER.Text = "Draw Game!!";
                    WINNER.Visible = true;
                }
            }
            else
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        pieces[i, j].isPossible = false;
                    }
                }
            }

            Count_Piece();
            Turn_Change();
        }

        public void Turn_Change()
        {
            switch (turn)
            {
                case Turn.black:
                    Player1_Image.Visible = true;
                    Player2_Image.Visible = false;
                    break;
                case Turn.white:
                    Player1_Image.Visible = false;
                    Player2_Image.Visible = true;
                    break;
            }
        }

        public void Count_Piece()
        {
            num_Black = 0;
            num_White = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    switch (pieces[x, y].status)
                    {
                        case Piece.Status.black:
                            num_Black++;
                            break;
                        case Piece.Status.white:
                            num_White++;
                            break;
                    }
                }
            }
            Player1_Num.Text = num_Black.ToString();
            Player2_Num.Text = num_White.ToString();
        }

        public void Reburse()
        {
            Piece.Status stat = new Piece.Status();
            if (turn == Turn.black) stat = Piece.Status.black;
            else if (turn == Turn.white) stat = Piece.Status.white;

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
                    turn = Turn.game_over;
                    is_Passed = false;
                }
                else
                {
                    if (turn == Turn.black) turn = Turn.white;
                    else if (turn == Turn.white) turn = Turn.black;
                    is_Passed = true;
                    Scan_all();
                }
            }
            else is_Passed = false;
        }

        public void Scan(int x, int y, int index)
        {
            pieces[x, y].isPossible = false;

            Piece.Status stat = new Piece.Status();
            if (turn == Turn.black) stat = Piece.Status.white;
            else if (turn == Turn.white) stat = Piece.Status.black;

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
