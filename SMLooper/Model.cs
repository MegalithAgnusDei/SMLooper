using SMLooper.Chart;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NAudio.Lame;
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
            simFileInfo.path = Path.GetDirectoryName(path);
            StreamReader sr = new StreamReader(path);
            string line;
            string[] words;

            while((line = sr.ReadLine()) != null)
            {
                words = line.Split(':');

                if(words[0] == "#TITLE")
                    simFileInfo.title = words[1].Trim(';');

                if (words[0] == "#SUBTITLE")
                    simFileInfo.subtitle = words[1].Trim(';');

                if (words[0] == "#ARTIST")
                    simFileInfo.artist = words[1].Trim(';');

                if (words[0] == "#CREDIT")
                    simFileInfo.credit = words[1].Trim(';');

                if (words[0] == "#MUSIC")
                    simFileInfo.music = words[1].Trim(';');

                if (words[0] == "#BANNER")
                    simFileInfo.banner = words[1].Trim(';');

                if (words[0] == "#BACKGROUND")
                    simFileInfo.bg = words[1].Trim(';');

                if (words[0] == "#CDTITLE")
                    simFileInfo.cdtitle = words[1].Trim(';');

                if (words[0] == "#OFFSET")
                    simFileInfo.offset = Convert.ToDouble(words[1].Trim(';').Replace('.', ','));

                if (words[0] == "#BPMS")
                {
                    if (words[1].EndsWith(";"))
                    {
                        if (words[1].Split(',').Length > 1)
                        {
                            List<BPM> bpmList = new List<BPM>();
                            string[] bpofarr = words[1].Trim(';').Split(',');
                            foreach (string element in bpofarr)
                            {
                                BPM bpm = new BPM();
                                string[] bpof = element.Split('=');
                                bpm.measure = Convert.ToDouble(bpof[0].Replace('.', ','));
                                bpm.bpm = Convert.ToDouble(bpof[1].Replace('.', ','));
                                bpmList.Add(bpm);
                            }
                            simFileInfo.bpms = bpmList.ToArray();
                        }
                        else
                        {
                            BPM bpm = new BPM();
                            string[] bpof;
                            bpof = words[1].Split('=');
                            bpm.measure = Convert.ToDouble(bpof[0].Replace('.', ','));
                            bpm.bpm = Convert.ToDouble(bpof[1].Trim(';').Replace('.', ','));
                            simFileInfo.bpms = new BPM[] { bpm };
                        }
                    }
                    else
                    {
                        List<BPM> bpmList = new List<BPM>();
                        string[] bpof = words[1].Split('=');
                        BPM bpm = new BPM();
                        bpm.measure = Convert.ToDouble(bpof[0].Replace('.', ','));
                        bpm.bpm = Convert.ToDouble(bpof[1].Replace('.', ','));
                        bpmList.Add(bpm);
                        while ((line = sr.ReadLine().Trim(',')) != ";")
                        {
                            bpof = line.Split('=');
                            bpm = new BPM();
                            bpm.measure = Convert.ToDouble(bpof[0].Replace('.', ',')) / 4;
                            bpm.bpm = Convert.ToDouble(bpof[1].Replace('.', ','));
                            bpmList.Add(bpm);
                        }
                        simFileInfo.bpms = bpmList.ToArray();
                    }
                }

                if (words[0] == "#NOTES")
                {
                    List<Chart.Chart> chartList = new List<Chart.Chart>();
                    while ((line = sr.ReadLine()) != null)
                    {
                        Chart.Chart chart = new Chart.Chart();
                        line = sr.ReadLine();
                        chart.stepAutor = line.Trim(' ', ':');
                        line = sr.ReadLine();
                        chart.diff = line.Trim(' ', ':');
                        line = sr.ReadLine(); line = sr.ReadLine();
                        string notes = "";
                        while ((line = sr.ReadLine()) != ";")
                        {
                            if (line == ",")
                                notes += line;
                            else
                                notes += line + "\n";
                        }
                        chart.noteData = notes.Split(',');
                        chartList.Add(chart);
                        if ((line = sr.ReadLine()) == null)
                            break;
                        else
                        {
                            sr.ReadLine();
                            continue;
                        }
                    }
                    simFileInfo.charts = chartList.ToArray();
                }
            }
            sr.Close();
            return simFileInfo;
        }

        private static void TrimWavFile(string inPath, string outPath, double cutFromStart, double cutFromEnd)
        {
            double kastilEbany =   0.0001 * Math.Pow(cutFromStart / 1000, 2) + 2.2575 * (cutFromStart / 1000) + 3.6794; //0;// 0.0001*Math.Pow(cutFromStart / 1000,3) + (-1)*0.0216*Math.Pow(cutFromStart / 1000,2) + 4.3671*(cutFromStart / 1000) + 1.5984; // -1*0.0034*(cutFromStart/1000) * (cutFromStart / 1000) + 2.9181*(cutFromStart / 1000) + 16.5694;
            cutFromStart += kastilEbany;
            cutFromEnd += kastilEbany;
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    int bytesPerMillisecond =(int)((double)reader.Length/reader.TotalTime.TotalMilliseconds);

                    int startPos = (int)((cutFromStart) * bytesPerMillisecond);
                    int endPos = (int)((cutFromEnd + 2.24 * (cutFromEnd - cutFromStart) / 1000) * bytesPerMillisecond); 

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            int mod = (endPos - startPos) % 4;
            endPos -= mod;
            byte[] bytes = new byte[endPos - startPos];
            reader.Position = startPos;
            reader.Read(bytes, 0, endPos - startPos);
            writer.Write(bytes, 0, bytes.Length);
        }

        public ChartSlice Cut(double left, double right, Measure measure, SimFileInfo simFileInfo, int diffIndex, WaveFormat wf)
        {
            double begin = MeasureToMs((int)left, simFileInfo.offset, simFileInfo.bpms);
            double end = MeasureToMs(Math.Ceiling(right), simFileInfo.offset, simFileInfo.bpms);
            
            TrimWavFile(Path.GetTempPath() + "/" + simFileInfo.music + ".wav", Path.GetTempPath() + "/temp.wav", begin, end);
            var w = File.Open(Path.GetTempPath() + "/temp.wav", FileMode.Open);
            
            byte[] bytes = new byte[w.Length];
            w.Read(bytes, 0, (int)w.Length);
            w.Close();

            File.Delete(Path.GetTempPath() + "/temp.wav");

            ///////////////////////

            int totalChunks = (int)(Math.Ceiling(right) - (int)left);
            string[] notes = new string[totalChunks];
            for(int i = 0; i < totalChunks; i++)
            {
                List<string> notesTemp = new List<string>();
                if(i == 0)
                {
                    double skipPart = left - (int)left;
                    string[] arr = simFileInfo.charts[diffIndex].noteData[(int)left + i].Split('\n');

                    for(int j = 0; j < arr.Length; j++)
                    {
                        if((double)j/ (double)(arr.Length-1) >= skipPart)
                        {
                            notesTemp.Add(arr[j]);
                        }
                        else
                        {
                            notesTemp.Add("0000");
                        }
                    }
                }
                else if(i == totalChunks - 1)
                {
                    double skipPart = Math.Ceiling(right) - right;
                    string[] arr = simFileInfo.charts[diffIndex].noteData[(int)left + i].Split('\n');

                    for (int j = 0; j < arr.Length; j++)
                    {
                        if (1 - ((double)j / (double)(arr.Length - 1)) >= skipPart)
                        {
                            notesTemp.Add(arr[j]);
                        }
                        else
                        {
                            notesTemp.Add("0000");
                        }
                    }
                }
                else
                {
                    string[] arr = simFileInfo.charts[diffIndex].noteData[(int)left + i].Split('\n');
                    for (int j = 0; j < arr.Length; j++)
                    {
                        notesTemp.Add(arr[j]);
                    }
                }
                notes[i] = string.Join("\n", notesTemp.ToArray());
            }

            List<BPM> bpmList = new List<BPM>();
            BPM leftBpm = new BPM();
            for (int i = 0; i < simFileInfo.bpms.Length; i++)
            {
                if(simFileInfo.bpms[i].measure < (int)left)
                {
                    leftBpm.bpm = simFileInfo.bpms[i].bpm;
                    leftBpm.measure = 0;
                }
                else if(simFileInfo.bpms[i].measure > (int)left && simFileInfo.bpms[i].measure < Math.Ceiling(right))
                {
                    BPM bpm = new BPM();
                    bpm.measure = simFileInfo.bpms[i].measure - (int)left;
                    bpm.bpm = simFileInfo.bpms[i].bpm;
                    bpmList.Add(bpm);
                }
            }
            bpmList.Insert(0, leftBpm);


            return new ChartSlice(1.0, bytes, notes,bpmList.ToArray(), wf, (int)left);
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

        public byte[] CutPreview(double right, SimFileInfo simFileInfo, out double offset)
        {
            double left = right - 0.5 >= 0 ? right - 0.5 : 0;
            double begin = MeasureToMs((int)left, simFileInfo.offset, simFileInfo.bpms);
            double end = MeasureToMs(right, simFileInfo.offset, simFileInfo.bpms);
            offset = (double)(begin-end)/1000;

            Mp3FileReader reader = new Mp3FileReader(simFileInfo.path + @"/" + simFileInfo.music);

            List<byte> bytes = new List<byte>();
            Mp3Frame frame;
            while ((frame = reader.ReadNextFrame()) != null)
            {
                if (reader.CurrentTime.TotalMilliseconds >= begin)
                {
                    if (reader.CurrentTime.TotalMilliseconds < end)
                        bytes.AddRange(frame.RawData);
                }
            }
            int br = (int)Math.Round((double)reader.Mp3WaveFormat.AverageBytesPerSecond * 8 / 1000);
            reader.Close();

            return bytes.ToArray();
        }
        private byte[] ConvertMp3ToWav(byte[] data)
        {
            using (var ms = new MemoryStream())
            using (var mp3 = new Mp3FileReader(new MemoryStream(data)))
            using (var pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
            {
                WaveFileWriter.WriteWavFileToStream(ms, pcm);
                return ms.ToArray();
            }
        }
        public byte[] ConvertWavToMp3(byte[] data, int br)
        {

            using (var retMs = new MemoryStream())
            using (var ms = new MemoryStream(data))
            using (var rdr = new WaveFileReader(ms))
            using (var wtr = new LameMP3FileWriter(retMs, rdr.WaveFormat, br))
            {
                rdr.CopyTo(wtr);
                return retMs.ToArray();
            }


        }
    }
}
