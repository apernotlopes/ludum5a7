using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    Rigidbody Rigidbody { get; set;}
    Transform Cursor { get; set; }

    void BeginInteraction(Transform cursor);
    void EndInteraction();
    void UpdateInteraction();
}
