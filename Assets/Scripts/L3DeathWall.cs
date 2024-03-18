using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3DeathWall : MonoBehaviour
{
    [SerializeField] L3PartyLogic pLObj;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            pLObj.respawn(player);
        }
    }
}
