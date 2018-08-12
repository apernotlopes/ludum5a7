using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FileViewer : MonoBehaviour
{
	public TextMeshProUGUI titleBarText;
	public Image jifAnchor;
	public VideoPlayer fapAnchor;
	public AudioSource lelAnchor;
	public TextMeshProUGUI txxxtAnchor;

	public FileData currentFile;
	
	public Sprite baseIcon;
	public Sprite lelIcon;
	
	public void Display(JifData jif)
	{
		Clear();

		currentFile = jif;

		titleBarText.text = jif.FileName + "." + jif.Extension.ToString("f");
		
		jifAnchor.DOFade(1f, 0f);
		jifAnchor.sprite = jif.Image;
	}

	public void Display(FapData fap)
	{
		Clear();

		currentFile = fap;

		titleBarText.text = fap.FileName + "." + fap.Extension.ToString("f");
		
		fapAnchor.transform.GetChild(0).gameObject.SetActive(true);
		fapAnchor.clip = fap.Clip;
		fapAnchor.Play();
	}

	public void Display(LelData lel)
	{
		Clear();
		
		Debug.Log("Playing lel");

		currentFile = lel;

		titleBarText.text = lel.FileName + "." + lel.Extension.ToString("f");

		jifAnchor.DOFade(1f, 0f);
		jifAnchor.sprite = lelIcon;
		lelAnchor.clip = lel.Clip;
		lelAnchor.Play();
		lelAnchor.loop = true;
	}

	public void Display(TxxxtData txxxt)
	{
		Clear();

		currentFile = txxxt;
		
		titleBarText.text = txxxt.FileName + "." + txxxt.Extension.ToString("F");

		txxxtAnchor.text = txxxt.Text;
	}

	public void Clear()
	{
		currentFile = null;
		
		jifAnchor.DOFade(0f, 0f);
		lelAnchor.Stop();
		fapAnchor.Stop();
		fapAnchor.transform.GetChild(0).gameObject.SetActive(false);
		txxxtAnchor.text = "";
	}
}
