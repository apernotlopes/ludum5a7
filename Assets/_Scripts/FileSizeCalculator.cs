using System;
using UnityEngine;

public class FileSizeCalculator : MonoBehaviour 
{
	private void Start()
	{
		GetFileSize("fronde.png");
	}

	public string GetFileSize(string fileName)
	{
		var fileInfo  = new System.IO.FileInfo(String.Concat(Application.dataPath, "/Resources/", fileName));

		return BytesToString(fileInfo.Length);
	}
	
	String BytesToString(long byteCount)
	{
		string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
		if (byteCount == 0)
			return "0" + suf[0];
		long bytes = Math.Abs(byteCount);
		int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
		double num = Math.Round(bytes / Math.Pow(1024, place), 1);
		return (Math.Sign(byteCount) * num).ToString() + suf[place];
	}
}
