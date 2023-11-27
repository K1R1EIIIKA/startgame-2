using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperComics : MonoBehaviour
{
    [SerializeField] private GameObject _comics;
    [SerializeField] private GameObject _button;

    public static bool IsFirst = true;

    private void Start()
    {
        if (IsFirst)
        {
            Time.timeScale = 0;
            IsFirst = false;
            _comics.SetActive(true);
            StartCoroutine(StartButton());
        }
    }

    private IEnumerator StartButton()
    {
        yield return new WaitForSeconds(3);
        _button.SetActive(true);
    }

    public void StartGame()
    {
        _comics.SetActive(false);
        _button.SetActive(false);
        Time.timeScale = 1;
    }
}
