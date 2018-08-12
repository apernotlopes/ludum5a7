using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DELMat : MonoBehaviour
{
	private MeshRenderer rend;

	public Color Lit;
	public Color Unlit;
	
	private void Start()
	{
		rend = GetComponent<MeshRenderer>();
	}

	public void Blink(int loops = 3)
	{
		rend.materials[3].DOFloat(1f, "_SelfIllumPower", 0.2f).SetLoops(loops, LoopType.Yoyo).OnComplete(Off);
	}

	private void Off()
	{
		rend.materials[3].DOFloat(0.2f, "_SelfIllumPower", 0.2f);
	}
}
