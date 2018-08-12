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

    internal FloppyReader reader;

    public void SetFloppy(int index)
    {
        floppyData = new FloppyData();
        floppyData.Title = ((FileCategory)index).ToString();

        capacity = 0;

        foreach (FileData d in FileGenerator.instance.allData[(FileCategory)index])
            capacity += d.Size;
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
        }
    }

    public void UpdateInteraction()
    {
        Rigidbody.velocity = (Cursor.position - transform.position) * Time.fixedDeltaTime * speed;
    }
}