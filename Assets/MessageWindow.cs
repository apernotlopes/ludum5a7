using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
	public TextMeshProUGUI titleBar;
	public TextMeshProUGUI message;

	public void SetWindow(string content, bool isError)
	{
		titleBar.text = isError ? "ERROR" : "WARNING";
		message.text = content;
	}
}
