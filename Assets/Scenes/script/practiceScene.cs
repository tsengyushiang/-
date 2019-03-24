using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practiceScene : MonoBehaviour {

    public StageManager stage;

    void Start()
    {
        StartCoroutine(Example());
    }

    IEnumerator Example()
    {
        yield return new WaitForSeconds(12);
        stage.Next();

    }
}
