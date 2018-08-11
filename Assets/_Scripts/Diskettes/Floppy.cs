using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : Storage
{
    public FloppyData floppyData;

    private void OnEnable()
    {
        totalSize = 1474560;
    }
}
