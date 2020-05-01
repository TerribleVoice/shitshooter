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
        private List<Target> defaultTargets = new List<Target>
        {
            new Target(new Point(1, 0), 1),
            new Target(new Point(2, 0), 1),
            new Target(new Point(3, 0), 1)
        };

        private Game initGame(List<Target> targets)
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

            foreach (var target in targets)
            {
                Assert.AreEqual(target, game.Map[target.Position.X, target.Position.Y]);
            }
        }

        [Test]
        public void PlayerMoveTest()
        {
            var game = initGame(new List<Target>());

            Assert.IsTrue(game.Map[2, 2] is Player);

            game.Player.MoveLeft();
            Assert.AreEqual(game.Player, game.Map[1, 2]);

            game.Player.MoveRight();
            game.Player.MoveRight();
            Assert.AreEqual(game.Player, game.Map[3, 2]);

        }

        [Test]
        public void PlayerShootTest()
        {
            var game = initGame(defaultTargets);
            game.Player.Shoot();

            Assert.AreEqual(1, game.Bullets.Count);
        }

        [Test]
        public void BulletMoveTest()
        {
            var game = initGame(defaultTargets);
            game.Player.Shoot();
            game.Update();

            var expectedBulletPos = new Point(game.Player.Position.X, game.Player.Position.Y - 1);

            Assert.AreEqual(1, game.Bullets.Count);
            Assert.IsTrue(game.Map[expectedBulletPos.X, expectedBulletPos.Y] is Bullet);
        }

        [Test]
        public void HitTargetTest()
        {
            var game = initGame(defaultTargets);
            game.Player.Shoot();
            game.Update();
            game.Update();

            var expectedBulletPos = new Point(game.Player.Position.X, game.Player.Position.Y - 2);

            Assert.AreEqual(0, game.Bullets.Count);
            Assert.AreEqual(2, game.Targets.Count);
            Assert.IsNull(game.Map[expectedBulletPos.X, expectedBulletPos.Y]);
        }
    }
}