﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    private int currentStage =1;
    public GameObject[] stages;
    public PathAndActionRecorder recorder;

	// Use this for initialization
	void Start () {
        setStage(currentStage);
    }

    public void Next()
    {
        currentStage++;
        if (currentStage >= stages.Length)
            currentStage = 0;

        setStage(currentStage);

    }

    private void setStage(int i) {


        for (int index = 0; index < stages.Length; index++) {

            if (index == i)
            {
                stages[index].SetActive(true);
            }
            else {
                stages[index].SetActive(false);
            }

        }


        if (i == 1)
        {
            recorder.Stop();
            recorder.StartRecord();
        }
        else if (i == 2)
        {
            recorder.Stop();
            recorder.StartReplay();
        }

    }

    public void EndGame() {
        Application.Quit();
    }


}
