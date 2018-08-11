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

    void SetFloppy(FileData[] files)
    {
        floppyData = new FloppyData();
        totalSize = 1474560;

        for (int i = 0; i < files.Length; i++)
        {
            AddFile(files[i]);
        }
    }

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void BeginInteraction(Transform cursor)
    {
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = false;
        Cursor = cursor;
    }

    public void EndInteraction()
    {
        Rigidbody.useGravity = true;
    }

    public void UpdateInteraction()
    {
        Rigidbody.velocity = (Cursor.position - transform.position) * Time.fixedDeltaTime * speed;
    }
}