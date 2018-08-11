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
    public Sprite[] Jifs;
    public AudioClip[] Lels;
    public VideoClip[] Faps;
    public TextAsset[] Txxxts;

    private string GetRandName()
    {
        var color = new string[]{"blue", "yellow", "red", "green"};

        var animal = new string[] {"panther", "turtle", "bird", "giraffe"};

        return color + "_" + animal;
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

    public FileData GetRandFile<T>() where T : FileData
    {
        var fileData = new FileData(GetRandName());
        
        if (typeof(T) == typeof(JifData))
        {
            var file = Jifs[UnityEngine.Random.Range(0, Jifs.Length)];
        
            fileData = new JifData(GetRandName(), file);

        }
        if (typeof(T) == typeof(LelData))
        {
            var file = Lels[UnityEngine.Random.Range(0, Lels.Length)];
            
            fileData = new LelData(GetRandName(), file);
        }
        if (typeof(T) == typeof(FapData))
        {
            var file = Faps[UnityEngine.Random.Range(0, Faps.Length)];
            
            fileData = new FapData(GetRandName(), file);
        }
        if (typeof(T) == typeof(TxxxtData))
        {
            var file = Txxxts[UnityEngine.Random.Range(0, Txxxts.Length)];
            
            fileData = new TxxxtData(GetRandName(), file.text);
        }
        
        return fileData;
    }

}
