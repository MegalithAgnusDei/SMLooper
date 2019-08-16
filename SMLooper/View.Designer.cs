namespace SMLooper
{
    partial class View
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openFileButton = new System.Windows.Forms.Button();
            this.fileLocationTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxMeasure = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rightRangeTextBox = new System.Windows.Forms.TextBox();
            this.leftRangeTextBox = new System.Windows.Forms.TextBox();
            this.start = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // openFileButton
            // 
            this.openFileButton.Location = new System.Drawing.Point(204, 17);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.Text = "Open";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // fileLocationTextBox
            // 
            this.fileLocationTextBox.Location = new System.Drawing.Point(6, 19);
            this.fileLocationTextBox.Name = "fileLocationTextBox";
            this.fileLocationTextBox.Size = new System.Drawing.Size(192, 20);
            this.fileLocationTextBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fileLocationTextBox);
            this.groupBox1.Controls.Add(this.openFileButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 114);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxMeasure);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rightRangeTextBox);
            this.groupBox2.Controls.Add(this.leftRangeTextBox);
            this.groupBox2.Controls.Add(this.start);
            this.groupBox2.Location = new System.Drawing.Point(316, 324);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 114);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // comboBoxMeasure
            // 
            this.comboBoxMeasure.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMeasure.FormattingEnabled = true;
            this.comboBoxMeasure.Items.AddRange(new object[] {
            "Measures"});
            this.comboBoxMeasure.Location = new System.Drawing.Point(167, 41);
            this.comboBoxMeasure.Name = "comboBoxMeasure";
            this.comboBoxMeasure.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMeasure.TabIndex = 6;
            this.comboBoxMeasure.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(146, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "in";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select section:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "-";
            // 
            // rightRangeTextBox
            // 
            this.rightRangeTextBox.Location = new System.Drawing.Point(84, 42);
            this.rightRangeTextBox.Name = "rightRangeTextBox";
            this.rightRangeTextBox.Size = new System.Drawing.Size(56, 20);
            this.rightRangeTextBox.TabIndex = 2;
            // 
            // leftRangeTextBox
            // 
            this.leftRangeTextBox.Location = new System.Drawing.Point(6, 43);
            this.leftRangeTextBox.Name = "leftRangeTextBox";
            this.leftRangeTextBox.Size = new System.Drawing.Size(56, 20);
            this.leftRangeTextBox.TabIndex = 1;
            // 
            // start
            // 
            this.start.Location = new System.Drawing.Point(391, 85);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 0;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "View";
            this.ShowIcon = false;
            this.Text = "Stepmania chart looper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.TextBox fileLocationTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comboBoxMeasure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox rightRangeTextBox;
        private System.Windows.Forms.TextBox leftRangeTextBox;
        private System.Windows.Forms.Button start;
    }
}

