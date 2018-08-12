using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloppyReader : MonoBehaviour 
{
    public bool Loaded;
    public Floppy LoadedDisck
    {
        get
        {
            return Loaded ? currentDisk : null;
        }
    }

    Floppy currentDisk;
    Transform floppyAnchor;
    Sequence LoadingSequence;

    void Start()
    {
        floppyAnchor = transform.Find("FloppyAnchor");
    }

    public void ReadFloppyDisk(Floppy Disk)
    {
        if (currentDisk != null) return;

        currentDisk = Disk;
        currentDisk.Rigidbody.isKinematic = true;
        currentDisk.reader = this;

        LoadingSequence = DOTween.Sequence();
        LoadingSequence.Append(Disk.transform.DORotate(floppyAnchor.rotation.eulerAngles, 0.5f));
        LoadingSequence.Join(Disk.transform.DOMove(floppyAnchor.position, 0.5f));
        LoadingSequence.Append(Disk.transform.DOMove(floppyAnchor.position + transform.forward * 0.5f, 1.0f));
        LoadingSequence.onComplete += LoadData;
    }

    void LoadData()
    {
        Loaded = true;
        
        PCManager.Instance.DisplayExplorer(false);

        PCManager.Instance.Explorer.FloppyTab.text = "Floppy(A:)";
    }

    public void UnloadFloppy()
    {
        LoadingSequence.Kill();
        Loaded = false;
        currentDisk = null;
        
        PCManager.Instance.Viewer.Clear();
        PCManager.Instance.DisplayExplorer(true);
        
        PCManager.Instance.Explorer.FloppyTab.text = "Empty(A:)";

    }
}
