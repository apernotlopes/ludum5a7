using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FapData : FileData
{
    public VideoClip Clip;
    
    public FapData(string fileName, VideoClip clip) : base(fileName)
    {
        Extension = FileExtensions.FAP;

        Clip = clip;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
