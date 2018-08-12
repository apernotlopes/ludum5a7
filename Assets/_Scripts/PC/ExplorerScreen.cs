﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorerScreen : MonoBehaviour
{
	public Transform IconHolder;

	public FileIcon IconPrefab;

	public Sprite[] Icons;

	public JifData debugJif;

	private void Start()
	{
		DisplayIcon(debugJif);
	}

	public Sprite GetIcon(FileExtensions ext)
	{
		return Icons[(int) ext];
	}
	
	public void DisplayIcon(FileData fileData)
	{
		FileIcon icon = Instantiate(IconPrefab, IconHolder);
		icon.Setup(GetIcon(fileData.Extension), fileData.FileName);
	}
}