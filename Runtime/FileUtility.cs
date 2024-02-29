using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Ryuu.Unity.Persistence
{
    public static class FileUtility
    {
        public static string Read(string path)
        {
            string fullPath = Combine(Application.persistentDataPath, path);
            return File.ReadAllText(fullPath);
        }

        public static async Task<string> ReadAsync(string path, Action afterRead = null)
        {
            string fullPath = Combine(Application.persistentDataPath, path);
            string text = await File.ReadAllTextAsync(fullPath);
            afterRead?.Invoke();
            return text;
        }

        public static void Write(string path, string contents)
        {
            string fullPath = Combine(Application.persistentDataPath, path);
            string directoryPath = Path.GetDirectoryName(fullPath);
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentException($"Invalid file path, path={path}");
            }

            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(fullPath, contents);
        }

        public static async void WriteAsync(string path, string contents, Action afterWrite = null)
        {
            string fullPath = Combine(Application.persistentDataPath, path);
            string directoryPath = Path.GetDirectoryName(fullPath);
            if (string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentException($"Invalid file path, path={path}");
            }

            Directory.CreateDirectory(directoryPath);
            await File.WriteAllTextAsync(fullPath, contents);
            afterWrite?.Invoke();
        }

        private static string Combine(string path1, string path2)
        {
            return Path.Combine(ReplaceBackSlashWithForwardSlash(path1), TrimStart(path2));
        }

        private static string TrimStart(string path)
        {
            return ReplaceBackSlashWithForwardSlash(path).TrimStart('/');
        }

        private static string ReplaceBackSlashWithForwardSlash(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}