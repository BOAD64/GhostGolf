using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GhostGolfClientTest
{
    [TestClass]
    public class ClientUnitTest
    {
        [TestMethod]
        public void TestCourseFinished()
        {
            GhostGolfClient.Level level = new GhostGolfClient.Level("",
                new GhostGolfClient.ServerConnection(new System.Net.Sockets.TcpClient(), ""));
            bool result = level.CourseFinished(new float[] { 1, 1 });
            Assert.IsTrue(result);

            result = level.CourseFinished(new float[] { 1, 2 });
            Assert.IsFalse(result);
        }
    }
}
