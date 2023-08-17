using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AdvancedSharpAdbClient;
using AdvancedSharpAdbClient.DeviceCommands;
using Ancon.Helpers;
using Ancon.Receivers;

namespace Ancon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AdvancedAdbClient client;
        private DeviceData device;
        private DumbReceiver dumbReceiver;
        private static string userIp;
        private string inputText;

        public MainWindow()
        {
            InitializeComponent();
            Server.Start();


            client = new AdvancedAdbClient(); 
            device = client.GetDevices().FirstOrDefault();
            dumbReceiver = new DumbReceiver();
        }

        private void KillRecentsButton_Click(object sender, RoutedEventArgs e)
        {
            RecentIdsReceiver recentIdsReceiver = new RecentIdsReceiver();

            client.ExecuteShellCommand(device, "dumpsys activity recents", recentIdsReceiver);

            foreach (var id in recentIdsReceiver.IdOfActivities)
            {
                client.ExecuteShellCommand(device, $"am stack remove {id}", dumbReceiver);
            }

        }

        private void IpLabel_Loaded(object sender, TextChangedEventArgs e)
        {
            var ip = sender as Label;
            ip.Content = userIp;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Refactoring
            if (inputText == "my%20ip%20address".ToLower())
            {
                IpLable.Content = "Loading...";
                client.ExecuteShellCommand(device, $"am start -a android.intent.action.VIEW -d https://www.google.com/search?q={inputText}", dumbReceiver);
                client.ExecuteShellCommand(device, "uiautomator dump", dumbReceiver);
                string pathToXml = Environment.CurrentDirectory + "/window_dump.xml";
                var xmlFile = File.Create(pathToXml);
                client.Pull(device, "/sdcard/window_dump.xml", xmlFile);
                xmlFile.Close();
                userIp =XmlHelper.FindIdInXml(pathToXml);

                IpLable.Content = "Your IP: " + userIp;

            }
            
            client.ExecuteShellCommand(device, $"am start -a android.intent.action.VIEW -d https://www.google.com/search?q={inputText}", dumbReceiver);
        }

        private void SearchInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            inputText = Uri.EscapeDataString(((TextBox)sender).Text);
        }
    }
}
