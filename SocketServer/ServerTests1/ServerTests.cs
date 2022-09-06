using NUnit.Framework;
using SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Tests
{
    [TestFixture()]
    public class ServerTests
    {
        [Test]
        public void StartServerTest()
        {
            try
            {
                Server server = new Server();
                Assert.DoesNotThrow(()=> server.Start());
            }
            catch (Exception ex)
            {
                Assert.Fail("Test fail with exception : " + ex.Message);
            }
        }
    }
}