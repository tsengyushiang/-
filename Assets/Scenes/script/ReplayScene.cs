using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.IO;  //StreamWrite會用到
public class ReplayScene : MonoBehaviour {

    public GameObject Action_character;

    public GameObject animation_throwup;
    public GameObject animation_nod;
    public GameObject animation_backichy;
    public PathAndActionRecorder recorder;

    public Text ActionName;
    public Text CountNumber;

    public bool startCountDay = false;
    public Text Day;

    float step = 0.001f;
    float dayAnimate = 0;

    void VoiceDown() {
        GameObject.Find("backgroundMusic").gameObject.GetComponent<Animator>().Play("volumeDown");
    }

    void StartCount() {
        startCountDay = true;
    }

    void Update() {

        if (startCountDay) {

            dayAnimate += step;
            step += 0.001f;
            Day.text = ((int)dayAnimate) .ToString();

            if (((int)dayAnimate) == PathAndActionRecorder.DayCount)
                startCountDay = false;
        }
    }

    int g_backcount = 0;
    int g_throwupCount = 0;
    int g_nodCount = 0;

    void OnEnable() {
        LoadGlobalActions();        
    }

    void StartRoute() {

        recorder.StartReplay();
        saveAction();
    }

    void StopRoute() {

        recorder.Stop();

    }

    void LoadGlobalActions() {

        g_backcount = 0;
        g_throwupCount = 0;
        g_nodCount = 0;

        DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
        FileInfo[] actionCounts = directoryInfo.GetFiles("ActionCounts.txt");

        StreamReader strReader = actionCounts[0].OpenText();
        string counts = "";       

        while ((counts = strReader.ReadLine() )!= null) {
            string[] splitString = counts.Split(',');
            g_backcount += int.Parse(splitString[1]);
            g_throwupCount += int.Parse(splitString[2]);
            g_nodCount += int.Parse(splitString[3]);
        }

        showAnimation();
    }

    void showGlobal() {        

        if (g_backcount != -1)
        {
            CountNumber.text = g_backcount.ToString();
        }
        else if (g_throwupCount != -1)
        {
            CountNumber.text = g_throwupCount.ToString();
        }
        else if (g_nodCount != -1)
        {
            CountNumber.text = g_nodCount.ToString();
        }

    }

    void saveAction() {

        animationNormal component = Action_character.GetComponent<animationNormal>();

        int backcount = component.backichyCount;
        int throwupCount = component.throwupCount;
        int nodCount = component.nodCount;       

        string saveString =
            "Day " +PathAndActionRecorder.DayCount.ToString()+" : " + ","+ backcount.ToString()+","+
            throwupCount.ToString()+","+
            nodCount.ToString()+ "\n";
        
        System.IO.File.AppendAllText(Application.streamingAssetsPath + "/" + "ActionCounts.txt", saveString);

    }

    public void showAnimation() {

        animationNormal component = Action_character.GetComponent<animationNormal>();

        int backcount= component.backichyCount;
        int throwupCount = component.throwupCount;
        int nodCount = component.nodCount;

        if (backcount >= throwupCount && backcount >= nodCount) {

            animation_throwup.SetActive(false);
            animation_nod.SetActive(false);
            animation_backichy.SetActive(true);
            ActionName.text = "搓背";
            CountNumber.text = backcount.ToString();

            g_backcount += backcount;
            g_throwupCount = -1;
            g_nodCount = -1;
        }
        else if (throwupCount >= backcount && throwupCount >= nodCount)
        {
            animation_throwup.SetActive(true);
            animation_nod.SetActive(false);
            animation_backichy.SetActive(false);
            ActionName.text = "嘔吐";
            CountNumber.text = throwupCount.ToString();

            g_throwupCount += throwupCount;
            g_backcount = -1;
            g_nodCount = -1;
        }
        else if (nodCount >= backcount && nodCount >= throwupCount)
        {
            animation_throwup.SetActive(false);
            animation_nod.SetActive(true);
            animation_backichy.SetActive(false);
            ActionName.text = "點頭";
            CountNumber.text = nodCount.ToString();

            g_nodCount += nodCount;
            g_backcount = -1;
            g_throwupCount = -1;      
        }

    }

    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }
}
