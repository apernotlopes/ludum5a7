using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SimpleButton : MonoBehaviour, IInteractable 
{
    public Rigidbody Rigidbody { get; set; }
    public Transform Cursor { get; set; }

    public UnityEvent OnTrigger;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Trigger()
    {
        OnTrigger.Invoke();
    }

    public void BeginInteraction(Transform cursor)
    {
        Trigger();
        EndInteraction();
    }

    public void EndInteraction()
    {
        GrabManager.instance.currentInteractable = null;
    }

    public void UpdateInteraction()
    {
      
    }
}
