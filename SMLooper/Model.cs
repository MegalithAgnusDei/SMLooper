﻿using SMLooper.Chart;
using System;
using System.IO;
using System.Collections;
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
                        BPM bpm = new BPM();
                        string[] bpof;
                        bpof = words[1].Split('=');
                        bpm.measure = Convert.ToDouble(bpof[0].Replace('.', ','));
                        bpm.bpm = Convert.ToDouble(bpof[1].Trim(';').Replace('.', ','));
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
    }
}
