using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
	public float sizeToTransfer;
	public bool isTransferring;

	public HardDrive HardDrive;

	public ExplorerScreen Explorer;
	public FileViewer Viewer;
	public LoadingWindow Loading;
	public MessageWindow Message;

	public CanvasGroup ExplorerCanvas;
	public CanvasGroup ViewerCanvas;
	public CanvasGroup LoadingCanvas;
	public CanvasGroup MessageCanvas;

	private bool lastScreen;

	private void Awake()
	{
		Instance = this;
	}


	public void Clear()
	{
		HideAll();
		BlockAll();
	}

	private void HideAll()
	{
		
		ExplorerCanvas.DOFade(0f, 0f);
		ViewerCanvas.DOFade(0f, 0f);
		LoadingCanvas.DOFade(0f, 0f);
		MessageCanvas.DOFade(0f, 0f);
	}

	private void BlockAll()
	{
		ExplorerCanvas.interactable = false;
		ExplorerCanvas.blocksRaycasts = false;
		ViewerCanvas.interactable = false;
		ViewerCanvas.blocksRaycasts = false;
		LoadingCanvas.interactable = false;
		LoadingCanvas.blocksRaycasts = false;
		MessageCanvas.interactable = false;
		MessageCanvas.blocksRaycasts = false;
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

	public void CloseLoading()
	{
		LoadingCanvas.DOFade(0f, 0f);
		LoadingCanvas.blocksRaycasts = false;
		LoadingCanvas.interactable = false;
		
		DisplayExplorer(lastScreen);
	}

	public void CloseMessage()
	{
		DisplayExplorer(true);
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
				DisplayMessage("Insert Floppy!", true);
			}
		}
	}

	public void TransferForReal()
	{
		CloseLoading();
		
		var file = Viewer.currentFile;
		var transferSuccess = false;

		if (lastScreen) // check si egal a true soit isDrive
		{
			if (!Reader.Loaded)
			{
				Debug.Log("No Floppy!");
				DisplayMessage("Insert Floppy!", true);
			}
			
			if (Reader.LoadedDisck.AddFile(file))
			{
				HardDrive.Files.Remove(file);
				transferSuccess = true;
			}
			else
			{
				print("Not enough space!");
				DisplayMessage("Not enough space!", true);
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
				DisplayMessage("Not enough space!", true);
			}
		}

		if (transferSuccess)
		{
			// TRANSFER SUCCESS MESSAGE
			DisplayMessage("Transfer success!", false);
		}
	}

	public void Transfer()
	{
		Viewer.fapAnchor.Stop();
		Viewer.fapAnchor.transform.GetChild(0).gameObject.SetActive(false);
		Viewer.lelAnchor.Stop();
		
		if (Reader.Loaded)
		{
			Debug.Log("transferring...");
			
			LoadingCanvas.DOFade(1f, 0f);
			LoadingCanvas.blocksRaycasts = true;
			LoadingCanvas.interactable = true;

			sizeToTransfer = Viewer.currentFile.Size;
			isTransferring = true;
			
			
		}
		else
		{
			DisplayMessage("Insert Floppy!", true);
		}
	}

	public void CancelTransfer()
	{
		sizeToTransfer = 0;
		isTransferring = false;

		CloseLoading();
	}

	private void Update()
	{
		if (isTransferring)
		{
			if (Reader.Loaded)
			{
				if (sizeToTransfer > 0)
				{
					sizeToTransfer -= TransferSpeed * Time.deltaTime;
					Loading.UpdateBarProgress((float)Mathf.Abs(1 - (sizeToTransfer/Viewer.currentFile.Size)));
				}
				if (sizeToTransfer <= 0)
				{
					isTransferring = false;
					sizeToTransfer = 0;
					
					TransferForReal();
				}
			}
			else
			{
				if (sizeToTransfer > 0)
				{
					sizeToTransfer = 0;
					isTransferring = false;
					// ERROR, floppy removed, CANCEL
					CancelTransfer();
					DisplayMessage("Floppy removed!", true);
				}
			}
		}
	}

	public void DisplayMessage(string text, bool isError)
	{
		Clear();
		
		MessageCanvas.DOFade(1f, 0f);
		MessageCanvas.blocksRaycasts = true;
		MessageCanvas.interactable = true;
		
		Message.SetWindow(text, isError);
	}
}
