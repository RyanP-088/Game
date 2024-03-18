using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] PartyLogic plObj;
    private bool used = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !used)
        {
            used = true;
            plObj.updateCheckpoint(this.gameObject);
        }
    }
}
