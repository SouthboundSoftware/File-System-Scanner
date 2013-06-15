using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Southbound.FileSystemScanner
{
    class HashFunctions
    {
        public static string CalculateHash(FileInformationItem file, HashMethod method)
        {
            if (HashMethod.Simple == method)
            {
                return CalculateSimpleHash(file);
            }
            else if (HashMethod.Lazy == method)
            {
                return CalculateLazyHash(file);
            }
            else if (HashMethod.Full == method)
            {
                return CalculateFullHash(file);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static string CalculateSimpleHash(FileInformationItem file)
        {
            return CalculateHash(file.LastChange.ToString() + file.Size.ToString());
        }

        private static string CalculateLazyHash(FileInformationItem file)
        {
            try
            {
                byte[] buffer = new byte[1024 * 8];

                using (FileStream fs = new FileStream(file.FullPath, FileMode.Open))
                {
                    int read = fs.Read(buffer, 0, buffer.Length);

                    return CalculateHash(buffer, 0, read);
                }
            }
            catch (UnauthorizedAccessException)
            {
                return CalculateSimpleHash(file);
            }
            catch (IOException)
            {
                return CalculateSimpleHash(file);
            }
        }

        private static string CalculateFullHash(FileInformationItem file)
        {
            byte[] buffer = new byte[4 * 1024];
            byte[] runningHash = null;

            using (FileStream fs = new FileStream(file.FullPath, FileMode.Open))
            {

                int read = fs.Read(buffer, 0, buffer.Length);
                while (0 < read)
                {
                    runningHash = null == runningHash ? CalculateRawHash(buffer, 0, read) : CalculateRawHash(Merge(runningHash, buffer), 0, runningHash.Length + read);
                    read = fs.Read(buffer, 0, buffer.Length);
                }
            }

            return CalculateHash(buffer, 0, buffer.Length);
        }

        private static string CalculateHash(string data)
        {
            byte[] byteData = System.Text.Encoding.UTF8.GetBytes(data);
            return CalculateHash(byteData, 0, byteData.Length);
        }

        private static string CalculateHash(byte[] data, int offset, int length)
        {
            return Convert.ToBase64String(CalculateRawHash(data, offset, length));
        }

        private static byte[] CalculateRawHash(byte[] data, int offset, int length)
        {
            using (HashAlgorithm algorithm = SHA1Managed.Create())
            {
                return algorithm.ComputeHash(data, offset, length);
            }
        }

        private static byte[] Merge(byte[] data1, byte[] data2)
        {
            byte[] tmp = new byte[data1.Length + data2.Length];
            for (int i = 0; i < data1.Length; i++) tmp[i] = data1[i];
            for (int i = 0; i < data2.Length; i++) tmp[data1.Length + i] = data2[i];
            return tmp;
        }
    }


    public enum HashMethod
    {
        Simple,
        Lazy,
        Full
    }
}