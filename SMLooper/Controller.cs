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

        public void Cut(string left, string right, string _measure, int diffIndex)
        {
            Measure measure = Measure.Measures;
            if(_measure == "Measures")
            {
                measure = Measure.Measures;
            }
            double left_d = Double.Parse(left);
            double right_d = Double.Parse(right);

            Model model = new Model();
            ChartSlice chartSlice = model.Cut(left_d, right_d, measure, simFileInfo, diffIndex);
            chartSlices.Add(chartSlice);

            // возвращать что нибудь
        }

        public void Save(string path)
        {
            path = path + @"/" + simFileInfo.title;
            Directory.CreateDirectory(path);
            //write music
            using (var writer = File.Create(path+@"/"+simFileInfo.music))
            {
                for(int i = 0; i < chartSlices.Count(); i++)
                {
                    writer.Write(chartSlices[i].rawData, 0, chartSlices[i].rawData.Length);
                }
            }
            //write simfile
            //...
            using (var writer = File.CreateText(path + @"/" + Path.GetFileName(simFileInfo.path)+".sm"))
            {
                writer.WriteLine("#TITLE:" + simFileInfo.title + ";");
                writer.WriteLine("#SUBTITLE:;");
                writer.WriteLine("#ARTIST:" + simFileInfo.artist + ";");
                writer.WriteLine("#TITLETRANSLIT:;");
                writer.WriteLine("#SUBTITLETRANSLIT:;");
                writer.WriteLine("#ARTISTTRANSLIT:;");
                writer.WriteLine("#GENRE:;");
                writer.WriteLine("#CREDIT:" + simFileInfo.credit + ";");
                writer.WriteLine("#MUSIC:" + simFileInfo.music + ";");
                writer.WriteLine("#BANNER:;");
                writer.WriteLine("#BACKGROUND:;");
                writer.WriteLine("#CDTITLE:;");
                writer.WriteLine("#SAMPLESTART:0.000;");
                writer.WriteLine("#SAMPLELENGTH:0.000;");
                writer.WriteLine("#SELECTABLE:YES;");
                writer.WriteLine("#OFFSET:0.000;");
                string bpmString = "#BPMS:";
                int tempMeasureNum = 0;
                for(int i = 0; i < chartSlices.Count(); i++)
                {
                    for(int j = 0; j < chartSlices[i].bpms.Length; j++)
                    {
                        bpmString += chartSlices[i].bpms[j].measure*4 + tempMeasureNum;
                        bpmString += "=";
                        bpmString += chartSlices[i].bpms[j].bpm;
                        bpmString += ",\n";
                    }
                    tempMeasureNum += chartSlices[i].notes.Length*4;
                }
                writer.WriteLine(bpmString.TrimEnd('\n').TrimEnd(',') + ";\n");
                writer.WriteLine("#STOPS:;");
                writer.WriteLine("#BGCHANGES:;");
                writer.WriteLine("#FGCHANGES:;");
                writer.WriteLine("//--------------- dance-single - SMLooper ----------------");
                writer.WriteLine("#NOTES:\n\tdance-single:\n\tSMLooper:\n\tEdit:\n\t1:\n\t0,0,0,0,0:");
                for (int i = 0; i < chartSlices.Count(); i++)
                {
                    writer.Write(string.Join(",\n", chartSlices[i].notes));
                    writer.Write(",\n");
                }
                writer.WriteLine(";");

            }
        }
    }
}
