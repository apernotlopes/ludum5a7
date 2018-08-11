using System;
using UnityEngine;

public class FileSizeCalculator : MonoBehaviour 
{
	private void Start()
	{
		Debug.Log(BytesToString(1440000));

		var deb = "";
		FileExtensions[] fex = (FileExtensions[]) Enum.GetValues(typeof(FileExtensions));

		for (int l = 0; l < 10; l++)
		{
			for (int i = 0; i < fex.Length; i++)
			{
				deb += fex[i].ToString("f") + " / Size = " + BytesToString(GetRandFileSize(fex[i])) + "\n";
			}
		
			Debug.Log(deb);
		}
	}
	
	static string GetFileSize(string fileName)
	{
		var fileInfo  = new System.IO.FileInfo(String.Concat(Application.dataPath, "/Resources/", fileName));

		return BytesToString(fileInfo.Length);
	}

	public static int GetRandFileSize(FileExtensions ext)
	{
		var fileSize = 0;
		
		switch (ext)
		{
			case FileExtensions.JIF:
				fileSize = 900000;

				fileSize += UnityEngine.Random.Range(-350000, 50000);
				break;
			case FileExtensions.TXXXT:
				fileSize = 1000;

				fileSize += UnityEngine.Random.Range(-800, 800);
				break;
			case FileExtensions.FAP:
				fileSize = 1474560;

				fileSize += UnityEngine.Random.Range(-150000, 10000);
				break;
			case FileExtensions.LEL:
				fileSize = UnityEngine.Random.Range(200000, 1300000);
				break;
			default:
				break;
		}

		return fileSize;
	}

	public static String BytesToString(long byteCount)
	{
		string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
		if (byteCount == 0)
			return "0" + suf[0];
		long bytes = Math.Abs(byteCount);
		int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
		double num = Math.Round(bytes / Math.Pow(1024, place), 2);
		return (Math.Sign(byteCount) * num).ToString() + suf[place];
	}
}
