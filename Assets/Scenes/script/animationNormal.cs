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
    public GameObject foodPot;

    // kind of animation
    private int currentState=0;
    // which sprite
    public float currentMotion=0;
    public float speed=0.05f;

    private bool Isbleeding = false;
    public int RemainLockPlayTime = 0;
    private bool AnyBtnDown = true;
    public string CurrentAnimationName = "";

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
            CurrentAnimationName = Animations[currentState].Name;

            if (CurrentAnimationName == "backichy" && currentMotion == 0.0f)
            {
                backichyCount++;
                if (backichyCount == 5)
                {
                    hintWords.changeState("backichy", "阿...搓太多次流血了", "按C搓背", "backichy");
                    Isbleeding = true;
                }
                else if (backichyCount == 6)
                {
                    hintWords.changeState("backichy", "雖然痛痛的還是很舒服", "按C搓背", "backichy");
                }
            }
            else if (CurrentAnimationName == "eat" && currentMotion == 0.0f)
            {
                foodPot.GetComponent<SpriteRenderer>().enabled = true;
                foodPot.transform.GetChild(0).gameObject.SetActive(false);
                Animations[currentState].enable = false;
                hintWords.changeState("eat", "在吃一次剛剛的東西好了", "按X吐出來", "throwup");
            }

            if (Isbleeding==true)
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
