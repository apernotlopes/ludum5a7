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
	public int TransferSpeed;
	private float sizeToTransfer;
	public bool isTransferring;

	public HardDrive HardDrive;

	public ExplorerScreen Explorer;
	public FileViewer Viewer;
	public LoadingWindow Loading;

	public CanvasGroup ExplorerCanvas;
	public CanvasGroup ViewerCanvas;
	public CanvasGroup LoadingCanvas;

	private bool lastScreen;

	private void Awake()
	{
		Instance = this;
	}


	public void Clear()
	{
		ExplorerCanvas.DOFade(0f, 0f);
		ExplorerCanvas.interactable = false;
		ExplorerCanvas.blocksRaycasts = false;
		ViewerCanvas.DOFade(0f, 0f);
		ViewerCanvas.interactable = false;
		ViewerCanvas.blocksRaycasts = false;
		LoadingCanvas.DOFade(0f, 0f);
		LoadingCanvas.interactable = false;
		LoadingCanvas.blocksRaycasts = false;
	}
	
	public void DisplayViewer(FileData file)
	{
		Clear();
		
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
		DisplayExplorer(lastScreen);
	}
	
	public void DisplayExplorer(bool isDrive)
	{
		lastScreen = isDrive;
		
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

	public void TransferForReal()
	{
		LoadingCanvas.DOFade(0f, 0f);
		LoadingCanvas.blocksRaycasts = false;
		LoadingCanvas.interactable = false;
		
		var file = Viewer.currentFile;
		var transferSuccess = false;

		if (lastScreen) // check si egal a true soit isDrive
		{
			if (!Reader.Loaded)
			{
				Debug.Log("No Floppy!");
				return;
			}
			
			if (Reader.LoadedDisck.AddFile(file))
			{
				HardDrive.Files.Remove(file);
				transferSuccess = true;
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
				transferSuccess = true;
			}
			else
			{
				print("Not enough space!");
			}
		}

		if (transferSuccess)
		{
			// TRANSFER SUCCESS MESSAGE
			
			CloseViewer();
		}
	}

	public void Transfer()
	{
		if (Reader.Loaded)
		{
			Debug.Log("transferring...");
			
			LoadingCanvas.DOFade(1f, 0f);
			LoadingCanvas.blocksRaycasts = true;
			LoadingCanvas.interactable = true;

			sizeToTransfer = Viewer.currentFile.Size;
			isTransferring = true;
		}
	}
	
	private void Update()
	{
		if (isTransferring)
		{
			if (sizeToTransfer > 0 && !Reader.Loaded)
			{
				sizeToTransfer = 0;
				isTransferring = false;
				// ERROR, floppy removed, CANCEL
			}
			if (sizeToTransfer > 0 && Reader.Loaded)
			{
				sizeToTransfer -= TransferSpeed * Time.deltaTime;
				Loading.UpdateBarProgress((float)sizeToTransfer/Viewer.currentFile.Size);
			}

			if (sizeToTransfer <= 0 && Reader.Loaded)
			{
				isTransferring = false;
				// Transfer success
				sizeToTransfer = 0;
				TransferForReal();
			}
		}
		
	}
	
}
