using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoalManager : MonoBehaviour 
{
    public float CountDown = 180.0f;
    public string[] Tutorials;
    TextMeshProUGUI text;

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
                    return PCManager.Instance.viewerActive;

                case 3:
                    return !PCManager.Instance.viewerActive;

                case 4:
                    return PCManager.Instance.viewerActive;

                case 5:
                    return !PCManager.Instance.viewerActive;

                case 6:
                    return PCManager.Instance.viewerActive;

                case 7:
                    return !PCManager.Instance.viewerActive;

                case 8:
                    return PCManager.Instance.viewerActive;

                case 9:
                    return !PCManager.Instance.viewerActive;

                case 10:
                    return PCManager.Instance.viewerActive;

                case 11:
                    return !PCManager.Instance.viewerActive;

                default:
                    return false;
            }
        }
    }


    FloppyReader reader;

    int index = 0;

    void Start () 
	{
        text = GetComponent<TextMeshProUGUI>();
        text.DOText(Tutorials[index], 2.0f);
        reader = FindObjectOfType<FloppyReader>();
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
        else if(index > 11)
        {
            CountDown -= Time.deltaTime;
            text.text = "Your Computer is infected ! \n Time before shutdown \n" + (int)CountDown;
        }
    }


}
