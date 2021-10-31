using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GhostGolfServerTest
{
    [TestClass]
    public class ServerUnitTest
    {
        [TestMethod]
        public void TestCalculatePar()
        {
            GhostGolfServer.ServerUtil util = new GhostGolfServer.ServerUtil();
            double par = 26;
            int weight = 10;
            int newScore = 15;

            Tuple<double, int> tuple = util.calculatePar(par, weight, newScore);

            Assert.AreEqual(25, tuple.Item1); //assert par
            Assert.AreEqual(11, tuple.Item2); //assert weight
        }

        [TestMethod]
        public void TestHighscore()
        {
            GhostGolfServer.FileIO fileIO = new GhostGolfServer.FileIO();

            fileIO.AddHighscore("Mark", 21);
            int highscore = fileIO.GetHighScore("Mark");

            Assert.AreEqual(21, highscore);
        }

        [TestMethod]
        public void TestGetPar()
        {
            GhostGolfServer.FileIO fileIO = new GhostGolfServer.FileIO();

            int par = fileIO.GetPar();

            Assert.AreEqual(0, par);
        }
    }
}
