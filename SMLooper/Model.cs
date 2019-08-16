using SMLooper.Chart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Start(double left, double right, Measure measure, SimFileInfo simFileInfo)
        {
            
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
            /*

            for(int curMeas = 0; curMeas < (int)measure; curMeas++)
            {
                double curBpm = 0;
                for(int i = 0; i < bpms.Length; i++)
                {
                    if (curMeas >= bpms[i].measure)
                    {
                        curBpm = bpms[i].bpm;
                    }
                    else
                    {
                        break;
                    }
                }
                tempMs += 240000 / curBpm;
            }

            double remainder = measure - (int)measure;
            double remainderBpm = 0;
            for (int i = 0; i < bpms.Length; i++)
            {
                if ((int)measure >= bpms[i].measure)
                {
                    remainderBpm = bpms[i].bpm;
                }
                else
                {
                    break;
                }
            }

            tempMs += 240000 * remainder / remainderBpm;
            */
            tempMs += trueOffset;

            return tempMs;
        } 
    }
}
