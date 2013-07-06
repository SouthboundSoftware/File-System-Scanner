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

        public IList<FileInformationItem> FileInformationItems
        {
            get
            {
                return this.items;
            }
        }

        private void Scan(DirectoryInfo root)
        {
            FileInfo[] files = null;
            try
            {
                files = root.GetFiles("*");
            }
            catch (UnauthorizedAccessException)
            {
                files = new FileInfo[0];
            }

            foreach (FileInfo file in files)
            {
                if ((file.Attributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint && (file.Attributes & FileAttributes.System) != FileAttributes.System)
                {
                    FileInformationItem item = new FileInformationItem(file);
                    item.Hash = HashFunctions.CalculateHash(item, this.hashMethod);
                    this.items.Add(item);
                }
            }

            DirectoryInfo[] directories = null;
            try
            {
                directories = root.GetDirectories();
            }
            catch (UnauthorizedAccessException)
            {
                directories = new DirectoryInfo[0];
            }

            foreach (DirectoryInfo newRoot in directories)
            {
                Scan(newRoot);
            }
        }

        public HashMethod HashMethod { get { return this.hashMethod; } }
    }
}