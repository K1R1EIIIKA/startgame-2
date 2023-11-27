using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipManager : MonoBehaviour
{
    [SerializeField] private GameObject tipText;

    void Update()
    {
        if (MagnitTrigger.magnitActive)
            tipText.SetActive(true);
        else 
            tipText.SetActive(false);
    }
}