using System;
using System.Collections.Generic;

namespace ShitShooter
{
    class Game
    {
        public Player Player { get; }
        public List<Target> Targets { get; }

        public Game()
        {
            Player = new Player();
            Targets = new List<Target>();
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            throw new NotImplementedException();
        }
    }
}