using SMLooper.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.IO;

namespace SMLooper
{
    class Model
    {
        public Model()
        {

        }

        public SimFileInfo ParseSmFile(String path)
        {
            SimFileInfo simFileInfo = new SimFileInfo();

            //TODO: парсинг файла
            
            return simFileInfo;
        }

        public ChartSlice Cut(double left, double right, Measure measure, SimFileInfo simFileInfo)
        {
            double begin = MeasureToMs(left, simFileInfo.offset, simFileInfo.bpms);
            double end = MeasureToMs(right, simFileInfo.offset, simFileInfo.bpms);

            Mp3FileReader reader = new Mp3FileReader(simFileInfo.path+@"/"+simFileInfo.music);

            List<byte> bytes = new List<byte>();
            Mp3Frame frame;
            while ((frame = reader.ReadNextFrame()) != null)
            {
                if (reader.CurrentTime.TotalMilliseconds >= begin)
                {
                    if (reader.CurrentTime.TotalMilliseconds <= end)
                        bytes.AddRange(frame.RawData);
                }
            }
            reader.Close();

            return new ChartSlice(1.0, bytes.ToArray(), null);
        }

        private double MeasureToMs(double measure, double offset, BPM[] bpms)
        {
            double trueOffset = -offset * 1000;

            double tempMs = 0;
            for(int i = 0; i < bpms.Length; i++)
            {
                if (i < bpms.Length - 1)
                {
                    tempMs += 240000 / bpms[i].bpm * (Math.Min(bpms[i + 1].measure, measure) - bpms[i].measure);
                    if (Math.Min(bpms[i + 1].measure, measure) == measure) break;
                }
                else
                {
                    tempMs += 240000 / bpms[i].bpm * (measure - bpms[i].measure);
                }
            }
            tempMs += trueOffset;

            return tempMs;
        } 
    }
}
