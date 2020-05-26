using System.Drawing;
using System.Windows.Forms.VisualStyles;

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

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Target))
                return false;
            var secondTarget = (Target)obj;
            return Position == secondTarget.Position;

        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}