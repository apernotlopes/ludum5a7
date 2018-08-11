using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;

public class FileGenerator : MonoBehaviour
{
    public Sprite[] Jifs;
    public AudioClip[] Lels;
    public VideoClip[] Faps;
    public TextAsset[] Txxxts;

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
    
//    public JifData GetRandJif()
//    {
//        
//    }
}
