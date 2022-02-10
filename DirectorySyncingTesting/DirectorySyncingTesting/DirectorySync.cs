using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using jExtensions;

namespace DirectorySyncingTesting
{
    public partial class DirectorySync : Form
    {
        public DirectorySync()
        {
            InitializeComponent();
            
        }
        async Task Sync()
        {
            string[] directorys = { };
            BtnSync.Enabled = false;
            RTBSyncDirectorys.Enabled = false;
            directorys = RTBSyncDirectorys.Text.Split('\n');
            Console.WriteLine($"Syncing Directorys:\n {string.Join("\n",directorys)}");
            await Task.Factory.StartNew(() => SyncDirectories(directorys));
            await Task.Factory.StartNew(() => SyncFiles(directorys));
            BtnSync.Enabled = true;
            RTBSyncDirectorys.Enabled = true;
            Console.WriteLine("Syning | DONE");
        }
        async Task SyncFiles(string[] directories)
        {
            // Check if core directorys exist
            foreach (string dir in directories)
                if (!Directory.Exists(dir))
                {
                    MessageBox.Show($"Unable to find core directory {dir}", "Error");
                    return;
                }

            //Get all files from each core directroy 
            foreach (string dir in directories)
                foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    // Get index of start of core directroy
                    int index = file.IndexOfInPath(dir.GetParentDirs()[0]);
                    string[] parents = file.Split('\\');
                    if (index == -1 || parents.Count() == 0)
                        continue;

                    // Split the path string from index of core directroy
                    // and join path back after
                    string after = string.Join(@"\", parents.Skip(index + 1));

                    foreach (string dirPath in directories)
                        if (dirPath == dir)
                            continue;
                        else
                        {
                            // If file doesnt exist copy it
                            if(!File.Exists($"{dirPath}{after}"))
                                try
                                {
                                    File.Copy(file, $"{dirPath}{after}");
                                }catch(Exception e)
                                {
                                    Console.WriteLine($"after: {after} \n\n\n {e} \n\n\n");
                                }
                            else
                            {
                                try
                                {
                                    // Compare to find newest version of file
                                    DateTime copyingFromTime = new FileInfo(file).LastWriteTime;
                                    DateTime copyingToTime = new FileInfo($"{dirPath}{after}").LastWriteTime;
                                    if (copyingFromTime > copyingToTime)
                                    {
                                        File.Delete($"{dirPath}{after}");
                                        File.Copy(file, $"{dirPath}{after}");
                                    }
                                } catch(Exception e)
                                {
                                    Console.WriteLine($"after: {after} \n\n\n {e} \n\n\n");
                                }
                                
                            }
                        }
                            
                }
                    
        }
        
        async Task SyncDirectories(string[] directories)
        {
            // Check if core directroys exist
            foreach(string dir in directories)
                if(!Directory.Exists(dir))
                {
                    MessageBox.Show($"Unable to find core directory {dir}", "Error");
                    return;
                }

            //Get all directories from each core directroy 
            foreach (string dir in directories)
                foreach (string s in GetEndDirectorys(dir))
                    foreach(string directory in directories)
                    {
                        // Get index of core directroy
                        int index = s.IndexOfInPath(directory.GetParentDirs()[0]);
                        string[] parents = s.Split('\\');
                        if (index == -1 || parents.Count() == 0)
                            continue;

                        // Split the path string from index of core directroy
                        // and join path back after
                        string after = string.Join(@"\", parents.Skip(index + 1));

                        // Mirror to other core directorys
                        foreach (string dirPath in directories)
                            if (!Directory.Exists($"{dirPath}{after}"))
                                Directory.CreateDirectory($"{dirPath}{after}");
                    }
        }

        List<string> GetEndDirectorys(string directory)
        {
            //Searches through all directroys from point x and returns the end of each path
            List<string> topDirectories = new List<string>();
            try
            {
                foreach (string dir in Directory.GetDirectories(directory, "*", SearchOption.TopDirectoryOnly))
                    if (Directory.GetDirectories(dir).Count() == 0)
                        topDirectories.Add(dir);   
                    else
                        foreach(string d in GetEndDirectorys(dir))
                        topDirectories.Add(d);
            } catch { }
            
            return topDirectories;
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {
            Sync();
        }
    }
}
namespace jExtensions
{
    public static class StringExtension
    {
        public static List<string> GetParentDirs(this String str)
        {
            List<string> filePaths = new List<string>(str.Split('\\').Reverse());
            foreach (string fp in filePaths.ToList())
                if (fp == string.Empty || fp == null)
                    filePaths.Remove(fp);
            if (Path.GetPathRoot(str) == null)
                return null;
            return filePaths;
        }

        public static int IndexOfInPath(this String str, string searchFor)
        {
            if (Path.GetPathRoot(str) == null)
                return -1;
            string[] parents = str.Split('\\');
            if (parents.Count() == 0)
                return -1;
            for (int i = 0; i < parents.Count(); i++)
                if (searchFor == parents[i])
                    return i;
            return -1;
        }
    }
}
