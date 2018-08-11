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
    public int Size;

    public FileData()
    {
        
    }
    
    public FileData(string fileName)
    {
        FileName = fileName;
    }
}
