﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kNN.Content
{
    public class Face
    {
        public string Class { get; set; }
        public List<int> Gradients { get; set; }
        public string Path { get; set; }
        public double Distance { get; set; }
        public int ClassIndex { get; set; }

        public Face()
        {
            this.Class = "UNKNOWN";
            this.Gradients = new List<int>();
            this.ClassIndex = -1;
        }
        public Face(string _class, List<int> _gradient, int _index)
        {
            this.Class = _class;
            this.Gradients = _gradient;
            this.ClassIndex = _index;
        }

        public Face(string _class, List<int> _gradient, string _path, int _index)
        {
            this.Class = _class;
            this.Gradients = _gradient;
            this.Path = _path;
            this.ClassIndex = _index;
        }
        public Face(Face _face)
        {
            this.Class = _face.Class;
            this.Gradients = _face.Gradients;
            this.Path = _face.Path;
            this.Distance = _face.Distance;
            this.ClassIndex = _face.ClassIndex;
        }
        public Face(Face _face, double distance)
        {
            this.Class = _face.Class;
            this.Gradients = _face.Gradients;
            this.Path = _face.Path;
            this.Distance = distance;
            this.ClassIndex = _face.ClassIndex;
        }
    }
}
