using System;
using System.Drawing;
using System.Windows.Forms;

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
            this.ClientSize = new System.Drawing.Size(1000, 380);
            this.SizeChanged += new EventHandler(sizeChanged);
            this.MinimumSize = new Size(500, 250);
            this.Text = "PowerDocu GUI";
            openFileDialog1 = new OpenFileDialog()
            {
                FileName = "*.zip",
                Filter = "ZIP files (*.zip)|*.zip",
                Title = "Open exported Flow ZIP file"
            };

            selectButton = new Button()
            {
                //this should properly size the button so that the Text is shown correctly
                Size = new Size((int)(210 * this.DeviceDpi / 96), (int)(30 * this.DeviceDpi / 96)),
                Location = new Point(15, 15),
                Text = "Select Flow or Solution to document"
            };
            selectButton.Click += new EventHandler(selectButton_Click);
            Controls.Add(selectButton);
            textBox1 = new TextBox
            {
                Size = new Size(ClientSize.Width - 30, ClientSize.Height - 70),
                Location = new Point(15, 60),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ReadOnly = true
            };
            Controls.Add(textBox1);
        }



        private Button selectButton;
        private OpenFileDialog openFileDialog1;
        private TextBox textBox1;

        #endregion
    }
}

