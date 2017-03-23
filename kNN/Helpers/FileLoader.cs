using kNN.Content;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kNN.Helpers
{
    public class FileLoader
    {
        //flag - true ; jezeli wgrywamy zbior uczacy
        //flag - false ; jezeli wgrywamy zbior testowy
        public static List<List<String>> GetImages(bool flag)
        {
            List<List<string>> imagesList = new List<List<string>>();
            int s = 4;
            if (flag == false)
            {
                s = 1;
            }
            for (int i = 0 ; i < s ; i ++)
            {
                imagesList.Add(new List<string>());
            }
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;
                if (flag == true)
                {
                    List<string> paths = Directory.GetDirectories(path).ToList();
                    if (paths.Count() == 4)
                    {
                        for (int i = 0; i < paths.Count(); i++)
                        {
                            switch (paths[i].Substring(paths[i].Length - 2))
                            {
                                case "BK":
                                    foreach (var file in Directory.GetFiles(paths[i]))
                                    {
                                        imagesList[0].Add(file);
                                    }
                                    break;
                                case "BM":
                                    foreach (var file in Directory.GetFiles(paths[i]))
                                    {
                                        imagesList[1].Add(file);
                                    }
                                    break;
                                case "LK":
                                    foreach (var file in Directory.GetFiles(paths[i]))
                                    {
                                        imagesList[2].Add(file);
                                    }
                                    break;
                                case "LM":
                                    foreach (var file in Directory.GetFiles(paths[i]))
                                    {
                                        imagesList[3].Add(file);
                                    }
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    List<string> images = new List<string>();
                    foreach (var file in Directory.GetFiles(path))
                    {
                        images.Add(file);
                    }
                    imagesList.Add(images);
                }
            }
            return imagesList;
        }

        public static string GetDirectory()
        {
            string path = "";
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
            }
            return path;
        }

        public static void CopyAnswers(List<Face> solution, string answerPath)
        {
            for(int i = 0 ; i < solution.Count(); i++)
            {
                string picDir = solution[i].Path;
                string finalDir = answerPath + "\\" + solution[i].Class + "\\" + Path.GetFileName(picDir);
                File.Copy(@picDir, @finalDir);
            }
            /*
            foreach( var dir in Directory.GetDirectories(path))
               {
                   List<string> images = new List<string>();
                   foreach ( var file in Directory.GetFiles(dir))
                   {
                       images.Add(file);
                   }
                   imagesList.Add(images);
               }
            */
            //File.Copy(@"someDirectory\someFile.txt", @"otherDirectory\someFile.txt");
        }
    }
}
