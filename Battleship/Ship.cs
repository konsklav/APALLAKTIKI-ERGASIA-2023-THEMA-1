using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Battleship
{
    public class Ship
    {
        protected int size;
        protected string name;
        protected bool submerged;
        protected int startp_l, endp_l, startp_c, endp_c;
        protected int lives;
        protected string P_E;
        public Ship(string name, int size, bool submerged, int startp_l, int endp_l,int startp_c, int endp_c, int lives, string P_E)
        {
            this.name = name;
            this.size = size;
            this.submerged = submerged;
            this.startp_l = startp_l;
            this.startp_c = startp_c;
            this.endp_l = endp_l;
            this.endp_c = endp_c;
            this.lives = lives;
            this.P_E = P_E;
        }

        public Ship(string name, int size, int startp_l, int endp_l, int startp_c, int endp_c, string P_E)
        {
            this.name = name;
            this.size = size;
            this.startp_l =startp_l;
            this.startp_c = startp_c;
            this.endp_l =endp_l;
            this.endp_c =endp_c;
            submerged = false;
            lives = size;
            this.P_E = P_E;
        }

        public int Size { get { return size; } }

        public bool Submerged { get { return submerged; } }
        public int Startp_l { get { return startp_l; } }
        public int Endp_l { get { return endp_l;} }

        public int Startp_c { get { return startp_c;} }

        public int Endp_c { get { return endp_c;} }

        public int Lives { get { return lives; } }

        public void check(int f)         // Checks if the ship has been hit
        {
            if(f==0)
            {
                submerged= true;
                if (P_E == "E")
                {
                    MessageBox.Show("The enemy's " + name + " has been sunk!");
                }
                else
                {
                    MessageBox.Show("My " + name + " has been sunk!");
                }
            }
        }

        public void Lose_Life()
        {
            lives--;
            check(lives);
        }

    }
}
