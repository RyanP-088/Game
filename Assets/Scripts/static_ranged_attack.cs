using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;

    private float time = 0.0f;
    public float delayPeriod = 2.0f;
    public float projectileLifetime = 5.0f; // Lifetime of the projectile

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time >= delayPeriod)
        {
            time = time - delayPeriod;
            GameObject newProjectile = Instantiate(projectile, firePosition.position, firePosition.rotation);
            Destroy(newProjectile, projectileLifetime); // Destroy the projectile after a set time
            
        }
    }
}
