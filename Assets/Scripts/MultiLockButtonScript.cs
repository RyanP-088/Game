using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLockButtonScript : MonoBehaviour
{
    [SerializeField] MultiLockDoorScript doorScript;
    [SerializeField] MultiLockDoorScript doorScript1;
    public Vector3 depressedPositionOffset = new Vector3(0, -0.1f, 0);

    public bool sticky;
    public bool pressed = false;
    public int pressCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.CompareTag("Enemy") || collision.CompareTag("Weight"))
        {
            pressed = true;
            pressCount++;
            if(pressCount == 1)
            {
                transform.position += depressedPositionOffset;
                doorScript.addButt();
                if(doorScript1 != null)
                {
                    doorScript1.addButt();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.CompareTag("Enemy") || collision.CompareTag("Weight"))
        {

            if (!sticky)
            {
                pressCount--;
                if (pressCount == 0)
                {
                    pressed = false;
                    transform.position -= depressedPositionOffset;
                    doorScript.subButt();
                    if(doorScript1 != null)
                    {
                        doorScript1.subButt();
                    }
                }
            }
        }
    }
}
