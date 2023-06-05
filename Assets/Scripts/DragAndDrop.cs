using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private float _touchDragPhysicsSpeed = 10f;
    [SerializeField] private float _touchDragSpeed = 0.1f;

    [SerializeField] private Bottle _bottleController;
    [SerializeField] private ShelfSorting _shelfSortingController;
    [SerializeField] private MatchCounter _matchCounterController;

    private DragAndDrop _dragAndDropController;

    private Camera _mainCamera;
    private Vector3 velocity = Vector3.zero;
    private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private bool _isDragging = false;
    private GameObject _draggedObject;

    public void Initialize(DragAndDrop dragAndDrop)
    {
        _dragAndDropController = dragAndDrop;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _bottleController.Initialize(_bottleController);
        _shelfSortingController.Initialize(_shelfSortingController);
        _matchCounterController.Initialize(_matchCounterController);
    }

    private void OnEnable()
    {
        Input.simulateMouseWithTouches = true; 
    }

    private void OnDisable()
    {
        Input.simulateMouseWithTouches = false; 
    }

    private void Update()
    {
        Dragging();
    }

    private void Dragging()
    {
        if (_isDragging)
        {
            if (Input.touchCount > 0)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    ContinueDrag(touchPosition);
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    StopDrag();
                }
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Vector2 touchPosition = Input.GetTouch(0).position;
                StartDrag(touchPosition);
            }
        }
    }
    
    private void StartDrag(Vector2 touchPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.GetComponent<IDraggable>() != null)
            {
                _isDragging = true;
                _draggedObject = hit.collider.gameObject;
                _draggedObject.GetComponent<IDraggable>().OnStartDrag();
            }
        }
    }

    private void ContinueDrag(Vector2 touchPosition)
    {
        Ray ray = _mainCamera.ScreenPointToRay(touchPosition);
        if (_draggedObject != null)
        {
            Vector3 newPosition = ray.GetPoint(Vector3.Distance(_draggedObject.transform.position, _mainCamera.transform.position));
            newPosition.z = _draggedObject.transform.position.z;
            _draggedObject.transform.position = Vector3.SmoothDamp(_draggedObject.transform.position, newPosition, ref velocity, _touchDragSpeed);
        }
    }

    private void StopDrag()
    {
        if (_draggedObject != null)
        {
            _isDragging = false;
            _draggedObject.GetComponent<IDraggable>().OnEndDrag();
            _draggedObject = null;
        }
    }
}
