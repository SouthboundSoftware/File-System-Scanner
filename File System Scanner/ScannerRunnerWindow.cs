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
    public partial class ScannerRunnerWindow : Form
    {
        private Scanner scanner;

        public ScannerRunnerWindow()
        {
            InitializeComponent();
        }

        public void RunScan(string root, HashMethod hashMethod)
        {
            this.scanner = new Scanner(root, hashMethod);
            this.interfaceUpdateTimer.Start();
            new Thread(new ThreadStart(delegate() {
                this.scanner.Start();
                Console.WriteLine("Done!");
            })).Start();
            this.ShowDialog();
        }

        private void interfaceUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (this.scanner.IsRunning)
            {
                this.progressBar.Update();
                this.statusLabel.Text = string.Format("Running, found {0} files...", this.scanner.getFileInformationItems().Count);
            }
            else
            {
                this.statusLabel.Text = "Done";
                this.interfaceUpdateTimer.Stop();
                this.progressBar.Style = ProgressBarStyle.Blocks;

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

            this.Close();
        }
    }
}
