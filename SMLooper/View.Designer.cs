﻿namespace SMLooper
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
            this.easyInChBox = new System.Windows.Forms.CheckBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBoxMeasure = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rightRangeTextBox = new System.Windows.Forms.TextBox();
            this.leftRangeTextBox = new System.Windows.Forms.TextBox();
            this.cutButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.bannerPicBox = new System.Windows.Forms.PictureBox();
            this.mainWaveViewer = new NAudio.Gui.WaveViewer();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicBox)).BeginInit();
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
            this.groupBox1.Controls.Add(this.clearButton);
            this.groupBox1.Controls.Add(this.easyInChBox);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Controls.Add(this.fileLocationTextBox);
            this.groupBox1.Controls.Add(this.openFileButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 344);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 94);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // easyInChBox
            // 
            this.easyInChBox.AutoSize = true;
            this.easyInChBox.Location = new System.Drawing.Point(91, 64);
            this.easyInChBox.Name = "easyInChBox";
            this.easyInChBox.Size = new System.Drawing.Size(107, 17);
            this.easyInChBox.TabIndex = 3;
            this.easyInChBox.Text = "Add short easy-in";
            this.easyInChBox.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(204, 60);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxMeasure);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rightRangeTextBox);
            this.groupBox2.Controls.Add(this.leftRangeTextBox);
            this.groupBox2.Controls.Add(this.cutButton);
            this.groupBox2.Location = new System.Drawing.Point(316, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 94);
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
            // cutButton
            // 
            this.cutButton.Location = new System.Drawing.Point(391, 60);
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(75, 23);
            this.cutButton.TabIndex = 0;
            this.cutButton.Text = "Cut";
            this.cutButton.UseVisualStyleBackColor = true;
            this.cutButton.Click += new System.EventHandler(this.cutButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panel);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 326);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // panel
            // 
            this.panel.AutoScroll = true;
            this.panel.BackColor = System.Drawing.SystemColors.Window;
            this.panel.Location = new System.Drawing.Point(6, 10);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(286, 310);
            this.panel.TabIndex = 7;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.comboBox1);
            this.groupBox4.Controls.Add(this.titleLabel);
            this.groupBox4.Controls.Add(this.bannerPicBox);
            this.groupBox4.Location = new System.Drawing.Point(316, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(472, 176);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(345, 144);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.SystemColors.Window;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.titleLabel.Location = new System.Drawing.Point(6, 144);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(51, 20);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "label4";
            // 
            // bannerPicBox
            // 
            this.bannerPicBox.Location = new System.Drawing.Point(6, 10);
            this.bannerPicBox.Name = "bannerPicBox";
            this.bannerPicBox.Size = new System.Drawing.Size(460, 125);
            this.bannerPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bannerPicBox.TabIndex = 0;
            this.bannerPicBox.TabStop = false;
            // 
            // mainWaveViewer
            // 
            this.mainWaveViewer.AutoScroll = true;
            this.mainWaveViewer.BackColor = System.Drawing.Color.LightGray;
            this.mainWaveViewer.Location = new System.Drawing.Point(316, 194);
            this.mainWaveViewer.Name = "mainWaveViewer";
            this.mainWaveViewer.SamplesPerPixel = 128;
            this.mainWaveViewer.Size = new System.Drawing.Size(472, 120);
            this.mainWaveViewer.StartPosition = ((long)(0));
            this.mainWaveViewer.TabIndex = 7;
            this.mainWaveViewer.WaveStream = null;
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.BackColor = System.Drawing.SystemColors.Window;
            this.lengthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lengthLabel.Location = new System.Drawing.Point(318, 318);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(51, 20);
            this.lengthLabel.TabIndex = 3;
            this.lengthLabel.Text = "label5";
            this.lengthLabel.Visible = false;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(6, 60);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // View
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.mainWaveViewer);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "View";
            this.ShowIcon = false;
            this.Text = "Stepmania chart looper";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.View_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.View_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bannerPicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button cutButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox bannerPicBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel;
        private NAudio.Gui.WaveViewer mainWaveViewer;
        private System.Windows.Forms.CheckBox easyInChBox;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Button clearButton;
    }
}

