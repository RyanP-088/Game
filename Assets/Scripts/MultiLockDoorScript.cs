using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLockDoorScript : MonoBehaviour
{
    [SerializeField] int buttNeeds;
    [SerializeField] int buttsPressed;

    BoxCollider2D colliderVar;
    SpriteRenderer sprite;
    public bool sticky;

    private void Awake()
    {
        colliderVar = gameObject.GetComponent<BoxCollider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(buttsPressed == buttNeeds)
        {
            colliderVar.enabled = false;
            sprite.enabled = false;
        } else
        {
            if (!sticky)
            {
                colliderVar.enabled = true;
                sprite.enabled = true;
            }

        }
    }
    public void addButt()
    {
        buttsPressed++;
    }

    public void subButt()
    {
        buttsPressed--;
    }

}
