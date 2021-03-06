using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyTabs;
using System.IO;
using MetroFramework;
using CefSharp;

namespace RndProgram
{
    public partial class Form1 : Form
    {
        //very important for easytabs to work :)
        protected TitleBarTabs ParentTab { get { return (ParentForm as TitleBarTabs); } }
        public Form1()
        {
            InitializeComponent();
        }

        private void chromiumWebBrowser1_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DownloadHandler down = new DownloadHandler();
            web.DownloadHandler = down;
            if (File.Exists("x.txt"))
            {
                // Write the file
                //The webpage to open can be specified in the x.txt file
                using (StreamReader x = File.OpenText("x.txt"))
                {
                    string streams = ""; while ((streams = x.ReadLine()) != null) { metroTextBox1.Text = streams; web.Load(metroTextBox1.Text); }

                }
            }
            else
            {
                /* If there is no file load google.com */
                web.Load("google.com");
                //Write the file so it navigates to google.com 
                File.WriteAllText("x.txt", "https://www.google.com");
            }


            //theme
            if (File.Exists("t.txt"))
            {
                using (StreamReader fx = File.OpenText("t.txt"))
                {
                    string streams2 = "";
                    while ((streams2 = fx.ReadLine()) != null)
                    {
                        //I orginally planned to use if but switch is better for this job.
                        //if (streams2 == "default")
                        //{
                        //    metroButton1.Theme = MetroThemeStyle.Default;
                        //    metroButton2.Theme = MetroThemeStyle.Default;
                        //    metroButton3.Theme = MetroThemeStyle.Default;
                        //    metroTextBox1.Theme = MetroThemeStyle.Default;
                        //    metroPanel1.Theme = MetroThemeStyle.Default;

                        //}
                        //else if(streams2 == "light")
                        //{
                        //    metroButton1.Theme = MetroThemeStyle.Light;
                        //    metroButton2.Theme = MetroThemeStyle.Light;
                        //    metroButton3.Theme = MetroThemeStyle.Light;
                        //    metroTextBox1.Theme = MetroThemeStyle.Light;
                        //    metroPanel1.Theme = MetroThemeStyle.Light;
                        //}
                        //else if(streams2 == "Dark")
                        //{


                        //}
                        switch (streams2)
                        {
                            case "default":
                                
                                metroButton3.Theme = MetroThemeStyle.Default;
                                metroTextBox1.Theme = MetroThemeStyle.Default;
                                metroPanel1.Theme = MetroThemeStyle.Default;
                                break;
                            case "light":
                                
                                metroButton3.Theme = MetroThemeStyle.Light;
                                metroTextBox1.Theme = MetroThemeStyle.Light;
                                metroPanel1.Theme = MetroThemeStyle.Light;
                                break;
                            case "dark":
                              
                                metroButton3.Theme = MetroThemeStyle.Dark;
                                metroTextBox1.Theme = MetroThemeStyle.Dark;
                                metroPanel1.Theme = MetroThemeStyle.Dark;
                                break;
                            default:
                                
                                metroButton3.Theme = MetroThemeStyle.Default;
                                metroTextBox1.Theme = MetroThemeStyle.Default;
                                metroPanel1.Theme = MetroThemeStyle.Default;
                                break;

                        }

                    }
                }
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            web.Refresh();
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13)) // enter
            {
                web.Load(metroTextBox1.Text);

            }
        }

        private void web_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool irp = e.KeyChar == (char)Keys.R;
            bool icp = (Control.ModifierKeys & Keys.Control) == Keys.Control;
            if (irp & icp)
            {
                web.Refresh();

            }
        }

        private void web_TitleChanged(object sender, CefSharp.TitleChangedEventArgs e)
        {
            
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }
    }
}

public class DownloadHandler : IDownloadHandler
{
    public event EventHandler<DownloadItem> OnBeforeDownloadFired;

    public event EventHandler<DownloadItem> OnDownloadUpdatedFired;

    public void OnBeforeDownload(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
        IBeforeDownloadCallback callback)
    {
        OnBeforeDownloadFired?.Invoke(this, downloadItem);

        if (!callback.IsDisposed)
        {
            using (callback)
            {
                callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
            }
        }
    }

    public void OnDownloadUpdated(IWebBrowser chromiumWebBrowser, IBrowser browser, DownloadItem downloadItem,
        IDownloadItemCallback callback)
    {
        OnDownloadUpdatedFired?.Invoke(this, downloadItem);
    }
}