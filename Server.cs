using AdvancedSharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ancon
{
    internal static class Server
    {
        public static bool IsRunning { private set; get; }

        public static void Start()
        {
            if (!AdbServer.Instance.GetStatus().IsRunning)
            {
                AdbServer server = new AdbServer();
                StartServerResult result = server.StartServer("", false);
                IsRunning = (result == StartServerResult.Started);
            }
        }


    }
}
