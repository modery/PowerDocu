using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PowerDocu.AppDocumenter;
using PowerDocu.SolutionDocumenter;
using PowerDocu.Common;

namespace PowerDocu.GUI
{
    public partial class PowerDocuForm : Form
    {
        public PowerDocuForm()
        {
            InitializeComponent();
            NotificationHelper.AddNotificationReceiver(
                new PowerDocuFormNotificationReceiver(appStatusTextBox)
            );
            using (var stream = File.OpenRead("Icons\\PowerDocu.ico"))
            {
                this.Icon = new Icon(stream);
            }
            InitialChecks();
        }

        private async void InitialChecks()
        {
            //check for newer release
            if (await PowerDocuReleaseHelper.HasNewerPowerDocuRelease())
            {
                newReleaseButton.Visible = true;
                newReleaseLabel.Text += PowerDocuReleaseHelper.latestVersionTag;
                newReleaseLabel.Visible = true;
                NotificationHelper.SendNotification("A new PowerDocu release has been found: " + PowerDocuReleaseHelper.latestVersionTag);
                NotificationHelper.SendNotification("Please visit " + PowerDocuReleaseHelper.latestVersionUrl + " or press the Update button to download it");
                NotificationHelper.SendNotification(Environment.NewLine);
            }
            //check for number of files
            int connectorIcons = ConnectorHelper.numberOfConnectorIcons();
            if (connectorIcons < 100)
            {
                NotificationHelper.SendNotification($"Only {connectorIcons} connector icons were found. Please update the Connectors list (press the Green Cloud Download icon)");
            }
        }

        private void SelectZIPFileButton_Click(object sender, EventArgs e)
        {
            if (openFileToParseDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string fileName in openFileToParseDialog.FileNames)
                {
                    try
                    {
                        NotificationHelper.SendNotification(
                            "Preparing to parse file " +
                                fileName
                                + ", please wait."
                        );
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        if (fileName.EndsWith(".zip"))
                        {
                            NotificationHelper.SendNotification(
                                "Trying to process Solution, Apps, and Flows"
                            );
                            SolutionDocumentationGenerator.GenerateDocumentation(
                                fileName,
                                outputFormatComboBox.SelectedItem.ToString(),
                                documentChangesOnlyRadioButton.Checked,
                                documentDefaultsCheckBox.Checked,
                                (openWordTemplateDialog.FileName != "")
                                    ? openWordTemplateDialog.FileName
                                    : null
                            );
                        }
                        else if (fileName.EndsWith(".msapp"))
                        {
                            AppDocumentationGenerator.GenerateDocumentation(
                                fileName,
                                outputFormatComboBox.SelectedItem.ToString(),
                                documentChangesOnlyRadioButton.Checked,
                                documentDefaultsCheckBox.Checked,
                                (openWordTemplateDialog.FileName != "")
                                    ? openWordTemplateDialog.FileName
                                    : null
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Security error.\n\nError message: {ex.Message}\n\n"
                                + $"Details:\n\n{ex.StackTrace}"
                        );
                    }
                    finally
                    {
                        NotificationHelper.SendNotification(Environment.NewLine);
                        Cursor = Cursors.Arrow; // change cursor to normal type
                    }
                }
            }
        }

        private void SelectWordTemplateButton_Click(object sender, EventArgs e)
        {
            if (openWordTemplateDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    wordTemplateInfoLabel.Text =
                        "Template: " + Path.GetFileName(openWordTemplateDialog.FileName);
                    NotificationHelper.SendNotification(
                        "Selected Word template " + openWordTemplateDialog.FileName
                    );
                }
                catch (Exception ex)
                {
                    NotificationHelper.SendNotification("Security error:");
                    NotificationHelper.SendNotification("Error message: " + ex.Message);
                    NotificationHelper.SendNotification(Environment.NewLine);
                    NotificationHelper.SendNotification("Details:");
                    NotificationHelper.SendNotification(ex.StackTrace);
                    NotificationHelper.SendNotification(Environment.NewLine);
                }
            }
        }

        private void NewReleaseButton_Click(object sender, EventArgs e)
        {
            var sInfo = new System.Diagnostics.ProcessStartInfo(PowerDocuReleaseHelper.latestVersionUrl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }

        private async void UpdateConnectorIconsButton_Click(object sender, EventArgs e)
        {
            await ConnectorHelper.UpdateConnectorIcons();
            updateConnectorIconsLabel.Text = "Update your existing set of connector icons\n(" + ConnectorHelper.numberOfConnectors() + " connectors, " + ConnectorHelper.numberOfConnectorIcons() + " icons)";
        }

        private void BackNextButton_Click(object sender, EventArgs e)
        {
            if (sender.Equals(nextLeftPanelButton))
            {
                step1Panel.Visible = false;
                step2Panel.Visible = true;
                nextLeftPanelButton.Enabled = false;
                nextLeftPanelButton.BackColor = Color.LightGray;
                backLeftPanelButton.Enabled = true;
                backLeftPanelButton.BackColor = Color.DodgerBlue;
                settingsDetailsLabel.Text = "  Output format: " + outputFormatComboBox.SelectedItem.ToString() + "\n" +
                                            ((openWordTemplateDialog.FileName != "") ? "  Word Template: " + openWordTemplateDialog.FileName + "\n" : "") +
                                            (documentChangesOnlyRadioButton.Checked ?
                                                "  Include changed App properties "
                                                : "  Include all App properties ") +
                                            (documentDefaultsCheckBox.Checked ? "and default values" : "") +
                                             " in documentation";
            }
            if (sender.Equals(backLeftPanelButton))
            {
                step1Panel.Visible = true;
                step2Panel.Visible = false;
                nextLeftPanelButton.Enabled = true;
                nextLeftPanelButton.BackColor = Color.DodgerBlue;
                backLeftPanelButton.Enabled = false;
                backLeftPanelButton.BackColor = Color.LightGray;
            }
            UpdateNavigation();
        }

        private void SizeChangedHandler(object sender, EventArgs e)
        {
            appStatusTextBox.Size = new Size(ClientSize.Width - convertToDPISpecific(30), ClientSize.Height - convertToDPISpecific(405));
        }

        private void DpiChangedHandler(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(convertToDPISpecific(800), convertToDPISpecific(350));
        }

        private void OutputFormatComboBox_Changed(object sender, EventArgs e)
        {
            if (outputFormatComboBox != null && selectWordTemplateButton != null)
            {
                if (outputFormatComboBox.SelectedItem.ToString().Equals(OutputFormatHelper.Word) || outputFormatComboBox.SelectedItem.ToString().Equals(OutputFormatHelper.All))
                {
                    selectWordTemplateButton.Enabled = true;
                    wordTemplateInfoLabel.ForeColor = Color.Black;
                }
                else
                {
                    selectWordTemplateButton.Enabled = false;
                    wordTemplateInfoLabel.ForeColor = Color.Gray;
                }
            }
        }
    }

    public class PowerDocuFormNotificationReceiver : NotificationReceiverBase
    {
        private readonly TextBox notificationTextBox;

        public PowerDocuFormNotificationReceiver(TextBox textBox)
        {
            notificationTextBox = textBox;
        }

        public override void Notify(string notification)
        {
            notificationTextBox.AppendText(notification);
            notificationTextBox.AppendText(Environment.NewLine);
        }
    }
}
