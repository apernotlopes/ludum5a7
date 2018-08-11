using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class FloppyData : ScriptableObject
{
    public string Title;

    public List<FileData> Files;

    public FloppyData(string title)
    {
        Title = title;
    }
}
