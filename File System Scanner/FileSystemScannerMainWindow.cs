using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Southbound.FileSystemScanner
{
    public partial class FileSystemScannerMainWindow : Form
    {
        private HashMethod selectedHash;
        private Scanner scanner;

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
            this.scanner = new Scanner(this.selectedRootTextBox.Text, this.selectedHash);
            this.selectRootButton.Enabled = false;
            this.startButton.Enabled = false;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.timer.Start();
            new Thread(new ThreadStart(delegate()
            {
                this.scanner.Start();
            })).Start();
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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.scanner.IsRunning)
            {
                this.progressBar.Update();
            }
            else
            {
                this.timer.Stop();
                this.progressBar.Style = ProgressBarStyle.Blocks;
                this.startButton.Enabled = true;
                this.selectRootButton.Enabled = true;

                int fileCount = this.scanner.getFileInformationItems().Count;
                MessageBox.Show(string.Format("Scanned {0} file{1}", fileCount, (fileCount > 1 ? "s" : "")), "Done", MessageBoxButtons.OK);

                this.saveResult();
            }
        }

        private void saveResult()
        {
            if (this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileInformationItem.Save(this.saveFileDialog.FileName, this.scanner.getFileInformationItems());
            }
        }

    }
}