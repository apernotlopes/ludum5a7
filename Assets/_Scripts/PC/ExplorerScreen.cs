using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplorerScreen : MonoBehaviour
{
	public Transform IconHolder;

	public FileIcon IconPrefab;

	public TextMeshProUGUI SizeDisplay;

	public Image BGIcon;
	public Sprite[] folders;

	public TextMeshProUGUI FloppyTab;

	public Sprite[] Icons;

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
		if (PCManager.Instance.isLoading) return;
		
		Clear();

		PCManager.Instance.isLoading = true;

		BGIcon.sprite = PCManager.Instance.isHardDrive ? folders[0] : folders[1];

		StartCoroutine(DelayDisplay(files));
	}

	private IEnumerator DelayDisplay(List<FileData> files)
	{
		for (int i = 0; i < files.Count; i++)
		{
			DisplayIcon(files[i]);
			
			yield return new WaitForSeconds(0.1f);
		}
		PCManager.Instance.isLoading = false;
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
