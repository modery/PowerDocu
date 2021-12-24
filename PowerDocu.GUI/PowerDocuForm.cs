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
            NotificationHelper.AddNotificationReceiver(new PowerDocuFormNotificationReceiver(appStatusTextBox));
        }

        private void selectZIPFileButton_Click(object sender, EventArgs e)
        {
            if (openFileToParseDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    NotificationHelper.SendNotification("Preparing to parse file " + openFileToParseDialog.FileName + ", please wait.");
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    if (openFileToParseDialog.FileName.EndsWith(".zip"))
                    {
                        NotificationHelper.SendNotification("Trying to process Power Automate Flows");
                        FlowDocumentationGenerator.GenerateWordDocumentation(openFileToParseDialog.FileName, (openWordTemplateDialog.FileName != "") ? openWordTemplateDialog.FileName : null);
                        NotificationHelper.SendNotification("Trying to process Power Apps");
                        AppDocumentationGenerator.GenerateWordDocumentation(openFileToParseDialog.FileName, (openWordTemplateDialog.FileName != "") ? openWordTemplateDialog.FileName : null);
                    }
                    else if (openFileToParseDialog.FileName.EndsWith(".msapp"))
                    {
                        AppDocumentationGenerator.GenerateWordDocumentation(openFileToParseDialog.FileName, (openWordTemplateDialog.FileName != "") ? openWordTemplateDialog.FileName : null);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                finally
                {
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
            }
        }

        private void selectWordTemplateButton_Click(object sender, EventArgs e)
        {
            if (openWordTemplateDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    wordTemplateInfoLabel.Text = "Template: " + Path.GetFileName(openWordTemplateDialog.FileName);
                    NotificationHelper.SendNotification("Selected Word template " + openWordTemplateDialog.FileName);
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

        private void sizeChanged(object sender, EventArgs e)
        {
            appStatusTextBox.Size = new Size(ClientSize.Width - 30, ClientSize.Height - selectFileToParseButton.Height - selectWordTemplateButton.Height - 40);
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
