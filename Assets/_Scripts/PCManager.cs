using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using UnityEngine;

public enum Letters
{
	B = 0,
	C = 1,
	D = 2
}

public class PCManager : MonoBehaviour
{
	public static PCManager Instance;
	
	public FloppyReader Reader;

	public HardDrive HardDrive;

	public ExplorerScreen Explorer;
	public FileViewer Viewer;

	public CanvasGroup ExplorerCanvas;
	public CanvasGroup ViewerCanvas;

    internal bool viewerActive = false;

    private void Awake()
	{
		Instance = this;
	}

	public void Clear()
	{
        viewerActive = false;
        explorerActive = false;

        ExplorerCanvas.DOFade(0f, 0f);
		ExplorerCanvas.interactable = false;
		ViewerCanvas.DOFade(0f, 0f);
		ViewerCanvas.interactable = false;
	}

	public void DisplayViewer(FileData file)
	{
		Clear();

        viewerActive = true;

        ViewerCanvas.DOFade(1f, 0f);
		ViewerCanvas.interactable = true;
		
		switch (file.Extension)
		{
			case FileExtensions.JIF:
				Viewer.Display((JifData)file);
				break;
			case FileExtensions.TXXXT:
				Viewer.Display((TxxxtData)file);
				break;
			case FileExtensions.FAP:
				Viewer.Display((FapData)file);
				break;
			case FileExtensions.LEL:
				Viewer.Display((LelData)file);
				break;
		}
	}
	
	public void DisplayExplorer(bool isDrive)
	{
		Clear();
		
		ExplorerCanvas.DOFade(1f, 0f);
		ExplorerCanvas.interactable = true;
		
		if (isDrive)
		{
			Explorer.Display(HardDrive.Files);
		}
		else
		{
			Explorer.Display(Reader.LoadedDisck.Files);
		}
	}

	public void Transfer(FileData file, bool toDrive)
	{
		
	}
}
