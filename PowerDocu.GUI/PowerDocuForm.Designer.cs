using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;
using PowerDocu.Common;

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
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.MinimumSize = new Size(convertToDPISpecific(800), convertToDPISpecific(250));
            this.SizeChanged += new EventHandler(sizeChanged);
            this.Text = "PowerDocu GUI (" + PowerDocuReleaseHelper.currentVersion.ToString() + ")";
            openFileToParseDialog = new OpenFileDialog()
            {
                FileName = "*.zip;*.msapp",
                Filter = "Parseable files (*.zip,*.msapp) |*.zip;*.msapp|Flow ZIP files (*.zip)|*.zip|Power Apps files (*.msapp)|*.msapp",
                Title = "Open exported Flow ZIP or Power Apps MSAPP file",
                Multiselect = true
            };
            openWordTemplateDialog = new OpenFileDialog()
            {
                FileName = "",
                Filter = "Word Documents (*.docx)|*.docx",
                Title = "Select the Word document to use as template"
            };
            selectWordTemplateButton = new IconButton()
            {
                //this should properly size the button so that the Text is shown correctly
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(15, 15),
                //Text = "Optional: Select Word document to use as Template",
                IconChar = IconChar.FileWord,
                IconColor = Color.Blue,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleCenter
            };
            outputFormatComboBox = new ComboBox()
            {
                Location = new Point(15, 25 + selectWordTemplateButton.Height),
                Size = new System.Drawing.Size(85, 21),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            outputFormatComboBox.Items.AddRange(new object[] {OutputFormatHelper.Word,
                        OutputFormatHelper.Markdown,
                        OutputFormatHelper.All
                        });
            outputFormatComboBox.SelectedIndex = 0;
            Controls.Add(outputFormatComboBox);

            selectFileToParseButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(15, 40 + selectWordTemplateButton.Height + outputFormatComboBox.Height),
                IconChar = IconChar.FileArchive,
                IconColor = Color.Orange,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,

            };
            selectFileToParseButton.Click += new EventHandler(selectZIPFileButton_Click);
            selectWordTemplateButton.Click += new EventHandler(selectWordTemplateButton_Click);
            Controls.Add(selectFileToParseButton);
            Controls.Add(selectWordTemplateButton);

            wordTemplateInfoLabel = new Label()
            {
                Location = new Point(30 + selectWordTemplateButton.Width, 25),
                Text = "Optional: Select a Word template",
                Width = convertToDPISpecific(300),
                Height = convertToDPISpecific(30)
            };
            Controls.Add(wordTemplateInfoLabel);


            fileToParseInfoLabel = new Label()
            {
                Location = new Point(30 + selectFileToParseButton.Width, 50 + selectWordTemplateButton.Height + outputFormatComboBox.Height),
                Text = "Select App, Flow, or Solution to document",
                Width = convertToDPISpecific(300),
                Height = convertToDPISpecific(30)
            };
            Controls.Add(fileToParseInfoLabel);
            outputFormatInfoLabel = new Label()
            {
                Location = new Point(30 + outputFormatComboBox.Width, outputFormatComboBox.Location.Y + 5),
                Text = "Select output file format",
                Width = convertToDPISpecific(300),
                Height = convertToDPISpecific(30)
            };
            Controls.Add(outputFormatInfoLabel);

            appStatusTextBox = new TextBox
            {
                Size = new Size(ClientSize.Width - 30, ClientSize.Height - selectFileToParseButton.Height - selectWordTemplateButton.Height - outputFormatComboBox.Height - 65),
                Location = new Point(15, 45 + selectFileToParseButton.Height + selectWordTemplateButton.Height + outputFormatComboBox.Height),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            Controls.Add(appStatusTextBox);

            updateConnectorIconsButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(ClientSize.Width - 80, 15),
                IconChar = IconChar.CloudDownloadAlt,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                Visible = true
            };
            updateConnectorIconsButton.Click += new EventHandler(updateConnectorIconsButton_Click);
            Controls.Add(updateConnectorIconsButton);

            newReleaseButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(ClientSize.Width - 80, 15 + updateConnectorIconsButton.Height),
                IconChar = IconChar.PlusCircle,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                Visible = false
            };
            newReleaseButton.Click += new EventHandler(newReleaseButton_Click);
            Controls.Add(newReleaseButton);
        }

        private int convertToDPISpecific(int number)
        {
            //96 DPI is the default
            return (int)number * this.DeviceDpi / 96;
        }
        private IconButton selectFileToParseButton;
        private IconButton selectWordTemplateButton;
        private IconButton newReleaseButton;
        private IconButton updateConnectorIconsButton;
        private OpenFileDialog openFileToParseDialog;
        private OpenFileDialog openWordTemplateDialog;
        private TextBox appStatusTextBox;
        private Label wordTemplateInfoLabel;
        private Label fileToParseInfoLabel;
        private Label outputFormatInfoLabel;
        private ComboBox outputFormatComboBox;

        #endregion
    }
}

