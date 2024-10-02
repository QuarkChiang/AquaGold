using System.Security.Cryptography;

namespace AquaGold
{
    public struct FileCrc(FileInfo fileInfo, string sha512)
    {
        public FileInfo Info { get; private set; } = fileInfo;
        public string SHA512 { get; private set; } = sha512;
    }

    internal class DirectoryFile
    {
        public const string DIRECTORY_ROOT_NAME = "root";
        public Dictionary<string, List<FileCrc>> DirectoryFiles { get; private set; } = [];

        public void GenerateDirectoryFiles(string directoryPath)
        {
            foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(filePath);
                GenerateSHA512(filePath, out string sha512);

                if (fileInfo.DirectoryName is null)
                    throw new ArgumentNullException($"FilePaht : {filePath}, Directory Name Is Null.");

                string? currentPath;

                if (fileInfo.DirectoryName.Equals(directoryPath))
                    currentPath = DIRECTORY_ROOT_NAME;
                else
                    currentPath = fileInfo.DirectoryName.Replace(directoryPath, string.Empty).Remove(0, 1);

                if (!DirectoryFiles.TryGetValue(currentPath, out List<FileCrc>? value))
                {
                    List<FileCrc> files =
                    [
                        new FileCrc(fileInfo, sha512)
                    ];
                    DirectoryFiles.Add(currentPath, files);
                }
                else
                    value.Add(new FileCrc(fileInfo, sha512));
            }
        }

        private static void GenerateSHA512(string filePath, out string sha512)
        {
            using FileStream fs = File.OpenRead(filePath);
            using var hashAlgorithm = SHA512.Create();
            sha512 = Convert.ToBase64String(hashAlgorithm.ComputeHash(fs));
        }
    }
}