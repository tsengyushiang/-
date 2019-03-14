using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionReact : MonoBehaviour {

    public Text sentenceText;
    public Text htintText;

    void Awake() {
        sentenceText.text = "無聊的時候可以點點頭，世界就會上下移動";
        htintText.text = "按Q點頭";
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        hintWords tmp = collision.gameObject.GetComponent<hintWords>();

        if (tmp != null)
        {
            sentenceText.text = tmp.sentence;
            htintText.text = tmp.hint;
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        hintWords tmp = collision.gameObject.GetComponent<hintWords>();
        if (tmp != null)
        {
            sentenceText.text = "";
            htintText.text = "";
            transform.parent.GetComponent<animationNormal>().setAnimationEnableByName(tmp.activeName, false);
        }
    }



}
