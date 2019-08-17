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

        public ChartSlice(double rate, byte[] rawData, string[] notes)
        {
            this.rate = rate;
            this.rawData = rawData;
            this.notes = notes;
        }
    }
}
