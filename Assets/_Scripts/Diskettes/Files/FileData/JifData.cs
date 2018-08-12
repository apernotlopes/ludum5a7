using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JifData : FileData
{
    public Sprite Image;
    
    public JifData(string fileName, FileCategory category, Sprite image) : base(fileName, category)
    {
        Extension = FileExtensions.JIF;

        Image = image;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
