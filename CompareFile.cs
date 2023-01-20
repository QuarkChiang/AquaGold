namespace AquaGold
{
    internal class CompareFile
    {
        public Dictionary<string, List<FileCrc>> DiffFiles { get; private set; } = new();

        public void Compare(Dictionary<string, List<FileCrc>> sourceFiles, Dictionary<string, List<FileCrc>> targetFiles)
        {
            foreach (var targetItem in targetFiles)
            {
                if (sourceFiles.TryGetValue(targetItem.Key, out List<FileCrc>? sourceValue))
                {
                    if (sourceValue == null)
                        throw new ArgumentNullException($"Key : {targetItem.Key}, SourceValue Is Null.");

                    var diffFileList = targetItem.Value.
                        Where(target => !sourceValue.Any(source => target.SHA512 == source.SHA512 && target.Info.Name.Equals(source.Info.Name)));

                    if (diffFileList != null)
                    {
                        foreach (var file in diffFileList)
                        {
                            if (DiffFiles.TryGetValue(targetItem.Key, out List<FileCrc>? value))
                                value.Add(file);
                            else
                            {
                                List<FileCrc> files = new()
                                {
                                    file
                                };
                                DiffFiles.Add(targetItem.Key, files);
                            }
                        }
                    }

                }
                else
                    DiffFiles.Add(targetItem.Key, targetItem.Value);
            }
        }
    }
}