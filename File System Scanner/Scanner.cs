using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Southbound.FileSystemScanner
{
    class Scanner
    {
        private HashMethod hashMethod;
        private DirectoryInfo root;
        private List<FileInformationItem> items;
        private bool running;

        public Scanner(string rootPath, HashMethod hashMethod)
        {
            this.root = new DirectoryInfo(rootPath);
            this.hashMethod = hashMethod;
            this.items = new List<FileInformationItem>();
            this.running = false;
        }

        public void Start()
        {
            this.running = true;
            this.Scan(this.root);
            this.running = false;
        }

        public bool IsRunning { get { return this.running; } }

        public IList<FileInformationItem> getFileInformationItems()
        {
            return this.items;
        }

        private void Scan(DirectoryInfo root)
        {
            try
            {
                foreach (FileInfo file in root.GetFiles())
                {
                    FileInformationItem item = new FileInformationItem(file);
                    item.Hash = HashFunctions.CalculateHash(item, this.hashMethod);
                    this.items.Add(item);
                }

                foreach (DirectoryInfo newRoot in root.GetDirectories())
                {
                    Scan(newRoot);
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }
        }

        public HashMethod HashMethod { get { return this.hashMethod; } }
    }
}