using System;
using System.Collections.Generic;
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

        public static void Save(string file, IList<FileInformationItem> items)
        {
            StreamWriter writer = new StreamWriter(new FileStream(file, FileMode.CreateNew));
            writer.WriteLine(CreateHeaderLine());
            for (int i = 0; i < items.Count; i++)
            {
                writer.WriteLine(GetSingleLine(items[i]));
            }
            writer.Flush();
            writer.Close();
        }

        private static string CreateHeaderLine()
        {
            List<string> fieldNames = new List<string>();
            fieldNames.Add("FileName");
            fieldNames.Add("Extension");
            fieldNames.Add("Hash");
            fieldNames.Add("FullPath");
            fieldNames.Add("Directory");
            fieldNames.Add("Size");
            fieldNames.Add("Created");
            fieldNames.Add("LastChange");
            fieldNames.Add("LastOpen");
            fieldNames.Add("Hidden");
            fieldNames.Add("ReadOnly");
            return string.Join(";", fieldNames.ToArray());
        }



        private static string GetSingleLine(FileInformationItem item)
        {
            List<string> fields = new List<string>();

            fields.Add(EscapeField(item.FileName));
            fields.Add(EscapeField(item.Extension));
            fields.Add(EscapeField(item.Hash));
            fields.Add(EscapeField(item.FullPath));
            fields.Add(EscapeField(item.Directory));
            fields.Add(EscapeField(item.Size.ToString()));
            fields.Add(EscapeField(item.Created.ToShortDateString()));
            fields.Add(EscapeField(item.LastChange.ToShortDateString()));
            fields.Add(EscapeField(item.LastOpen.ToShortDateString()));
            fields.Add(EscapeField(ResolveBoolean(item.Hidden).ToString()));
            fields.Add(EscapeField(ResolveBoolean(item.ReadOnly).ToString()));

            return string.Join(";", fields.ToArray());
        }

        private static int ResolveBoolean(bool value)
        {
            return value ? 1 : 0;
        }

        private static string EscapeField(string fieldValue)
        {
            fieldValue = fieldValue.Replace("\"", "\"\"");
            if (fieldValue.Contains(";")) fieldValue = String.Format("\"{0}\"", fieldValue);
            return fieldValue;
        }
    }
}