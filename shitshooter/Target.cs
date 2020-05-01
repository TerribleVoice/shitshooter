using System.Drawing;

namespace ShitShooter
{
    public class Target : ICreature
    {
       

        public int Hp { get; set; }
        public Point Position { get; }

        public Target(Point position, int hp)
        {
            Position = position;
            Hp = hp;
        }

        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }
    }
}