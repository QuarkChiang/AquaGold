using CommandLine;

namespace AquaGold
{
    internal class Program
    {
        public class Options
        {
            [Option('s', "SoucreDirectory", Required = true, HelpText = "比對來源資料夾")]
            public string? SoucreDirectory { get; set; }

            [Option('t', "TargetDirectory", Required = true, HelpText = "比對目標資料夾")]
            public string? TargetDirectory { get; set; }

            [Option('c', "CopyDirectory", Required = true, HelpText = "把差異複製到資料夾")]
            public string? CopyDirectory { get; set; }
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Parser.Default.ParseArguments<Options>(args).WithParsed(Run);
        }

        private static void Run(Options option)
        {
            if (!Directory.Exists(option.SoucreDirectory))
                throw new ArgumentNullException($"Directory Path Not Exists : {option.SoucreDirectory}");
            if (!Directory.Exists(option.TargetDirectory))
                throw new ArgumentNullException($"Directory Path Not Exists : {option.TargetDirectory}");

            DirectoryFile sourceDirectoryFile = new();
            DirectoryFile targetDirectoryFile = new();
            Console.WriteLine("SourceGenerateDirectoryFiles Start.");
            sourceDirectoryFile.GenerateDirectoryFiles(option.SoucreDirectory);
            Console.WriteLine("SourceGenerateDirectoryFiles Done.");
            Console.WriteLine("TargetGenerateDirectoryFiles Start.");
            targetDirectoryFile.GenerateDirectoryFiles(option.TargetDirectory);
            Console.WriteLine("TargetGenerateDirectoryFiles Done.");
            CompareFile compareFile = new();
            Console.WriteLine("CompareFiles Start.");
            compareFile.Compare(sourceDirectoryFile.DirectoryFiles, targetDirectoryFile.DirectoryFiles);
            Console.WriteLine("CompareFiles Done.");
            Console.WriteLine("CopyFiles Start.");
            CopyFiles.Copy(compareFile.DiffFiles, option.CopyDirectory);
            Console.WriteLine("CopyFiles Done.");
        }
    }
}