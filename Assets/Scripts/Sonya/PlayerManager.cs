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
    [SerializeField] private GameObject gameOverPanel;
    //[SerializeField] private GameObject startText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI record;
 

    void Start()
    {
        record.text = "Record: "+ PlayerPrefs.GetFloat("Record",0).ToString();
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = true;
        distance = 0;
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     distance+=0.01f;
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
        distanceText.text = ""+(int)distance;
        if (distance>= PlayerPrefs.GetFloat("Record", 0))
        {
            PlayerPrefs.SetFloat("Record",distance);
            record.text = "Record: "+(int)distance;
        }
              
        //if (){
        //    isGameStarted = true;
         //   Destroy(startText);
        //}
    }
    
}
