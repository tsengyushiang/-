using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class practiceScene : MonoBehaviour {

    public StageManager stage;
    public GameObject startMenu;
    public GameObject character;
    private bool isActive=true;

    public void ShowFirst() {
        startMenu.SetActive(true);
        startMenu.GetComponent<StartMenuContorl>().enabled = false;
        character.GetComponent<animalMove>().enabled = false;
        character.GetComponent<animationNormal>().enabled = false;
        isActive = false;
    }

    public void animateOver() {
        startMenu.SetActive(false);       
    }

    public void ableToGo() {
        startMenu.SetActive(false);

        character.GetComponent<animalMove>().enabled = true;
        character.GetComponent<animationNormal>().enabled = true;

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
            GetComponent<Animator>().Play("practice");
        }

    } 

    public void goPlay() {
        stage.Next();
    }

}
