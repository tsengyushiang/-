using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuContorl : MonoBehaviour {

  
    public Text Day;
    public GameObject choseIcon;
    public Button[] buttons;

    private int currentSelect = 0;

	// Use this for initialization
	void Start () {
        Day.text = PathAndActionRecorder.DayCount.ToString();
	}
	
    void selecBtn(int s)
    {
        for (int i = 0; i < buttons.Length; i++) {

            if (i == s)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }

        }
    }


	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (currentSelect + 1 < buttons.Length)
            {
                currentSelect++;
                choseIcon.transform.position -= new Vector3(0, 0.4f, 0);
                selecBtn(currentSelect);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentSelect - 1 >= 0)
            {
                currentSelect--;
                choseIcon.transform.position += new Vector3(0, 0.4f, 0);
                selecBtn(currentSelect);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            buttons[currentSelect].onClick.Invoke();
        }




	}
}
