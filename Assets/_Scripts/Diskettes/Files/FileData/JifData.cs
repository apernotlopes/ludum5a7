using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JifData : FileData
{
    public Sprite Image;
    
    public JifData(string fileName, Sprite image) : base(fileName)
    {
        Extension = FileExtensions.JIF;

        Image = image;

        Size = FileSizeCalculator.GetRandFileSize(Extension);
    }
}
