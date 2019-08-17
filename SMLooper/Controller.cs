using SMLooper.Chart;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLooper
{
    public enum Measure { Measures }

    class Controller
    {
        SimFileInfo simFileInfo = new SimFileInfo();
        List<ChartSlice> chartSlices = new List<ChartSlice>();

        public Controller()
        {

        }

        public void ParseSmFile(String path)
        {
            Model model = new Model();
            simFileInfo = model.ParseSmFile(path);

            // возвращать инфо чтобы красивенько было, баннер там и название например
        }

        public void Cut(string left, string right, string _measure)
        {
            Measure measure = Measure.Measures;
            if(_measure == "Measures")
            {
                measure = Measure.Measures;
            }
            double left_d = Double.Parse(left);
            double right_d = Double.Parse(right);

            Model model = new Model();
            ChartSlice chartSlice = model.Cut(left_d, right_d, measure, simFileInfo);
            chartSlices.Add(chartSlice);

            // возвращать что нибудь
        }

        public void Save(string path)
        {
            //write music
            using (var writer = File.Create(path))
            {
                for(int i = 0; i < chartSlices.Count(); i++)
                {
                    writer.Write(chartSlices[i].rawData, 0, chartSlices[i].rawData.Length);
                }
            }
            //write simfile
            //...
        }
    }
}
