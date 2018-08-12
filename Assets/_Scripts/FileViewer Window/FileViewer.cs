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
	
	public Sprite baseIcon;
	public Sprite lelIcon;
	
	public JifData debugJif;

	public FapData debugFap;

	public LelData debugLel;

	public TxxxtData debugTxxxt;
//
//	private void Start()
//	{
//		Display(debugTxxxt);
//	}

	public void Display(JifData jif)
	{
		Clear();

		titleBarText.text = jif.FileName + "." + jif.Extension.ToString("f");
		
		jifAnchor.DOFade(1f, 0f);
		jifAnchor.sprite = jif.Image;
	}

	public void Display(FapData fap)
	{
		Clear();

		titleBarText.text = fap.FileName + "." + fap.Extension.ToString("f");
		
		fapAnchor.transform.GetChild(0).gameObject.SetActive(true);
		fapAnchor.clip = fap.Clip;
		fapAnchor.Play();
	}

	public void Display(LelData lel)
	{
		Clear();

		titleBarText.text = lel.FileName + "." + lel.Extension.ToString("f");

		jifAnchor.DOFade(1f, 0f);
		jifAnchor.sprite = lelIcon;
		lelAnchor.clip = debugLel.Clip;
		lelAnchor.Play();
		lelAnchor.loop = true;
	}

	public void Display(TxxxtData txxxt)
	{
		Clear();
		
		titleBarText.text = txxxt.FileName + "." + txxxt.Extension.ToString("F");

		txxxtAnchor.text = txxxt.Text;
	}

	private void Clear()
	{
		jifAnchor.DOFade(0f, 0f);
		lelAnchor.Stop();
		fapAnchor.Stop();
		fapAnchor.transform.GetChild(0).gameObject.SetActive(false);
		txxxtAnchor.text = "";
	}
}
