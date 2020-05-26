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
            //InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer , true);
            Text = "Space Battle";
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.game = game;
            game.StartGame(player, targets);


            mapUpdateTimer = CreateTimer(100, DoIteration);
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

            ClientSize = new Size(game.Width * 50, game.Height *50);
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
            }
        }

        private void DoIteration()
        {
            game.Update();
            Refresh();
            //DrawMap();
        }

        private void DrawMap()
        {
            table.Controls.Clear();
            var playerPic = new PictureBox { Image = playerTexture, Name = "player" };
            playerPic.BackColor = Color.Transparent;
            var usedPoints = new HashSet<Point>();

            table.Controls.Add(playerPic, game.Player.Position.X, game.Player.Position.Y);
            usedPoints.Add(game.Player.Position);

            foreach (var target in game.Targets)
            {
                var targetPic = new PictureBox { Name = "target", Image = targetTexture };
                targetPic.BackColor = Color.Transparent;
                table.Controls.Add(targetPic, target.Position.X, target.Position.Y);
                usedPoints.Add(target.Position);
            }

            foreach (var bullet in game.Bullets)
            {
                var bulletPic = new PictureBox { Image = bulletTexture, Name = "bullet" };
                bulletPic.BackColor = Color.Transparent;
                table.Controls.Add(bulletPic, bullet.Position.X, bullet.Position.Y);
                usedPoints.Add(bullet.Position);
            }
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