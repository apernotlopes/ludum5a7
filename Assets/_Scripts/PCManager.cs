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
    internal bool isHardrive;

    private void Awake()
	{
		Instance = this;
	}

    void Start()
    {
        StartCoroutine(LateStart()); 
    }

    IEnumerator LateStart()
    {
        yield return 0;
        DisplayExplorer(true);
    }

	public void Clear()
	{
        viewerActive = false;

        ExplorerCanvas.DOFade(0f, 0f);
		ExplorerCanvas.interactable = false;
		ExplorerCanvas.blocksRaycasts = false;
		ViewerCanvas.DOFade(0f, 0f);
		ViewerCanvas.interactable = false;
		ViewerCanvas.blocksRaycasts = false;
	}
	
	public void DisplayViewer(FileData file)
	{
		Clear();

        viewerActive = true;

        ViewerCanvas.DOFade(1f, 0f);
		ViewerCanvas.interactable = true;
		ViewerCanvas.blocksRaycasts = true;
		
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

	public void CloseViewer()
	{
		Viewer.Clear();
		DisplayExplorer(isHardrive);
	}
	
	public void DisplayExplorer(bool isDrive)
	{
		isHardrive = isDrive;
		
		Clear();
		
		ExplorerCanvas.DOFade(1f, 0f);
		ExplorerCanvas.interactable = true;
		ExplorerCanvas.blocksRaycasts = true;
		
		
		if (isDrive)
		{
			Explorer.Display(HardDrive.Files);

			Explorer.SizeDisplay.text = FileSizeCalculator.BytesToString(HardDrive.GetUsedSpace()) + "/"
			                                                                                 +
			                                                                                 FileSizeCalculator
				                                                                                 .BytesToString(
					                                                                                 HardDrive
						                                                                                 .capacity);

		}
		else
		{
			if (Reader.Loaded)
			{
				Explorer.Display(Reader.LoadedDisck.Files);
				
				Explorer.SizeDisplay.text = FileSizeCalculator.BytesToString(Reader.LoadedDisck.GetUsedSpace()) + "/"
				                                                                                 +
				                                                                                 FileSizeCalculator
					                                                                                 .BytesToString(
						                                                                                 Reader.LoadedDisck
							                                                                                 .capacity);
			}
			else
			{
				// NO FLOPPY IN READER
			}
		}
	}

	public void Transfer()
	{
		var file = Viewer.currentFile;

		if (isHardrive) // check si egal a true soit isDrive
		{
			if (!Reader.Loaded)
			{
				Debug.Log("No Floppy!");
				return;
			}
			
			if (Reader.LoadedDisck.AddFile(file))
			{
				HardDrive.Files.Remove(file);
			}
			else
			{
				print("Not enough space!");
			}
		}
		else
		{
			if (HardDrive.AddFile(file))
			{
				Reader.LoadedDisck.Files.Remove(file);
			}
			else
			{
				print("Not enough space!");
			}
		}
		
		DisplayExplorer(isHardrive);
	}
}
