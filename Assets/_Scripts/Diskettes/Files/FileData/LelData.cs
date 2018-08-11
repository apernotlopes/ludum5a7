using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LelData : FileData
{
    public AudioClip Clip;
    
    public LelData(string fileName, FileExtensions ext, AudioClip clip) : base(fileName)
    {
        Extension = FileExtensions.LEL;

        Clip = clip;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
