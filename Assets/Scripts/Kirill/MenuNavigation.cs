using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    
    private bool _isPaused;
    
    public void SetPause()
    {
        if (_isPaused)
            Pause();
        else 
            UnPause();

        _isPaused = !_isPaused;
    }
    
    public void Pause()
    {
        Time.timeScale = 0;
        _pauseCanvas.SetActive(true);
    }
    
    public void UnPause()
    {
        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
