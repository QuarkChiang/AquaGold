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

                    var diffFileList = targetItem.Value
                        .Where(target => !sourceValue.Any(source => target.SHA512 == source.SHA512 && target.Info.Name.Equals(source.Info.Name)))
                        .ToList();

                    if (diffFileList.Count > 0)
                    {
                        if (DiffFiles.TryGetValue(targetItem.Key, out List<FileCrc>? value))
                            value.AddRange(diffFileList);
                        else
                            DiffFiles.Add(targetItem.Key, diffFileList);
                    }
                }
                else
                {
                    DiffFiles.Add(targetItem.Key, targetItem.Value);
                }
            }
        }
    }
}