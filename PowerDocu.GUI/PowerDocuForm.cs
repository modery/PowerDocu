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

        private void selectButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    textBox1.AppendText("Preparing to parse file " + openFileDialog1.FileName + ", please wait.");
                    Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.AppendText(FlowDocumentationGenerator.GenerateWordDocumentation(openFileDialog1.FileName));
                    textBox1.AppendText(Environment.NewLine);
                    Cursor = Cursors.Arrow; // change cursor to normal type
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");

                }
            }
        }


        private void sizeChanged(object sender, EventArgs e)
        {
            textBox1.Size = new Size(ClientSize.Width - 30, ClientSize.Height - selectButton.Height - 40);
        }

    }
}
