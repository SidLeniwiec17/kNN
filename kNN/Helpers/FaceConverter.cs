using kNN.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace kNN.Helpers
{
    public class FaceConverter
    {
        //flag - true ; jezeli wgrywamy zbior uczacy
        //flag - false ; jezeli wgrywamy zbior testowy
        public static List<Face> ConvertFaces(List<List<string>> pictures, bool flag)
        {
            List<Face> faces = new List<Face>();

            for (int c = 0; c < pictures.Count(); c++)
            {
                Parallel.For(0, pictures[c].Count(), i =>
                {
                    List<int> gradients = new List<int>();
                    string _class = "UNKNOWN";

                    Bitmap picture = GrayScale(new Bitmap(pictures[c][i]));
                    gradients = GetGradient(picture);
                    if (flag == true)
                    {
                        switch (c)
                        {
                            case 0:
                                _class = "BK";
                                break;
                            case 1:
                                _class = "BM";
                                break;
                            case 2:
                                _class = "LK";
                                break;
                            case 3:
                                _class = "LM";
                                break;
                        }
                    }
                    if (flag == true)
                    {
                        Face tempFace = new Face(_class, gradients);
                        faces.Add(tempFace);
                    }
                    else
                    {
                        Face tempFace = new Face(_class, gradients, pictures[c][i]);
                        faces.Add(tempFace);
                    }
                });
            }
            return faces;
        }

        public static List<int> GetGradient(Bitmap picture)
        {
            List<int> grad = new List<int>();
            int[,] smallGradients = new int[picture.Width-2, picture.Height-2];

            for (int y = 1; y < picture.Height - 1; y++)
            {
                for (int x = 1; x < picture.Width - 1; x++)
                {
                    Color[,] neighbours = new Color[3, 3];
                    neighbours[0, 0] = picture.GetPixel(x - 1, y - 1);
                    neighbours[1, 0] = picture.GetPixel(x, y - 1);
                    neighbours[2, 0] = picture.GetPixel(x + 1, y - 1);
                    neighbours[0, 1] = picture.GetPixel(x - 1, y);
                    neighbours[1, 1] = picture.GetPixel(x, y);
                    neighbours[2, 1] = picture.GetPixel(x + 1, y);
                    neighbours[0, 2] = picture.GetPixel(x - 1, y + 1);
                    neighbours[1, 2] = picture.GetPixel(x, y + 1);
                    neighbours[2, 2] = picture.GetPixel(x + 1, y + 1);
                    //GRADIENT
                    smallGradients[x - 1, y - 1] = GetDirection(neighbours);
                }
            }

            grad = SmallGradientToBigGradient(smallGradients, picture.Width - 2, picture.Height - 2);

            return grad;
        }

        public static List<int> SmallGradientToBigGradient(int[,] smallGradient, int width, int height)
        {
            
            int gX = 10;
            int gY = 15;
            int dX = (width / gX) + 1;
            int dY = (height / gY) + 1;
            List<int> gradient = new List<int>();
            double[,] values = new double[gX, gY];
            int[,] numbers = new int[gX, gY];

            for (int y = 0; y < height; y++ )
            {
                for(int x = 0 ; x < width ; x++)
                {
                    values[x / dX, y / dY] = values[x / dX, y / dY] + (double)smallGradient[x, y];
                    numbers[x / dX, y / dY] = numbers[x / dX, y / dY] + 1;
                }
            }

            for (int y = 0; y < gY; y++)
            {
                for (int x = 0; x < gX; x++)
                {
                    gradient.Add((int)(values[x, y] / numbers[x, y]));
                }
            }
            
            return gradient;
        }

        /*
         * directions
         * 8 1 2
         * 7   3
         * 6 5 4
         * */
        public static int GetDirection(Color[,] pixels)
        {
            int direction = -1;
            int maxValue = -255;
            int maxX = -1;
            int maxY = -1;

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                {
                    if (pixels[x, y].R >= maxValue && (x != 1 && y != 1))
                    {
                        maxValue = pixels[x, y].R;
                        maxX = x;
                        maxY = y;
                    }
                }

            int tempDir = (maxY * 3) + maxX;
            if (pixels[1, 1].R == 255 && maxValue == 255)
            {
                direction = 0;
            }

            else if (pixels[1, 1].R <= maxValue)
            {
                direction = DirectionTranslator.TranslateBiggerCenterDirection(tempDir);
            }
            else
            {
                direction = DirectionTranslator.TranslateBiggerCenterDirection(tempDir);
            }
            return direction;
        }

        public static Bitmap GrayScale(Bitmap color)
        {
            Bitmap gray = new Bitmap(color.Width, color.Height);

            for (int i = 0; i < color.Width; i++)
            {
                for (int x = 0; x < color.Height; x ++ )
                {
                    Color oc = color.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    gray.SetPixel(i, x, nc);
                }
            }
            return gray;
        }
    }
}
