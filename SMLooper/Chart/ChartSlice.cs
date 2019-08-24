using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper.Chart
{
    class ChartSlice
    {
        public double rate;
        public byte[] rawData;
        public string[] notes;
        public BPM[] bpms;
        public NAudio.Wave.WaveFormat wf;
        public double start;
        public double duration;

        public ChartSlice(double rate, byte[] rawData, string[] notes, BPM[] bpms, NAudio.Wave.WaveFormat wf, double start, double duration)
        {
            this.rate = rate;
            this.rawData = rawData;
            this.notes = notes;
            this.bpms = bpms;
            this.wf = wf;
            this.start = start;
            this.duration = duration;
        }
    }
}
