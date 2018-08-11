using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractable : MonoBehaviour, IInteractable 
{
    public Rigidbody Rigidbody { get; set; }
    public Transform Cursor { get; set; }

    public float speed = 1.0f;

    void Start () 
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
