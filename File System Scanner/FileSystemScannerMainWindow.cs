using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MichaelBanzon.FileSystemScanner
{
    public partial class FileSystemScannerMainWindow : Form
    {
        private Thread scannerThread;
        private Thread saveThread;
        private HashMethod selectedHash;
        private Scanner scanner;
        private bool isSaving;

        public FileSystemScannerMainWindow()
        {
            InitializeComponent();
            this.lazyHashRadioButton.Checked = true;
            this.statusLabel.Text = string.Empty;
            this.isSaving = false;
            this.scannerThread = null;
            this.saveThread = null;
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
            this.simpleHashRadioButton.Enabled = false;
            this.lazyHashRadioButton.Enabled = false;
            this.fullHashRadioButton.Enabled = false;
            this.progressBar.Style = ProgressBarStyle.Marquee;
            this.timer.Start();
            this.scannerThread = new Thread(new ThreadStart(delegate()
            {
                this.scanner.Start();
                this.stopButton.Invoke((MethodInvoker)delegate
                {
                    this.stopButton.Enabled = false;
                });
            }));
            this.scannerThread.Start();
            this.stopButton.Enabled = true;
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
                int count = this.scanner.FileInformationItems.Count;
                this.statusLabel.Text = string.Format("Status: {0} file{1} scanned", count, count > 1 ? "s" : string.Empty);
            }
            else if (this.isSaving)
            {
                this.progressBar.Update();
                this.statusLabel.Text = "Saving output...";
            }
            else
            {
                this.timer.Stop();
                this.progressBar.Style = ProgressBarStyle.Blocks;
                this.startButton.Enabled = true;
                this.selectRootButton.Enabled = true;
                this.simpleHashRadioButton.Enabled = true;
                this.lazyHashRadioButton.Enabled = true;
                this.fullHashRadioButton.Enabled = true;

                int fileCount = this.scanner.FileInformationItems.Count;
                MessageBox.Show(string.Format("Scanned {0} file{1}", fileCount, (fileCount > 1 ? "s" : "")), "Done", MessageBoxButtons.OK);

                this.saveResult();
            }
        }

        private void saveResult()
        {
            if (this.saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.isSaving = true;
                this.timer.Start();
                this.saveThread = new Thread(new ThreadStart(delegate()
                {
                    FileInformationItem.Save(this.saveFileDialog.FileName, this.scanner.FileInformationItems);
                }));
                this.saveThread.Start();
                this.isSaving = false;
            }
        }

        private void FileSystemScannerMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (null != this.scannerThread && this.scannerThread.IsAlive) this.scannerThread.Abort();
            if (null != this.saveThread && this.saveThread.IsAlive) this.saveThread.Abort();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (this.scanner.IsRunning)
            {
                this.scanner.Stop();
            }
        }

    }
}