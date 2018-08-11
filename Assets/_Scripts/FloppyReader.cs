using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyReader : MonoBehaviour 
{
    public List<FileData> datas = new List<FileData>();

    Transform floppyAnchor;

    void Start()
    {
        floppyAnchor = transform.Find("FloppyAnchor");
    }

    public void ReadFloppyDisk(Floppy Disk)
    {

    }
}
