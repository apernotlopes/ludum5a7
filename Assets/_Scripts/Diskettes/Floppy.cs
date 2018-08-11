using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : Storage
{
    public void SetData(FloppyData data)
    {
        Files = data.Files;
        name = "Floppy_" + data.Title;
    }
}
