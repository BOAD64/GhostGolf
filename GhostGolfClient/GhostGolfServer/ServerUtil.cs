using System;

namespace GhostGolfServer
{
    public class ServerUtil
    {
        public Tuple<double, int> calculatePar(double oldPar, int weight, int modifier)
        {
            double newPar = (oldPar * weight + modifier) / (weight + 1);
            return Tuple.Create(newPar, weight + 1);
        }
    }
}
