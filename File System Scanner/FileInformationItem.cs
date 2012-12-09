using System;
using System.IO;

namespace Southbound.FileSystemScanner
{
    class FileInformationItem
    {
        private string filename;
        private string extension;
        private string hash;
        private string fullpath;
        private string directory;
        private long size;
        private DateTime creationTime;
        private DateTime lastChangeTime;
        private DateTime lastOpenTime;
        private bool hidden;
        private bool readOnly;

        public FileInformationItem(FileInfo file)
        {
            this.filename = file.Name;
            this.extension = this.filename.Contains(".") ? this.filename.Substring(this.filename.LastIndexOf(".")) : string.Empty;
            this.hash = string.Empty;
            this.fullpath = file.FullName;
            this.directory = file.DirectoryName;
            this.size = file.Length;
            this.creationTime = file.CreationTime;
            this.lastChangeTime = file.LastWriteTime;
            this.lastOpenTime = file.LastAccessTime;
            this.hidden = (file.Attributes | FileAttributes.Hidden) == FileAttributes.Hidden;
            this.readOnly = file.IsReadOnly;
        }

        public string FileName { get { return this.filename; } }
        public string Extension { get { return this.extension; } }
        public string Hash { get { return this.hash; } set { this.hash = value; } }
        public string FullPath { get { return this.fullpath; } }
        public string Directory { get { return this.directory; } }
        public long Size { get { return this.size; } }
        public DateTime Created { get { return this.creationTime; } }
        public DateTime LastChange { get { return this.lastChangeTime; } }
        public DateTime LastOpen { get { return this.lastOpenTime; } }
        public bool Hidden { get { return this.hidden; } }
        public bool ReadOnly { get { return this.readOnly; } }
    }
}