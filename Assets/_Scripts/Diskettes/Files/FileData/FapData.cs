﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FapData : FileData
{
    public VideoClip Clip;
    
    public FapData(string fileName, FileCategory category, VideoClip clip) : base(fileName, category)
    {
        Extension = FileExtensions.FAP;

        Clip = clip;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
