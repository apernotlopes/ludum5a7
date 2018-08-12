using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingWindow : MonoBehaviour {

	public enum Storage {Web, Local, FloppyDisk};

	Storage from, to;
	public Sprite[] xButtonSprites;
	public Sprite[] transferIcons;
	public RectTransform fromRect, toRect;
	public Image fromIcon, toIcon;

	public Image loadingBar, xSprite;
	public bool xEnabled;
	public bool isTransferring;

	//transfer parameters
	public Image transferFile;
	private RectTransform transferFileRect;
	public float fileTransferDuration = 1.5f;
	Vector3 vPunch = new Vector3(0f, 0f, 25f);

	public void Start() 
	{
		transferFileRect = transferFile.GetComponent<RectTransform>();
	}

	public void UpdateBarProgress(float progress) 
	{
		loadingBar.fillAmount = Mathf.Floor(progress / 10f);
	}
	public void SetXButtonState(bool isEnabled)
	{
		int i = isEnabled ? 1 : 0;
		xSprite.sprite = xButtonSprites[i];
	}

	public void SetTransferIcons(Storage from, Storage to)
	{
		fromIcon.sprite = transferIcons[(int)from];
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.D))
		{
			isTransferring = true;
			StartCoroutine("TransferAnimation");
		}
	}

	public IEnumerator TransferAnimation() {
		Tween transferTween;
		transferFile.DOFade(1f, 0f);
		while (isTransferring == true) {
			transferTween = transferFileRect.DOAnchorPosX(toRect.anchoredPosition.x, fileTransferDuration).SetEase(Ease.InOutCubic);
			transferFileRect.DOPunchRotation(vPunch, fileTransferDuration);
			yield return transferTween.WaitForCompletion();
			yield return new WaitForSeconds(1f);
			transferFileRect.DOAnchorPosX(fromRect.anchoredPosition.x, 0f);
			yield return new WaitForSeconds(0.05f);
		}
		transferFile.DOFade(0f, 0f);
		transferFileRect.DOAnchorPosX(fromRect.anchoredPosition.x, 0f);
	}
}
