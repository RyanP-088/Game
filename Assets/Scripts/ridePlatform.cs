using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ridePlatform : MonoBehaviour
{

    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;

    private void Awake()
    {
        target = null;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Ground")
        {
            target = collision.gameObject;
            offset = target.transform.position - gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    private void LateUpdate()
    {
        target.transform.position = gameObject.transform.position + offset;
    }
}
