using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISeeYou
{
    class Entry
    {
        public string dir { get; set; }
        public string name { get; set; }
        public string side { get; set; }
        public string phase { get; set; }
        public int[] histogram { get; set; }
        public Size size { get; set; }
  
    }
}
