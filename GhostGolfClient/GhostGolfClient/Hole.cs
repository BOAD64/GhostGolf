using System;
namespace GhostGolfClient
{
    public class Hole : GameObject
    {
        private readonly float[] pos = new float[2];
        public float radius { get; }

        public Hole(float x, float y, float radius)
        {
            pos[0] = x;
            pos[1] = y;
            this.radius = radius;
        }

        public float[] getPos()
        {
            return this.pos;
        }
    }
}
