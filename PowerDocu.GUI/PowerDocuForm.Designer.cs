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
                Dock = DockStyle.Fill,
                AutoScroll = true
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
                                                                OutputFormatHelper.Markdown,
                                                                OutputFormatHelper.Html
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
            addTableOfContentsCheckBox = new CheckBox()
            {
                Location = new Point(convertToDPISpecific(15), clearWordTemplateButton.Location.Y + clearWordTemplateButton.Height + convertToDPISpecific(5)),
                Text = "Add Table of Contents (Word)",
                Width = convertToDPISpecific(250),
                Checked = false
            };
            outputFormatGroup.Controls.Add(addTableOfContentsCheckBox);
            documentationOptionsGroup = new GroupBox()
            {
                Text = "Documentation Options",
                Padding = new Padding(10),
                Location = new Point(outputFormatGroup.Location.X + outputFormatGroup.Width + convertToDPISpecific(15), convertToDPISpecific(15)),
                Size = new System.Drawing.Size(convertToDPISpecific(450), convertToDPISpecific(145)),
                AutoSize = false
            };
            settingsPanel.Controls.Add(documentationOptionsGroup);

            // Inner panel to make Documentation Options scrollable
            Panel docOptionsInnerPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            docOptionsInnerPanel.HorizontalScroll.Enabled = false;
            docOptionsInnerPanel.HorizontalScroll.Visible = false;
            docOptionsInnerPanel.HorizontalScroll.Maximum = 0;
            documentationOptionsGroup.Controls.Add(docOptionsInnerPanel);
            // Solutions Checkbox
            solutionCheckBox = new CheckBox()
            {
                Text = "Solution",
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(5)),
                Checked = true,
                AutoSize = true
            };
            docOptionsInnerPanel.Controls.Add(solutionCheckBox);

            // Solution: Document Default Columns Checkbox
            documentDefaultColumnsCheckBox = new CheckBox()
            {
                Text = "Document Dataverse table default columns",
                Checked = false,
                AutoSize = true,
                Location = new Point(convertToDPISpecific(30), solutionCheckBox.Location.Y + solutionCheckBox.Height + convertToDPISpecific(5))
            };
            docOptionsInnerPanel.Controls.Add(documentDefaultColumnsCheckBox);

            // Solution: Show All Components In Graph Checkbox
            showAllComponentsInGraphCheckBox = new CheckBox()
            {
                Text = "Show all components in graph",
                Checked = true,
                AutoSize = true,
                Location = Location = new Point(convertToDPISpecific(30), documentDefaultColumnsCheckBox.Location.Y + documentDefaultColumnsCheckBox.Height + convertToDPISpecific(5)),
                Visible = true
            };
            docOptionsInnerPanel.Controls.Add(showAllComponentsInGraphCheckBox);

            // Flows Checkbox
            flowsCheckBox = new CheckBox()
            {
                Text = "Cloud + Agent Flows",
                Location = new Point(convertToDPISpecific(15), showAllComponentsInGraphCheckBox.Location.Y + showAllComponentsInGraphCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            docOptionsInnerPanel.Controls.Add(flowsCheckBox);

            // Flows: Sort Flow Actions Label
            flowActionSortOrderInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30), flowsCheckBox.Location.Y + flowsCheckBox.Height + convertToDPISpecific(10)),
                Text = "Sort Flow Actions",
                Width = convertToDPISpecific(150),
                Height = convertToDPISpecific(30)
            };
            docOptionsInnerPanel.Controls.Add(flowActionSortOrderInfoLabel);

            // Flows: Sort Flow Actions ComboBox
            flowActionSortOrderComboBox = new ComboBox()
            {
                Location = new Point(convertToDPISpecific(30) + flowActionSortOrderInfoLabel.Width, flowActionSortOrderInfoLabel.Location.Y - convertToDPISpecific(5)),
                Size = new System.Drawing.Size(convertToDPISpecific(150), convertToDPISpecific(21)),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            flowActionSortOrderComboBox.Items.AddRange(new object[] { "By name", "By order of appearance" });
            flowActionSortOrderComboBox.SelectedIndex = 0;
            docOptionsInnerPanel.Controls.Add(flowActionSortOrderComboBox);

            // Agents Checkbox
            agentsCheckBox = new CheckBox()
            {
                Text = "Agents",
                Location = new Point(convertToDPISpecific(15), flowActionSortOrderComboBox.Location.Y + flowActionSortOrderComboBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true,
            };
            docOptionsInnerPanel.Controls.Add(agentsCheckBox);

            // Model-Driven Apps Checkbox
            modelDrivenAppsCheckBox = new CheckBox()
            {
                Text = "Model-Driven Apps",
                Location = new Point(convertToDPISpecific(15), agentsCheckBox.Location.Y + agentsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true,
            };
            docOptionsInnerPanel.Controls.Add(modelDrivenAppsCheckBox);

            // Business Process Flows Checkbox
            businessProcessFlowsCheckBox = new CheckBox()
            {
                Text = "Business Process Flows",
                Location = new Point(convertToDPISpecific(15), modelDrivenAppsCheckBox.Location.Y + modelDrivenAppsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true,
            };
            docOptionsInnerPanel.Controls.Add(businessProcessFlowsCheckBox);

            // Desktop Flows Checkbox
            desktopFlowsCheckBox = new CheckBox()
            {
                Text = "Desktop Flows",
                Location = new Point(convertToDPISpecific(15), businessProcessFlowsCheckBox.Location.Y + businessProcessFlowsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true,
            };
            docOptionsInnerPanel.Controls.Add(desktopFlowsCheckBox);

            // Classic Workflows Checkbox
            classicWorkflowsCheckBox = new CheckBox()
            {
                Text = "Classic Workflows",
                Location = new Point(convertToDPISpecific(15), desktopFlowsCheckBox.Location.Y + desktopFlowsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            docOptionsInnerPanel.Controls.Add(classicWorkflowsCheckBox);
            // Dataflows Checkbox
            dataflowsCheckBox = new CheckBox()
            {
                Text = "Dataflows",
                Location = new Point(convertToDPISpecific(15), classicWorkflowsCheckBox.Location.Y + classicWorkflowsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true,
                Enabled = false
            };
            
            docOptionsInnerPanel.Controls.Add(dataflowsCheckBox);

            // Apps Checkbox
            appsCheckBox = new CheckBox()
            {
                Text = "Canvas Apps",
                Location = new Point(convertToDPISpecific(15), dataflowsCheckBox.Location.Y + dataflowsCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            docOptionsInnerPanel.Controls.Add(appsCheckBox);

            // Canvas Apps: Document Changed Properties or All Properties Label
            documentChangesOrEverythingLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(30), appsCheckBox.Location.Y + appsCheckBox.Height + convertToDPISpecific(10)),
                Text = "Document Changed Properties or All Properties",
                Width = convertToDPISpecific(350),
                Height = convertToDPISpecific(20)
            };
            docOptionsInnerPanel.Controls.Add(documentChangesOrEverythingLabel);

            // Canvas Apps: Changes Only RadioButton
            documentChangesOnlyRadioButton = new RadioButton()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Changes only",
                Checked = true,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(45), documentChangesOrEverythingLabel.Location.Y + documentChangesOrEverythingLabel.Height)
            };
            docOptionsInnerPanel.Controls.Add(documentChangesOnlyRadioButton);

            // Canvas Apps: All Properties RadioButton
            documentEverythingRadioButton = new RadioButton()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "All Properties",
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(45), documentChangesOnlyRadioButton.Location.Y + documentChangesOnlyRadioButton.Height)
            };
            docOptionsInnerPanel.Controls.Add(documentEverythingRadioButton);

            // Canvas Apps: Document Default Values Checkbox
            documentDefaultsCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Document default values of properties",
                Checked = true,
                Size = new Size(convertToDPISpecific(350), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(45), documentEverythingRadioButton.Location.Y + documentEverythingRadioButton.Height)
            };
            docOptionsInnerPanel.Controls.Add(documentDefaultsCheckBox);

            // Canvas Apps: Document Sample DataSources Checkbox
            documentSampleDataCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Document sample datasources",
                Checked = false,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(45), documentDefaultsCheckBox.Location.Y + documentDefaultsCheckBox.Height)
            };
            docOptionsInnerPanel.Controls.Add(documentSampleDataCheckBox);

            // Apps Options GroupBox
            GroupBox appsSubOptionsGroup = new GroupBox()
            {
                Text = "Canvas Apps Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(30), documentSampleDataCheckBox.Location.Y + documentSampleDataCheckBox.Height + convertToDPISpecific(10)),
                Size = new Size(convertToDPISpecific(350), convertToDPISpecific(150)),
                AutoSize = true
            };
            docOptionsInnerPanel.Controls.Add(appsSubOptionsGroup);

            // Apps Options: Properties Checkbox
            appPropertiesCheckBox = new CheckBox()
            {
                Text = "App Properties",
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                Checked = true,
                AutoSize = true
            };
            appsSubOptionsGroup.Controls.Add(appPropertiesCheckBox);

            // Apps Options: Variables Checkbox
            variablesCheckBox = new CheckBox()
            {
                Text = "Variables",
                Location = new Point(convertToDPISpecific(15), appPropertiesCheckBox.Location.Y + appPropertiesCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            appsSubOptionsGroup.Controls.Add(variablesCheckBox);

            // Apps Options: DataSources Checkbox
            dataSourcesCheckBox = new CheckBox()
            {
                Text = "DataSources",
                Location = new Point(convertToDPISpecific(15), variablesCheckBox.Location.Y + variablesCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            appsSubOptionsGroup.Controls.Add(dataSourcesCheckBox);

            // Apps Options: Resources Checkbox
            resourcesCheckBox = new CheckBox()
            {
                Text = "Resources",
                Location = new Point(convertToDPISpecific(15), dataSourcesCheckBox.Location.Y + dataSourcesCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            appsSubOptionsGroup.Controls.Add(resourcesCheckBox);

            // Apps Options: Controls Checkbox
            controlsCheckBox = new CheckBox()
            {
                Text = "Controls",
                Location = new Point(convertToDPISpecific(15), resourcesCheckBox.Location.Y + resourcesCheckBox.Height + convertToDPISpecific(10)),
                Checked = true,
                AutoSize = true
            };
            appsSubOptionsGroup.Controls.Add(controlsCheckBox);

            // Event handlers for enabling/disabling controls
            solutionCheckBox.CheckedChanged += (sender, e) =>
            {
                documentDefaultColumnsCheckBox.Enabled = solutionCheckBox.Checked;
                showAllComponentsInGraphCheckBox.Enabled = solutionCheckBox.Checked;
            };

            flowsCheckBox.CheckedChanged += (sender, e) =>
            {
                flowActionSortOrderInfoLabel.Enabled = flowsCheckBox.Checked;
                flowActionSortOrderComboBox.Enabled = flowsCheckBox.Checked;
            };

            appsCheckBox.CheckedChanged += (sender, e) =>
            {
                bool isChecked = appsCheckBox.Checked;
                documentChangesOrEverythingLabel.Enabled = isChecked;
                documentChangesOnlyRadioButton.Enabled = isChecked;
                documentEverythingRadioButton.Enabled = isChecked;
                documentDefaultsCheckBox.Enabled = isChecked;
                documentSampleDataCheckBox.Enabled = isChecked;
                appPropertiesCheckBox.Enabled = isChecked;
                variablesCheckBox.Enabled = isChecked;
                dataSourcesCheckBox.Enabled = isChecked;
                resourcesCheckBox.Enabled = isChecked;
                controlsCheckBox.Enabled = isChecked;
            };

            otherOptionsGroup = new GroupBox()
            {
                Text = "Other Options",
                Padding = new Padding(10),
                Location = new Point(convertToDPISpecific(15), outputFormatGroup.Height + outputFormatGroup.Location.Y + convertToDPISpecific(10)),
                Size = new System.Drawing.Size(convertToDPISpecific(400), convertToDPISpecific(120)),
                AutoSize = true
            };
            settingsPanel.Controls.Add(otherOptionsGroup);
            checkForUpdatesOnLaunchCheckBox = new CheckBox()
            {
                TextAlign = ContentAlignment.MiddleLeft,
                Text = "Check for updates on launch",
                Checked = true,
                Size = new Size(convertToDPISpecific(300), convertToDPISpecific(30)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(20))
            };
            otherOptionsGroup.Controls.Add(checkForUpdatesOnLaunchCheckBox);
            saveConfigButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(42), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), checkForUpdatesOnLaunchCheckBox.Height + checkForUpdatesOnLaunchCheckBox.Location.Y + convertToDPISpecific(10)),
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
                Location = new Point(convertToDPISpecific(10) + saveConfigButton.Width + saveConfigButton.Location.X, saveConfigButton.Location.Y + convertToDPISpecific(5)),
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
                ImageAlign = ContentAlignment.MiddleCenter,
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
                ImageAlign = ContentAlignment.MiddleCenter,
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

            // Adjust the height of the settingsPanel if necessary
            settingsPanel.Height = otherOptionsGroup.Location.Y + otherOptionsGroup.Height + convertToDPISpecific(20);

            // Make documentationOptionsGroup resize with the tab
            settingsPanel.Resize += (sender, e) =>
            {
                int availableHeight = settingsPanel.ClientSize.Height - documentationOptionsGroup.Location.Y - convertToDPISpecific(15);
                if (availableHeight > convertToDPISpecific(145))
                {
                    documentationOptionsGroup.AutoSize = false;
                    documentationOptionsGroup.Height = availableHeight;
                }
            };

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
                Dock = DockStyle.Fill
            };

            powerDocuInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25)),
                Text = "Welcome to PowerDocu!",
                Width = convertToDPISpecific(450),
                Height = convertToDPISpecific(60),
                AutoSize = true,
                Font = new Font(this.Font.FontFamily, 1.5f * this.Font.Size)
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
                Size = new Size(convertToDPISpecific(180), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), convertToDPISpecific(25) + powerDocuInfoLabel.Location.Y + powerDocuInfoLabel.Height),
                IconChar = IconChar.FileArchive,
                IconColor = Color.Purple,
                IconSize = convertToDPISpecific(24),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Select Files",
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            selectFileToParseButton.Click += new EventHandler(SelectZIPFileButton_Click);
            generateDocuPanel.Controls.Add(selectFileToParseButton);
            fileToParseInfoLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15) + selectFileToParseButton.Width, selectFileToParseButton.Location.Y + (selectFileToParseButton.Height - convertToDPISpecific(13)) / 2),
                Text = "Select Solution(s) (.zip) or Power Apps file(s) (.msapp). Multiple items can be selected via Ctrl + Left Click.",
                Width = convertToDPISpecific(450),
                Height = convertToDPISpecific(60),
                AutoSize = true
            };
            generateDocuPanel.Controls.Add(fileToParseInfoLabel);
            startDocumentationButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(180), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), selectFileToParseButton.Location.Y + selectFileToParseButton.Height + convertToDPISpecific(25)),
                IconChar = IconChar.FileExport,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(24),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Generate Documentation",
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            startDocumentationButton.Click += new EventHandler(StartDocumentationButton_Click);
            generateDocuPanel.Controls.Add(startDocumentationButton);
            startImageGenerationButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(180), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), startDocumentationButton.Location.Y + startDocumentationButton.Height + convertToDPISpecific(0)),
                IconChar = IconChar.Images,
                IconColor = Color.Green,
                IconSize = convertToDPISpecific(24),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Generate Images Only",
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            startImageGenerationButton.Click += new EventHandler(StartImageGenerationButton_Click);
            generateDocuPanel.Controls.Add(startImageGenerationButton);
            openOutputFolderButton = new IconButton()
            {
                Size = new Size(convertToDPISpecific(180), convertToDPISpecific(42)),
                Location = new Point(convertToDPISpecific(15), startImageGenerationButton.Location.Y + startImageGenerationButton.Height + convertToDPISpecific(0)),
                IconChar = IconChar.FolderOpen,
                IconColor = Color.Orange,
                IconSize = convertToDPISpecific(24),
                IconFont = IconFont.Auto,
                ImageAlign = ContentAlignment.MiddleLeft,
                Text = "Open Output Folder",
                TextImageRelation = TextImageRelation.ImageBeforeText,
                TextAlign = ContentAlignment.MiddleLeft,
                Visible = false
            };
            openOutputFolderButton.Click += new EventHandler(openOutputFolderButton_Click);
            generateDocuPanel.Controls.Add(openOutputFolderButton);
            selectedFilesToDocumentLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15) + startDocumentationButton.Width, startDocumentationButton.Location.Y + (startDocumentationButton.Height - convertToDPISpecific(13)) / 2),
                Text = "Start the full documentation generation for the selected files",
                //Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Width = convertToDPISpecific(300),
                Height = convertToDPISpecific(15),
                AutoSize = true,
                Visible = startDocumentationButton.Visible
            };
            generateDocuPanel.Controls.Add(selectedFilesToDocumentLabel);
            selectedFilesToGenerateImageLabel = new Label()
            {
                Location = new Point(convertToDPISpecific(15) + startImageGenerationButton.Width, startImageGenerationButton.Location.Y + (startImageGenerationButton.Height - convertToDPISpecific(13)) / 2),
                Text = "Start the image generation only for the selected files",
                //Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Width = convertToDPISpecific(300),
                Height = convertToDPISpecific(15),
                AutoSize = true,
                Visible = startImageGenerationButton.Visible
            };
            generateDocuPanel.Controls.Add(selectedFilesToGenerateImageLabel);

            // Process status image list for file tracking
            processStatusImageList = new ImageList()
            {
                ImageSize = new Size(convertToDPISpecific(16), convertToDPISpecific(16)),
                ColorDepth = ColorDepth.Depth32Bit
            };
            processStatusImageList.Images.Add("pending", FontAwesome.Sharp.IconChar.Circle.ToBitmap(Color.LightGray, convertToDPISpecific(16)));
            processStatusImageList.Images.Add("processing", FontAwesome.Sharp.IconChar.Spinner.ToBitmap(Color.DodgerBlue, convertToDPISpecific(16)));
            processStatusImageList.Images.Add("completed", FontAwesome.Sharp.IconChar.CheckCircle.ToBitmap(Color.Green, convertToDPISpecific(16)));
            processStatusImageList.Images.Add("error", FontAwesome.Sharp.IconChar.ExclamationCircle.ToBitmap(Color.Red, convertToDPISpecific(16)));

            // Process info list view showing files and their processing status
            processInfoListView = new ListView()
            {
                Location = new Point(convertToDPISpecific(15), openOutputFolderButton.Location.Y + openOutputFolderButton.Height + convertToDPISpecific(15)),
                Size = new Size(convertToDPISpecific(400), convertToDPISpecific(200)),
                View = View.Details,
                SmallImageList = processStatusImageList,
                FullRowSelect = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                GridLines = false,
                Visible = false
            };
            processInfoListView.Columns.Add("File", convertToDPISpecific(350));
            processInfoListView.Columns.Add("Status", convertToDPISpecific(100));
            processInfoListView.Columns.Add("Progress", -2);
            generateDocuPanel.Controls.Add(processInfoListView);

            generateDocuPanel.Resize += (sender, e) =>
            {
                int newWidth = generateDocuPanel.ClientSize.Width - convertToDPISpecific(30);
                processInfoListView.Size = new Size(
                    newWidth,
                    generateDocuPanel.ClientSize.Height - processInfoListView.Location.Y - convertToDPISpecific(10)
                );
                if (processInfoListView.Columns.Count > 2)
                {
                    processInfoListView.Columns[2].Width = newWidth - processInfoListView.Columns[0].Width - processInfoListView.Columns[1].Width - convertToDPISpecific(5);
                }
            };

            tabPage.Controls.Add(generateDocuPanel);
            return tabPage;
        }

        private int convertToDPISpecific(int number)
        {
            //96 DPI is the default
            return (int)number * this.DeviceDpi / 96;
        }

        private IconButton selectFileToParseButton, selectWordTemplateButton, newReleaseButton, updateConnectorIconsButton, saveConfigButton, startDocumentationButton, startImageGenerationButton, clearWordTemplateButton, openOutputFolderButton;
        private OpenFileDialog openFileToParseDialog, openWordTemplateDialog;
        private TextBox appStatusTextBox;
        private ComboBox outputFormatComboBox, flowActionSortOrderComboBox;
        private GroupBox outputFormatGroup, documentationOptionsGroup, otherOptionsGroup;
        private CheckBox documentDefaultsCheckBox, documentSampleDataCheckBox, documentDefaultColumnsCheckBox, appPropertiesCheckBox, variablesCheckBox, dataSourcesCheckBox, resourcesCheckBox, controlsCheckBox, appsCheckBox, agentsCheckBox, modelDrivenAppsCheckBox, businessProcessFlowsCheckBox, desktopFlowsCheckBox, dataflowsCheckBox, classicWorkflowsCheckBox, flowsCheckBox, solutionCheckBox, checkForUpdatesOnLaunchCheckBox, addTableOfContentsCheckBox, showAllComponentsInGraphCheckBox;

        private RadioButton documentChangesOnlyRadioButton, documentEverythingRadioButton;
        private Label wordTemplateInfoLabel, fileToParseInfoLabel, outputFormatInfoLabel,
                        flowActionSortOrderInfoLabel, newReleaseLabel, updateConnectorIconsLabel,
                        selectedFilesToDocumentLabel, selectedFilesToGenerateImageLabel, statusLabel, saveConfigLabel,
                        documentChangesOrEverythingLabel, powerDocuInfoLabel;
        private TabControl dynamicTabControl;
        private PictureBox statusIconPictureBox;
        private Panel settingsPanel, generateDocuPanel;
        private ListView processInfoListView;
        private ImageList processStatusImageList;
        public ConfigHelper configHelper;

        #endregion
    }
}

