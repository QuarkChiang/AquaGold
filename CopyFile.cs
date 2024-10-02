namespace AquaGold
{
    internal static class CopyFiles
    {
        public static void Copy(Dictionary<string, List<FileCrc>> moveFiles, string? moveTargetPath)
        {
            ArgumentNullException.ThrowIfNull(moveTargetPath);

            if (Directory.Exists(moveTargetPath))
            {
                Directory.Delete(moveTargetPath, true);
            }
            else
            {
                Directory.CreateDirectory(moveTargetPath);
            }

            foreach (var files in moveFiles)
            {
                string destinationDirectory = Path.Combine(moveTargetPath, files.Key);
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }

                foreach (var fileCrc in files.Value)
                {
                    string destinationPath = Path.Combine(destinationDirectory, Path.GetFileName(fileCrc.Info.FullName));
                    File.Copy(fileCrc.Info.FullName, destinationPath);
                }
            }
        }
    }
}