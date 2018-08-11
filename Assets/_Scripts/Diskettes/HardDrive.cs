using System;
using UnityEngine;

[Serializable]
public class HardDrive : Storage
{
	public Letters Letter;

	private void OnEnable()
	{
		totalSize = 1474560 * 2;

		Debug.Log("Drive " + Letter + " " + FileSizeCalculator.BytesToString(totalSize));
	}
}
