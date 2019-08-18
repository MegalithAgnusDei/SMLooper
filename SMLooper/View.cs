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
                    pictureBox1.Image = Image.FromFile(smInfo.path + "//" + smInfo.banner);
                label4.Text = smInfo.artist + " - " + smInfo.title;
                foreach (Chart.Chart element in smInfo.charts)
                {
                    comboBox1.Items.Add(element.diff);
                }
                comboBox1.SelectedIndex = 0;
            }
        }
        


        private void cutButton_Click(object sender, EventArgs e)
        {
            updateList(controller.Cut(leftRangeTextBox.Text, rightRangeTextBox.Text, comboBoxMeasure.Text, 0));
        }

        private void updateList(Chart.ChartSlice slice)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = Convert.ToString(slice.rate);

            listView.Items.Add(lvi);
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            String filePath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = folderBrowserDialog.SelectedPath;
                controller.Save(filePath);
            }
        }

    }
}
