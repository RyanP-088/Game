using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaAttack : MonoBehaviour
{
    [SerializeField] PartyLogic pLog;
    [SerializeField] L3PartyLogic l3Log;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dude (sticky)" || collision.gameObject.name == "dude(smart)" || collision.gameObject.name == "dude (roid)")
        {
            if (l3Log == null)
            {
                pLog.respawn(collision.gameObject);
            } else
            {
                l3Log.respawn(collision.gameObject);
            }
        }
    }

}
