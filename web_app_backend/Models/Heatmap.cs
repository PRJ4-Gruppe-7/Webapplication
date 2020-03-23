using System;
using System.Collections.Generic;
using System.Text;

namespace web_app_backend.Models
{
    class Heatmap
    {
        public int x { get; set; }
        public int y { get; set; }
        public float Strength { get; set; }
        public Referencepoint Referencepoint { get; set; }
    }
}
