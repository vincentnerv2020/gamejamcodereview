using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public int minAmmount = 5;
    public float currentTime;
    public Text timeText;
    
    public float oneMin;
    int secundes;
    public float realTime;
    public GameManager gm;
    private void Start()
    {
        oneMin = 60f;
        realTime = minAmmount * 60;
        CountMinutes();
        gm = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gm.gameStarts)
        {
            CountSecundes();
            realTime -= Time.deltaTime;
            if (realTime <= 0)
            {
                timeText.text = "00:00";
                GameManager gm = FindObjectOfType<GameManager>();
                gm.GameOver();

            }
        }
       
    }
    void CountSecundes()
    {
        
            oneMin -= Time.deltaTime;
            secundes = (int)oneMin;
            string sec = secundes.ToString();

            if (oneMin <= 0)
            {
                oneMin = 60f;
                CountMinutes();
            }
         
            if (oneMin <= 10)
            {
                timeText.text = "0" + minAmmount + ":" + "0" + sec; ;
            }
                else
                {
                    timeText.text = "0" + minAmmount + ":" + sec; ;
                }
    }
    void CountMinutes()
    {
        minAmmount--; 
    }

}
