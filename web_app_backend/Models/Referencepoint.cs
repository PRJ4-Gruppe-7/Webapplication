using System;
using System.Collections.Generic;
using System.Text;

namespace web_app_backend.Models
{
    class Referencepoint
    {
        public int ReferencepointID { get; set; }
        public int Category { get; set; } //ClusterID
        public int RSSI1 { get; set; }
        public int RSSI2 { get; set; }
        public int RSSI3 { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int HeatmapID { get; set; }
        public Heatmap Heatmap { get; set; }
    }
}
