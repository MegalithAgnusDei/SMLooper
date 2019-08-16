using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper.Chart
{
    struct BPM
    {
        double measure;
        double bpm;
    }

    struct Chart
    {
        string stepAutor;
        string diff;
        string[] noteData; // Для каждого measure одна ячейка массива. Например noteData[0] = "0000\n0001\n0010\n0000"
    }
    
    class SimFileInfo
    {
        string title;
        string subtitle;
        string artist;
        string credit;

        string music;

        string banner;
        string bg;
        string cdtitle;

        double offset;
        BPM[] bpms;
        Chart[] charts;
    }
}
