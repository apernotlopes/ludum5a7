using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerScreen : MonoBehaviour
{
	public Transform IconHolder;

	public FileIcon IconPrefab;

	public Sprite[] Icons;

	public JifData debugJif;

	public Sprite GetIcon(FileExtensions ext)
	{
		return Icons[(int) ext];
	}

	private void DisplayIcon(FileData fileData)
	{
		FileIcon icon = Instantiate(IconPrefab, IconHolder);
		icon.Setup(GetIcon(fileData.Extension), fileData.FileName);
	}

	public void Display(List<FileData> files)
	{
		for (int i = 0; i < files.Count; i++)
		{
			DisplayIcon(files[i]);
		}
	}

	public void Clear()
	{
		
	}
}
