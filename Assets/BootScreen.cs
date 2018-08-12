using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class BootScreen : MonoBehaviour
{
	public TextMeshProUGUI textBox;
	public string[] BootText;
	public string totalText;

	public bool isBooted;
	public bool bypassBoot;

	private void Start()
	{
		StartCoroutine(BootSequence());
	}

	public IEnumerator BootSequence()
	{
		if (!bypassBoot)
		{
			for (int i = 0; i < BootText.Length; i++)
			{
				if (BootText[i] == "")
				{
					totalText += BootText[i] + "\n\n";
					textBox.text = totalText;
					yield return new WaitForSeconds(Random.Range(0.3f,2f));
				}
				else
				{
					totalText += BootText[i] + "\n";
					Tween doT = textBox.DOText(totalText, 0.7f);
		
					yield return doT.WaitForCompletion();
				}
			}
		}
		yield return new WaitForSeconds(0.5f);
		
		this.gameObject.SetActive(false);

		isBooted = true;
		FindObjectOfType<GoalManager>().StartTuto();
		FindObjectOfType<FloppyDiskSpawner>().SpawnFloppyDisks();

	}
}
