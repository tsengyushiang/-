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
        public float speed;
        public bool writeDefault;
        public int lockPlayTimes;
        public Texture2D[] normalMaps;
        public Sprite[] Normalsprites;
        public Sprite[] Bleedingsprites;
    };

    private animates NULL;
    public animates[] Animations;
    public GameObject throwUpHint;

    // kind of animation
    private int currentState=0;
    // which sprite
    public float currentMotion=0;

    private bool Isbleeding = false;
    public int RemainLockPlayTime = 0;
    private bool AnyBtnDown = true;
    public string CurrentAnimationName = "";

    private int forceAnimate = -1;

    private int backichyCount = 0; 

    public void setAnimationEnableByName(string name,bool enable) {

        for (int i = 0; i < Animations.Length; i++) {
            if (Animations[i].Name == name)
            {
                Animations[i].enable = enable;
                return;
            }
        }
    }
    void OnEnable() {
       ForceAnimation("standup");
    }

    public void endScene() {
        ForceAnimation("sitDown");
        
    }

    public void ForceAnimation(string name) {

        int index = -1;

        for (int i = 0; i < Animations.Length; i++)
        {
            if (Animations[i].Name == name)
            {
                index=i;
                break;
            }
        }

        if (forceAnimate == index) return;

        if (index >= Animations.Length)
        {
            forceAnimate = -1;
        }
        else {
            RemainLockPlayTime = 0;
            forceAnimate = index;
        }
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void Update()
    {       
        if (RemainLockPlayTime == 0) {
            for (int i = 0; i < Animations.Length; i++)
            {
                bool forced = false;
                if (forceAnimate == i)
                {
                    forced = true;
                }
                else {
                    if (forceAnimate != -1) continue;
                    if (Animations[i].enable == false) continue;
                }

                AnyBtnDown = true;
                if (Input.GetKey(Animations[i].key)|| forced)
                {
                    if (i != currentState)
                    {
                        currentMotion = 0.0f;
                    }
                    currentState = i;

                    if (Animations[i].lockwhenPlay == true)
                    {
                        RemainLockPlayTime = Animations[i].lockPlayTimes;
                    }
                                      

                        if (Animations[i].Name == "backichy")
                        {
                            backichyCount++;
                            if (backichyCount > 5)
                            {
                                Isbleeding = true;
                            }
                        }
                        else if (Animations[i].Name == "eat" || Animations[i].Name == "drinkleft")
                        {
                            throwUpHint.SetActive(true);
                             setAnimationEnableByName("throwup", true);
                        }


                    forceAnimate = -1;
                    break;
                }
                AnyBtnDown = false;
            }
        }
        

        if (AnyBtnDown == true)
        { 
            CurrentAnimationName = Animations[currentState].Name;
            
            if (Isbleeding==true)
                GetComponent<SpriteRenderer>().sprite = Animations[currentState].Bleedingsprites[(int)currentMotion];
            else
                GetComponent<SpriteRenderer>().sprite = Animations[currentState].Normalsprites[(int)currentMotion];

            //material.SetTexture("_BumpMap", Animations[currentState].normalMaps[(int)currentMotion]);

            Destroy(GetComponent<PolygonCollider2D>());
     
            gameObject.AddComponent<PolygonCollider2D>();
            
            GetComponent<PolygonCollider2D>().isTrigger = true;

            currentMotion += Animations[currentState].speed*Time.deltaTime;
            if ((int)currentMotion >= Animations[currentState].Normalsprites.Length)
            {
                currentMotion = 0;

                if (Animations[currentState].writeDefault) {
                    if (Isbleeding == true)
                        GetComponent<SpriteRenderer>().sprite = Animations[currentState].Bleedingsprites[(int)currentMotion];
                    else
                        GetComponent<SpriteRenderer>().sprite = Animations[currentState].Normalsprites[(int)currentMotion];

                }

                RemainLockPlayTime--;
                if (RemainLockPlayTime <= 0)
                    RemainLockPlayTime = 0;
            }
        }
    }

}
