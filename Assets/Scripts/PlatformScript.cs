using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    [SerializeField] bool move;
    [SerializeField] GameObject anchor1;
    [SerializeField] GameObject anchor2;
    [SerializeField] GameObject target;
    [SerializeField] float speedVal;
    [SerializeField] bool xOrY; //false if x, true if y
    [SerializeField] bool lOrR; //false if object left/below of anchor, true if object right/above of anchor


    private void Awake()
    {
        target = anchor2;
        directions();
    }

    private void directions()
    {
        if (!xOrY)
        {
            if (gameObject.transform.position.x <= target.transform.position.x)
            {
                lOrR = false;
                if (speedVal < 0) speedVal = speedVal * -1;
            }
            else if (gameObject.transform.position.x > target.transform.position.x)
            {
                lOrR = true;
                if (speedVal > 0) speedVal = speedVal * -1;
            }
        } else
        {
            if (gameObject.transform.position.y < target.transform.position.y)
            {
                lOrR = false;
                if (speedVal < 0) speedVal = speedVal * -1;
            }
            else if (gameObject.transform.position.y > target.transform.position.y)
            {
                lOrR = true;
                if (speedVal > 0) speedVal = speedVal * -1;
            }
        }
        

 
    }

    public void changeDirections()
    {
        if (target == anchor2) 
        { 
            target = anchor1; 
        } else if (target == anchor1)
        {
            target = anchor2;
        }
    }

    private void FixedUpdate()
    {
        if (move)
        {
            if (!xOrY)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + speedVal, gameObject.transform.position.y, gameObject.transform.position.z);
                if (!lOrR)
                {
                    if(gameObject.transform.position.x >= target.transform.position.x)
                    {
                        changeDirections();
                        directions();
                    }
                } else
                {
                    if(gameObject.transform.position.x <= target.transform.position.x)
                    {
                        changeDirections();
                        directions();
                    }
                }
            } else
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + speedVal, gameObject.transform.position.z);

                if (!lOrR)
                {
                    if(gameObject.transform.position.y >= target.transform.position.y)
                    {
                        changeDirections();
                        directions();
                    }
                } else
                {
                    if(gameObject.transform.position.y <= target.transform.position.y)
                    {
                        changeDirections();
                        directions();
                    }
                }
            }
        }



    }
    public void activateMove()
    {
        move = true;
    }

    public void deactivateMove()
    {
        move = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (move && collision.tag == "Player")
        {
            if (!xOrY)
            {
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x + speedVal, collision.gameObject.transform.position.y, collision.gameObject.transform.position.z);
            }
            else
            {
                collision.gameObject.transform.position = new Vector3(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y + speedVal, collision.gameObject.transform.position.z);

            }
        }
       
    }
}
