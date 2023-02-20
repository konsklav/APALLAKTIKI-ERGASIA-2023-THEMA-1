using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Battleship
{
    public partial class Form4 : Form
    {
        Player player;
        int tries;
        int win_count = 0; 
        int loss_count = 0;
        String connectionString = "Data source=games.db;Version=3";
        SQLiteConnection connection;
        public Form4(Player player, int tries)
        {
            InitializeComponent();
            this.player = player;
            this.tries = tries;
            connection = new SQLiteConnection(connectionString);
            connection.Open();
            String createSQL = "Create table if not exists Games(ID integer auto increment primary key,Usrnm Text,Win Text,Time Text)";
            SQLiteCommand command = new SQLiteCommand(createSQL, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new Form1(player)).Show();
            this.Hide();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            player.time = player.timeCountMin.ToString() + ":" + player.timeCountSec.ToString();
            string win;
            if (player.winner == true)
                win = "True";
            else
                win = "False";
            connection.Open();
            String insertSQL = "Insert into Games(Usrnm,Win,Time) values(@username,@win,@time)";
            SQLiteCommand command = new SQLiteCommand(insertSQL, connection);
            command.Parameters.AddWithValue("username", player.name);
            command.Parameters.AddWithValue("win", win);
            command.Parameters.AddWithValue("time", player.time);
            command.ExecuteNonQuery();
            connection.Close();

            connection.Open();
            String selectSQL = "Select * from Games where Usrnm=@username";
            SQLiteCommand command2 = new SQLiteCommand(selectSQL, connection);
            command2.Parameters.AddWithValue("username", player.name);
            SQLiteDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {
                if (reader.GetString(2) == "True")
                {
                    win_count++;
                }
                else
                {
                    loss_count++;
                }
            }
            connection.Close();
            if (player.winner == true)
            {
                   
                label1.Text = player.WinGame(tries) + "\nTotal Wins: " + win_count.ToString() + " Total Losses: "+ loss_count.ToString();
            }else
            {
                label1.Text = player.LoseGame() + "\nTotal Wins: " + win_count.ToString() + " Total Losses: " + loss_count.ToString();
            }
        }
    }
}
