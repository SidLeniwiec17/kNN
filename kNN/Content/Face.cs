using System;
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

        public Face()
        {
            this.Class = "UNKNOWN";
            this.Gradients = new List<int>();
        }
        public Face(string _class, List<int> _gradient)
        {
            this.Class = _class;
            this.Gradients = _gradient;
        }
    }
}
