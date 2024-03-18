using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Goomba_move : MonoBehaviour
{

    [SerializeField] private int enemySpeed;
    [SerializeField] private int xMoveDirection;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, new Vector2(xMoveDirection, 0), GetComponent<Collider2D>().bounds.extents.x + 0.1f);
        
        transform.position += new Vector3(xMoveDirection, 0, 0) * (enemySpeed * Time.deltaTime);
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (xMoveDirection, 0) * enemySpeed;
        
        if (ValidateCollision(hits))
        {
            Flip();
        }
    }

    private bool ValidateCollision(RaycastHit2D[] hits){
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Intangible Entity"))
            {
                return true;
            }
        }
        return false;
    }

    void Flip ()
    {
        Vector2 localScale = gameObject.transform.localScale;
        
        if (xMoveDirection > 0)
        {
            xMoveDirection = -1;
            localScale.x *= -1;
            transform.localScale = localScale;
        }

        else
        {
            xMoveDirection = 1;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
