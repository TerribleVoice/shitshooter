using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace ShitShooter
{
    public class Form1 : Form
    {
        private readonly Game game;
        private readonly Timer mapUpdateTimer;
        private readonly Timer shootTimer;
        private readonly TableLayoutPanel table;

        private Bitmap skyTexture = Resources.sky;
        private Bitmap bulletTexture = Resources.bullet;
        private Bitmap targetTexture = Resources.target;
        private Bitmap playerTexture = Resources.player;


        public Form1(Game game, Player player, HashSet<Target> targets)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            Text = Resources.Game_Name;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.game = game;
            game.StartGame(player, targets);


            mapUpdateTimer = CreateTimer(50, DoIteration);
            mapUpdateTimer.Start();

            shootTimer = CreateTimer(1000, game.Player.Shoot);
            shootTimer.Start();

            KeyDown += Form1_KeyDown;
            Paint += PaintMap;

            game.EndGame += () =>
            {
                mapUpdateTimer.Stop();
                shootTimer.Stop();
                var result = MessageBox.Show("Игра окончена", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    Close();
            };

            ClientSize = new Size(game.Width * 50, game.Height * 50);
            BackgroundImage = skyTexture;
        }

        private void PaintMap(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(playerTexture, game.Player.Position.Multiply(50));
            foreach (var target in game.Targets)
                g.DrawImage(targetTexture, target.Position.Multiply(50));
            foreach (var bullet in game.Bullets)
                g.DrawImage(bulletTexture, bullet.Position.Multiply(50));
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    game.Player.MoveLeft();
                    break;
                case Keys.Right:
                    game.Player.MoveRight();
                    break;
                case Keys.Up:
                    game.Player.MoveUp();
                    break;
                case Keys.Down:
                    game.Player.MoveDown();
                    break;
            }
        }

        private void DoIteration()
        {
            game.Update();
            Refresh();
        }

        private Timer CreateTimer(int interval, params Action[] actions)
        {
            var timer = new Timer { Interval = interval };
            foreach (var action in actions)
                timer.Tick += (sender, args) => action();

            return timer;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.BackgroundImage = global::ShitShooter.Resources.sky;
            this.ClientSize = new System.Drawing.Size(483, 553);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }
    }
}