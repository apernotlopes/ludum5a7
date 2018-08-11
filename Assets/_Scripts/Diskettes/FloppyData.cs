using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloppyData 
{
    public string Title;

    public List<FileData> Files;

    public FloppyData()
    {
        
    }
    
    public FloppyData(string title)
    {
        Title = title;
    }
}
