using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZindexChange : MonoBehaviour {


    void OnCollision2D(Collision col) {
        Debug.Log("Enter c " + col.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter "+collision.gameObject.name);
        transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exit" + collision.gameObject.name);
        transform.parent.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
