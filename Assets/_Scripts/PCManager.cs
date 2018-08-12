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
	public bool isTransferring, isLoading;

	public HardDrive hardDrive;
	private int initialCapacity;

	public ExplorerScreen Explorer;
	public FileViewer Viewer;
	public LoadingWindow Loading;
	public MessageWindow Message;

	public CanvasGroup ExplorerCanvas;
	public CanvasGroup ViewerCanvas;
	public CanvasGroup LoadingCanvas;
	public CanvasGroup MessageCanvas;

    public AudioClip transferSound;
    public AudioClip errorSound;

    internal bool viewerActive = false;
    internal bool isHardDrive;

    private void Awake()
	{
		Instance = this;
	}

    public IEnumerator LateStart()
    {
        yield return 0;
        DisplayExplorer(true);
	    initialCapacity = hardDrive.capacity;
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

		isLoading = true;
		viewerActive = true;

		ViewerCanvas.DOFade(1f, 0f);
		
		StartCoroutine(DelayedDisplay(file));
	}

	private IEnumerator DelayedDisplay(FileData file)
	{
		switch (file.Extension)
		{
			case FileExtensions.JIF:
				yield return new WaitForSeconds(0.4f);

				Viewer.Display((JifData)file);
				break;
			case FileExtensions.TXXXT:
				yield return new WaitForSeconds(0.2f);

				Viewer.Display((TxxxtData)file);
				break;
			case FileExtensions.FAP:
				yield return new WaitForSeconds(0.6f);

				Viewer.Display((FapData)file);
				break;
			case FileExtensions.LEL:
				yield return new WaitForSeconds(0.2f);

				Viewer.Display((LelData)file);
				break;
		}
		
		ViewerCanvas.interactable = true;
		ViewerCanvas.blocksRaycasts = true;

		isLoading = false;
	}

	public void VirusPropagation()
	{
		// Check if used space > capacity

		hardDrive.capacity -= (int)(initialCapacity * 0.005f);

		if (hardDrive.GetUsedSpace() >= hardDrive.capacity)
		{
			var icon = Explorer.IconHolder.GetChild(0)?.GetComponent<FileIcon>();
			if (icon != null)
			{
				hardDrive.Files.Remove(icon.fileData);
				Destroy(Explorer.IconHolder.GetChild(0).GetComponent<FileIcon>());
			}
		}
		
		RefreshSizeDisplay(isHardDrive);
	}

	public void CloseViewer()
	{
		Viewer.Clear();
		DisplayExplorer(isHardDrive);
	}

	public void CloseLoading()
	{
		LoadingCanvas.DOFade(0f, 0f);
		LoadingCanvas.blocksRaycasts = false;
		LoadingCanvas.interactable = false;
		
		DisplayExplorer(isHardDrive);
	}

	public void CloseMessage()
	{
		if (isTransferring)
		{
			CancelTransfer();
		}
		
		DisplayExplorer(true);
	}
	
	public void DisplayExplorer(bool isDrive)
	{
        viewerActive = false;

        isHardDrive = isDrive;
		
		Clear();

		isLoading = true;
		
		ExplorerCanvas.DOFade(1f, 0f);
		RefreshSizeDisplay(isDrive);
		
		StartCoroutine(DelayExplorer(isDrive));
	}

	private void RefreshSizeDisplay(bool isDrive)
	{
		if (isDrive)
		{
			Explorer.SizeDisplay.text = FileSizeCalculator.BytesToString(hardDrive.GetUsedSpace()) + "/"
																								   +
																								   FileSizeCalculator
																									   .BytesToString(
																										   hardDrive
																											   .capacity);
		}
		else if(Reader.Loaded)
		{
			Explorer.SizeDisplay.text = FileSizeCalculator.BytesToString(Reader.LoadedDisck.GetUsedSpace()) + "/"
			                                                                                                +
			                                                                                                FileSizeCalculator
				                                                                                                .BytesToString(
					                                                                                                Reader.LoadedDisck
						                                                                                                .capacity);
		}

	}

	private IEnumerator DelayExplorer(bool isDrive)
	{
		yield return new WaitForSeconds(0.5f);
		
		isLoading = false;
		
		if (isDrive)
		{
			Explorer.Display(hardDrive.Files);

		}
		else
		{
			if (Reader.Loaded)
			{
				Explorer.Display(Reader.LoadedDisck.Files);
			}
			else
			{
				// NO FLOPPY IN READER
				DisplayMessage("Insert Floppy!", true);
			}
		}
		
		ExplorerCanvas.interactable = true;
		ExplorerCanvas.blocksRaycasts = true;

	}

	public void TransferForReal()
	{
		CloseLoading();
		
		var file = Viewer.currentFile;
		var transferSuccess = false;

		if (isHardDrive) // check si egal a true soit isDrive
		{
			if (!Reader.Loaded)
			{
				Debug.Log("No Floppy!");
				DisplayMessage("Insert Floppy!", true);
			}
			
			if (Reader.LoadedDisck.AddFile(file))
			{
				hardDrive.Files.Remove(file);
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
			if (hardDrive.AddFile(file))
			{
				Reader.LoadedDisck.Files.Remove(file);
				transferSuccess = true;
				Reader.DEL.Blink(3);

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
            SoundManager.instance.PlayOnEmptyTrack(transferSound, true, false);
		}
		else
		{
			DisplayMessage("Insert Floppy!", true);
		}
	}

	public void CancelTransfer()
	{
		EndLoading();

		CloseLoading();
	}

	private void EndLoading()
	{
		sizeToTransfer = 0;
		isTransferring = false;
        SoundManager.instance.StopThisClip(transferSound, false);
    }

	private void Update()
	{
		if (isTransferring)
		{
			if (Reader.Loaded)
			{
				if (sizeToTransfer > 0) // loading
				{
					sizeToTransfer -= TransferSpeed * Time.deltaTime;
					Loading.UpdateBarProgress((float)Mathf.Abs(1 - (sizeToTransfer/Viewer.currentFile.Size)));
				}
				if (sizeToTransfer <= 0) // loading finished
				{
					EndLoading();
					
					TransferForReal();
				}
			}
			else
			{
				if (sizeToTransfer > 0) // loading interrupted
				{
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

        if(isError)
            SoundManager.instance.PlayOnEmptyTrack(errorSound, false, false);
	}
}
