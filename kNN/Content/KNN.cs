using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNN.Content
{
    public class KNN
    {
        public static List<Face> PerformKnn(List<Face> knownFaces, List<Face>faceToCheck, int k)
        {
            List<Face> solution = new List<Face>();

            /*WERSJA STANDARD
             */


            Parallel.For(0, faceToCheck.Count(), i =>
            {
                List<Face> tempKnownFaces = new List<Face>();
                for (int j = 0; j < knownFaces.Count(); j++)
                    tempKnownFaces.Add(new Face(knownFaces[j]));

                Face chceckedFace = new Face(faceToCheck[i]);

                for (int j = 0; j < tempKnownFaces.Count(); j++)
                {
                    tempKnownFaces[j].Distance = Distance(tempKnownFaces[j], chceckedFace);
                }
                QuickSort(tempKnownFaces, 0, tempKnownFaces.Count - 1);

                List<Face> neighbourFaces = new List<Face>();

                for (int j = 0; j < k; j++)
                {
                    neighbourFaces.Add(new Face(tempKnownFaces[j]));
                }
                int answer = CountMostClass(neighbourFaces);
                chceckedFace.ClassIndex = answer;
                switch (answer)
                {
                    case 0:
                        chceckedFace.Class = "BK";
                        break;
                    case 1:
                        chceckedFace.Class = "BM";
                        break;
                    case 2:
                        chceckedFace.Class = "LK";
                        break;
                    case 3:
                        chceckedFace.Class = "LM";
                        break;
                }
                solution.Add(chceckedFace);
            });
            //----------------
            return solution;
        }

        public static int CountMostClass(List<Face> faces)
        {
            int answer = -1;
            int[] counter = new int[4] { 0, 0, 0, 0 };
            for(int i = 0 ; i < faces.Count(); i++)
            {
                counter[faces[i].ClassIndex]++;
            }

            int max = 0;
            for (int i = 0; i < 4; i ++ )
            {
                if(counter[i]> max)
                {
                    max = counter[i];
                    answer = i;
                }
            }
            return answer;
        }

        public static double Distance (Face face1, Face face2)
        {
            double distance = 0.0;
            for (int i = 0; i < face1.Gradients.Count(); i++ )
            {
                double difference =  Math.Abs(face1.Gradients[i] - face2.Gradients[i]);
                if (difference == 7)
                    difference = 1;
                else if (difference == 6)
                    difference = 2;
                else if (difference == 5)
                    difference = 3;

                distance = distance + difference;                 
            }
            
            return distance;
        }
        public static void QuickSort(List<Face> array, int left, int right)
        {
            var i = left;
            var j = right;
            var pivot = array[(left + right) / 2].Distance;
            while (i < j)
            {
                while (array[i].Distance < pivot) i++;
                while (array[j].Distance > pivot) j--;
                if (i <= j)
                {
                    // swap
                    var tmp = array[i];
                    array[i++] = array[j];  // ++ and -- inside array braces for shorter code
                    array[j--] = tmp;
                }
            }
            if (left < j) QuickSort(array, left, j);
            if (i < right) QuickSort(array, i, right);
        }
    }
}
