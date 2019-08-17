using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper.Chart
{
    struct BPM
    {
        public double measure;
        public double bpm;
    }

    struct Chart
    {
        public string stepAutor;
        public string diff;
        public string[] noteData; // Для каждого measure одна ячейка массива. Например noteData[0] = "0000\n0001\n0010\n0000"
    }
    
    class SimFileInfo
    {
        public string title;
        public string subtitle;
        public string artist;
        public string credit;

        public string music;

        public string banner;
        public string bg;
        public string cdtitle;

        public double offset;
        public BPM[] bpms;
        public Chart[] charts;

        public string path;
    }
}
