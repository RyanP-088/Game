using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] float totalTime; 
    private float currentTime;

    [SerializeField] TMPro.TextMeshProUGUI timerMesh;
    [SerializeField] PartyLogic pLog;
    [SerializeField] L3PartyLogic l3Log;


    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            if(l3Log == null)
            {
                pLog.gameOver();
            } else
            {
                l3Log.gameOver();
            }
        }
    }

    public void penalty()
    {
        currentTime -= 60f;
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerMesh.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
