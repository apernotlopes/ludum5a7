using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloppyData : ScriptableObject
{
    public string Title;
    public Sprite Cover;
    public Sprite SideCover;

    public List<FileData> Files;
}
