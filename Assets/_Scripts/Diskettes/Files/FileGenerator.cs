using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;
using Random = System.Random;

public class FileGenerator : MonoBehaviour
{
    public static FileGenerator Temple;
    
    public Sprite[] Jifs;
    public AudioClip[] Lels;
    public VideoClip[] Faps;
    public TextAsset[] Txxxts;

    private void Awake()
    {
        Temple = this;
    }

    private void Start()
    {
        LoadAll();
    }

    public void LoadAll()
    {
        Jifs = Resources.LoadAll("JIF", typeof(Sprite)).Cast<Sprite>().ToArray();
        Lels =  Resources.LoadAll("LEL", typeof(AudioClip)).Cast<AudioClip>().ToArray();
        Faps =  Resources.LoadAll("FAP", typeof(VideoClip)).Cast<VideoClip>().ToArray();
        Txxxts =  Resources.LoadAll("TXXXT", typeof(TextAsset)).Cast<TextAsset>().ToArray();
    }

    public FileData GetRandFile()
    {
        var v = UnityEngine.Random.value;

        if (v < 1 && v >= 0.6f)
        {
            return GetRandFile<JifData>();
        }
        if (v < 0.6f && v >= 0.35f)
        {
            return GetRandFile<TxxxtData>();
        }
        if(v < 0.35f && v >= 0.15f)
        {
            return GetRandFile<FapData>();
        }
        if(v < 0.15f)
        {
            return GetRandFile<LelData>();
        }

        return GetRandFile<TxxxtData>();
    }

    private FileData GetRandFile<T>() where T : FileData
    {
        var n = GetRandName();
        var fileData = new FileData(n, FindCategory(n));
        
        if (typeof(T) == typeof(JifData))
        {
            var file = Jifs[UnityEngine.Random.Range(0, Jifs.Length)];
            
            fileData = new JifData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(LelData))
        {
            var file = Lels[UnityEngine.Random.Range(0, Lels.Length)];
            
            fileData = new LelData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(FapData))
        {
            var file = Faps[UnityEngine.Random.Range(0, Faps.Length)];
            
            fileData = new FapData(n, FindCategory(n), file);
        }
        if (typeof(T) == typeof(TxxxtData))
        {
            var file = Txxxts[UnityEngine.Random.Range(0, Txxxts.Length)];
            
            fileData = new TxxxtData(n, FindCategory(n), file.text);
        }
        
        return fileData;
    }

    private FileCategory FindCategory(string fileName)
    {
        var categories = new string[] {"SOD", "C", "A", "SW", "P", "B", "O", "F"};

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
        var color = new string[]{"blue", "yellow", "red", "green"};

        var animal = new string[] {"panther", "turtle", "bird", "giraffe"};

        return color + "_" + animal;
    }

}
