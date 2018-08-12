using System;
using UnityEngine;

[Serializable]
public class HardDrive : Storage
{
	public Letters Letter;

	private void Start()
	{
		capacity = (int)(Mathf.Pow(2, 20)) * 10;

		Debug.Log("Drive " + Letter + " " + FileSizeCalculator.BytesToString(capacity));
	}
}
