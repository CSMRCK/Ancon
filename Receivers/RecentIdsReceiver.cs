using AdvancedSharpAdbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ancon.Receivers
{
    public class RecentIdsReceiver : IShellOutputReceiver
    {
        public List<int> IdOfActivities { private set; get; } = new List<int>();
        public void AddOutput(string line)
        {
            ExtractTaskIds(line);
        }

        public void Flush()
        {
            Console.WriteLine("Command data has been received!");
        }

        public bool ParsesErrors { get; }

        private void ExtractTaskIds(string input)
        {
            Regex regex = new Regex(@"taskId=(\d+)");

            MatchCollection matches = regex.Matches(input);
            foreach (Match match in matches)
            {
                if (match.Groups.Count > 1 && int.TryParse(match.Groups[1].Value, out int taskId))
                {
                    IdOfActivities.Add(taskId);
                }
            }
        }
    }
}
