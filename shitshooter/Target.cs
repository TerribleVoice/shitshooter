using System.Drawing;

namespace ShitShooter
{
    class Target:ICreature
    {
        public int Hp { get; set; }
        public Point Position { get; }
        public string GetImageFileName()
        {
            throw new System.NotImplementedException();
        }
    }
}