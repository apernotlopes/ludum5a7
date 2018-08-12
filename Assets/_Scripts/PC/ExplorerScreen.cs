using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplorerScreen : MonoBehaviour
{
	public Transform IconHolder;

	public FileIcon IconPrefab;

	public TextMeshProUGUI SizeDisplay;

	public TextMeshProUGUI FloppyTab;

	public Sprite[] Icons;

	public JifData debugJif;

	public Sprite GetIcon(FileExtensions ext)
	{
		return Icons[(int) ext];
	}

	private void DisplayIcon(FileData fileData)
	{
		FileIcon icon = Instantiate(IconPrefab, IconHolder);
		icon.Setup(GetIcon(fileData.Extension), String.Concat(RemoveFileNamePrefix(fileData.FileName), ".", fileData.Extension.ToString("F")));
		icon.fileData = fileData;
	}

	private String RemoveFileNamePrefix(string fileName)
	{
		var cats = FileGenerator.instance.categories;
		
		for (int i = 0; i < cats.Length; i++)
		{
			if (fileName.Trim().StartsWith(cats[i]))
			{
				int lastLocation = fileName.IndexOf("_");
				if (lastLocation >= 0) 
				{
					fileName =  fileName.Substring(lastLocation + 1);
				}

				return fileName;
			}
		}
		return fileName;
	}

	public void Display(List<FileData> files)
	{
		Clear();
		
		for (int i = 0; i < files.Count; i++)
		{
			DisplayIcon(files[i]);
		}
	}

	public void Clear()
	{
		var ic = IconHolder.GetComponentsInChildren<FileIcon>();

		for (int i = ic.Length - 1; i >= 0; i--)
		{
			Destroy(ic[i].gameObject);
		}
	}
}
