using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionReact : MonoBehaviour {

    public Text sentenceText;
    public Text htintText;
    public GameObject hintDrinkLeft;
    public GameObject hinkDrinkDown;
    public GameObject hinkBackichy;

    void setSentence(string left,string right) {

        int count = 0;
        for (int i = 0; i < left.Length; i++) {
            count++;
            if (count >10) {
                count = 0;
                left = left.Substring(0, i) +"\n"+left.Substring(i);
            }
        }
        
        sentenceText.text = "";
        htintText.text = right;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "drinkleft")
        {
            hinkDrinkDown.SetActive(false);
        }
        else if (collision.gameObject.name == "drinkdown")
        {
            hintDrinkLeft.SetActive(false);
        }
        else  if (collision.gameObject.name == "wood")
        {
            hinkBackichy.SetActive(false);
        }


        hintWords tmp = collision.gameObject.GetComponent<hintWords>();
        if (tmp != null)
        {
            transform.parent.GetComponent<animalMove>().
                    MovieToGameObject(transform.parent.gameObject,"", KeyCode.Alpha0);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "drinkleft")
        {
            hinkDrinkDown.SetActive(true);
        }
        else if (collision.gameObject.name == "drinkdown")
        {
            hintDrinkLeft.SetActive(true);
        }
        else if (collision.gameObject.name == "wood")
        {
            hinkBackichy.SetActive(true);
        }

        hintWords tmp = collision.gameObject.GetComponent<hintWords>();

        if (tmp != null)
        {
            KeyCode key = transform.parent.GetComponent<animationNormal>().getKeyCode(tmp.activeName, true);

            transform.parent.GetComponent<animalMove>().
                MovieToGameObject(
                collision.gameObject,
                collision.gameObject.GetComponent<hintWords>().activeName, key);
        }

    }

}
