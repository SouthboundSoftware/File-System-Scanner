namespace Southbound.FileSystemScanner
{
    partial class FileSystemScannerMainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.selectRootButton = new System.Windows.Forms.Button();
            this.rootLabel = new System.Windows.Forms.Label();
            this.selectedRootTextBox = new System.Windows.Forms.TextBox();
            this.rootSelectionDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.simpleHashRadioButton = new System.Windows.Forms.RadioButton();
            this.lazyHashRadioButton = new System.Windows.Forms.RadioButton();
            this.fullHashRadioButton = new System.Windows.Forms.RadioButton();
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // selectRootButton
            // 
            this.selectRootButton.Location = new System.Drawing.Point(405, 28);
            this.selectRootButton.Name = "selectRootButton";
            this.selectRootButton.Size = new System.Drawing.Size(75, 23);
            this.selectRootButton.TabIndex = 0;
            this.selectRootButton.Text = "Select...";
            this.selectRootButton.UseVisualStyleBackColor = true;
            this.selectRootButton.Click += new System.EventHandler(this.selectRootButton_Click);
            // 
            // rootLabel
            // 
            this.rootLabel.AutoSize = true;
            this.rootLabel.Location = new System.Drawing.Point(13, 13);
            this.rootLabel.Name = "rootLabel";
            this.rootLabel.Size = new System.Drawing.Size(33, 13);
            this.rootLabel.TabIndex = 1;
            this.rootLabel.Text = "Root:";
            // 
            // selectedRootTextBox
            // 
            this.selectedRootTextBox.Location = new System.Drawing.Point(13, 30);
            this.selectedRootTextBox.Name = "selectedRootTextBox";
            this.selectedRootTextBox.ReadOnly = true;
            this.selectedRootTextBox.Size = new System.Drawing.Size(386, 20);
            this.selectedRootTextBox.TabIndex = 2;
            // 
            // simpleHashRadioButton
            // 
            this.simpleHashRadioButton.AutoSize = true;
            this.simpleHashRadioButton.Checked = true;
            this.simpleHashRadioButton.Location = new System.Drawing.Point(16, 85);
            this.simpleHashRadioButton.Name = "simpleHashRadioButton";
            this.simpleHashRadioButton.Size = new System.Drawing.Size(98, 17);
            this.simpleHashRadioButton.TabIndex = 3;
            this.simpleHashRadioButton.TabStop = true;
            this.simpleHashRadioButton.Text = "Simple file hash";
            this.simpleHashRadioButton.UseVisualStyleBackColor = true;
            this.simpleHashRadioButton.CheckedChanged += new System.EventHandler(this.hashMethodChanged);
            // 
            // lazyHashRadioButton
            // 
            this.lazyHashRadioButton.AutoSize = true;
            this.lazyHashRadioButton.Location = new System.Drawing.Point(16, 108);
            this.lazyHashRadioButton.Name = "lazyHashRadioButton";
            this.lazyHashRadioButton.Size = new System.Drawing.Size(89, 17);
            this.lazyHashRadioButton.TabIndex = 4;
            this.lazyHashRadioButton.Text = "Lazy file hash";
            this.lazyHashRadioButton.UseVisualStyleBackColor = true;
            this.lazyHashRadioButton.CheckedChanged += new System.EventHandler(this.hashMethodChanged);
            // 
            // fullHashRadioButton
            // 
            this.fullHashRadioButton.AutoSize = true;
            this.fullHashRadioButton.Location = new System.Drawing.Point(16, 132);
            this.fullHashRadioButton.Name = "fullHashRadioButton";
            this.fullHashRadioButton.Size = new System.Drawing.Size(83, 17);
            this.fullHashRadioButton.TabIndex = 5;
            this.fullHashRadioButton.Text = "Full file hash";
            this.fullHashRadioButton.UseVisualStyleBackColor = true;
            this.fullHashRadioButton.CheckedChanged += new System.EventHandler(this.hashMethodChanged);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(328, 85);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(152, 64);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(13, 162);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(467, 23);
            this.progressBar.TabIndex = 7;
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "CSV file|*.csv";
            // 
            // FileSystemScannerMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 197);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.fullHashRadioButton);
            this.Controls.Add(this.lazyHashRadioButton);
            this.Controls.Add(this.simpleHashRadioButton);
            this.Controls.Add(this.selectedRootTextBox);
            this.Controls.Add(this.rootLabel);
            this.Controls.Add(this.selectRootButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FileSystemScannerMainWindow";
            this.ShowIcon = false;
            this.Text = "Southbound File System Scanner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button selectRootButton;
        private System.Windows.Forms.Label rootLabel;
        private System.Windows.Forms.TextBox selectedRootTextBox;
        private System.Windows.Forms.FolderBrowserDialog rootSelectionDialog;
        private System.Windows.Forms.RadioButton simpleHashRadioButton;
        private System.Windows.Forms.RadioButton lazyHashRadioButton;
        private System.Windows.Forms.RadioButton fullHashRadioButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

