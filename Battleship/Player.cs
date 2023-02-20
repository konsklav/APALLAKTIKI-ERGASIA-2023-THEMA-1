using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class Player
    {
        public bool winner;
        public string name;
        public string time;
        public int timeCountMin, timeCountSec;

        public Player(string name)
        {
            winner = false;
            this.name = name;
            time = "";
            timeCountMin = 0;
            timeCountSec = 0;
        }


        public string WinGame(int tries)
        {

            string result = "You Won! Time: " + timeCountMin.ToString() + ":" + timeCountSec.ToString() + " Guesses: " + tries.ToString();
            return result;

        }

        public string LoseGame()
        {

            string result = "Enemy Won! Try Again!";
            return result;

        }
    }
}
