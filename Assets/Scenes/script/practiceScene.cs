using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practiceScene : MonoBehaviour {

    public StageManager stage;
    public GameObject startMenu;
    private bool isActive=true;

    public void ShowFirst() {
        startMenu.SetActive(true);
        startMenu.GetComponent<StartMenuContorl>().enabled = false;
        isActive = false;
    }

    public void animateOver() {
        startMenu.SetActive(false);
       
    }

    public void ableToGo() {
        isActive = true;
    }

    void Update() {

        if ((Input.GetKeyDown(KeyCode.W)|| 
            Input.GetKeyDown(KeyCode.S)||
            Input.GetKeyDown(KeyCode.A)||
            Input.GetKeyDown(KeyCode.D)) && isActive)
        {
            isActive = false;
            transform.GetChild(1).gameObject.SetActive(false);
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
