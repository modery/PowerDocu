using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using PowerDocu.FlowDocumenter;

namespace PowerDocu.GUI
{
    public partial class PowerDocuForm : Form
    {
        public PowerDocuForm()
        {
            InitializeComponent();
        }

        private void selectZIPFileButton_Click(object sender, EventArgs e)
        {
            if (openFileToParseDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    appStatusTextBox.AppendText("Preparing to parse file " + openFileToParseDialog.FileName + ", please wait.");
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText(FlowDocumentationGenerator.GenerateWordDocumentation(openFileToParseDialog.FileName, (openWordTemplateDialog.FileName != "") ? openWordTemplateDialog.FileName : null));
                    appStatusTextBox.AppendText(Environment.NewLine);
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
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
                    appStatusTextBox.AppendText("Selected Word template " + openWordTemplateDialog.FileName);
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    appStatusTextBox.AppendText("Security error:");
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText("Error message: " + ex.Message);
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText("Details:");
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText(ex.StackTrace);
                    appStatusTextBox.AppendText(Environment.NewLine);
                    appStatusTextBox.AppendText(Environment.NewLine);
                }
            }
        }

        private void sizeChanged(object sender, EventArgs e)
        {
            appStatusTextBox.Size = new Size(ClientSize.Width - 30, ClientSize.Height - selectFileToParseButton.Height - selectWordTemplateButton.Height - 40);
        }
    }
}
