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
        private readonly Dictionary<string, string> images;

        public Form1(Game game, Player player, HashSet<Target> targets)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer , true);
            Text = "Space Battle";
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.game = game;
            game.StartGame(player, targets);
            images = new Dictionary<string, string>();
            GetImages();

            table = CreateTable(game.Width, game.Height);
            Controls.Add(table);

            mapUpdateTimer = CreateTimer(100, Update);
            mapUpdateTimer.Start();

            shootTimer = CreateTimer(1000, game.Player.Shoot);
            shootTimer.Start();

            KeyDown += Form1_KeyDown;

            game.EndGame += () =>
            {
                mapUpdateTimer.Stop();
                shootTimer.Stop();
                var result = MessageBox.Show("Игра окончена", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    Close();
            };

            ClientSize = table.Size;

            table.BackgroundImage = Resources.sky;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                game.Player.MoveLeft();
            if (e.KeyCode == Keys.Right)
                game.Player.MoveRight();
        }

        private void Update()
        {
            game.Update();
            DrawMap();
        }

        private void DrawMap()
        {
            table.Controls.Clear();
            var playerPic = new PictureBox { Image = Image.FromFile(images["player.png"]), Name = "player" };
            playerPic.BackColor = Color.Transparent;
            var usedPoints = new HashSet<Point>();

            table.Controls.Add(playerPic, game.Player.Position.X, game.Player.Position.Y);
            usedPoints.Add(game.Player.Position);

            foreach (var target in game.Targets)
            {
                var targetPic = new PictureBox { Name = "target", Image = Image.FromFile(images["target.png"]) };
                targetPic.BackColor = Color.Transparent;
                table.Controls.Add(targetPic, target.Position.X, target.Position.Y);
                usedPoints.Add(target.Position);
            }

            foreach (var bullet in game.Bullets)
            {
                var bulletPic = new PictureBox { Image = Image.FromFile(images["bullet.png"]), Name = "bullet" };
                bulletPic.BackColor = Color.Transparent;
                table.Controls.Add(bulletPic, bullet.Position.X, bullet.Position.Y);
                usedPoints.Add(bullet.Position);
            }

            for (var x = 0; x < game.Width; x++)
            {
                for (var y = 0; y < game.Height; y++)
                {
                    if (usedPoints.Contains(new Point(x, y))) continue;

                }
            }
        }

        private void GetImages()
        {
            var directory = new DirectoryInfo("./imgs");

            foreach (var file in directory.GetFiles())
            {
                images.Add(file.Name, file.FullName);
            }
        }

        private TableLayoutPanel CreateTable(int width, int height)
        {
            var table = new TableLayoutPanel
            {
                RowCount = game.Height,
                ColumnCount = game.Width
            };
            table.Size = new Size(table.RowCount * 50, table.ColumnCount * 50);
            for (var i = 0; i < game.Height; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            for (var i = 0; i < game.Width; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));

            return table;
        }

        private Timer CreateTimer(int interval, params Action[] actions)
        {
            var timer = new Timer { Interval = interval };
            foreach (var action in actions)
                timer.Tick += (sender, args) => action();

            return timer;
        }

    }
}