using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchCounter : MonoBehaviour
{
    public int _matches;
    public int _spotFilledCount;
    private MatchCounter _matchCounterController;
    
    private void Update()
    {
        CheckWin();
        CheckLose();
    }

    public void Initialize(MatchCounter matchCounterController)
    {
        _matchCounterController = matchCounterController;
    }
    
    private void CheckWin()
    {
        if (_spotFilledCount == 6 && _matches == _spotFilledCount)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    
    private void CheckLose()
    {
        if (_spotFilledCount == 6 && _matches != _spotFilledCount)
        {
            SceneManager.LoadScene("LoseScene");
        }
    }
}