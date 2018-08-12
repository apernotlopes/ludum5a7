using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LelData : FileData
{
    public AudioClip Clip;
    
    public LelData(string fileName, FileCategory category, AudioClip clip) : base(fileName, category)
    {
        Extension = FileExtensions.LEL;

        Clip = clip;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
