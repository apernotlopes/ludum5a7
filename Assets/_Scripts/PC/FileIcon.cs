using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FileIcon : MonoBehaviour
{
	public Image Icon;
	public TextMeshProUGUI Label;
	public FileData fileData;

	public void Setup(Sprite sprite, string text)
	{
		Icon.sprite = sprite;
		Label.text = text;
	}

	public void OnClick()
	{
		PCManager.Instance.DisplayViewer(fileData);
	}
}
