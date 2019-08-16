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

                controller.ParseSmFile(filePath);
            }
        }

    }
}
