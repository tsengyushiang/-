using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zindexChange : MonoBehaviour {

    void Awake() {
        transform.parent.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
        transform.parent.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "characterCollider")
        {
            transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 3;
            transform.parent.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
            transform.parent.GetChild(1).GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "characterCollider") {

            transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 1;
            transform.parent.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            transform.parent.GetChild(1).GetComponent<BoxCollider2D>().enabled = true;

        }
    }
}
