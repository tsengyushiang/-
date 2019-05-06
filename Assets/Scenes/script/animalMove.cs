using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMove : MonoBehaviour
{

    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.    

    float moveHorizontal = 0;
    float moveVertical = 0;

    public Vector3 dst;

    private bool isAutoMoving = false;
    private string Animation;
    public KeyCode waitKey;

    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        dst = transform.position;      

    }

    public void MovieToGameObject(GameObject target,string anim,KeyCode k) {

        if (isAutoMoving) return;

        dst.x = target.transform.position.x;
        dst.y = target.transform.position.y;
        Animation = anim;
        waitKey = k;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
             
        if (GetComponent<animationNormal>().RemainLockPlayTime != 0)
        {
            rb2d.velocity = Vector2.zero;
            return;
        }

        if (Input.GetKey(waitKey))
        {
            isAutoMoving = true;
        }

        if (isAutoMoving)
        {
            if (GetComponent<animationNormal>().RemainLockPlayTime != 0) return;
            rb2d.velocity = Vector2.zero;
            
            var step = 0.05f;
            if (dst.x > transform.position.x)
            {
                
                transform.position += new Vector3(step, 0, 0);
                if (dst.x - transform.position.x < step)
                    transform.position = new Vector3(dst.x, transform.position.y);
                else
                    GetComponent<animationNormal>().ForceAnimation("walkRight");

                GetComponent<animationNormal>().setAnimationEnableByName("nodright", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);

            }
            else if (dst.x < transform.position.x)
            {
               
                transform.position -= new Vector3(step, 0, 0);
                if (transform.position.x- dst.x < step)
                    transform.position = new Vector3(dst.x, transform.position.y);
                else
                    GetComponent<animationNormal>().ForceAnimation("walkleft");

                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);


            }
            else if (dst.y > transform.position.y)
            {
                
                transform.position += new Vector3(0, step, 0);
                if (dst.y-transform.position.y < step)
                    transform.position = new Vector3(transform.position.x, dst.y);
                else
                    GetComponent<animationNormal>().ForceAnimation("walkback");

                GetComponent<animationNormal>().setAnimationEnableByName("nodback", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);
            }
            else if (dst.y < transform.position.y)
            {
                
                transform.position -= new Vector3(0, step, 0);
                if (transform.position.y- dst.y < step)
                    transform.position = new Vector3(transform.position.x, dst.y);
                else
                    GetComponent<animationNormal>().ForceAnimation("walkfront");

                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);

            }
            else
            {
                GetComponent<animationNormal>().ForceAnimation(Animation);
                isAutoMoving = false;
            }
        }
        else {
            if (Input.GetKey(KeyCode.D))
            {
                moveHorizontal = 1;
                moveVertical = 0;
                GetComponent<animationNormal>().ForceAnimation("walkRight");
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                moveHorizontal = -1;
                moveVertical = 0;

                GetComponent<animationNormal>().ForceAnimation("walkleft");
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);
            }
            else if (Input.GetKey(KeyCode.W) )
            {
                moveHorizontal = 0;
                moveVertical = 1;
                GetComponent<animationNormal>().ForceAnimation("walkback");
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", false);
            }
            else if (Input.GetKey(KeyCode.S) )
            {
                moveHorizontal = 0;
                moveVertical = -1;
                GetComponent<animationNormal>().ForceAnimation("walkfront");
                GetComponent<animationNormal>().setAnimationEnableByName("nodfront", true);
                GetComponent<animationNormal>().setAnimationEnableByName("nodright", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodleft", false);
                GetComponent<animationNormal>().setAnimationEnableByName("nodback", false);
            }
            else
            {
                if (!Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.S))
                {                  
                    moveHorizontal = 0;
                    moveVertical = 0;
                }
            }

            //Use the two store floats to create a new Vector2 variable movement.
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
            rb2d.velocity = movement * speed * Time.deltaTime;
        }
    } 
}
