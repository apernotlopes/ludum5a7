using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileIcon : MonoBehaviour
{
	public Image Icon;
	public TextMeshProUGUI Label;

	public void Setup(Sprite sprite, string text)
	{
		Icon.sprite = sprite;
		Label.text = text;
	}
}
