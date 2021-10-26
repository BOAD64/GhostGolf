using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GhostGolfClient
{
    public class Level
    {
        private Ball ball;
        private Hole hole;
        public List<Ball> opponents { get; set; }
        private readonly int[] bounds = new int[4] { 0, 0, 100, 100 }; //ToDo change values
        // bounds: waardes -> x min, y min, x max, y max.

        public Level(string name)
        {
            this.ball = new Ball(name, 0, 0); //ToDo change values
            this.hole = new Hole(0, 0, 2); //ToDo change values
            opponents = new List<Ball>();
        }

        public async void makeMove(float xDir, float yDir)
        {
            double force = Math.Sqrt(xDir * xDir + yDir * yDir);

            xDir = (float)(xDir / force);
            yDir = (float)(yDir / force);

            float[] holePos = hole.getPos();
            float radius = hole.radius;

            await Task.Run(() =>
            {
                for (int i = 0; i < force; i++)
                {
                    float[] newPos = this.ball.getPos();

                    if (newPos[0] <= bounds[0] || newPos[0] >= bounds[2])
                    {
                        xDir = -xDir;
                    }
                    if (newPos[1] <= bounds[1] || newPos[1] >= bounds[3])
                    {
                        yDir = -yDir;
                    }

                    newPos[0] += xDir;
                    newPos[0] += yDir;
                    this.ball.setPos(newPos);

                    double distance = Math.Sqrt(Math.Pow(newPos[0] - holePos[0], 2) + Math.Pow(newPos[1] - holePos[1], 2));
                    if (distance <= radius)
                    {
                        //ToDo sent finish message.
                        break;
                    }
                }
            });
        }
    }
}
