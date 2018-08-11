using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabManager : MonoBehaviour
{
    private static GrabManager _instance;
    public static GrabManager instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("_GrabManager").AddComponent<GrabManager>();
                _instance.Initialize();
                DontDestroyOnLoad(_instance.gameObject);
            }
            
            return _instance;
        }
    }

    public LayerMask iteractableMask;
    public IInteractable currentInteractable;

    public bool isInteracting
    {
        get
        {
            return currentInteractable != null;
        }
    }

    Transform _cursor;
    Camera _camera;

    void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        _instance.Initialize();
        DontDestroyOnLoad(_instance.gameObject);
    }
    
    void Initialize()
    {
        _camera = Camera.main ?? FindObjectOfType<Camera>();
        _cursor = new GameObject("_Cursor").GetComponent<Transform>();
        _cursor.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        if (isInteracting) return;

        RaycastHit _hit;

        if(DoRaycast(out _hit, iteractableMask))
        {
            IInteractable _interactable = _hit.collider.attachedRigidbody.gameObject.GetComponent<IInteractable>();

            if (_interactable != null && Input.GetMouseButtonDown(0))
            {
                currentInteractable = _interactable;
                currentInteractable.BeginInteraction(_cursor);
            }
        }
    }

    void FixedUpdate()
    {
        if (isInteracting && Input.GetMouseButton(0))
        {
            _cursor.position = _camera.ScreenToWorldPoint(Input.mousePosition) + _camera.ScreenPointToRay(Input.mousePosition).direction * 5.0f;
            currentInteractable.UpdateInteraction();
            currentInteractable.Rigidbody.AddTorque(new Vector3(1,1,0) * Input.mouseScrollDelta.y * 2.0f);
        }
        else if (isInteracting)
        {
            currentInteractable.EndInteraction();
            currentInteractable = null;
        }
    }

    public bool DoRaycast(out RaycastHit hit, LayerMask mask)
    {
        return Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 10.0f, iteractableMask);
    }
}
