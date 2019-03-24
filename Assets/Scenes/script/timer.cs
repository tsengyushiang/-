using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class timer : MonoBehaviour {

    public Scrollbar timerSlider;
    public StageManager stage;
    public int TotalTime = 60;
    private int currentTime = 0;

    void TimerCountDown()
    {
        currentTime ++;       
        timerSlider.value = (float)currentTime / TotalTime;

        if (currentTime == TotalTime)
        {
            stage.Next();
            CancelInvoke();
        }
        else if (currentTime == TotalTime*0.6) {

            hintWords.changeState("lickleft", "等等管理員來熄燈後，我的一天就結束了", "按Z舔欄杆", "lickleft");
            hintWords.changeState("noFoodPot", "跟昨天一樣的食物", "按Space吃東西", "eat");
        }
    }
    // Start is called before the first frame update
    public void OnEnable()
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
