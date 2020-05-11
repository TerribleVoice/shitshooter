using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShitShooter
{
    class Program
    {
        public static HashSet<Target> defaultTargets = new HashSet<Target>
        {
            new Target(new Point(1, 0), 1),
            new Target(new Point(2, 0), 1),
            new Target(new Point(3, 0), 1)
        };

        static void Main(string[] args)
        {
            var Game = new Game(5,5);
            var Player = new Player(new Point(2,4), Game);
            var targets = defaultTargets;
            Application.Run(new Form1(Game, Player, defaultTargets));
        }
    }
}
