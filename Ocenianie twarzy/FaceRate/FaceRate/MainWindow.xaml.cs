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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FaceRate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> imagesList = new List<string>();
        string targetFolder;
        int currentIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            GetImages();
            SelectTargetFolder();
            SetImageSource(imagesList[0]);
        }
        private void GetImages()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Wybierz folder z twarzami";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;
                foreach(var imagePath in Directory.GetFiles(path))
                {
                    imagesList.Add(imagePath);
                }
            }
        }
        private void SelectTargetFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Wybierz folder z docelowy z folderami BM,LM,LK,BK";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                targetFolder = folderBrowserDialog.SelectedPath;
            }
        }
        private void SetImageSource(string path)
        {
            var uriSource = new Uri(path, UriKind.Absolute);
            imageDisplay.Source = new BitmapImage(uriSource);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D1:
                    MovePhotoTo("\\LK\\");
                    break;
                case Key.D2:
                    MovePhotoTo("\\BK\\");
                    break;
                case Key.D0:
                    MovePhotoTo("\\LM\\");
                    break;
                case Key.OemMinus:
                    MovePhotoTo("\\BM\\");
                    break;
                case Key.Space:
                    NextPhoto();
                    break;
            }
        }
        private void MovePhotoTo(string folder)
        {
            SetImageSource(imagesList[currentIndex + 1]);
            var time = DateTime.Now.ToFileTime();
            File.Copy(imagesList[currentIndex], targetFolder + folder + time + ".jpg", true);
    
            currentIndex++;
        }
        private void NextPhoto()
        {
            SetImageSource(imagesList[currentIndex+1]);
            currentIndex++;
        }
        
    }
}
