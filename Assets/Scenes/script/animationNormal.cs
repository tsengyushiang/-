using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class animationNormal : MonoBehaviour {

    private Material material;

    [System.Serializable]
    public struct animates
    {
        public string Name;
        public KeyCode key;
        public bool enable;
        public bool lockwhenPlay;
        public int lockPlayTimes;
        public Texture2D[] normalMaps;
        public Sprite[] Normalsprites;
        public Sprite[] Bleedingsprites;
    };

    private animates NULL;
    public animates[] Animations;

    // kind of animation
    private int currentState=0;
    // which sprite
    private float currentMotion=0;
    public float speed=0.05f;

    private bool Isbleeding = false;
    public int RemainLockPlayTime = 0;
    private bool AnyBtnDown = true;

    public void setAnimationEnableByName(string name,bool enable) {

        for (int i = 0; i < Animations.Length; i++) {
            if (Animations[i].Name == name)
            {
                Animations[i].enable = enable;
                return;
            }
        }
        Debug.Log(name + " is not found!");
    }
    // Use this for initialization
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.EnableKeyword("_NORMALMAP");
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {       
        if (RemainLockPlayTime == 0) {
            for (int i = 0; i < Animations.Length; i++)
            {
                if (Animations[i].enable == false) continue;

                AnyBtnDown = true;
                if (Input.GetKey(Animations[i].key))
                {
                    if (i != currentState)
                        currentMotion = 0.0f;

                    currentState = i;

                    if (Animations[i].lockwhenPlay == true)
                    {
                        RemainLockPlayTime = Animations[i].lockPlayTimes;
                    }
                    break;
                }
                AnyBtnDown = false;
            }
        }
        

        if (AnyBtnDown == true)
        {

            if(Isbleeding==true)
                GetComponent<SpriteRenderer>().sprite = Animations[currentState].Bleedingsprites[(int)currentMotion];
            else
                GetComponent<SpriteRenderer>().sprite = Animations[currentState].Normalsprites[(int)currentMotion];

            material.SetTexture("_BumpMap", Animations[currentState].normalMaps[(int)currentMotion]);

            Destroy(GetComponent<PolygonCollider2D>());
     
            gameObject.AddComponent<PolygonCollider2D>();
            GetComponent<PolygonCollider2D>().isTrigger = true;

            currentMotion += speed*Time.deltaTime;
            if ((int)currentMotion >= Animations[currentState].Normalsprites.Length)
            {
                currentMotion = 0;
                RemainLockPlayTime--;
                if (RemainLockPlayTime <= 0)
                    RemainLockPlayTime = 0;
            }
        }
    }

}
