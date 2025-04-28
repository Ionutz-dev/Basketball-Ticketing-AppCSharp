using System;
using System.Windows.Forms;
using networking;
using services;

namespace client
{
    internal static class StartBasketballClient
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IBasketballServices server = new BasketballServerProxy("127.0.0.1", 55556);
            var ctrl = new BasketballClientCtrl(server);
            
            while (true)
            {
                using (var loginWindow = new LoginWindow(ctrl))
                {
                    if (loginWindow.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }
                }

                using (var mainWindow = new MainWindow(ctrl))
                {
                    Application.Run(mainWindow);
                }
            }
        }
    }
}