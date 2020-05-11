﻿using System.Collections.Generic;
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
            Text = "Стреляющий по говну";
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            this.game = game;
            game.StartGame(player, targets);
            table = new TableLayoutPanel();
            mapUpdateTimer = new Timer();
            shootTimer = new Timer();
            images = new Dictionary<string, string>();
            GetImages();

            table.RowCount = game.Height;
            table.ColumnCount = game.Width;
            table.Size = new Size(table.RowCount * 50, table.ColumnCount * 50);
            Controls.Add(table);

            for (var i = 0; i < game.Height; i++)
                table.RowStyles.Add(new RowStyle(SizeType.Absolute, 50f));
            for (var i = 0; i < game.Width; i++)
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50f));
            DrawMap();

            mapUpdateTimer.Interval = 100;
            mapUpdateTimer.Tick += (sender, args) => Update();
            mapUpdateTimer.Start();


            shootTimer.Interval = 1000;
            shootTimer.Tick += (sender, args) => game.Player.Shoot();
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

        private void UpdateMap()
        {
          
            foreach (var bullet in game.Bullets)
            {
                var pic = new PictureBox {Image = Image.FromFile(images["bullet.png"]), Name = "bullet"};
                var prevIndex = bullet.Position.X * game.Height + bullet.Position.Y - 1;

                if (table.Controls[prevIndex].Name == "bullet")
                {
                    table.Controls.RemoveAt(prevIndex);
                    var skyPic = new PictureBox { Name = "sky", Image = Image.FromFile(images["sky.png"]) };
                    table.Controls.Add(skyPic, bullet.Position.X, bullet.Position.Y-1);
                }

                var currentIndex = prevIndex + 1;
                table.Controls.RemoveAt(currentIndex);
                table.Controls.Add(pic, bullet.Position.X, bullet.Position.Y);
                table.Controls.Add(pic);
            }
        }

        private void DrawMap()
        {
            table.Controls.Clear();
            var playerPic = new PictureBox {Image = Image.FromFile(images["player.png"]), Name = "player"};

            var usedPoints = new HashSet<Point>();

            table.Controls.Add(playerPic, game.Player.Position.X, game.Player.Position.Y);
            usedPoints.Add(game.Player.Position);

            foreach (var target in game.Targets)
            {
                var targetPic = new PictureBox {Name = "target", Image = Image.FromFile(images["target.png"])};
                table.Controls.Add(targetPic, target.Position.X, target.Position.Y);
                usedPoints.Add(target.Position);
            }

            foreach (var bullet in game.Bullets)
            {
                var bulletPic = new PictureBox { Image = Image.FromFile(images["bullet.png"]), Name = "bullet" };
                table.Controls.Add(bulletPic, bullet.Position.X, bullet.Position.Y);
                usedPoints.Add(bullet.Position);
            }

            for (var x = 0; x < game.Width; x++)
            {
                for (var y = 0; y < game.Height; y++)
                {
                    if (usedPoints.Contains(new Point(x, y))) continue;

                    //можно включить, но тогда все будет очень сильно мерцать

                    //var skyPic = new PictureBox { Name = "sky", Image = Image.FromFile(images["sky.png"]) };
                    //table.Controls.Add(skyPic, x, y);


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
    }
}