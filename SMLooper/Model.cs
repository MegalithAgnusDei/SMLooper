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
using SoundTouchNet;

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
            double kastilEbany = 0.0001 * Math.Pow(cutFromStart / 1000, 2) + 2.2575 * (cutFromStart / 1000) + 3.6794; //0;// 0.0001*Math.Pow(cutFromStart / 1000,3) + (-1)*0.0216*Math.Pow(cutFromStart / 1000,2) + 4.3671*(cutFromStart / 1000) + 1.5984; // -1*0.0034*(cutFromStart/1000) * (cutFromStart / 1000) + 2.9181*(cutFromStart / 1000) + 16.5694;
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
            double left = right - 1.5 >= 0 ? right - 1.5 : 0;
            double begin = MeasureToMs((int)left, simFileInfo.offset, simFileInfo.bpms);
            double end = MeasureToMs(right, simFileInfo.offset, simFileInfo.bpms);

            offset = (double)(begin - end) / 1000 + 0.01;

            TrimWavFile(Path.GetTempPath() + "/" + simFileInfo.music + ".wav", Path.GetTempPath() + "/temp.wav", begin, end);
            using (var wr = new WaveFileReader(Path.GetTempPath() + "/temp.wav"))
            {
                using (var ww = new WaveFileWriter(Path.GetTempPath() + "/temp_ei.wav",wr.WaveFormat))
                {
                    byte[] wrb = new byte[wr.Length];
                    wr.Read(wrb, 0, wrb.Length);
                    short[] shorts = new short[wrb.Length / sizeof(short)];
                    Buffer.BlockCopy(wrb, 0, shorts, 0, wrb.Length);
                    for (int i = 0; i < shorts.Length; i++)
                    {
                        shorts[i] =(short)( shorts[i] * (double)i/ shorts.Length);
                    }
                    wrb = shorts.SelectMany(BitConverter.GetBytes).ToArray();
                    ww.Write(wrb, 0, wrb.Length);
                }
            }
            var w = File.Open(Path.GetTempPath() + "/temp_ei.wav", FileMode.Open);

            byte[] bytes = new byte[w.Length];
            w.Read(bytes, 0, (int)w.Length);
            w.Close();

            File.Delete(Path.GetTempPath() + "/temp.wav");
            File.Delete(Path.GetTempPath() + "/temp_ei.wav");

            return bytes;
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

        public byte[] GetRatedData(byte[] data, double rate, WaveFormat wf)
        {
            MemoryStream memRead = new MemoryStream(data);
            WaveFileReader reader = new WaveFileReader(memRead);
            int numChannels = wf.Channels;

            if (numChannels > 2)
                throw new Exception("SoundTouch supports only mono or stereo.");

            int sampleRate = wf.SampleRate;

            int bitPerSample = wf.BitsPerSample;

            const int BUFFER_SIZE = 1024 * 16;

            SoundStretcher stretcher = new SoundStretcher(sampleRate, numChannels);
            MemoryStream memWrite = new MemoryStream();
            WaveFileWriter writer = new WaveFileWriter(memWrite, new WaveFormat(sampleRate, 16, numChannels));

            stretcher.Tempo = (float)rate;

            byte[] buffer = new byte[BUFFER_SIZE];
            short[] buffer2 = null;

            if (bitPerSample != 16 && bitPerSample != 8)
            {
                throw new Exception("Not implemented yet.");
            }

            if (bitPerSample == 8)
            {
                buffer2 = new short[BUFFER_SIZE];
            }

            bool finished = false;

            while (true)
            {
                int bytesRead = 0;
                if (!finished)
                {
                    bytesRead = reader.Read(buffer, 0, BUFFER_SIZE);

                    if (bytesRead == 0)
                    {
                        finished = true;
                        stretcher.Flush();
                    }
                    else
                    {
                        if (bitPerSample == 16)
                        {
                            stretcher.PutSamplesFromBuffer(buffer, 0, bytesRead);
                        }
                        else if (bitPerSample == 8)
                        {
                            for (int i = 0; i < BUFFER_SIZE; i++)
                                buffer2[i] = (short)((buffer[i] - 128) * 256);
                            stretcher.PutSamples(buffer2);
                        }
                    }
                }
                bytesRead = stretcher.ReceiveSamplesToBuffer(buffer, 0, BUFFER_SIZE);
                writer.Write(buffer, 0, bytesRead);

                if (finished && bytesRead == 0)
                    break;
            }

            reader.Close();
            writer.Close();
            return memWrite.ToArray();
        }
    }
}
