using System;
namespace GhostGolfClient
{
    public class Ball : GameObject
    {
        public string owner { get; private set; }
        private float[] pos = new float[2];
        private readonly float radius;

        public Ball(string name, float x, float y)
        {
            owner = name;
            pos[0] = x;
            pos[1] = y;
            this.radius = 1.5f;
        }

        public float[] getPos()
        {
            return this.pos;
        }

        public void setPos(float[] pos)
        {
            this.pos = pos;
        }
    }
}
