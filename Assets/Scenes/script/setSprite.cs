using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setSprite : MonoBehaviour {


    [System.Serializable]
    public struct animates
    {
        public string Name;       
        public Sprite[] sprites;
    };

    public animates[] animations;

    public void setAction(Vector3 pos,string actionName,int index) {

        transform.position = pos+new Vector3(-0.7f,-0.67f,0);

        foreach (animates a in animations) {
            if (a.Name == actionName)
            {
                GetComponent<SpriteRenderer>().sprite = a.sprites[index];
                return;
            }
        }
    }

}
