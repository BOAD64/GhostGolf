using System;
using System.Threading.Tasks;

namespace GhostGolfClient
{
    public class Level
    {
        private Ball ball;
        private Hole hole;

        public Level()
        {
            this.ball = new Ball(0, 0); //ToDo change values
            this.hole = new Hole(0, 0, 2); //ToDo change values
        }

        public async void makeMove(float xDir, float yDir)
        {
            double force = Math.Sqrt(xDir * xDir + yDir * yDir);

            xDir = (float)(xDir / force);
            yDir = (float)(yDir / force);

            float[] holePos = hole.getPos();
            float radius = hole.radius;

            await Task.Run(() => { 
            for (int i = 0; i < force; i++)
            {
                float[] newPos = this.ball.getPos();
                newPos[0] += xDir;
                newPos[0] += yDir;
                this.ball.setPos(newPos);

                //ToDo check for walls to change xDir en yDir.

                double distance = Math.Sqrt(Math.Pow(newPos[0] - holePos[0], 2) + Math.Pow(newPos[1] - holePos[1], 2));
                if (distance <= radius)
                {
                    //ToDo sent finish message.
                    break;
                }
            }});
        }


    }
}
