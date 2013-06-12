using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
            this.ShowDialog();
        }

        private void interfaceUpdateTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
