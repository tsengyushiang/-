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
    public GameObject CounNumberUnit;

    public bool startCountDay = false;
    public Text Day;
    public GameObject dayUnit;

    float step = 0.005f;
    float dayAnimate = 0;
    float currentAddTime = 0;

    void VoiceDown() {
        GameObject.Find("backgroundMusic").gameObject.GetComponent<Animator>().Play("volumeDown");
    }

    void StartCount() {
        startCountDay = true;
    }

    void Update() {

        if (startCountDay) {

            currentAddTime += Time.deltaTime;

            if (currentAddTime > 2.0)
            {
                step = (PathAndActionRecorder.DayCount - dayAnimate) / ((4.0f- currentAddTime)/ Time.deltaTime);
            }
            else if (currentAddTime < 2.0) {
                step = (PathAndActionRecorder.DayCount - dayAnimate / 4)  / (2.0f/Time.deltaTime);
            }

            dayAnimate += step;
            Day.text = ((int)dayAnimate) .ToString();
            dayUnit.transform.localPosition = new Vector3(Day.text.Length * 40.0f, 0, 0);

            if (((int)dayAnimate) >= PathAndActionRecorder.DayCount)
            {
                Day.text = ((int)PathAndActionRecorder.DayCount).ToString();
                dayUnit.transform.localPosition = new Vector3(Day.text.Length * 40.0f, 0, 0);
                startCountDay = false;                
            }
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

        if (g_nodCount != -1)
        {
            CountNumber.text = g_nodCount.ToString();
            CounNumberUnit.transform.localPosition = new Vector3(g_nodCount.ToString().Length * 20f, 0, 0);
            
        }
        else if(g_throwupCount != -1)
        {
            CountNumber.text = g_throwupCount.ToString();
            CounNumberUnit.transform.localPosition = new Vector3(g_throwupCount.ToString().Length * 20f, 0, 0);

        }
        else if (g_backcount != -1)
        {
            CountNumber.text = g_backcount.ToString();
            CounNumberUnit.transform.localPosition = new Vector3(g_backcount.ToString().Length * 20f, 0, 0);

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

        if (nodCount >= backcount && nodCount >= throwupCount)
        {
            animation_throwup.SetActive(false);
            animation_nod.SetActive(true);
            animation_backichy.SetActive(false);
            ActionName.text = "點頭";
            CountNumber.text = nodCount.ToString();
            CounNumberUnit.transform.localPosition = new Vector3(nodCount.ToString().Length * 20f, 0, 0);


            g_nodCount += nodCount;
            g_backcount = -1;
            g_throwupCount = -1;
        }
        else if (backcount >= throwupCount && backcount >= nodCount) {

            animation_throwup.SetActive(false);
            animation_nod.SetActive(false);
            animation_backichy.SetActive(true);
            ActionName.text = "搓背";
            CountNumber.text = backcount.ToString();
            CounNumberUnit.transform.localPosition = new Vector3(backcount.ToString().Length * 20f, 0, 0);


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
            CounNumberUnit.transform.localPosition = new Vector3(throwupCount.ToString().Length * 20f, 0, 0);


            g_throwupCount += throwupCount;
            g_backcount = -1;
            g_nodCount = -1;
        }
        

    }

    public void PlayAgain() {
        SceneManager.LoadScene(0);
    }
}
