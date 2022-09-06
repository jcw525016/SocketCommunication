using NUnit.Framework;
using SocketClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketClient.Tests
{
    [TestFixture()]
    public class ClientTests
    {
        [Test]
        public void RequestDownloadFileTest()
        {
            try
            {
                Client client = new Client();
                Assert.DoesNotThrow(() => client.RequestDownloadFile());
            }
            catch (Exception ex)
            {
                Assert.Fail("Test fail with exception : " + ex.Message);
            }
        }
    }
}