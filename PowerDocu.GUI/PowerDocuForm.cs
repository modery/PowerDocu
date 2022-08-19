using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PowerDocu.AppDocumenter;
using PowerDocu.FlowDocumenter;
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
            CheckForNewerRelease();
        }

        private async void CheckForNewerRelease()
        {
            if (await PowerDocuReleaseHelper.HasNewerPowerDocuRelease())
            {
                newReleaseButton.Visible = true;
                NotificationHelper.SendNotification("A new PowerDocu release has been found: " + PowerDocuReleaseHelper.latestVersionTag);
                NotificationHelper.SendNotification("Please visit " + PowerDocuReleaseHelper.latestVersionUrl + " to download it");
                NotificationHelper.SendNotification(Environment.NewLine);
            }
        }

        private void selectZIPFileButton_Click(object sender, EventArgs e)
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
                                "Trying to process Power Automate Flows"
                            );
                            FlowDocumentationGenerator.GenerateWordDocumentation(
                                fileName,
                                (openWordTemplateDialog.FileName != "")
                                    ? openWordTemplateDialog.FileName
                                    : null
                            );
                            NotificationHelper.SendNotification("Trying to process Power Apps");
                            AppDocumentationGenerator.GenerateWordDocumentation(
                                fileName,
                                (openWordTemplateDialog.FileName != "")
                                    ? openWordTemplateDialog.FileName
                                    : null
                            );
                        }
                        else if (fileName.EndsWith(".msapp"))
                        {
                            AppDocumentationGenerator.GenerateWordDocumentation(
                                fileName,
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

        private void selectWordTemplateButton_Click(object sender, EventArgs e)
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

        private void newReleaseButton_Click(object sender, EventArgs e)
        {
            var sInfo = new System.Diagnostics.ProcessStartInfo(PowerDocuReleaseHelper.latestVersionUrl)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            appStatusTextBox.Size = new Size(
                ClientSize.Width - 30,
                ClientSize.Height
                    - selectFileToParseButton.Height
                    - selectWordTemplateButton.Height
                    - 40
            );
            newReleaseButton.Location = new Point(ClientSize.Width - 80, 15);
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
