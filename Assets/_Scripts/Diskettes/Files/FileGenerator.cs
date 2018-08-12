using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;
using Random = System.Random;
using Object = UnityEngine.Object;

public class FileGenerator : MonoBehaviour
{
    public static FileGenerator instance;
    
    public Sprite[] Jifs;
    public AudioClip[] Lels;
    public VideoClip[] Faps;
    public TextAsset[] Txxxts;

    public Dictionary<FileCategory, FileData[]> allData = new Dictionary<FileCategory, FileData[]>();
    public string[] categories = new string[] { "SOD", "C", "A", "SW", "P", "B", "O", "F", "M" };

    public int Lenght = 0;
    public List<FileData> dataToSpawn = new List<FileData>();

    private void Awake()
    {
        instance = this;
        LoadAll();
    }

    private void Start()
    {
        
    }

    public void LoadAll()
    {
        Jifs = Resources.LoadAll("JIF", typeof(Sprite)).Cast<Sprite>().ToArray();
        Lels =  Resources.LoadAll("LEL", typeof(AudioClip)).Cast<AudioClip>().ToArray();
        Faps =  Resources.LoadAll("FAP", typeof(VideoClip)).Cast<VideoClip>().ToArray();
        Txxxts =  Resources.LoadAll("TXXXT", typeof(TextAsset)).Cast<TextAsset>().ToArray();

        Lenght = Jifs.Length + Lels.Length + Faps.Length + Txxxts.Length;

        for (int i = 0; i < categories.Length; i++)
        {
            List<FileData> data = GetFilesFromCategorie(categories[i]);
            allData.Add((FileCategory)i, data.ToArray());
            dataToSpawn.AddRange(data);
        }
    }

    List<FileData> GetFilesFromCategorie(string categorie)
    {
        List<FileData> files = new List<FileData>();

        for (int i = 0; i < Jifs.Length; i++)
        {
            string name = Jifs[i].name;
            if (name.StartsWith(categorie))
            {
                files.Add(GetFile<JifData>(name, i));
            }
        }

        for (int i = 0; i < Lels.Length; i++)
        {
            string name = Lels[i].name;
            if (name.StartsWith(categorie))
            {
                files.Add(GetFile<LelData>(name, i));
            }
        }

        for (int i = 0; i < Faps.Length; i++)
        {
            string name = Faps[i].name;
            if (name.StartsWith(categorie))
            {
                files.Add(GetFile<FapData>(name, i));
            }
        }

        for (int i = 0; i < Txxxts.Length; i++)
        {
            string name = Txxxts[i].name;
            if (name.StartsWith(categorie))
            {
                files.Add(GetFile<TxxxtData>(name, i));
            }
        }

        return files;
    }

    FileData GetFile<T>(string name, int index) where T : FileData
    {
        var n = name;
        var fileData = new FileData(n, FindCategory(n));
        
        if (typeof(T) == typeof(JifData))
        {
            var file = Jifs[index];
            
            fileData = new JifData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(LelData))
        {
            var file = Lels[index];
            
            fileData = new LelData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(FapData))
        {
            var file = Faps[index];
            
            fileData = new FapData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(TxxxtData))
        {
            var file = Txxxts[index];
            
            fileData = new TxxxtData(n, FindCategory(n), file.text);
        }
        
        return fileData;
    }

    private FileCategory FindCategory(string fileName)
    {
        for (int i = 0; i < categories.Length; i++)
        {
            if (fileName.StartsWith(categories[i]))
            {
                return (FileCategory) i;
            }
        }

        return FileCategory.Misc;
    }
    
    private string GetRandName()
    {
        //var color = new string[]{"blue", "yellow", "red", "green"};

        //var animal = new string[] {"panther", "turtle", "bird", "giraffe"};

        //return color + "_" + animal;
        return "Random";
    }

}
