using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour 
{
    public float CountDown = 180.0f;
    public string[] Tutorials;
    public GameObject BlueScreen;

    public AudioClip BG;
    public AudioClip Boot;
    public bool isGameOver;

    public AudioClip Alert;

    public AudioClip[] musiques;
    bool musiquePlaying = false;
    int musiqueIndex;

    TextMeshProUGUI text;
    Image image;

    int _score = 0;
    int _perfectFloppy = 0;
    int score
    {
        get
        {
            _score = _score != 0 ? _score : GetScore();
            return _score;
        }
    }


    bool Trigger
    {
        get
        {
            switch (index)
            {
                case 0:
                    return GrabManager.instance.isInteracting;

                case 1:
                    return reader.Loaded;

                case 2:
                    return PCManager.Instance.isHardDrive;

                case 3:
                    return PCManager.Instance.viewerActive;

                case 4:
                    return PCManager.Instance.isTransferring;

                case 5:
                    return !PCManager.Instance.isTransferring;

                case 6:
                    return !PCManager.Instance.viewerActive;

                case 7:
                    return PCManager.Instance.isTransferring;

                case 8:
                    return !PCManager.Instance.isTransferring;

                case 9:
                    return PCManager.Instance.isTransferring;

                case 10:
                    return !PCManager.Instance.isTransferring;

                case 11:
                    return PCManager.Instance.isTransferring;

                case 12:
                    return !PCManager.Instance.isTransferring;

                case 13:
                    return PCManager.Instance.isTransferring;

                case 14:
                    return !PCManager.Instance.isTransferring;

                default:
                    return false;
            }
        }
    }


    FloppyReader reader;
    float awraf;
    int index = 0;

    void Start () 
	{
        text = GetComponent<TextMeshProUGUI>();
        image = transform.parent.GetComponentInChildren<Image>();
        reader = FindObjectOfType<FloppyReader>();
        SoundManager.instance.PlayOnEmptyTrack(BG, true, true, 2.0f);
        SoundManager.instance.PlayOnEmptyTrack(Boot, false, false);

        awraf = CountDown;
    }

    public void StartTuto()
    {
        text.DOText(Tutorials[index], 2.0f);
    }

    private bool isVirusStarted;
    private float t;
    
	void Update ()
	{
	    if (isGameOver) return;

	    if (isVirusStarted)
	    {
	        t += Time.deltaTime;

	        if (t > 1f)
	        {
	            PCManager.Instance.VirusPropagation();
	            t = 0;
	        }
	    }
	    
	    if (Trigger)
        {
            index++;

            if (index < Tutorials.Length && text.text != Tutorials[index])
            {
                text.text = "";
                text.DOText(Tutorials[index], 2.0f);
            }
        }
        else if(index > 14)
        {
            if(CountDown <= 0)
            {
                SoundManager.instance.StopThisClip(musiques[musiqueIndex], false);
                image.transform.localScale = Vector3.zero;

                if (!isGameOver)
                {
                    GameOver();
                }
            }
            else
            {
                if (!isVirusStarted)
                {
                    isVirusStarted = true;
                    PCManager.Instance.DisplayMessage("Your Computer is infected !", true);
                }

                text.text = "Your Computer is infected ! \n Time before shutdown \n" + (int)CountDown;

                if (CountDown <= awraf)
                {
                    awraf -= 30;
                    image.transform.localScale = Vector3.one;
                    image.transform.DOPunchScale(Vector3.one * 1.2f, 0.5f);
                    SoundManager.instance.PlayOnEmptyTrack(Alert, false, false);

                    if(musiquePlaying)
                    {
                        SoundManager.instance.StopThisClip(musiques[musiqueIndex], false);
                        musiqueIndex = Mathf.Clamp(musiqueIndex + 1, 0 , musiques.Length - 1);
                    }
                    SoundManager.instance.PlayOnEmptyTrack(musiques[musiqueIndex], true, false);
                    musiquePlaying = true;
                }

                CountDown -= Time.deltaTime;
            }
        }
    }

    private void GameOver()
    {
        isGameOver = true;
        
        BlueScreen.SetActive(true);
        text.text = "Your Score : \n" + score + "\n Complete Collections Bonus : \n" +
                    _perfectFloppy + " X " + "10.000.000 \n Total : \n" + (score + _perfectFloppy * 10000000);

        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    int GetScore()
    {
        Floppy[] floppys = FindObjectsOfType<Floppy>();

        for(int i = 0; i < floppys.Length; i++)
        {
            int usedSpace = floppys[i].GetUsedSpace();
            _score +=  usedSpace;
            _perfectFloppy += usedSpace >= floppys[i].capacity ? 1 : 0; 
        }

        return _score;
    }
}
