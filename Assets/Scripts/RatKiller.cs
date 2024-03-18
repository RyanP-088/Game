using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatKiller : MonoBehaviour
{

    [SerializeField] AudioSource ratDeath;
    [SerializeField] AudioSource acidNoise;
    [SerializeField] GameObject rat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "dude (sticky)" || collision.gameObject.name == "dude (roid)")
        {
            rat = collision.gameObject;
            Object.Destroy(rat);
            ratDeath.Play();
            acidNoise.Play();
        }
    }

}
