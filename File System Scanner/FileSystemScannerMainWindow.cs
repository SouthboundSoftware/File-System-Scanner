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
        private HashMethod selectedHash;

        public FileSystemScannerMainWindow()
        {
            InitializeComponent();
            this.lazyHashRadioButton.Checked = true;
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
            using (ScannerRunnerWindow runnerWindow = new ScannerRunnerWindow())
            {
                this.Hide();
                runnerWindow.RunScan(this.selectedRootTextBox.Text, this.selectedHash);
            }

            this.Show();
        }

        private void hashMethodChanged(object sender, EventArgs e)
        {
            if (lazyHashRadioButton.Checked)
            {
                this.selectedHash = HashMethod.Lazy;
            }
            else if (simpleHashRadioButton.Checked)
            {
                this.selectedHash = HashMethod.Simple;
            }
            else
            {
                this.selectedHash = HashMethod.Full;
            }
        }
    }
}