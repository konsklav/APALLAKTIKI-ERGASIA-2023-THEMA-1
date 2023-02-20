using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Security;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
//----------------NAMESPACES-----------------------------------------------------------------------------------------------------------------------
namespace Battleship
{
    public partial class Form1 : Form
    {
//---------------DECLARATIONS------------------------------------------------------------------------------------------------------------
        protected int[,] Pos = new int[10, 10];                                                    //Tracks the positions of ships in player's table
        protected int[,] Pos_E = new int[10, 10];                                                  //Tracks posiotions of ships in enemy's table
        protected int[,] Pos_Check = new int[10, 10];                                              //Keeps track if the panels in the players table have been checked by enemy
        protected Panel[,] P_Panel1 = new Panel[10, 10];
        protected Panel[,] E_Panel1 = new Panel[10, 10];
        protected string[] Lines = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
        protected int[] Start_position = new int[4];                                               //temporary table for positions
        protected int[] Start_position_E = new int[4];                                             //temporary table for positions 
        protected int[] SP_Line = new int[4];                                                      //Start-position line for every ship
        protected int[] EP_Line = new int[4];                                                      //End-pos line
        protected int[] SP_Column = new int[4];                                                    //Start-pos column
        protected int[] EP_Column = new int[4];                                                    //End-pos column
        public PictureBox[] P_P = new PictureBox[4];                                               //Player's lives visualized
        public PictureBox[] P_E = new PictureBox[4];                                               //Enemy's lives visualized
        public Ship[] P_ships_list = new Ship[4];                                                  //Player ships as objects
        public Ship[] E_ships_list = new Ship[4];                                                  //Enemy ships as objects
        protected int line, line1;                                                                 //Temporary vars
        protected int direction, direction1;
        protected int column, column1;
        protected int Elives, Plives;                                                              //Lives
        Player player;
        public int tries;
        protected int temp1 = 0;
        protected int temp2 = 0;



        //-----------------CONSTRUCTOR----------------------------------------------------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        public Form1(Player player)
        {
            InitializeComponent();
            this.player = player;
        }

        //--------------FORM--LOAD----------------------------------------------------------------------------------------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
           
