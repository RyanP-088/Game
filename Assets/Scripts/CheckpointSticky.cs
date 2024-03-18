using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSticky : MonoBehaviour
{
    [SerializeField] L3PartyLogic plObj;
    [SerializeField] bool sticky;
    [SerializeField] bool smart;
    [SerializeField] bool roid;
    public bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!used)
        {
            if (sticky)
            {
                if (collision.gameObject.name == "dude (sticky)")
                {
                    used = true;
                    plObj.updateStickyCpoint(gameObject);
                }
            } else if (roid)
            {
                if (collision.gameObject.name == "dude (roid)")
                {
                    used = true;
                    plObj.updateRoidCpoint(gameObject);
                }
            } else if (smart)
            {
                if (collision.gameObject.name == "dude(smart)")
                {
                    used = true;
                    plObj.updateSmartCpoint(gameObject);
                }
            }

        }
        
    }



}
