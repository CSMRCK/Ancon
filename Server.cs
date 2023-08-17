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

        //public static Server(string pathToAdb = "D:\\Soft\\ABD\\adb.exe")
        //{
        //    Start(pathToAdb);
        //}

        public static void Start(string pathToAdb = "D:\\Soft\\ABD\\adb.exe")
        {
            if (!AdbServer.Instance.GetStatus().IsRunning)
            {
                AdbServer server = new AdbServer();
                StartServerResult result = server.StartServer(pathToAdb, false);
                IsRunning = (result == StartServerResult.Started);
            }
        }


    }
}
