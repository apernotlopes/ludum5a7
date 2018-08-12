using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExplorerScreen : MonoBehaviour
{
	public Transform IconHolder;

	public FileIcon IconPrefab;

	public TextMeshProUGUI SizeDisplay;

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
		icon.fileData = fileData;
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
