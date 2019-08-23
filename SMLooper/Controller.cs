using NAudio.Lame;
using NAudio.Wave;
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
        WaveFormat wf;

        public Controller()
        {

        }

        public SimFileInfo ParseSmFile(String path)
        {
            Model model = new Model();
            simFileInfo = model.ParseSmFile(path);

            return simFileInfo;
        }
        
        public ChartSlice Cut(string left, string right, string _measure, int diffIndex)
        {
            Measure measure = Measure.Measures;
            if(_measure == "Measures")
            {
                measure = Measure.Measures;
            }
            double left_d = Double.Parse(left);
            double right_d = Double.Parse(right);

            Model model = new Model();
            ChartSlice chartSlice = model.Cut(left_d, right_d, measure, simFileInfo, diffIndex, wf);
            chartSlices.Add(chartSlice);

            return chartSlice;
        }

        public void MakeWav()
        {
            Mp3FileReader reader = new Mp3FileReader(simFileInfo.path + @"/" + simFileInfo.music);
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(reader);
            wf = pcm.WaveFormat;
            WaveFileWriter.CreateWaveFile(Path.GetTempPath() + "/"+ simFileInfo.music+".wav", pcm);
        }

        public void Save(string path, bool easyIn)
        {
            path = path + @"/" + Path.GetFileName(simFileInfo.path) + " (Looped)";
            Directory.CreateDirectory(path);

            //bg bn copy
            if (simFileInfo.bg != "")
                try { File.Copy(simFileInfo.path + @"/" + simFileInfo.bg, path + @"/" + simFileInfo.bg, true);} catch { }
            if (simFileInfo.banner != "")
                try { File.Copy(simFileInfo.path + @"/" + simFileInfo.banner, path + @"/" + simFileInfo.banner, true); } catch { }
            if (simFileInfo.cdtitle != "")
                try { File.Copy(simFileInfo.path + @"/" + simFileInfo.cdtitle, path + @"/" + simFileInfo.cdtitle, true); } catch { }

            double offset = 0;

            //write music
            using (var writer = new NAudio.Wave.WaveFileWriter(path + @"/temp.wav", chartSlices[0].wf))
            {
                if (easyIn)
                {
                    byte[] preview = new Model().CutPreview(chartSlices[0].start, simFileInfo, out offset);
                    writer.Write(preview, 44, preview.Length-44);
                }
                for (int i = 0; i < chartSlices.Count(); i++)
                {
                    Model model = new Model();
                    byte[] data = model.GetRatedData(chartSlices[i].rawData, chartSlices[i].rate, chartSlices[i].wf);
                    for(int j = 0; j < chartSlices[i].bpms.Length;j++)
                    {
                        chartSlices[i].bpms[j].bpm *= chartSlices[i].rate;
                    }
                    writer.Write(data, 44, data.Length-44);
                }
            }
            using (var rdr = new WaveFileReader(path + @"/temp.wav"))
            using (var wtr = new LameMP3FileWriter(path + @"/" + simFileInfo.music, rdr.WaveFormat, 128))
            {
                rdr.CopyTo(wtr);
            }
            File.Delete(path + @"/temp.wav");

            offset -= 0.050;

            //write file info
            using (var writer = File.CreateText(path + @"/" + Path.GetFileName(simFileInfo.path)+".sm"))
            {
                writer.WriteLine("#TITLE:" + simFileInfo.title + ";");
                writer.WriteLine("#TITLETRANSLIT:;");
                writer.WriteLine("#SUBTITLE:" + simFileInfo.subtitle + ";");
                writer.WriteLine("#SUBTITLETRANSLIT:;");
                writer.WriteLine("#ARTIST:" + simFileInfo.artist + ";");
                writer.WriteLine("#ARTISTTRANSLIT:;");
                writer.WriteLine("#GENRE:;");
                writer.WriteLine("#CREDIT:" + simFileInfo.credit + ";");
                writer.WriteLine("#MUSIC:" + simFileInfo.music + ";");
                writer.WriteLine("#BANNER:" + simFileInfo.banner + ";");
                writer.WriteLine("#BACKGROUND:" + simFileInfo.bg + ";");
                writer.WriteLine("#CDTITLE:" + simFileInfo.cdtitle + ";");
                writer.WriteLine("#SAMPLESTART:0.000;");
                writer.WriteLine("#SAMPLELENGTH:0.000;");
                writer.WriteLine("#SELECTABLE:YES;");
                writer.WriteLine("#OFFSET:" + String.Format("{0:0.0000}", offset).Replace(',','.') +";");
                string bpmString = "#BPMS:";
                int tempMeasureNum = 0;
                for(int i = 0; i < chartSlices.Count(); i++)
                {
                    for(int j = 0; j < chartSlices[i].bpms.Length; j++)
                    {
                        bpmString += (chartSlices[i].bpms[j].measure*4 + tempMeasureNum).ToString().Replace(',','.');
                        bpmString += "=";
                        bpmString += chartSlices[i].bpms[j].bpm.ToString().Replace(',', '.');
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

        public void RemoveSliceByHash(int hash)
        {
            for(int i = 0; i < chartSlices.Count();i++)
            {
                if(chartSlices[i].GetHashCode() == hash)
                {
                    chartSlices.RemoveAt(i);
                    break;
                }
            }
        }

        public void UpdateRateByHash(int hash, double rate)
        {
            for (int i = 0; i < chartSlices.Count(); i++)
            {
                if (chartSlices[i].GetHashCode() == hash)
                {
                    chartSlices[i].rate = rate;
                    break;
                }
            }
        }

        public int GetCountOfSlices()
        {
            return chartSlices.Count();
        }

        public List<ChartSlice> GetChartSlices()
        {
            return chartSlices;
        }

        public void ClearChartSlicesList()
        {
            chartSlices.Clear();
        }
    }
}
