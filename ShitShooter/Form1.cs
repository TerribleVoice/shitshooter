using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ShitShooter
{
    public partial class Form1 : Form
    {
        private readonly Game game;
        private readonly TableLayoutPanel table;
        private readonly Timer mapUpdateTimer;
        private readonly Timer shootTimer;

        public Form1(Game game, Player player, List<Target> targets)
        {
            this.game = game;
            game.StartGame(player, targets);
            table = new TableLayoutPanel();
            mapUpdateTimer = new Timer();
            shootTimer = new Timer();

            table.RowCount = game.Height;
            table.ColumnCount = game.Width;
            table.Size = new Size(table.RowCount * 50, table.ColumnCount * 50);
            Controls.Add(table);

            for (var i = 0; i <game.Height; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            for (var i = 0; i < game.Width; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));

            mapUpdateTimer.Interval = 100;
            mapUpdateTimer.Tick += (sender, args) => game.Update();
            mapUpdateTimer.Tick += (sender, args) => DrawMap();
            mapUpdateTimer.Start();
            

            shootTimer.Interval = 1000;
            shootTimer.Tick += (sender, args) => game.Player.Shoot();
            shootTimer.Start();

            KeyDown += Form1_KeyDown;

            ClientSize = table.Size;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left)
                game.Player.MoveLeft();
            if (e.KeyCode == Keys.Right)
                game.Player.MoveRight();
        }

        private void DrawMap()
        {
            table.Controls.Clear();
            var controls = new TableLayoutControlCollection(table);
            foreach (var creature in game.Map)
            {
                var pic = new PictureBox();
                switch (creature)
                {
                    case Player _:
                        pic.BackColor = Color.Red;
                        break;
                    case Target _:
                        pic.BackColor = Color.SaddleBrown;
                        break;
                    case Bullet _:
                        pic.BackColor = Color.Orange;
                        break;
                    default:
                        pic.BackColor = Color.Blue;
                        break;
                }

                if (creature != null)
                    table.Controls.Add(pic, creature.Position.X, creature.Position.Y);
            }

        }
    }
}
