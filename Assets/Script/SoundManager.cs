using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("SoundManager").AddComponent<SoundManager>();
                _instance.Initialize();
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public GameObject trackObject;

	private AudioSource[] tracks;

    public void Initialize()
    {
        trackObject = new GameObject("TrackPrefab");
        trackObject.AddComponent<AudioSource>();
        DontDestroyOnLoad(trackObject);
    }

    public void PlayOnEmptyTrack( AudioClip clip, bool loop, bool fadeIn, float fadeDuration = 0, float volume = 1){
		tracks = transform.GetComponentsInChildren<AudioSource>();

		for(int i = 0; i < tracks.Length; i++){
			if(!tracks[i].isPlaying)
            {
				tracks[i].clip = clip;
				tracks[i].loop = loop;
				if(fadeIn)
                {
					StartCoroutine(Fade(tracks[i], true, fadeDuration, volume));
				}
				else
                {
					tracks[i].volume = volume;
					tracks[i].Play();
				}
                return;
			}
		}

		AudioSource track = Instantiate(trackObject).GetComponent<AudioSource>();
		track.transform.parent = this.transform;
		track.clip = clip;
		track.loop = loop;
		if(fadeIn)
        {
			StartCoroutine(Fade(track, true, fadeDuration, volume));
		}
		else
        {
			track.volume = volume;
			track.Play();
		}
	}

	public void StopThisClip(AudioClip clip, bool fadeOut, float fadeDuration = 0){
		tracks = transform.GetComponentsInChildren<AudioSource>();

		for(int i = 0; i < tracks.Length; i++){
			if(tracks[i].clip.name == clip.name && tracks[i].isPlaying){
				if(fadeOut){
					StartCoroutine(Fade(tracks[i], false, fadeDuration));
				}
				else{
					tracks[i].Stop();
				}
			}
		}
	}

	IEnumerator Fade(AudioSource track, bool In, float duration, float targetedVolume = 0){
		float time = 0;
		float baseVolume;
		float modifier = 1;

		if(In)
        {
			baseVolume = 0;
			track.volume = 0;
			track.Play();
		}
		else
        {
			baseVolume = track.volume;
			duration = duration;
			targetedVolume = baseVolume;
			modifier = -1;
		}

		while(time <= duration)
        {
			time += Time.deltaTime;
			track.volume = baseVolume + (targetedVolume * (time/duration) * modifier);
			yield return 0;
		}

		if(!In)
        {
			track.Stop();
		}
	}
}
