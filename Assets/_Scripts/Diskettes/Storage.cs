using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class Storage : MonoBehaviour
{
	public List<FileData> Files;

	public int capacity;

	public void Format()
	{
		Files = new List<FileData>(0);
	}
	
	public bool AddFile(FileData data)
	{
		if (data.Size <= (capacity - GetUsedSpace()))
		{
            Files.Add(data);
            return true;
		}
		else
		{
            // not enough space!
            return false;
        }
	}

	public int GetUsedSpace()
	{
		var sum = 0;
		
		Files.ForEach(f => sum += f.Size);

		return sum;
	}

	public void DeleteRandFile()
	{
		Files.Remove(Files[Random.Range(0, Files.Count)]);
	}
}
