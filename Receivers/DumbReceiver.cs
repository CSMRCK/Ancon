using AdvancedSharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ancon.Receivers
{
    public class DumbReceiver : IShellOutputReceiver
    {
        public void AddOutput(string line)
        {
            Console.WriteLine("Receiving...");
        }

        public void Flush()
        {
            Console.WriteLine("Done");
        }

        public bool ParsesErrors { get; }
    }
}
