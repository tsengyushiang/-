using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionReact : MonoBehaviour {

    public Text sentenceText;
    public Text htintText;

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

    void OnTriggerStay2D(Collider2D collision)
    {
        hintWords tmp = collision.gameObject.GetComponent<hintWords>();

        if (tmp != null)
        {
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hintWords tmp = collision.gameObject.GetComponent<hintWords>();
        if (tmp != null)
        {
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, false);
        }
    }  

}
