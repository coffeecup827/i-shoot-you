using System;
using System.Text;

namespace Tools;

public static class AudioInitialiser
{
    private const string audioFolderPath = ".\\assets\\audio";
    private const string outputFilePath = ".\\core\\audio\\AudioCue.cs";

    private static readonly HashSet<string> audioFormats = [".wav", ".mp3"];

    public static void Run()
    {
        Console.WriteLine("[TOOL] Running Audio Initialiser...");

        HashSet<string> sounds = new HashSet<string>();
        var audioSubFolders = Directory.GetDirectories(audioFolderPath).ToList();

        audioSubFolders.ForEach(subfolder => AddSound(subfolder, sounds));
        Console.WriteLine(string.Join(", ", sounds));

        var sb = new StringBuilder();
        sb.AppendLine("// ========================================== ");
        sb.AppendLine("// AUTO-GENERATED AT BUILD TIME. DO NOT EDIT.");
        sb.AppendLine("// ========================================== ");
        sb.AppendLine("public static class AudioCue");
        sb.AppendLine("{");
        foreach (var key in sounds)
        {
            string pascalIdentifier = ToPascalCase(key);
            sb.AppendLine($"    public const string {pascalIdentifier} = \"{key}\";");
        }
        sb.AppendLine("}");

        File.WriteAllText(outputFilePath, sb.ToString());
        
        Console.WriteLine("[TOOL] Audio Initialiser ");
    }

    private static void AddSound(string subfolder, HashSet<string> sounds)
    {
        var validAudioFiles = Directory.GetFiles(subfolder).Where(x => audioFormats.Contains(Path.GetExtension(x)));
        foreach (var audioFile in validAudioFiles)
        {
            sounds.Add($"{Path.GetFileName(Path.GetDirectoryName(audioFile))}_{Path.GetFileNameWithoutExtension(audioFile)}".ToLower());
        }
    }

    private static string ToPascalCase(string str)
    {
        var words = str.Split("_", StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < words.Length; i++)
        {
            words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
        }
        return string.Join("", words);
    }
}