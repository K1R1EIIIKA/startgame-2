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

    [SerializeField] private CharacterController controller;

    //[SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private GameObject lowSpeedImage;

    //[SerializeField] private GameObject startText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    [SerializeField] private float _distanceScale = 5;


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
        
        distance = Vector3.Distance(Vector3.back * 4.42f, controller.transform.position) * _distanceScale;
        Debug.Log(distance);
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        distanceText.text = ((int)distance).ToString();
        if (distance >= PlayerPrefs.GetFloat("Record", 0))
        {
            PlayerPrefs.SetFloat("Record", distance);
            record.text = "Record: " + (int)distance;
        }

        if (hitHappened)
        {
            Movement.forwardSpeed -= 1;
            StartCoroutine(LowSpeedImage());
        }
    }

    IEnumerator LowSpeedImage()
    {
        lowSpeedImage.SetActive(true);
        hitHappened = false;
        controller.enabled = false;
        Movement.animator.SetBool("Hit", true);
        yield return new WaitForSeconds(1f);
        lowSpeedImage.SetActive(false);
        controller.enabled = true;
        Movement.animator.SetBool("Hit", false);
    }
}