            timer1.Start();
            int x1 = 50;
            int temp3 = 0;
            int temp4 = 0;
            tries = 0;
            int y1 = 80;
            int x2 = 476;
            int y2 = 80;
            Plives = 4;                                                                             //4 lives bc of 4 ships
            Elives = 4;
            Panel P_Panel, E_Panel;
            PictureBox p1, p2;
            Label l1,l2;
            for(int i=0; i<10;i++)
            {
                
                for(int j=0; j<10;j++) 
                {   
                    Pos[i,j] = 0;                                                                   //put in all positions the value 0
                    Pos_E[i, j] = 0;
                    Pos_Check[i,j] = 0;
                    //=======================================================
                    P_Panel = new Panel();
                    E_Panel = new Panel();
                    //=======================================================
                    P_Panel.Name = Lines[i] + (j+1).ToString();
                    E_Panel.Name = Lines[i] + (j+1).ToString();
                    //=======================================================
                    P_Panel.Width = 34;
                    P_Panel.Height = 34;
                    E_Panel.Width = 34;
                    E_Panel.Height = 34;
                    //=======================================================
                    P_Panel.Location = new Point(x1, y1);
                    P_Panel.Visible = true;
                    //=======================================================
                    E_Panel.Location = new Point(x2, y2);
                    E_Panel.Visible = true;
                    //=======================================================
                    P_Panel.BorderStyle= BorderStyle.FixedSingle;
                    E_Panel.BorderStyle = BorderStyle.FixedSingle;
                    //=======================================================
                    P_Panel.Show();
                    E_Panel.Show();
                    //=======================================================
                    Controls.Add(P_Panel);
                    Controls.Add(E_Panel);
                    //=======================================================
                    P_Panel1[i, j] = P_Panel;
                    E_Panel1[i, j] = E_Panel;
                    //==============================================================
                    if (j == 0)
                    {
                        l1 = new Label();
                        l1.Text = Lines[i];
                        l1.Location = new Point(x1 - 34, y1);
                        l1.Visible = true;
                        l1.Width = 34;
                        l1.Height = 34;
                        l1.Show();
                        Controls.Add(l1);
                        //=======================================================
                        l2 = new Label();
                        l2.Text = Lines[i];
                        l2.Location = new Point(x2 - 34, y2);
                        l2.Visible = true;
                        l2.Width = 34;
                        l2.Height = 34;
                        l2.Show();
                        Controls.Add(l2);
                    }
                    //==============================================================
                    if (i == 0)
                    {
                        l1 = new Label();
                        l1.Text = (j+1).ToString();
                        l1.Location = new Point(x1, y1 - 34);
                        l1.Visible = true;
                        l1.Width = 34;
                        l1.Height = 34;
                        l1.Show();
                        Controls.Add(l1);
                        //=======================================================
                        l2 = new Label();
                        l2.Text = (j+1).ToString();
                        l2.Location = new Point(x2, y2 - 34);
                        l2.Visible = true;
                        l2.Width = 34;
                        l2.Height = 34;
                        l2.Show();
                        Controls.Add(l2);
                    }
                    //==============================================================
                    
                    if(i == 0 && j > 2 && j < 7)
                    {
                        p1 = new PictureBox();
                        p1.Location = new Point(x1, y1 - 34*2);
                        p1.Visible = true;
                        p1.SizeMode = PictureBoxSizeMode.StretchImage;
                        p1.Width = 34;
                        p1.Height = 34;
                        p1.ImageLocation = "Float.png";
                        p1.Show();
                        Controls.Add(p1);
                        P_P[temp3++] = p1;
                        //=======================================================
                        p2 = new PictureBox();
                        p2.Location = new Point(x2, y2 - 34 * 2);
                        p2.SizeMode = PictureBoxSizeMode.StretchImage;
                        p2.Visible = true;
                        p2.Width = 34;
                        p2.Height = 34;
                        p2.ImageLocation = "Float.png";
                        p2.Show();
                        Controls.Add(p2);
                        P_E[temp4++] = p2;
                    }
                    //==============================================================
                    x1 += 34;
                    x2 += 34;

                }
                x1 = 50;
                y1 += 34;
                x2 = 476;
                y2 += 34;
            }
            //--------------------------MARK--THE--SHIP--POSITIONS--IN--PLAYERS--ARRAY------------------------------------------------------------------------
            var Random = new Random();                                                                              //CREATE THE RANDOM VARIABLE
            for (int ships = 0; ships<4;ships++)                                                                    // Place the ships on the board
            {
                direction = Random.Next(0, 2);                                                                      //0 -> horizontal  1 -> vertical 
                //==============================================================
                if (direction == 0)
                {
                    bool flag = true;
                    do
                    {
                        Start_position[ships] = Random.Next(1, 6 + ships);                                          //Find the starting position of each ship
                        line = Random.Next(0, 10);                                                                  //Since it's horizonal, find a line to place the ship
                        for (int i = Start_position[ships]; i < Start_position[ships] + 5 - ships; i++)             //0 ->ships
                        {
                            if (Pos[line, i] == 1)
                            {
                                flag = false;
                                break;
                            }
                            if (line == 0 && i == 0)                                                                        
                                if (Pos[line+ 1, i] == 1 || Pos[line, i+1] == 1 || Pos[line +1, i+1] == 1)          //If its at the top left panel
                                {
                                    flag = false;
                                    break;
                                }
                            if (line == 9 && i == 9)
                                if(Pos[line - 1, i] == 1 || Pos[line, i - 1] == 1 || Pos[line - 1, i - 1] == 1)     //if its at the bottom right panel
                                { 
                                    flag = false; 
                                    break;
                                }
                            if (line == 0 && i == 9)
                                if (Pos[line + 1, i] == 1 || Pos[line, i - 1] == 1 || Pos[line + 1, i - 1] == 1)    //if its at the top right panel
                                {
                                    flag = false;
                                    break;
                                }
                            if (line == 9 && i == 0)
                                if (Pos[line - 1, i] == 1 || Pos[line, i + 1] == 1 || Pos[line - 1, i + 1] == 1)    //If its at the bottom left panel
                                {
                                    flag = false;
                                    break;
                                }

                        }
                    } while (flag == false);                                                                        //CHECK IF THE SHIPS ARE PLACED IN CORRECT POSITIONS WITHIN THE RULES
                    SP_Line[ships] = line;
                    SP_Column[ships] = Start_position[ships];
                    EP_Line[ships] = line;
                    for (int i = Start_position[ships]; i < Start_position[ships] + 5 - ships; i++)
                    {
                        Pos[line, i] = 1;                                                                           //Mark with 1 the positions that contain the ships
                        if( i + 1 == Start_position[ships] + 5 - ships)
                            EP_Column[ships] = i;
                    }
                }
                //==============================================================
                else
                {
                    bool flag = true;
                    do
                    {
                        Start_position[ships] = Random.Next(1, 6 + ships);                                          //Find the starting position of each ship
                        column = Random.Next(0, 10);                                                                //Since it's vertical, find a column to place the ship
                        for (int i = Start_position[ships]; i < Start_position[ships] + 5 - ships; i++)
                        {
                            if (Pos[i, column] == 1)
                            {
                                flag = false;
                                break;
                            }
                            if (i == 0 && column == 0)
                                if (Pos[i + 1, column] == 1 || Pos[i, column + 1] == 1 || Pos[i + 1, column + 1] == 1)          //If its at the top left panel
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 9 && column == 9)
                                if (Pos[i - 1, column] == 1 || Pos[i, column - 1] == 1 || Pos[i - 1, column - 1] == 1)          //if its at the bottom right panel
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 0 && column == 9)
                                if (Pos[i + 1, column] == 1 || Pos[i, column - 1] == 1 || Pos[i + 1, column - 1] == 1)          //if its at the top right panel
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 9 && column == 0)
                                if (Pos[i - 1, column] == 1 || Pos[i, column + 1] == 1 || Pos[i - 1, column + 1] == 1)          //If its at the bottom left panel
                                {
                                    flag = false;
                                    break;
                                }
                        }

                    } while (flag == false);                                                                            //CHECK IF THE SHIPS ARE PLACED PROPERLY
                    SP_Column[ships] = column;
                    EP_Column[ships] = column;
                    EP_Line[ships] = Start_position[ships];
                    for (int i = Start_position[ships]; i < Start_position[ships] + 5 - ships; i++)
                    {
                        Pos[i, column] = 1;                                                                            //Mark with 1 the positions that contain the ships
                        if (i + 1 == Start_position[ships] + 5 - ships)
                            EP_Line[ships] = i;

                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Pos[i,j] == 1)
                    {
                        P_Panel1[i,j].BackColor = Color.Gray;                                                           //MARK THE SHIPS ON SCREEN
                    }
                }
            }
            //--------------------CREATE--THE--SHIPS--AS--OBJECTS--PLAYER-------------------------------------------------------------------------------------------
            Ship Aircraft_carrier_P = new Ship("Aircraft Carrier",5, SP_Line[0], EP_Line[0], SP_Column[0], EP_Column[0],"P");
            Ship Destroyer_P = new Ship("Destroyer",4, SP_Line[1], EP_Line[1], SP_Column[1], EP_Column[1], "P");
            Ship Warship_P = new Ship("Warship",3, SP_Line[2], EP_Line[2], SP_Column[2], EP_Column[2], "P");
            Ship Submarine_P = new Ship("Submarine", 2, SP_Line[3], EP_Line[3], SP_Column[3], EP_Column[3], "P");
            P_ships_list[0] = Aircraft_carrier_P;
            P_ships_list[1] = Destroyer_P;
            P_ships_list[2] = Warship_P;
            P_ships_list[3] = Submarine_P;
            //-------------------RESET--LINES--AND--COLUMNS--TABLES--WITH--0-------------------------------------------------------------------------------------
            for (int i=0; i < 4; i++)
            {
                SP_Column[i] = 0;
                SP_Line[i] = 0;
                EP_Column[i] = 0;
                EP_Line[i] = 0;
            }
            //--------------------------MARK--THE--SHIP--POSITIONS--IN--ENEMY'S--ARRAY--------------------------------------------------------------------------------
            for (int ships = 0; ships < 4; ships++)                                                                     // Place the ships on the board
            {
                direction1 = Random.Next(0, 2);                                                                         //0 -> horizontal  1 -> vertical 
                //==============================================================
                if (direction1 == 0) 
                {
                    bool flag = true;
                    do
                    {
                        Start_position_E[ships] = Random.Next(1, 6 + ships);                                            //Find the starting position of each ship
                        line1 = Random.Next(0, 10);                                                                     //Since it's horizonal, find a line to place the ship
                        for (int i = Start_position_E[ships]; i < Start_position_E[ships] + 5 - ships; i++)
                        {
                            if (Pos_E[line1, i] == 1)
                            {
                                flag = false;
                                break;
                            }
                            if (line1 == 0 && i == 0)
                                if (Pos_E[line1 + 1, i] == 1 || Pos_E[line1, i + 1] == 1 || Pos_E[line1 + 1, i + 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (line1 == 9 && i == 9)
                                if (Pos_E[line1 - 1, i] == 1 || Pos_E[line1, i - 1] == 1 || Pos_E[line1 - 1, i - 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (line1 == 0 && i == 9)
                                if (Pos_E[line1 + 1, i] == 1 || Pos_E[line1, i - 1] == 1 || Pos_E[line1 + 1, i - 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (line1 == 9 && i == 0)
                                if (Pos_E[line1 - 1, i] == 1 || Pos_E[line1, i + 1] == 1 || Pos_E[line1 - 1, i + 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                        }

                    } while (flag == false);                                                                            //CHECKS THE ENEMY'S SHIP'S POSITIONS
                    //=========================================================
                    SP_Line[ships] = line1;
                    EP_Line[ships] = line1;
                    SP_Column[ships] = Start_position_E[ships];
                    for (int i = Start_position_E[ships]; i < Start_position_E[ships] + 5 - ships; i++)
                    {
                        Pos_E[line1, i] = 1;                                                                            //Mark with 1 the positions that contain the ships
                        if(i + 1 == Start_position_E[ships] + 5 - ships)
                            EP_Column[ships] = i;
                    }
                }
                //=================ELSE=============================================
                else
                {
                    bool flag = true;
                    do
                    {
                        Start_position_E[ships] = Random.Next(1, 6 + ships);                                             //Find the starting position of each ship
                        column1 = Random.Next(0, 10);                                                                    //Since it's horizonal, find a line to place the ship
                        for (int i = Start_position_E[ships]; i < Start_position_E[ships] + 5 - ships; i++)
                        {
                            if (Pos_E[i, column1] == 1)
                            {
                                flag = false;
                                break;
                            }
                            if (i == 0 && column1 == 0)
                                if (Pos_E[i + 1, column1] == 1 || Pos_E[i, column1 + 1] == 1 || Pos_E[i + 1, column1 + 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 9 && column1 == 9)
                                if (Pos_E[i - 1, column1] == 1 || Pos_E[i, column1 - 1] == 1 || Pos_E[i - 1, column1 - 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 0 && column1 == 9)
                                if (Pos_E[i + 1, column1] == 1 || Pos_E[i, column1 - 1] == 1 || Pos_E[i + 1, column1 - 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                            if (i == 9 && column1 == 0)
                                if (Pos_E[i - 1, column1] == 1 || Pos_E[i, column1 + 1] == 1 || Pos_E[i - 1, column1 + 1] == 1)
                                {
                                    flag = false;
                                    break;
                                }
                        }

                    } while (flag == false);                                                                             //CHECK THE SHIP'S POSITIONS
                    SP_Column[ships] = column1;
                    EP_Column[ships] = column1;
                    SP_Line[ships] = Start_position_E[ships];

                    for (int i = Start_position_E[ships]; i < Start_position_E[ships] + 5 - ships; i++)
                    {
                        Pos_E[i, column1] = 1;                                                                           //Mark with 1 the positions that contain the ships
                        if (i + 1 == Start_position_E[ships] + 5 - ships)
                            EP_Line[ships] = i;
                    }
                }
            }
            //--------------------CREATE--THE--SHIPS--AS--OBJECTS------------------------------------------------------------------------------
            Ship Aircraft_carrier_E = new Ship("Aircraft Carrier",5, SP_Line[0], EP_Line[0], SP_Column[0], EP_Column[0], "E");
            Ship Destroyer_E = new Ship("Destroyer", 4, SP_Line[1], EP_Line[1], SP_Column[1], EP_Column[1], "E");
            Ship Warship_E = new Ship("Warship", 3, SP_Line[2], EP_Line[2], SP_Column[2], EP_Column[2], "E");
            Ship Submarine_E= new Ship("Submarine", 2, SP_Line[3], EP_Line[3], SP_Column[3], EP_Column[3], "E");
            E_ships_list[0] = Aircraft_carrier_E;
            E_ships_list[1] = Destroyer_E;
            E_ships_list[2] = Warship_E;
            E_ships_list[3] = Submarine_E;
        }
        //------------------PLAY---------------------------------------------------------------------------------------------------------------
        
        private void button1_Click(object sender, EventArgs e)                                                          //when the button is clicked
        {

            if (textBox1.Text == "")
                return;
            tries++;
            bool flag = false;                                                                                          //Flag indicates if a valid coordinate has been typed in the textbox
            do                                                                                                          //and we check that with the do-while statement
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)                                                                        //in these loops we check the coordinate that is typed
                    {
                        if (E_Panel1[i, j].Name == textBox1.Text)                                                       //if the text == with the name of one of the panels
                        {
                            flag = true;                                                                                //true

                            if (E_Panel1[i, j].BackColor == Color.Red || E_Panel1[i, j].BackColor == Color.Green)       //case: already checked
                            {                                                                                           //      try again
                                MessageBox.Show("Coordinate already checked!");
                                return;
                            }
                            else if (Pos_E[i, j] == 1)                                                                  //case: never checked befor
                            {                                                                                           //      BG red, Pos=0, check if sybmerged and if game is over
                                E_Panel1[i, j].BackColor = Color.Red;
                                PictureBox p = new PictureBox();
                                p.ImageLocation = "X.png";
                                p.Size = new Size(34, 34);
                                p.SizeMode = PictureBoxSizeMode.StretchImage;
                                p.Location = E_Panel1[i, j].Location;
                                p.BorderStyle = BorderStyle.FixedSingle;
                                p.Show();
                                Controls.Add(p);
                                E_Panel1[i, j].Hide();

                                Pos_E[i, j] = 0;
                                foreach (Ship s in E_ships_list)
                                {
                                    if (s.Startp_l <= i && i <= s.Endp_l && s.Startp_c <= j && j <= s.Endp_c && !(s.Submerged))
                                    {
                                        s.Lose_Life();
                                        if (s.Submerged)            //MAKE THIS HERE, IT DOESNT WORK IT SHOWS FIRST THE SUBMARINE ALWAYS
                                        {
                                            --Elives;
                                            P_E[temp1++].Hide();                                                     //Manages lives

                                            if (Elives == 0)
                                            {
                                                timer1.Stop();
                                                player.winner = true;
                                                (new Form4(player,tries)).Show();
                                                this.Hide();
                                                return;
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                }

                                textBox1.Text = "";
                                break;
                            }
                            else                                                                                        //case: Never checked and not containing ship
                            {                                                                                           //      BG Green
                                E_Panel1[i, j].BackColor = Color.Green;
                                PictureBox p = new PictureBox();
                                p.ImageLocation = "Dash.png";
                                p.Size = new Size(34, 34);
                                p.SizeMode = PictureBoxSizeMode.StretchImage;
                                p.BorderStyle= BorderStyle.FixedSingle;
                                p.Location = E_Panel1[i, j].Location;
                                p.Show();
                                Controls.Add(p);
                                E_Panel1[i, j].Hide();
                                textBox1.Text = "";

                            }


                        }
                    }
                    if (flag)                                                                                           //If flag=true correct check so enemy can play, therefore stop the loop
                        break;
                }
                if (flag == false && textBox1.Text != "")
                {
                    MessageBox.Show("Please enter a valid position!");                                                  //If there is an invalid input, place message
                    textBox1.Text = "";
                    return;                                                                                             //Stop function and let user type new coordinate
                }

            } while (!flag);

            //--------------ENEMY--PLAY----------------------------------------------------------------------------------------------------
            Random random = new Random();
            flag = false;                                                                                               //Checks if coordinate has been checked before
            string text = Lines[random.Next(0, 10)] + random.Next(1, 11).ToString();                                     //Random coordinate
            do                                                                                                          //Check if the coordinate hasn't been checked before
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)                                                                        //These loops find the panel with the same name as the text
                    {
                        if (P_Panel1[i, j].Name == text)
                        {
                            if (P_Panel1[i, j].BackColor == Color.Red || Pos_Check[i, j] == 1)                          //case: Has been checked
                            {                                                                                           //      New text2, repeat proccess from the begining
                                text = Lines[random.Next(0, 10)] + random.Next(1, 11).ToString();
                                break;
                            }
                            else if (Pos[i, j] == 1)                                                                    //case: Hasn't been checked AND it contains ship
                            {                                                                                           //      BG red, Pos=0, Check if ship is submerged AND if the game is over
                                P_Panel1[i, j].BackColor = Color.Red;
                                PictureBox p = new PictureBox();
                                p.BackColor = Color.Gray;
                                p.ImageLocation = "X.png";
                                p.Size = new Size(34, 34);
                                p.SizeMode = PictureBoxSizeMode.StretchImage;
                                p.BorderStyle = BorderStyle.FixedSingle;
                                p.Location = P_Panel1[i, j].Location;
                                p.Show();
                                Controls.Add(p);
                                P_Panel1[i, j].Hide();//      Pos_Check = 1(Means that the panel has been checked before)
                                Pos[i, j] = 0;
                                flag = true;
                                foreach (Ship s in P_ships_list)
                                {
                                    if (s.Startp_l <= i && i <= s.Endp_l && s.Startp_c <= j && j <= s.Endp_c)
                                    {
                                        s.Lose_Life();
                                        if (s.Submerged)
                                        {
                                            --Plives;
                                            P_P[temp2++].Hide();                                                     // If a ship has been sunk, we hide one of its visualised lives
                                        }
                                        if (Plives == 0)
                                        {
                                            timer1.Stop();
                                            (new Form4(player, tries)).Show();
                                            this.Hide();
                                            return;
                                        }
                                        break;
                                    }
                                }
                                Pos_Check[i, j] = 1;
                                break;
                            }
                            Pos_Check[i, j] = 1;
                            flag = true;
                            break;
                        }
                    }
                }
            } while (!flag);                                                                                            //Break when the flag is true, in other words
                                                                                                                        //when a correct coordinate has been checked(->Not checked before)
            
        }
        //-----------------------TIMER----------------------------------------------------------------------------------------------------
        private void timer1_Tick(object sender, EventArgs e)
        {
            player.timeCountSec++;

            if (player.timeCountMin < 10 && player.timeCountSec < 10)
            {
                label1.Text = "Timer: " + "0" + player.timeCountMin.ToString() + ":" + "0" + player.timeCountSec.ToString();
            }
            else if (player.timeCountMin < 10)
            {
                label1.Text = "Timer: " + "0" + player.timeCountMin.ToString() + ":" + player.timeCountSec.ToString();
            }
            else if (player.timeCountSec < 10)
            {
                label1.Text = "Timer: " + player.timeCountMin.ToString() + ":" + "0" + player.timeCountSec.ToString();
            }
            else
            {
                label1.Text = "Timer: " + player.timeCountMin.ToString() + ":" + player.timeCountSec.ToString();
            }

            if (player.timeCountSec == 59)
            {
                player.timeCountSec = -1;
                player.timeCountMin++;
            }

        }
    }
}


//----------------END--OF--PROGRAM------------------------------------------------------------------------------------------------------

