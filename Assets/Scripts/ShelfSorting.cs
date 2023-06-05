using UnityEngine;

public class ShelfSorting : MonoBehaviour
{
    [SerializeField] private Bottle _bottleController;
    [SerializeField] private MatchCounter _matchCounterController;
    
    private ShelfSorting _shelfSortingController;
    
    private bool hasEntered = false;
    private bool previousMatch = false;
    
    public Gradient colorGradient;
    
    private void Start()
    {
        _bottleController.Initialize(_bottleController);
        _matchCounterController.Initialize(_matchCounterController);
    }
    
    public void Initialize(ShelfSorting shelfSortingController)
    {
        _shelfSortingController = shelfSortingController;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!hasEntered && other.CompareTag("Bottle"))
        {
            Bottle bottleProperties = other.GetComponent<Bottle>();
            if (bottleProperties != null)
            {
                Gradient bottleGradient = bottleProperties.GetGradient();

                bool gradientsMatch = CompareGradients(bottleGradient, colorGradient);
                if (gradientsMatch)
                {
                    _matchCounterController._matches++;
                    previousMatch = true;
                }
                else
                {
                    hasEntered = false; 
                    previousMatch = false; 
                }
            }

            hasEntered = true;
        }
        else if (hasEntered && !previousMatch && other.CompareTag("Bottle"))
        {
            hasEntered = false;
            previousMatch = false;
        }
    }
    
    private bool CompareGradients(Gradient gradient1, Gradient gradient2)
    {
        if (gradient1.colorKeys.Length != gradient2.colorKeys.Length)
        {
            return false;
        }
            
        for (int i = 0; i < gradient1.colorKeys.Length; i++)
        {
            Color color1 = gradient1.colorKeys[i].color;
            Color color2 = gradient2.colorKeys[i].color;

            if (!color1.Equals(color2))
            {
                return false;
            }
        }

        return true;
    }
}