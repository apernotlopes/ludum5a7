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
    public LayerMask cursorMask;
    public IInteractable currentInteractable;

    public float torqueSpeed = 2.0f;
    public float cursorDistance = 4.0f;

    public Texture2D[] cursorsImage;

    public bool isInteracting
    {
        get
        {
            return currentInteractable != null;
        }
    }

    bool onScreen;
    Transform _cursor;
    Camera _camera;

    void Awake()
    {
        if(_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Cursor.SetCursor(cursorsImage[0], Vector3.zero, CursorMode.ForceSoftware);

        _instance = this;
        _instance.Initialize();
        DontDestroyOnLoad(_instance.gameObject);
    }
    
    void Initialize()
    {
        _camera = Camera.main ? Camera.main : FindObjectOfType<Camera>();
        _cursor = new GameObject("_Cursor").GetComponent<Transform>();
        _cursor.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {

        ManageCusor();

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

    void ManageCusor()
    {
        int index;
        RaycastHit _hit;
        onScreen = DoRaycast(out _hit, cursorMask);

        if (isInteracting || !onScreen)
        {
            index = !isInteracting ? 0 : 1;
        }
        else
        {
            if (PCManager.Instance.isTransferring || PCManager.Instance.isLoading)
                index = 4;
            else
                index = !Input.GetMouseButton(0) ? 2 : 3;
        }
            


        Cursor.SetCursor(cursorsImage[index], Vector3.zero, CursorMode.ForceSoftware);
    }

    void FixedUpdate()
    {
        if (isInteracting && Input.GetMouseButton(0))
        {
            _cursor.position = _camera.ScreenToWorldPoint(Input.mousePosition) + _camera.ScreenPointToRay(Input.mousePosition).direction * cursorDistance;
            currentInteractable.UpdateInteraction();
            currentInteractable.Rigidbody.AddTorque(new Vector3(1,1,0) * Input.mouseScrollDelta.y * torqueSpeed);
        }
        else if (isInteracting)
        {
            currentInteractable.EndInteraction();
            currentInteractable = null;
        }
    }

    public bool DoRaycast(out RaycastHit hit, LayerMask mask)
    {
        return Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 10.0f, mask);
    }
}
