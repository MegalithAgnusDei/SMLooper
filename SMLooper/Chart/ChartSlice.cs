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

        public ChartSlice(double rate, byte[] rawData, string[] notes, BPM[] bpms)
        {
            this.rate = rate;
            this.rawData = rawData;
            this.notes = notes;
            this.bpms = bpms;
        }
    }
}
