using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Letters
{
	B = 0,
	C = 1,
	D = 2
}

public class PCManager : MonoBehaviour
{
	public HardDrive[] drives;

	public Floppy readingFloppy;

	public HardDrive GetDrive(Letters letter)
	{
		for (int i = 0; i < drives.Length; i++)
		{
			if (drives[i].Letter == letter)
			{
				return drives[i];
			}
		}

		return null;
	}
}
