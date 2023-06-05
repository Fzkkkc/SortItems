using UnityEngine;

public class Bottle : MonoBehaviour, IDraggable
{
    [SerializeField] private ShelfSorting _shelfSortingController;
    [SerializeField] private DragAndDrop _dragAndDropController;
    [SerializeField] private MatchCounter _matchCounterController;
    
    private Bottle _bottleController;
    
    private Vector3 _initialPosition;
    
    public Gradient BottleGradient;
    public Color ColorFX;
    
    public void Initialize(Bottle bottleController)
    {
        _bottleController = bottleController;
    }

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Start()
    {
        _shelfSortingController.Initialize(_shelfSortingController);
        _dragAndDropController.Initialize(_dragAndDropController);
        _matchCounterController.Initialize(_matchCounterController);
    }
    
    public void OnStartDrag()
    {
        RestorePosition();
    }
    
    public void OnEndDrag()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);

        bool isDroppedInSpot = false;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Spot"))
            {
                if (collider.transform.childCount == 0)
                {
                    transform.position = collider.transform.position;
                    transform.SetParent(collider.transform);
                    isDroppedInSpot = true;
                    _matchCounterController._spotFilledCount++;
                    FX.Instance.PlayGoalExplosionFX(collider.transform.position, ColorFX);
                }
                break;
            }
        }
        
        if (!isDroppedInSpot)
        {
            transform.SetParent(null);
        }

        if (transform.parent == null)
        {
            _matchCounterController._spotFilledCount--;
        }
    }

    private void RestorePosition()
    {
        Vector3 currentPosition = transform.position;
        currentPosition.z = _initialPosition.z;
        transform.position = currentPosition;
    }

    public Gradient GetGradient()
    {
        return BottleGradient;
    }
}