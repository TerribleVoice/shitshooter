using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ShitShooter
{
    public class Form1 : Form
    {
        private readonly Game game;
        private readonly Timer mapUpdateTimer;
        private readonly Timer shootTimer;
        private readonly TableLayoutPanel table;
        private readonly Dictionary<string, string> images;

        public Form1(Game game, Player player, List<Target> targets)
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

            mapUpdateTimer.Interval = 100;
            mapUpdateTimer.Tick += (sender, args) => game.Update();
            mapUpdateTimer.Tick += (sender, args) => DrawMap();
            mapUpdateTimer.Start();


            shootTimer.Interval = 1000;
            shootTimer.Tick += (sender, args) => game.Player.Shoot();
            shootTimer.Start();

            KeyDown += Form1_KeyDown;

            game.EndGame += () =>
            {
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

        private void DrawMap()
        {
            table.Controls.Clear();
            var controls = new TableLayoutControlCollection(table);
            for (var x = 0; x < game.Width; x++)
            {
                for (var y = 0; y < game.Height; y++)
                {
                    var pic = new PictureBox();
                    switch (game[x,y])
                    {
                        case Player _:
                            pic.Image = Image.FromFile(images["player.png"]);
                            break;
                        case Target _:
                            pic.Image = Image.FromFile(images["target.jpg"]);
                            break;
                        case Bullet _:
                            pic.Image = Image.FromFile(images["bullet.png"]);
                            break;
                    }

                    pic.Dock = DockStyle.Fill;
                    table.Controls.Add(pic, x, y);
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