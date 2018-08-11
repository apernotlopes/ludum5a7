using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxxxtData : FileData
{
	public string Text;
	
	public TxxxtData(string fileName, string text) : base(fileName)
	{
		Extension = FileExtensions.TXXXT;

		Text = text;

		Size = FileSizeCalculator.GetRandFileSize(Extension);
	}
}
