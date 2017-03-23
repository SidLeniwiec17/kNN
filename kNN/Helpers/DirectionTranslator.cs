using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNN.Helpers
{
    public class DirectionTranslator
    {
        public static int TranslateBiggerCenterDirection(int i)
        {
            int direction = -1;
            switch (i)
            {
                case 0:
                    direction = 4;
                    break;
                case 1:
                    direction = 5;
                    break;
                case 2:
                    direction = 6;
                    break;
                case 3:
                    direction = 3;
                    break;
                case 5:
                    direction = 7;
                    break;
                case 6:
                    direction = 2;
                    break;
                case 7:
                    direction = 1;
                    break;
                case 8:
                    direction = 8;
                    break;
            }
            return direction;
        }
        public static int TranslateSmallerCenterDirection(int i)
        {
            int direction = -1;
            switch (i)
            {
                case 0:
                    direction = 8;
                    break;
                case 1:
                    direction = 1;
                    break;
                case 2:
                    direction = 2;
                    break;
                case 3:
                    direction = 7;
                    break;
                case 5:
                    direction = 8;
                    break;
                case 6:
                    direction = 6;
                    break;
                case 7:
                    direction = 5;
                    break;
                case 8:
                    direction = 4;
                    break;
            }
            return direction;
        }
    }
}
