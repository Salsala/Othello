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

    public class Board
    {
        public Piece[,] pieces = new Piece[8, 8];
        public Panel Panel_Board = new Panel();
        public int Turn = 1;
        public int possible_num = 4;
        bool is_Passed = false;
        public int num_Black = 2;
        public int num_White = 2;
        public GroupBox Turn_Box;
        public Button btn_Reset;
        public PictureBox Player1_Image;
        private Label Player1_Name;
        public Label Player1_Num;
        public PictureBox Player2_Image;
        private Label Player2_Name;
        public Label Player2_Num;
        public Label WINNER;

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

            Turn_Box = new GroupBox();
            btn_Reset = new Button();
            Player1_Image = new PictureBox();
            Player1_Name = new Label();
            Player1_Num = new Label();
            Player2_Image = new PictureBox();
            Player2_Name = new Label();
            Player2_Num = new Label();
            WINNER = new Label();

            // TurnBox
            // 
            Turn_Box.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Turn_Box.Controls.Add(Player1_Image);
            Turn_Box.Controls.Add(Player1_Name);
            Turn_Box.Controls.Add(Player1_Num);
            Turn_Box.Controls.Add(Player2_Image);
            Turn_Box.Controls.Add(Player2_Name);
            Turn_Box.Controls.Add(Player2_Num);
            Turn_Box.Controls.Add(WINNER);
            Turn_Box.Controls.Add(btn_Reset);
            Turn_Box.Font = new Font("나눔고딕", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(129)));
            Turn_Box.Location = new Point(12, 12);
            Turn_Box.Name = "TurnBox";
            Turn_Box.Size = new Size(157, 180);
            Turn_Box.TabIndex = 1;
            Turn_Box.TabStop = false;
            Turn_Box.Text = "TURN";

            // btn_Reset
            //
            btn_Reset.Location = new Point(75, 140);
            btn_Reset.Name = "btn_Reset";
            btn_Reset.Size = new Size(73, 33);
            btn_Reset.TabIndex = 0;
            btn_Reset.Text = "재시작";
            btn_Reset.UseVisualStyleBackColor = true; ;

            // Player1_Image
            // 
            Player1_Image.Image = Properties.Resources.TrunBlack;
            Player1_Image.Location = new Point(10, 30);
            Player1_Image.Name = "Player1_Image";
            Player1_Image.Size = new Size(25, 25);
            Player1_Image.SizeMode = PictureBoxSizeMode.StretchImage;
            Player1_Image.TabIndex = 0;
            Player1_Image.TabStop = false;

            // Player1_Name
            // 
            Player1_Name.AutoSize = true;
            Player1_Name.Location = new Point(38, 33);
            Player1_Name.Name = "Player1_Name";
            Player1_Name.Size = new Size(60, 19);
            Player1_Name.TabIndex = 1;
            Player1_Name.Text = "BLACK";

            // Player1_Num
            // 
            Player1_Num.Location = new Point(114, 30);
            Player1_Num.Name = "Player1_Num";
            Player1_Num.Size = new Size(34, 25);
            Player1_Num.TabIndex = 4;
            Player1_Num.Text = "2";
            Player1_Num.TextAlign = ContentAlignment.MiddleRight;

            // Player2_Image
            // 
            Player2_Image.Image = Properties.Resources.TrunWhite;
            Player2_Image.Location = new Point(10, 61);
            Player2_Image.Name = "Player2_Image";
            Player2_Image.Size = new Size(25, 25);
            Player2_Image.SizeMode = PictureBoxSizeMode.StretchImage;
            Player2_Image.TabIndex = 2;
            Player2_Image.TabStop = false;
            Player2_Image.Visible = false;

            // Player2_Name
            // 
            Player2_Name.AutoSize = true;
            Player2_Name.Location = new Point(38, 64);
            Player2_Name.Name = "Player2_Name";
            Player2_Name.Size = new Size(59, 19);
            Player2_Name.TabIndex = 3;
            Player2_Name.Text = "WHITE";

            // Player2_Num
            // 
            Player2_Num.Location = new Point(114, 61);
            Player2_Num.Name = "Player2_Num";
            Player2_Num.Size = new Size(34, 25);
            Player2_Num.TabIndex = 5;
            Player2_Num.Text = "2";
            Player2_Num.TextAlign = ContentAlignment.MiddleRight;

            // WINNER
            // 
            WINNER.AutoSize = true;
            WINNER.Location = new Point(15, 100);
            WINNER.Name = "WINNER";
            WINNER.Size = new Size(60, 19);
            WINNER.TabIndex = 1;
            WINNER.Text = "Winner is Black!!";
            WINNER.Visible = false;

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

            Count_Piece();
            Turn_Change();
            Turn = 1;
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

            if (Turn == 0)
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
            switch (Turn)
            {
                case 1:
                    Player1_Image.Visible = true;
                    Player2_Image.Visible = false;
                    break;
                case 2:
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
            else is_Passed = false;
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
