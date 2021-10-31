using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServerClientConnection;

namespace GhostGolfClient
{
    public class Level
    {
        private ServerConnection connection;

        private int strokes = 0;

        public Ball ball { get; }
        public Hole hole { get; }
        public List<Ball> opponents { get; set; }
        private readonly int[] bounds = new int[4] { 10, 10, 610, 610 }; //ToDo change values
        // bounds: waardes -> x min, y min, x max, y max.

        public Level(string name, ServerConnection connection)
        {
            this.connection = connection;
            this.ball = new Ball(name, 305, 500); //ToDo change values
            this.hole = new Hole(305, 100, 60); //ToDo change values
            opponents = new List<Ball>();
        }

        public async Task makeMove(float xDir, float yDir)
        {
            this.strokes++;
            double force = Math.Sqrt(xDir * xDir + yDir * yDir);

            xDir = (float)(xDir / force);
            yDir = (float)(yDir / force);

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

                    if ((force - i <= 2) && CourseFinished(newPos))
                    {
                        Connection message = new Connection() { name = this.connection.name,
                            data = new Finish() { strokes = this.strokes} };
                        this.connection.writeMessage(message);
                        break;
                    }

                    Connection ballPos = new Connection()
                    {
                        name = this.connection.name,
                        data = new BallPos() { position = this.ball.getPos(), sender = this.connection.name }
                    };
                    this.connection.writeMessage(ballPos);
                }
            });
        }

        public int[] getBounds ()
        {
            return bounds;
        }

        public bool CourseFinished(float[] ballPos)
        {
            float radius = hole.radius;
            float[] holePos = hole.getPos();
            double distance = Math.Sqrt(Math.Pow(ballPos[0] - holePos[0], 2) + Math.Pow(ballPos[1] - holePos[1], 2));

            return distance <= radius;
        }
    }
}
