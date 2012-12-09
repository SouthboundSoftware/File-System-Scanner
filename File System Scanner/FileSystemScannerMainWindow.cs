using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Southbound.FileSystemScanner
{
    public partial class FileSystemScannerMainWindow : Form
    {
        public FileSystemScannerMainWindow()
        {
            InitializeComponent();
        }

        private void selectRootButton_Click(object sender, EventArgs e)
        {
            if (this.rootSelectionDialog.ShowDialog() == DialogResult.OK)
            {
                this.selectedRootTextBox.Text = this.rootSelectionDialog.SelectedPath;
                this.startButton.Enabled = true;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {

        }
    }
}