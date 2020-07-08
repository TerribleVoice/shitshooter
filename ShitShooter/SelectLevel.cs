using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShitShooter
{
    public partial class SelectLevel : Form
    {
        private class LevelConfig
        {
            public HashSet<Target> Targets;
            public Game Game;
            public Player Player;
        }

        private readonly List<LevelConfig> _levels;

        public SelectLevel()
        {
            InitializeComponent();
            level1.Click += LvlBtn_click;
            _levels = new List<LevelConfig>();

            var GameForLvl1 = new Game(10, 10);
            _levels.Add(new LevelConfig
            {
                
                Game = GameForLvl1,
                Targets = new HashSet<Target> 
                {
                    new Target(new Point(1, 0), 1),
                    new Target(new Point(2, 0), 2),
                    new Target(new Point(3, 0), 1)
                },
                Player = new Player(new Point(3,3), GameForLvl1),
        });
        }

        public void SelectingLevel(int level)
        {
            if(level > _levels.Count) return;
            Hide();
            var game = _levels[level].Game;
            var player = _levels[level].Player;
            var targets = _levels[level].Targets;
            new Form1(game, player, targets).Show();
        }

        public void LvlBtn_click(object sender, EventArgs e)
        {
            var pressedBtn = (Button) sender;
            var levelIndex = int.Parse(pressedBtn.Name.Last().ToString()) - 1;
            SelectingLevel(levelIndex);
        }
    }
}
