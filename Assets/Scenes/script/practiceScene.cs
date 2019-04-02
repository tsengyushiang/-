using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practiceScene : MonoBehaviour {

    public StageManager stage;
    private bool isActive=true;

    void Start()
    {
    }

    void Update() {

        if ((Input.GetKeyDown(KeyCode.W)|| 
            Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.A)||
            Input.GetKeyDown(KeyCode.D)) && isActive)
        {
            isActive = false;
            transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(Example());
        }

    } 

    IEnumerator Example()
    {
        GetComponent<Animator>().Play("practice");
        yield return new WaitForSeconds(12);
        stage.Next();

    }
}
