using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuContorl : MonoBehaviour {

  
    public Text Day;
    public GameObject choseIcon;
    public Button[] buttons;
    public AudioClip clickSound;
    public GameObject No;
    public GameObject day;

    private int currentSelect = 0;

	// Use this for initialization
	void Start () {
        Day.text = PathAndActionRecorder.DayCount.ToString();

        int addInterval = PathAndActionRecorder.DayCount.ToString().Length;
        No.transform.position =No.transform.position - new Vector3(addInterval * 0.05f,0,0);
        day.transform.position = day.transform.position + new Vector3(addInterval * 0.05f, 0, 0);

        GetComponent<Animator>().Play("fadein");
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

        float step = 0.3f;
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<AudioSource>().PlayOneShot(clickSound);
            if (currentSelect + 1 < buttons.Length)
            {
                currentSelect++;
                choseIcon.transform.position -= new Vector3(0, step, 0);
                selecBtn(currentSelect);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<AudioSource>().PlayOneShot(clickSound);

            if (currentSelect - 1 >= 0)
            {
                currentSelect--;
                choseIcon.transform.position += new Vector3(0, step, 0);
                selecBtn(currentSelect);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q)) {
            GetComponent<AudioSource>().PlayOneShot(clickSound);

            buttons[currentSelect].onClick.Invoke();
        }




	}
}
