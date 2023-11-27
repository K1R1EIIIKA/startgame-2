using System;
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

    public static bool IsWon;
    public static PlayerManager Instance;

    [Header("Components")]
    [SerializeField] private CharacterController controller;

    //[SerializeField] private Animator animator;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject _winCanvas;
    [SerializeField] private GameObject lowSpeedImage;

    //[SerializeField] private GameObject startText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI _enemyCountText;
    
    [Header("Properties")]
    [SerializeField] private float _distanceScale = 5;

    public float WinSpeed = 100;
    public int MaxEnemyCount = 20;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
        
    }

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
        _enemyCountText.text = EnemySpawn.EnemyCount + "/" + MaxEnemyCount;
        
        
        distance = Vector3.Distance(Vector3.back * 4.42f, controller.transform.position) * _distanceScale;
        // Debug.Log(distance);
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
              FindObjectOfType<AudioManager>().PlaySound("HitObj");
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

    public void Win()
    {
        Time.timeScale = 0;
        _winCanvas.SetActive(true);
    }
}