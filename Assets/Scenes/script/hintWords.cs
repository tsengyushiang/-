using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hintWords : MonoBehaviour {

    public string sentence;
    public string hint;
    public string activeName;


    private static ArrayList AllhintWords=new ArrayList();

    void Start() {
        AllhintWords.Add(this);
    }

    public static void changeState(string name, string sentence, string hint, string activeName) {


        foreach (hintWords a in AllhintWords) {

            if (a.activeName == name)
            {
                a.activeName = activeName;
                a.sentence = sentence;
                a.hint = hint;
                return;
            }
        }
    }

}
