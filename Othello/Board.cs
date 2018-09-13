using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Othello
{
    public partial class Board
    {
        public Piece[,] pieces = new Piece[8, 8];
        public Panel Panel_Board = new Panel();
        public Turn turn = Turn.black;
        public PlayMode playMode = PlayMode.single;
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


        public enum Turn { black, white, game_over }
        public enum PlayMode { single, server, client}

        public void Initialize_Board()
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

        public void Initialize_TurnBox()
        {
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
        }
    }
}
