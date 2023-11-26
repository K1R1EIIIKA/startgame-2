using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static float distance;
    public static bool playerIsUp;
    public static bool hitHappened;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject lowSpeedImage;

    //[SerializeField] private GameObject startText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI _enemyCountText;


    void Start()
    {
        record.text = "Record: " + PlayerPrefs.GetFloat("Record", 0).ToString("0.0");
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = true;
        distance = 0;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        _enemyCountText.text = EnemySpawn.EnemyCount.ToString();
        
        distance += 0.01f;
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        distanceText.text = "Distance: " + (int)distance;
        if (distance >= PlayerPrefs.GetFloat("Record", 0))
        {
            PlayerPrefs.SetFloat("Record", distance);
            record.text = "Record: " + (int)distance;
        }

        if (hitHappened)
        {
            StartCoroutine(LowSpeedImage());
        }
    }

    IEnumerator LowSpeedImage()
    {
        lowSpeedImage.SetActive(true);
        hitHappened = false;
        yield return new WaitForSeconds(1.5f);
        lowSpeedImage.SetActive(false);
    }
}