using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloppyData 
{
    public string Title;
    public Sprite Cover;
    public Sprite SideCover;

    public List<FileData> Files;
}
