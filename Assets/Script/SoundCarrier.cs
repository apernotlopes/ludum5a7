using UnityEngine;
using System.Collections;

public class SoundCarrier : MonoBehaviour {

	public AudioClip[] Clips;

	private SoundManager soundManager;

	void Start(){
		soundManager = FindObjectOfType<SoundManager>();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			soundManager.PlayOnEmptyTrack(Clips[0], true, true, 5, 1);
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			soundManager.StopThisClip(Clips[0], true, 5);
		}
	}
}
