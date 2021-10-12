using System;
namespace GhostGolfClient
{
    public class Ball : GameObject
    {
        private float[] pos = new float[2];
        private readonly float radius;

        public Ball(float x, float y)
        {
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
