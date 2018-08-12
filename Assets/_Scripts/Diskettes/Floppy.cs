using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floppy : Storage, IInteractable
{
    public Rigidbody Rigidbody { get; set; }
    public Transform Cursor { get; set; }

    public FloppyData floppyData;
    public float speed = 200.0f;
    public LayerMask readerDetectionMask;

    public AudioClip FloppyIn;

    internal FloppyReader reader;

    public void SetFloppy(int index)
    {
        floppyData = new FloppyData();
        floppyData.Title = ((FileCategory)index).ToString();

        capacity = 0;

        foreach (FileData d in FileGenerator.instance.allData[(FileCategory)index])
            capacity += d.Size;

        //Debug
        if (capacity == 0)
        {
            capacity = (int) (Mathf.Pow(2, 20)) * Random.Range(1,3);
        }

        Renderer rend = GetComponentInChildren<Renderer>();
        Material[] mats = rend.materials;

        float rand = Random.value * 3;

        if (rand > 2)
            mats[0].SetColor("Color_87CF86FE", new Color(1.0f, Random.value, 0.0f));
        else if (rand > 1)
            mats[0].SetColor("Color_87CF86FE", new Color(0.0f, 1.0f, Random.value));
        else
            mats[0].SetColor("Color_87CF86FE", new Color(Random.value, 0.0f, 1.0f));

        rend.materials = mats;
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void BeginInteraction(Transform cursor)
    {
        if(reader != null)
        {
            reader.UnloadFloppy();
            reader = null;
            SoundManager.instance.PlayOnEmptyTrack(FloppyIn, false, false);
        }

        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = false;
        Cursor = cursor;
    }

    public void EndInteraction()
    {
        Rigidbody.useGravity = true;

        RaycastHit _hit;

        if(GrabManager.instance.DoRaycast(out _hit, readerDetectionMask))
        {
            _hit.collider?.attachedRigidbody.GetComponent<FloppyReader>().ReadFloppyDisk(this);
            SoundManager.instance.PlayOnEmptyTrack(FloppyIn, false, false);
        }
    }

    public void UpdateInteraction()
    {
        Rigidbody.velocity = (Cursor.position - transform.position) * Time.fixedDeltaTime * speed;
    }
}