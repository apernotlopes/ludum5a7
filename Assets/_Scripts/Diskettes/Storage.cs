using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Storage : MonoBehaviour
{
	public List<FileData> Files;

	public int totalSize;

	public void Format()
	{
		Files = new List<FileData>(0);
	}
	
	public void AddFile(FileData data)
	{
		if (data.Size < totalSize)
		{
			// add file
		}
		else
		{
			// not enough space!
		}
	}

	public int GetUsedSpace()
	{
		var sum = 0;
		
		Files.ForEach(f => sum += f.Size);

		return sum;
	}
}
