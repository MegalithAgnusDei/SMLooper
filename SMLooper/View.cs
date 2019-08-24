using NAudio.Gui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SMLooper
{
    public partial class View : Form
    {
        private int countOfPanels = 0;

        Controller controller = new Controller();

        public View()
        {
            InitializeComponent();
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
                controller.MakeWav();

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
            updateList(
                controller.Cut(leftRangeTextBox.Text.Replace('.',','), 
                rightRangeTextBox.Text.Replace('.', ','), 
                comboBoxMeasure.Text, 
                comboBox1.SelectedIndex)
                );
        }
        
        private void updateList(Chart.ChartSlice slice)
        {
            Panel subPanel = new Panel();
            subPanel.Location = new Point(12, 12 + (countOfPanels) * (40 + 12));
            subPanel.Size = new Size(panel.Width - 44, 40);
            subPanel.BackColor = Color.LightGray;

            WaveViewer waveViewer = new WaveViewer();
            waveViewer.Width = subPanel.Width / 2;
            waveViewer.Height = subPanel.Height - 4;
            waveViewer.Location = new Point(2, 2);
            waveViewer.SamplesPerPixel = slice.rawData.Length / waveViewer.Width /slice.wf.BlockAlign;
            waveViewer.WaveStream = new NAudio.Wave.WaveFileReader(new MemoryStream(slice.rawData));
            waveViewer.BackColor = Color.White;

            NumericUpDown numUpDown = new NumericUpDown();
            numUpDown.DecimalPlaces = 2;
            numUpDown.Increment = 0.01M;
            numUpDown.Minimum = 0.70M;
            numUpDown.Maximum = 3.00M;
            numUpDown.Width = subPanel.Width / 4;
            numUpDown.Height = subPanel.Height / 2 - 4;
            numUpDown.Location = new Point(waveViewer.Width + 8, (subPanel.Height - numUpDown.Height) / 2);
            numUpDown.Text = String.Format("{0:0.00}", slice.rate);
            numUpDown.TextAlign = HorizontalAlignment.Right;
            numUpDown.ValueChanged += new EventHandler(UpdateRate);

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
            label1.Location = new Point(waveViewer.Width + numUpDown.Width + 8, (subPanel.Height - numUpDown.Height) / 2+4);

            subPanel.Controls.Add(waveViewer);
            subPanel.Controls.Add(numUpDown);
            subPanel.Controls.Add(label);
            subPanel.Controls.Add(label1);
            subPanel.Name = "subPanel" + slice.GetHashCode();
            
            panel.Controls.Add(subPanel);

            UpdateMainWaveform();

            countOfPanels++;
        }

        private void UpdateRate(object sender, EventArgs e)
        {
            NumericUpDown nud = sender as NumericUpDown;
            Panel pnl = (Panel)nud.Parent;
            string hash = pnl.Name.TrimStart("subPanel".ToCharArray());
            controller.UpdateRateByHash(int.Parse(hash), (double)nud.Value);
        }

        private void UpdateMainWaveform()
        {
            List<Chart.ChartSlice> chs = controller.GetChartSlices();
            if(chs.Count() == 0)
            {
                mainWaveViewer.WaveStream = null;
                return;
            }
            byte[] rawData = chs[0].rawData;
            for(int i = 1; i < chs.Count(); i++)
            {
                byte[] newArray = new byte[rawData.Length + chs[i].rawData.Length-44];
                Array.Copy(rawData, newArray, rawData.Length);
                Array.Copy(chs[i].rawData, 44, newArray, rawData.Length, chs[i].rawData.Length-44);
                rawData = newArray;
            }
           
            mainWaveViewer.SamplesPerPixel = rawData.Length / mainWaveViewer.Width / chs[0].wf.BlockAlign;
            mainWaveViewer.WaveStream = new NAudio.Wave.RawSourceWaveStream(new MemoryStream(rawData),chs[0].wf);
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
                panel.Controls.Clear();
                countOfPanels = 0;
                controller.ClearChartSlicesList();
                UpdateMainWaveform();
                titleLabel.Text = "";
                comboBox1.Items.Clear();
                bannerPicBox.Image = null;
                fileLocationTextBox.Text = "";
                controller = new Controller();

                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

    }
}
