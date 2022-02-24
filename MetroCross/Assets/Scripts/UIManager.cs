using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Text Timer;

    public GameObject GameUI;
    public GameObject MenuUI;
    
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }


    void Update()
    {

    }

    public void OnPlay()
    {
        
    }

    public void OnPlayVersus()
    {
        
    }

    public void UpdateTimer(float time)
    {
        string minute = ""+Mathf.Floor(time / 60);
        minute = minute.PadLeft(2, '0');
        
        string secondes = String.Format("{0:0.00}", time % 60);
        secondes = secondes.PadLeft(5, '0');

        Timer.text = minute + ":" + secondes;

    }
}
