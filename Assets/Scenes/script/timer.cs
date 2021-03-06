﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Scrollbar timerSlider;
    public StageManager stage;
    public int TotalTime = 60;
    private int currentTime = 0;
    public GameObject character;

    void TimerCountDown()
    {
        currentTime ++;       
        timerSlider.value = (float)currentTime / TotalTime;

        if (currentTime == TotalTime)
        {
            stage.Next();
            CancelInvoke();
        }
        /*
        else if (currentTime == TotalTime * 0.5)
        {
            GetComponent<Animator>().Play("keeper");
        }
        else if (currentTime == TotalTime * 0.5 + 7) {

            foodPot.GetComponent<SpriteRenderer>().enabled = false;
            foodPot.transform.GetChild(0).gameObject.SetActive(true);
            hintWords.changeState("lickleft", "等等管理員來熄燈後，我的一天就結束了", "按Z舔欄杆", "lickleft");
            hintWords.changeState("noFoodPot", "跟昨天一樣的食物", "按Space吃東西", "eat");

        }
        */

        if (currentTime == TotalTime - 3)
        {
            character.GetComponent<animationNormal>().GameOver();
            GetComponent<Animator>().Play("keeper");
        }
    }

    void Awake() {
        timerSlider.value = 0;
    }
    // Start is called before the first frame update

    public void OnEnable() {
        GameObject.Find("backgroundMusic").GetComponent<AudioSource>().enabled = true;

    }
    public void StartCount()
    {      
        currentTime = 0;
        InvokeRepeating("TimerCountDown", 1f, 1f);
    }
    public void replay()
    {
        SceneManager.LoadScene(0);
    }
    public void quit()
    {
        Application.Quit();
    }
}
