﻿using kNN.Content;
using kNN.Helpers;
using System;
using System.Collections.Generic;
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

namespace kNN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Face> dataSet;
        List<Face> unknownFaces;
        List<Face> solution;
        string answerPath;
        public MainWindow()
        {
            InitializeComponent();
            dataSet = new List<Face>();
            unknownFaces = new List<Face>();
            solution = new List<Face>();
            answerPath = "";
        }

        private async void LoadLearningPicture_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            List<List<string>> pictures = FileLoader.GetImages(true);
            await ConvertKnownPic(pictures);
            BlakWait.Visibility = Visibility.Collapsed;
        }
        private async void LoadTestingPicture_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            List<List<string>> tempUnknownPictures = FileLoader.GetImages(false);
            await ConvertUnKnownPic(tempUnknownPictures);
            BlakWait.Visibility = Visibility.Collapsed;
        }
        private void LoadAnswerDir_Click(object sender, RoutedEventArgs e)
        {
            answerPath = FileLoader.GetDirectory();
        }

        private async Task ConvertKnownPic(List<List<string>> pictures)
        {
            await Task.Run(() =>
               {
                   dataSet = FaceConverter.ConvertFaces(pictures, true);
               });
        }
        private async Task ConvertUnKnownPic(List<List<string>> tempUnknownPictures)
        {
            await Task.Run(() =>
               {
                   unknownFaces = FaceConverter.ConvertFaces(tempUnknownPictures, false);
               });
        }

        private async void Knn_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            int k = 0;
            if(!int.TryParse(kTextBox.Text,out k))
                return;
            if (k < 1 || dataSet.Count < 2 || unknownFaces.Count < 1|| answerPath.Length < 5)
                return;

           await PerformCalculations(k);

           BlakWait.Visibility = Visibility.Collapsed;
        }

        public async Task PerformCalculations(int k)
        {
            await Task.Run(() =>
               {
                   solution = KNN.PerformKnn(dataSet, unknownFaces, k);
                   FileLoader.CopyAnswers(solution, answerPath);
               });
        }

        private void SaveBin_Click(object sender, RoutedEventArgs e)
        {
            if (FileLoader.SaveBinary(dataSet) == 1)
                Console.WriteLine("zapisano do binarki");
        }
        private void LoadBin_Click(object sender, RoutedEventArgs e)
        {
            dataSet.Clear();
            dataSet = FileLoader.LoadBinary();
            if (dataSet.Count >= 1)
            {
                Console.WriteLine("wczytano z binarki " + dataSet.Count + " danych");
            }
        }
    }
}
