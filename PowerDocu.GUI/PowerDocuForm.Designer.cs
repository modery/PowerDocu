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
            InitializePanels();
        }

        private void InitializePanels()
        {
            statusIconPictureBox = new PictureBox()
            {
                Location = new Point(convertToDPISpecific(5), convertToDPISpecific(15)),
                Size = new Size(convertToDPISpecific(16), convertToDPISpecific(16)),
                Image = FontAwesome.Sharp.IconChar.InfoCircle.ToBitmap(Color.Green, convertToDPISpecific(16)),
            };
            Controls.Add(statusIconPictureBox);
            statusLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(25), convertToDPISpecific(15)),
                Text = "Welcome to PowerDocu",
                Width = convertToDPISpecific(ClientSize.Width - convertToDPISpecific(40)),
                Height = convertToDPISpecific(convertToDPISpecific(20))
            };
            Controls.Add(statusLabel);
            ImageList imageList1 = new ImageList();
            imageList1.Images.Add(FontAwesome.Sharp.IconChar.FileWord.ToBitmap(Color.Purple));
            imageList1.Images.Add(FontAwesome.Sharp.IconChar.Gear.ToBitmap(Color.Gray));
            imageList1.Images.Add(FontAwesome.Sharp.IconChar.Scroll.ToBitmap(Color.DarkOrange));
            dynamicTabControl = new TabControl()
            {
                Name = "DynamicTabControl",
                Dock = DockStyle.Bottom,
                ImageList = imageList1,
                Width = this.ClientSize.Width,
                Height = this.ClientSize.Height - convertToDPISpecific(50)
            };
            Controls.Add(dynamicTabControl);


            dynamicTabControl.TabPages.Add(createGenerateDocumentationTabPage());
            dynamicTabControl.TabPages.Add(createSettingsTabPage());
            // Add TabPage2  

            TabPage tabPage3 = new TabPage()
            {
                Name = "tabPage3",
                Text = "Log",
                ImageIndex = 2,
                AutoScroll = true
            };
            dynamicTabControl.TabPages.Add(tabPage3);


            //status box
            appStatusTextBox = new TextBox
            {
                Size = new Size(ClientSize.Width - convertToDPISpecific(40), ClientSize.Height - convertToDPISpecific(100)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(15)),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            tabPage3.Controls.Add(appStatusTextBox);
        }

        private TabPage createSettingsTabPage()
        {
            TabPage tabPage = new TabPage()
            {
                Name = "tabPage2",
                Text = "Settings",
                ImageIndex = 1,
                AutoScroll = true
            };
            settingsPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(convertToDPISpecific(450), convertToDPISpecific(600))

            };


            tabPage.Controls.Add(settingsPanel);

            //Tab 2 - Settings 

            outputFormatGroup = new GroupBox()
            {
                Text = "Output Selection",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(15)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(115)),
                AutoSize = true
            };
            settingsPanel.Controls.Add(outputFormatGroup);
            outputFormatInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                Text = "Output format:",
                Width = convertToDPISpecific(90),
                Height = convertToDPISpecific(30)
            };
            outputFormatGroup.Controls.Add(outputFormatInfoLabel);
            openWordTemplateDialog = new OpenFileDialog()
            {
                FileName = "",
                Filter = "Word Documents (*.docx, *.docm, *.dotx)|*.docx;*.docm;*.dotx",
                Title = "Select the Word document to use as template"
            };
            outputFormatComboBox = new ComboBox()
            {
                Location = new Point(convertToDPISpecific(15) + outputFormatInfoLabel.Width + outputFormatInfoLabel.Location.Y, convertToDPISpecific(25)),// + selectWordTemplateButton.Height),
                Size = new System.Drawing.Size(convertToDPISpecific(85), convertToDPISpecific(21)),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            outputFormatComboBox.Items.AddRange(new object[] {OutputFormatHelper.All,
                                                                OutputFormatHelper.Word,
                                                                OutputFormatHelper.Markdown
                        });
            outputFormatComboBox.SelectedIndexChanged += new EventHandler(OutputFormatComboBox_Changed);
            outputFormatComboBox.SelectedIndex = 0;
            outputFormatGroup.Controls.Add(outputFormatComboBox);

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
                Location = new Point(convertToDPISpecific(10) + selectWordTemplateButton.Width + selectWordTemplateButton.Location.X, convertToDPISpecific(35) + outputFormatComboBox.Height),
                Text = "Optional: Select a Word template",
                Width = convertToDPISpecific(200),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true
            };
            outputFormatGroup.Controls.Add(wordTemplateInfoLabel);
            clearWordTemplateButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(24), convertToDPISpecific(24)),
                Location = new Point(35 + selectWordTemplateButton.Width, wordTemplateInfoLabel.Height + wordTemplateInfoLabel.Location.Y),
                IconChar = IconChar.Eraser,
                IconColor = Color.Red,
                IconSize = convertToDPISpecific(24),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            clearWordTemplateButton.Click += new EventHandler(ClearWordTemplateButton_Click);
            outputFormatGroup.Controls.Add(clearWordTemplateButton);
            documentationOptionsGroup = new GroupBox()
            {
                Text = "Documentation Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), outputFormatGroup.Height + outputFormatGroup.Location.Y + convertToDPISpecific(10)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(145)),
                AutoSize = true
            };
            settingsPanel.Controls.Add(documentationOptionsGroup);
            documentChangesOrEverythingLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                Text = "Canvas Apps: Document Changed Properties or All Properties",
                Width = convertToDPISpecific(350),
                Height = convertToDPISpecific(20)
            };
            documentationOptionsGroup.Controls.Add(documentChangesOrEverythingLabel);
            documentChangesOnlyRadioButton = new RadioButton()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Changes only",
                Checked = true,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(30), documentChangesOrEverythingLabel.Height + documentChangesOrEverythingLabel.Location.Y)
            };
            documentationOptionsGroup.Controls.Add(documentChangesOnlyRadioButton);
            documentEverythingRadioButton = new RadioButton()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "All Properties",
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(30), documentChangesOnlyRadioButton.Height + documentChangesOnlyRadioButton.Location.Y)
            };
            documentationOptionsGroup.Controls.Add(documentEverythingRadioButton);
            documentDefaultsCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Canvas Apps: Document default values of properties",
                Checked = true,
                Size = new Size(convertToDPISpecific(350), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), documentEverythingRadioButton.Location.Y + documentEverythingRadioButton.Height)
            };
            documentationOptionsGroup.Controls.Add(documentDefaultsCheckBox);
            documentSampleDataCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Canvas Apps: Document sample datasources",
                Checked = false,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), documentDefaultsCheckBox.Location.Y + documentDefaultsCheckBox.Height)
            };
            documentationOptionsGroup.Controls.Add(documentSampleDataCheckBox);
            flowActionSortOrderInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), documentSampleDataCheckBox.Location.Y + documentSampleDataCheckBox.Height + convertToDPISpecific(5)),
                Text = "Flows: Sort Flow Actions",
                Width = convertToDPISpecific(150),
                Height = convertToDPISpecific(30)
            };
            documentationOptionsGroup.Controls.Add(flowActionSortOrderInfoLabel);
            flowActionSortOrderComboBox = new ComboBox()
            {
                Location = new Point(convertToDPISpecific(30) + flowActionSortOrderInfoLabel.Width, flowActionSortOrderInfoLabel.Location.Y - convertToDPISpecific(5)),
                Size = new System.Drawing.Size(convertToDPISpecific(150), convertToDPISpecific(21)),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            flowActionSortOrderComboBox.Items.AddRange(new object[] { "By name", "By order of appearance" });
            flowActionSortOrderComboBox.SelectedIndex = 0;
            documentationOptionsGroup.Controls.Add(flowActionSortOrderComboBox);

            otherOptionsGroup = new GroupBox()
            {
                Text = "Other Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), documentationOptionsGroup.Height + documentationOptionsGroup.Location.Y + convertToDPISpecific(10)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(120)),
                AutoSize = true
            };
            settingsPanel.Controls.Add(otherOptionsGroup);
            saveConfigButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(20)),
                IconChar = IconChar.Save,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleCenter
            };
            saveConfigButton.Click += new EventHandler(SaveConfigButton_Click);
            otherOptionsGroup.Controls.Add(saveConfigButton);
            saveConfigLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(10) + saveConfigButton.Width + saveConfigButton.Location.X, convertToDPISpecific(25)),
                Text = "Save current configuration as default",
                Width = convertToDPISpecific(250),
                Height = convertToDPISpecific(30)
            };
            otherOptionsGroup.Controls.Add(saveConfigLabel);
            updateConnectorIconsButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), saveConfigButton.Height + saveConfigButton.Location.Y + convertToDPISpecific(10)),
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
                Location = new Point(updateConnectorIconsButton.Location.X + updateConnectorIconsButton.Width + convertToDPISpecific(10), updateConnectorIconsButton.Location.Y),
                Text = "Update your existing set of connector icons\n(" + ConnectorHelper.numberOfConnectors() + " connectors, " + ConnectorHelper.numberOfConnectorIcons() + " icons)",
                Width = convertToDPISpecific(250),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true
            };
            otherOptionsGroup.Controls.Add(updateConnectorIconsLabel);
            newReleaseButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(10) + updateConnectorIconsButton.Height + updateConnectorIconsButton.Location.Y),
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
                Location = new Point(newReleaseButton.Location.X + newReleaseButton.Width + convertToDPISpecific(10), newReleaseButton.Location.Y),
                Text = "Download new release: ",
                Width = convertToDPISpecific(200),
                Height = convertToDPISpecific(30),
                MaximumSize = new Size(Width, Height + convertToDPISpecific(100)),
                AutoSize = true,
                Visible = false
            };
            otherOptionsGroup.Controls.Add(newReleaseLabel);
            return tabPage;
        }

        private TabPage createGenerateDocumentationTabPage()
        {
            TabPage tabPage = new TabPage()
            {
                Name = "tabPage1",
                Text = "Create Documentation",
                ImageIndex = 0
            };

            generateDocuPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(ClientSize.Width - convertToDPISpecific(30), ClientSize.Height - convertToDPISpecific(25))
            };

            powerDocuInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                Text = "Welcome to PowerDocu!",
                Width = convertToDPISpecific(450),
                Height = convertToDPISpecific(60),
                AutoSize = true,
                Font= new Font(this.Font.FontFamily,1.5f*this.Font.Size)
            };
            generateDocuPanel.Controls.Add(powerDocuInfoLabel);

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
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25) + powerDocuInfoLabel.Location.Y + powerDocuInfoLabel.Height),
                IconChar = IconChar.FileArchive,
                IconColor = Color.Purple,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,

            };
            selectFileToParseButton.Click += new EventHandler(SelectZIPFileButton_Click);
            generateDocuPanel.Controls.Add(selectFileToParseButton);
            fileToParseInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30) + selectFileToParseButton.Width, convertToDPISpecific(25) + powerDocuInfoLabel.Location.Y + powerDocuInfoLabel.Height),
                Text = "Select Apps, Flows, or Solutions to document. Multiple items can be selected via Ctrl + Left Click.",
                Width = convertToDPISpecific(450),
                Height = convertToDPISpecific(60),
                AutoSize = true
            };
            generateDocuPanel.Controls.Add(fileToParseInfoLabel);
            startDocumentationButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), selectFileToParseButton.Location.Y + selectFileToParseButton.Height + convertToDPISpecific(25)),
                IconChar = IconChar.HourglassStart,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(32),
                IconFont = IconFont.Auto,
                Visible = false
            };
            startDocumentationButton.Click += new EventHandler(StartDocumentationButton_Click);
            generateDocuPanel.Controls.Add(startDocumentationButton);
            selectedFilesToDocumentLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30) + startDocumentationButton.Width, selectFileToParseButton.Location.Y + selectFileToParseButton.Height + convertToDPISpecific(25)),
                Text = "",
                //Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Width = convertToDPISpecific(400),
                Height = convertToDPISpecific(15),
                AutoSize = true
            };
            generateDocuPanel.Controls.Add(selectedFilesToDocumentLabel);
            tabPage.Controls.Add(generateDocuPanel);
            return tabPage;
        }

        private int convertToDPISpecific(int number)
        {
            //96 DPI is the default
            return (int)number * this.DeviceDpi / 96;
        }

        private IconButton selectFileToParseButton, selectWordTemplateButton, newReleaseButton, updateConnectorIconsButton, saveConfigButton, startDocumentationButton, clearWordTemplateButton;
        private OpenFileDialog openFileToParseDialog, openWordTemplateDialog;
        private TextBox appStatusTextBox;
        private ComboBox outputFormatComboBox, flowActionSortOrderComboBox;
        private GroupBox outputFormatGroup, documentationOptionsGroup, otherOptionsGroup;
        private CheckBox documentDefaultsCheckBox, documentSampleDataCheckBox;
        private RadioButton documentChangesOnlyRadioButton, documentEverythingRadioButton;
        private Label wordTemplateInfoLabel, fileToParseInfoLabel, outputFormatInfoLabel,
                        flowActionSortOrderInfoLabel, newReleaseLabel, updateConnectorIconsLabel,
                        selectedFilesToDocumentLabel, statusLabel, saveConfigLabel,
                        documentChangesOrEverythingLabel, powerDocuInfoLabel;
        private TabControl dynamicTabControl;
        private PictureBox statusIconPictureBox;
        private Panel settingsPanel, generateDocuPanel;

        #endregion
    }
}

