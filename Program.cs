using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
namespace RndProgram
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          XBXBrowser appcontainercs = new XBXBrowser();
            appcontainercs.Tabs.Add(new TitleBarTab(appcontainercs) { Content = new Form1 { Text = "New Tab" } });
            appcontainercs.SelectedTabIndex = 0;
            TitleBarTabsApplicationContext context = new TitleBarTabsApplicationContext();
            context.Start(appcontainercs);
            Application.Run(context);

        }
    }
}
