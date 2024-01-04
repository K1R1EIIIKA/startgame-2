using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartStory()
    {
        SceneManager.LoadScene("StoryGame");
    }
    
    public void StartInfinite()
    {
        SceneManager.LoadScene("InfiniteGame");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
