using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screenscrapscrape
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void CheckForImage(string key)
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("User-Agent: Other");
                    string sc = wc.DownloadString($"https://prnt.sc/{key}");
                    if (sc.Contains($"https://image.prntscr.com/image/"))
                    {
                        string startOfUrl = sc.Substring(sc.IndexOf($"https://image.prntscr.com/image/"));
                        string outString = startOfUrl.Split('"')[0];
                        Task.Factory.StartNew(() => DownloadImg(outString, key));
                    }
                    else if (sc.Contains($"https://i.imgur.com/"))
                    {
                        string startOfUrl = sc.Substring(sc.IndexOf($"https://i.imgur.com/"));
                        string outString = startOfUrl.Split('"')[0];
                        Task.Factory.StartNew(() => DownloadImg(outString, key));
                    }


                }
                catch(Exception){}   
            }
        }

        void DownloadImg(string url, string key)
        {
            using (WebClient client = new WebClient())
            {
                client.DownloadFile(url, $@"H:\images\{key}.png");
                var size = new FileInfo($@"H:\images\{key}.png").Length;
                if($"{size} - {key} @ {DateTime.Now}".Contains("000 @"))
                    Console.WriteLine($"{size} - {key} @ {DateTime.Now}");
                if (size == 503)
                    File.Delete($@"H:\images\{key}.png");
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            List<string> entrys = new List<string>();
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            foreach (char c in alpha)
                for (int i = 0; i < 26; i++)
                    for (int b = 0; b < 10000; b++)
                        entrys.Add($"{c.ToString()}{alpha[i].ToString()}{b.ToString().PadLeft(4, '0')}".ToLower());

            entrys.Reverse();

            foreach (string s in entrys)
                if (!File.Exists($@"H:\images\{s}.png"))
                    CheckForImage(s);
        }
    }
}
