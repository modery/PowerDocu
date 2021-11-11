using System;
using System.Drawing;
using System.Windows.Forms;

namespace PowerDocu.GUI
{
    partial class PowerDocuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 380);
            this.SizeChanged += new EventHandler(sizeChanged);
            this.MinimumSize = new Size(500, 250);
            this.Text = "PowerDocu GUI";
            openFileToParseDialog = new OpenFileDialog()
            {
                FileName = "*.zip",
                Filter = "ZIP files (*.zip)|*.zip",
                Title = "Open exported Flow ZIP file"
            };
            openWordTemplateDialog = new OpenFileDialog()
            {
                FileName = "",
                Filter = "Word Documents (*.docx)|*.docx",
                Title = "Select the Word document to use as template"
            };
            selectWordTemplateButton = new Button()
            {
                //this should properly size the button so that the Text is shown correctly
                Size = new Size((int)(300 * this.DeviceDpi / 96), (int)(30 * this.DeviceDpi / 96)),
                Location = new Point(15, 15),
                Text = "Optional: Select Word document to use as Template"
            };
            selectFileToParseButton = new Button()
            {
                //this should properly size the button so that the Text is shown correctly
                Size = new Size((int)(210 * this.DeviceDpi / 96), (int)(30 * this.DeviceDpi / 96)),
                Location = new Point(15, 15 + selectWordTemplateButton.Height),
                Text = "Select Flow or Solution to document",
            };
            selectFileToParseButton.Click += new EventHandler(selectZIPFileButton_Click);
            selectWordTemplateButton.Click += new EventHandler(selectWordTemplateButton_Click);
            Controls.Add(selectFileToParseButton);
            Controls.Add(selectWordTemplateButton);
            wordTemplateInfoLabel = new Label()
            {
                Location = new Point(30 + selectWordTemplateButton.Width, 25),
                Text = "No template selected",
                Width = 300
            };
            Controls.Add(wordTemplateInfoLabel);
            appStatusTextBox = new TextBox
            {
                Size = new Size(ClientSize.Width - 30, ClientSize.Height - selectFileToParseButton.Height - selectWordTemplateButton.Height - 40),
                Location = new Point(15, 30 + selectFileToParseButton.Height + selectWordTemplateButton.Height),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            Controls.Add(appStatusTextBox);
        }



        private Button selectFileToParseButton;
        private Button selectWordTemplateButton;
        private OpenFileDialog openFileToParseDialog;
        private OpenFileDialog openWordTemplateDialog;
        private TextBox appStatusTextBox;
        private Label wordTemplateInfoLabel;

        #endregion
    }
}

