using System;
using EasyTabs;

namespace RndProgram
{
    public partial class XBXBrowser : TitleBarTabs
    {
        public XBXBrowser()
        {
            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);
            InitializeComponent();
        }

        private void Appcontainercs_Load(object sender, EventArgs e)
        {

        }

        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this) { Content = new Form1 { Text = "New Tab" } };
        }
    }
}
