using System;
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
                    //run FlowParser from here
                    textBox1.AppendText(FlowDocumentationGenerator.GenerateWordDocumentation(openFileDialog1.FileName));
                    textBox1.AppendText(Environment.NewLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");

                }
            }
        }

    }
}
