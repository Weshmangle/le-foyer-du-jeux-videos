using Godot;
using System.Collections.Generic;
using System.Linq;


public class FileUtils
{
    public static string[] ListDirectory(string path, params string [] ignoreExtensions)
    {
        List<string> directories = new List<string>();
        
        Directory directory = new Directory();
        directory.Open(path);
        
        directory.ListDirBegin(true);
        var file = directory.GetNext();

        while(file != "")
        {
            var split = file.Split(".");

            if(!ignoreExtensions.ToList().Contains(split[split.Length - 1]))
            {
                directories.Add(file);
            }

            file = directory.GetNext();
        }

        return directories.ToArray();
    }
}