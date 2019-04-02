using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionReact : MonoBehaviour {

    public Text sentenceText;
    public Text htintText;

    void Awake() {
        setSentence("無聊的時候可以點點頭，世界就會上下移動", "按Q點頭");
    }

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
            setSentence(tmp.sentence, tmp.hint);
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hintWords tmp = collision.gameObject.GetComponent<hintWords>();
        if (tmp != null)
        {
            setSentence("", "");
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, false);
        }
    }



}
