using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GoalManager : MonoBehaviour 
{
    public float CountDown = 180.0f;
    public string[] Tutorials;
    public GameObject BlueScreen;

    public AudioClip BG;
    public AudioClip Boot;
    public AudioClip Alert;

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
                    return PCManager.Instance.viewerActive;

                case 6:
                    return !PCManager.Instance.viewerActive;

                case 7:
                    return PCManager.Instance.viewerActive;

                case 8:
                    return !PCManager.Instance.viewerActive;

                case 9:
                    return PCManager.Instance.viewerActive;

                case 10:
                    return !PCManager.Instance.viewerActive;

                case 11:
                    return PCManager.Instance.viewerActive;

                case 12:
                    return !PCManager.Instance.viewerActive;

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
	
	void Update () 
	{
        if (Trigger)
        {
            index++;

            if (index < Tutorials.Length && text.text != Tutorials[index])
            {
                text.text = "";
                text.DOText(Tutorials[index], 2.0f);
            }
        }
        else if(index > 12)
        {
            if(CountDown < 0)
            {
                BlueScreen.SetActive(true);
                text.text = "Your Score : \n" + score + "\n Floppy Fulled Bonus : \n" +
                            _perfectFloppy + " X " + "1000000 \n Total : \n" + (score + _perfectFloppy * 1000000);
            }
            else
            {
                CountDown -= Time.deltaTime;
                text.text = "Your Computer is infected ! \n Time before shutdown \n" + (int)CountDown;

                if (CountDown <= awraf)
                {
                    awraf -= 30;
                    image.transform.localScale = Vector3.one;
                    image.transform.DOPunchScale(Vector3.one * 1.2f, 0.5f);
                    SoundManager.instance.PlayOnEmptyTrack(Alert, false, false);
                }
            }
        }
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
