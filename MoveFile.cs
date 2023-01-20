namespace AquaGold
{
    internal static class CopyFiles
    {
        public static void Copy(Dictionary<string, List<FileCrc>> moveFiles, string? moveTargetPath)
        {
            if (moveTargetPath == null)
                throw new ArgumentNullException(nameof(moveTargetPath));

            if (Directory.Exists(moveTargetPath))
            {
                Directory.Delete(moveTargetPath, true);
                Directory.CreateDirectory(moveTargetPath);
            }
            else
                Directory.CreateDirectory(moveTargetPath);

            foreach (var files in moveFiles)
            {
                if (files.Key.Equals(DirectoryFile.DIRECTORY_ROOT_NAME, StringComparison.Ordinal))
                {
                    for (int g = 0; g < files.Value.Count; g++)
                    {
                        FileCrc item = files.Value[g];
                        File.Copy(item.Info.FullName, Path.Combine(moveTargetPath, Path.GetFileName(item.Info.FullName)));
                    }
                }
                else
                {
                    if (!Directory.Exists(Path.Combine(moveTargetPath, files.Key)))
                        Directory.CreateDirectory(Path.Combine(moveTargetPath, files.Key));

                    for (int i = 0; i < files.Value.Count; i++)
                    {
                        FileCrc item = files.Value[i];
                        File.Copy(item.Info.FullName, Path.Combine(moveTargetPath, files.Key, Path.GetFileName(item.Info.FullName)));
                    }
                }
            }
        }
    }
}