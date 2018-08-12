using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[Serializable]
public class FileData
{
    public string FileName;
    public FileExtensions Extension;
    public FileCategory Category;
    public int Size;

    public FileData()
    {
        
    }
    
    public FileData(string fileName, FileCategory category)
    {
        FileName = fileName;
        Category = category;
    }
}
