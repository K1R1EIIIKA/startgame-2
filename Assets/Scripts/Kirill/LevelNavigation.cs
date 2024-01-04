using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelNavigation : MonoBehaviour
{
    [SerializeField] private GameObject _pauseCanvas;
    
    private bool _isPaused;
    
    public void SetPause()
    {
        if (!_isPaused)
            Pause();
        else 
            UnPause();
    }
    
    
    public void Pause()
    {
        Time.timeScale = 0.0001f;
        _isPaused = true;
        _pauseCanvas.SetActive(true);
    }
    
    public void UnPause()
    {
        Time.timeScale = 1;
        _isPaused = false;
        _pauseCanvas.SetActive(false);
    }
}
