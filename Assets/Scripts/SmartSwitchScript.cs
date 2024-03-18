using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartSwitchScript : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject smartRat;
    [SerializeField] SmartSpecialization smartScript;
    public Sprite onAsset;
    public Sprite offAsset;
    SpriteRenderer spriteRenderer;

    [SerializeField] bool on = true;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void activateSwitch()
    {
        if (on)
        {
            on = false;
            door.SetActive(false);
            spriteRenderer.sprite = onAsset;
           
        } else
        {
            on = true;
            door.SetActive(true);
            spriteRenderer.sprite = offAsset;
           

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "dude(smart)")
        {
            smartRat = collision.gameObject;
            smartScript = smartRat.GetComponent<SmartSpecialization>();

            smartScript.setRangeSwitch(gameObject);
          


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dude(smart)")
        {

            smartScript.deactivateSwitch();
          

        }
    }
}
