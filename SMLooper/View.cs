﻿using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMLooper
{
    public partial class View : Form
    {
        Controller controller = new Controller();

        public View()
        {
            InitializeComponent();
            comboBoxMeasure.SelectedIndex = 0;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "sm files (*.sm)|*.sm";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            String filePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                fileLocationTextBox.Text = filePath;

                Chart.SimFileInfo smInfo = controller.ParseSmFile(filePath);

                groupBox4.Visible = true;
                if (smInfo.banner != "")
                    bannerPicBox.Image = Image.FromFile(smInfo.path + "//" + smInfo.banner);
                titleLabel.Text = smInfo.artist + " - " + smInfo.title;
                comboBox1.Items.Clear();
                foreach (Chart.Chart element in smInfo.charts)
                {
                    comboBox1.Items.Add(element.diff);
                }
                comboBox1.SelectedIndex = 0;
            }
        }
        


        private void cutButton_Click(object sender, EventArgs e)
        {
            updateList(controller.Cut(leftRangeTextBox.Text.Replace('.',','), rightRangeTextBox.Text.Replace('.', ','), comboBoxMeasure.Text, comboBox1.SelectedIndex));
        }

        int countOfPanels = 0;

        private void updateList(Chart.ChartSlice slice)
        {
            Panel subPanel = new Panel();
            subPanel.Location = new System.Drawing.Point(12, 12 + (countOfPanels) * (40 + 12));
            subPanel.Size = new System.Drawing.Size(panel.Width - 44, 40);
            subPanel.BackColor = Color.LightGray;

            WaveViewer waveViewer = new WaveViewer();
            waveViewer.Width = subPanel.Width / 2;
            waveViewer.Height = subPanel.Height - 4;
            waveViewer.Location = new System.Drawing.Point(2, 2);
            waveViewer.SamplesPerPixel = slice.rawData.Length / waveViewer.Width;
            waveViewer.WaveStream = new NAudio.Wave.Mp3FileReader(new MemoryStream(slice.rawData));
            waveViewer.BackColor = Color.White;

            TextBox textBox = new TextBox();
            textBox.Width = subPanel.Width / 4;
            textBox.Height = subPanel.Height / 2 - 4;
            textBox.Location = new System.Drawing.Point(waveViewer.Width + 8, (subPanel.Height - textBox.Height) / 2);
            textBox.Text = String.Format("{0:0.00}", slice.rate);
            textBox.TextAlign = HorizontalAlignment.Right;

            Label label = new Label();
            label.Width = 12;
            label.Height = 12;
            label.Text = "X";
            label.BackColor = Color.White;
            label.Location = new Point(subPanel.Width - 14, 2);
            label.Click += new EventHandler(LB_Click);

            Label label1 = new Label();
            label1.Width = 12;
            label1.Height = 12;
            label1.Text = "X";
            label1.Location = new System.Drawing.Point(waveViewer.Width + textBox.Width + 8, (subPanel.Height - textBox.Height) / 2+4);

            subPanel.Controls.Add(waveViewer);
            subPanel.Controls.Add(textBox);
            subPanel.Controls.Add(label);
            subPanel.Controls.Add(label1);
            subPanel.Name = "subPanel" + slice.GetHashCode();
            
            panel.Controls.Add(subPanel);

            UpdateMainWaveform();

            countOfPanels++;
        }

        private void UpdateMainWaveform()
        {
            List<Chart.ChartSlice> chs = controller.GetChartSlices();
            byte[] rawData = chs[0].rawData;
            for(int i = 1; i < chs.Count(); i++)
            {
                byte[] newArray = new byte[rawData.Length + chs[i].rawData.Length];
                Array.Copy(rawData, newArray, rawData.Length);
                Array.Copy(chs[i].rawData, 0, newArray, rawData.Length, chs[i].rawData.Length);
                rawData = newArray;
            }

            mainWaveViewer.SamplesPerPixel = rawData.Length / mainWaveViewer.Width;
            mainWaveViewer.WaveStream = new NAudio.Wave.Mp3FileReader(new MemoryStream(rawData));
        }

        protected void LB_Click(object sender, EventArgs e)
        {
            Label lbl = sender as Label;
            Panel pnl = (Panel)lbl.Parent;
            string hash = pnl.Name.TrimStart("subPanel".ToCharArray());
            controller.RemoveSliceByHash(int.Parse(hash));
            pnl.Enabled = false;

            UpdateMainWaveform();
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            String filePath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = folderBrowserDialog.SelectedPath;
                controller.Save(filePath, easyInChBox.Checked);
            }
        }

    }
}
