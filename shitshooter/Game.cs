using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ShitShooter
{
    public class Game
    {
        public Player Player { get; private set; }
        public HashSet<Target> Targets { get; private set; }
        public HashSet<Bullet> Bullets { get; private set; }
        private HashSet<Bullet> bulletsToRemove;


        public int Width { get; }
        public int Height { get; }

        public Game(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void StartGame(Player player, HashSet<Target> targets)
        {
            if (!PointBelongsMap(player.Position))
                throw new ArgumentException("Player is outside the map");

            foreach (var target in targets)
            {
                if (!PointBelongsMap(target.Position))
                    throw new ArgumentException($"target ({target.Position.X},{target.Position.Y}) is outside the map");
            }

            Player = player;
            Targets = targets;
            Bullets = new HashSet<Bullet>();
            bulletsToRemove = new HashSet<Bullet>();

        }

        public bool ShouldGameEnd()
        {
            return Targets.Count == 0;
        }

        public event Action EndGame;

        public void Update()
        {
            if (ShouldGameEnd())
                EndGame?.Invoke();
            UpdateBullets();
            UpdateTargets();
            bulletsToRemove.Clear();
        }

        private void UpdateTargets()
        {
            var targetsToRemove = new HashSet<Target>();

            foreach (var target in Targets.Where(target => target.Hp <= 0))
                targetsToRemove.Add(target);

            Targets.ExceptWith(targetsToRemove);
        }
        private void UpdateBullets()
        {
            foreach (var bullet in Bullets)
            {
                bullet.Move();
                if (!PointBelongsMap(bullet.Position))
                    bulletsToRemove.Add(bullet);
            }

            Bullets.ExceptWith(bulletsToRemove);
        }

        public void HitTarget(Target target, Bullet bullet)
        {
            bulletsToRemove.Add(bullet);
            target.Hp -= bullet.Damage;
        }

        private bool PointBelongsMap(Point point)
        {

            return point.X >= 0 && point.Y >= 0 && point.X < Width && point.Y < Height;
        }

        public HashSet<Target> GenerateRandomTargetsSet(int count, int seed = 0)
        {
            if (seed == 0)
                seed = new Random().Next();

            var rnd = new Random(seed);
            var set = new HashSet<Target>();
            while (set.Count < count)
            {
                var target = new Target(new Point(rnd.Next(Width - 1), rnd.Next(Height - 2)), 1);
                set.Add(target);
            }

            return set;
        }
    }

    public static class PointExtensions
    {
        public static Point Multiply(this Point point, int value)
        {
            return new Point(point.X * value, point.Y * value);
        }
    }

}