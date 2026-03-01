using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleServiceLib
{
    public class VirusScanReport
    {
        public int Malicious { get; set; }
        public int Harmless { get; set; }
        public int Undetected { get; set; }
        public string Status { get; set; } // "clean", "malicious", "suspicious"
        public string DetailedUrl { get; set; }
    }
}
