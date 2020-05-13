using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Permissions;
using System.Text;
using NUnit.Framework;

namespace ShitShooter
{
    [TestFixture]
    class Tests
    {
        public HashSet<Target> defaultTargets = new HashSet<Target>
        {
            new Target(new Point(1, 0), 1),
            new Target(new Point(2, 0), 1),
            new Target(new Point(3, 0), 1)
        };

        private Game InitGame(HashSet<Target> targets)
        {
            var game = new Game(5, 5);
            var player = new Player(new Point(2, 2), game);
            game.StartGame(player, targets);
            return game;
        }


        [Test]
        public void GameWithTargetsInit()
        {
            var game = new Game(5, 5);
            var player = new Player(new Point(2, 2), game);
            var targets = defaultTargets;
            game.StartGame(player, targets);
            Assert.IsTrue(player == game.Player && targets == game.Targets);
        }

        [Test]
        public void PlayerMoveTest()
        {
            var game = InitGame(new HashSet<Target>());

            game.Player.MoveLeft();
            Assert.AreEqual(new Point(1, 2), game.Player.Position);
            game.Player.MoveRight();
            game.Player.MoveRight();
            Assert.AreEqual(new Point(3,2), game.Player.Position);
        }

        [Test]
        public void PlayerShootTest()
        {
            var game = InitGame(defaultTargets);
            game.Player.Shoot();

            Assert.AreEqual(1, game.Bullets.Count);
        }

        [Test]
        public void BulletMoveTest()
        {
            var game = InitGame(defaultTargets);
            game.Player.Shoot();
            game.Update();

            var expectedBulletPos = new Point(game.Player.Position.X, game.Player.Position.Y - 1);

            Assert.AreEqual(1, game.Bullets.Count);
        }

        [Test]
        public void HitTargetTest()
        {
            var game = InitGame(defaultTargets);
            game.Player.Shoot();
            game.Update();
            game.Update();

            var expectedBulletPos = new Point(game.Player.Position.X, game.Player.Position.Y - 2);

            Assert.AreEqual(0, game.Bullets.Count);
            Assert.AreEqual(2, game.Targets.Count);
        }
    }
}