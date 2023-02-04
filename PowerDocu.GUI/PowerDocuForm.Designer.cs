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
            this.ClientSize = new System.Drawing.Size(convertToDPISpecific(1000), convertToDPISpecific(600));
            this.MinimumSize = new Size(convertToDPISpecific(600), convertToDPISpecific(500));
            this.SizeChanged += new EventHandler(SizeChangedHandler);
            this.DpiChanged += new DpiChangedEventHandler(DpiChangedHandler);
            this.Text = "PowerDocu GUI (" + PowerDocuReleaseHelper.currentVersion.ToString() + ")";
            this.Paint += new PaintEventHandler(PowerDocuForm_Paint);

            InitializePanels();
        }

        private void PowerDocuForm_Paint(object sender, PaintEventArgs e)
        {
            UpdateNavigation();
        }

        private void InitializePanels()
        {
            leftPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(convertToDPISpecific(150), convertToDPISpecific(300))
            };
            step1Panel = new Panel()
            {
                Location = new Point(leftPanel.Width, 0),
                Size = new Size(ClientSize.Width - leftPanel.Width, convertToDPISpecific(380))
            };
            step2Panel = new Panel()
            {
                Location = new Point(leftPanel.Width, 0),
                Size = new Size(ClientSize.Width - leftPanel.Width, convertToDPISpecific(380)),
                Visible = false
            };
            Controls.Add(leftPanel);
            Controls.Add(step1Panel);
            Controls.Add(step2Panel);
            backLeftPanelButton = new Button()
            {
                Text = "Back",
                Size = new Size(convertToDPISpecific(50), convertToDPISpecific(30)),
                BackColor = Color.LightGray,
                ForeColor = Color.White,
                Location = new Point(convertToDPISpecific(20), convertToDPISpecific(110)),
                Enabled = false
            };
            backLeftPanelButton.Click += new EventHandler(BackNextButton_Click);
            leftPanel.Controls.Add(backLeftPanelButton);
            nextLeftPanelButton = new Button()
            {
                Text = "Next",
                Size = new Size(convertToDPISpecific(50), convertToDPISpecific(30)),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Location = new Point(convertToDPISpecific(80), convertToDPISpecific(110)),
            };
            nextLeftPanelButton.Click += new EventHandler(BackNextButton_Click);
            leftPanel.Controls.Add(nextLeftPanelButton);
            settingsLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(40), convertToDPISpecific(19)),
                Text = "Settings",
                Height = convertToDPISpecific(25)
            };
            leftPanel.Controls.Add(settingsLabel);
            documentLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(40), convertToDPISpecific(79)),
                Text = "Generate Documentation",
                 Height = convertToDPISpecific(20)
            };
            leftPanel.Controls.Add(documentLabel);
            UpdateNavigation();

            //Step 1 - select output format
            outputFormatGroup = new GroupBox()
            {
                Text = "Output Selection",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(15)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(115)),
                AutoSize = false
            };
            step1Panel.Controls.Add(outputFormatGroup);
            openWordTemplateDialog = new OpenFileDialog()
            {
                FileName = "",
                Filter = "Word Documents (*.docx, *.docm, *.dotx)|*.docx;*.docm;*.dotx",
                Title = "Select the Word document to use as template"
            };
            outputFormatComboBox = new ComboBox()
            {
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),// + selectWordTemplateButton.Height),
                Size = new System.Drawing.Size(convertToDPISpecific(85), convertToDPISpecific(21)),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            outputFormatComboBox.Items.AddRange(new object[] {OutputFormatHelper.Word,
                        OutputFormatHelper.Markdown,
                        OutputFormatHelper.All
                        });
            outputFormatComboBox.SelectedIndexChanged += new EventHandler(OutputFormatComboBox_Changed);
            outputFormatComboBox.SelectedIndex = 0;
            outputFormatGroup.Controls.Add(outputFormatComboBox);
            outputFormatInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30) + outputFormatComboBox.Width, outputFormatComboBox.Location.Y + convertToDPISpecific(5)),
                Text = "Select output format",
                Width = convertToDPISpecific(150),
                Height = convertToDPISpecific(30)
            };
            outputFormatGroup.Controls.Add(outputFormatInfoLabel);
            selectWordTemplateButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(35) + outputFormatComboBox.Height),
                IconChar = IconChar.FileWord,
                IconColor = Color.Blue,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleCenter
            };
            selectWordTemplateButton.Click += new EventHandler(SelectWordTemplateButton_Click);
            outputFormatGroup.Controls.Add(selectWordTemplateButton);
            wordTemplateInfoLabel = new Label()
            {
                Location = new Point(30 + selectWordTemplateButton.Width, convertToDPISpecific(45) + outputFormatComboBox.Height),
                Text = "Optional: Select a Word template",
                Width = convertToDPISpecific(200),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true
            };
            outputFormatGroup.Controls.Add(wordTemplateInfoLabel);
            documentationOptionsGroup = new GroupBox()
            {
                Text = "Documentation Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), outputFormatGroup.Height + convertToDPISpecific(25)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(105)),
                AutoSize = false
            };
            step1Panel.Controls.Add(documentationOptionsGroup);
            documentChangesOnlyRadioButton =  new RadioButton() {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Canvas Apps: Document changes only",
                Checked = true,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(15))
            };
            documentationOptionsGroup.Controls.Add(documentChangesOnlyRadioButton);
             documentEverythingRadioButton =  new RadioButton() {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Canvas Apps: Document all properties",
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), 10+documentChangesOnlyRadioButton.Height)
            };
            documentationOptionsGroup.Controls.Add(documentEverythingRadioButton);
            documentDefaultsCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Canvas Apps: Document default values",
                Checked = true,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), documentEverythingRadioButton.Location.Y + documentEverythingRadioButton.Height)
            };
            documentationOptionsGroup.Controls.Add(documentDefaultsCheckBox);
            otherOptionsGroup = new GroupBox()
            {
                Text = "Other Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), outputFormatGroup.Height + documentationOptionsGroup.Height + convertToDPISpecific(35)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(120)),
                AutoSize = false
            };
            step1Panel.Controls.Add(otherOptionsGroup);
            updateConnectorIconsButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(20)),
                IconChar = IconChar.CloudDownloadAlt,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                Visible = true
            };
            updateConnectorIconsButton.Click += new EventHandler(UpdateConnectorIconsButton_Click);
            otherOptionsGroup.Controls.Add(updateConnectorIconsButton);
            updateConnectorIconsLabel = new Label()
            {
                Location = new Point(updateConnectorIconsButton.Location.X + updateConnectorIconsButton.Width + convertToDPISpecific(10), updateConnectorIconsButton.Location.Y + convertToDPISpecific(10)),
                Text = "Update your existing set of connector icons\n(" + ConnectorHelper.numberOfConnectors() + " connectors, " + ConnectorHelper.numberOfConnectorIcons() + " icons)",
                Width = convertToDPISpecific(200),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true
            };
            otherOptionsGroup.Controls.Add(updateConnectorIconsLabel);
            newReleaseButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(30) + updateConnectorIconsButton.Height),
                IconChar = IconChar.PlusCircle,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                Visible = false
            };
            newReleaseButton.Click += new EventHandler(NewReleaseButton_Click);
            otherOptionsGroup.Controls.Add(newReleaseButton);
            newReleaseLabel = new Label()
            {
                Location = new Point(newReleaseButton.Location.X + newReleaseButton.Width + 10, newReleaseButton.Location.Y + 10),
                Text = "Download new release: ",
                Width = convertToDPISpecific(200),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true,
                Visible = false
            };
            otherOptionsGroup.Controls.Add(newReleaseLabel);

            //Step 2 - Process file
            openFileToParseDialog = new OpenFileDialog()
            {
                FileName = "*.zip;*.msapp",
                Filter = "Parseable files (*.zip,*.msapp) |*.zip;*.msapp|Solutions, Flow ZIP files (*.zip)|*.zip|Power Apps files (*.msapp)|*.msapp",
                Title = "Open exported Solution, Flow ZIP or Power Apps MSAPP file",
                Multiselect = true
            };
            selectFileToParseButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                IconChar = IconChar.FileArchive,
                IconColor = Color.Orange,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,

            };
            selectFileToParseButton.Click += new EventHandler(SelectZIPFileButton_Click);
            step2Panel.Controls.Add(selectFileToParseButton);
            fileToParseInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30) + selectFileToParseButton.Width, convertToDPISpecific(25)),
                Text = "Select App, Flow, or Solution to document. Multiple items can be selected as well via Ctrl + Left Click. Upon selecting 'Open' in the dialog, documentation will be generated for all selected items.",
                Width = convertToDPISpecific(350),
                Height = convertToDPISpecific(60)
            };
            step2Panel.Controls.Add(fileToParseInfoLabel);
            settingsTitleLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), selectFileToParseButton.Location.Y + selectFileToParseButton.Height + convertToDPISpecific(35)),
                Text = "Settings:",
                //Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Width = convertToDPISpecific(400),
                Height = convertToDPISpecific(15)
            };
            step2Panel.Controls.Add(settingsTitleLabel);
            settingsDetailsLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), settingsTitleLabel.Location.Y + settingsTitleLabel.Height + convertToDPISpecific(10)),
                Text = "",
                Width = convertToDPISpecific(400),
                Height = convertToDPISpecific(200)
            };
            step2Panel.Controls.Add(settingsDetailsLabel);

            //status box
            appStatusTextBox = new TextBox
            {
                Size = new Size(ClientSize.Width - convertToDPISpecific(30), ClientSize.Height - convertToDPISpecific(405)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(395)),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            Controls.Add(appStatusTextBox);
        }

        private void UpdateNavigation()
        {
            Graphics g = leftPanel.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
            SolidBrush grayBrush = new SolidBrush(Color.LightGray);
            g.FillEllipse(nextLeftPanelButton.Enabled ? blueBrush : grayBrush, convertToDPISpecific(20), convertToDPISpecific(20), convertToDPISpecific(15), convertToDPISpecific(15));
            g.FillRectangle(grayBrush, convertToDPISpecific(24), convertToDPISpecific(37), convertToDPISpecific(3), convertToDPISpecific(40));
            g.FillEllipse(backLeftPanelButton.Enabled ? blueBrush : grayBrush, convertToDPISpecific(20), convertToDPISpecific(80), convertToDPISpecific(15), convertToDPISpecific(15));
        }

        private int convertToDPISpecific(int number)
        {
            //96 DPI is the default
            return (int)number * this.DeviceDpi / 96;
        }

        private IconButton selectFileToParseButton, selectWordTemplateButton, newReleaseButton, updateConnectorIconsButton;
        private OpenFileDialog openFileToParseDialog, openWordTemplateDialog;
        private TextBox appStatusTextBox;
        private ComboBox outputFormatComboBox;
        private GroupBox outputFormatGroup, documentationOptionsGroup, otherOptionsGroup;
        private CheckBox documentDefaultsCheckBox;
        private RadioButton documentChangesOnlyRadioButton, documentEverythingRadioButton;
        private Panel leftPanel, step1Panel, step2Panel;
        private Button nextLeftPanelButton, backLeftPanelButton;
        private Label wordTemplateInfoLabel, fileToParseInfoLabel, outputFormatInfoLabel, settingsLabel, documentLabel, newReleaseLabel, updateConnectorIconsLabel, settingsTitleLabel, settingsDetailsLabel;

        #endregion
    }
}

