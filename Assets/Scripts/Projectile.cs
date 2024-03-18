using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    public GameObject impactEffect;
    public Transform tf;
    public float projectileLifetime = 1.0f; // Lifetime of the projectile

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * projectileSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject newImpactEffect = Instantiate(impactEffect, tf.position, tf.rotation);
        //Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(newImpactEffect, projectileLifetime);
        Destroy(gameObject);
        
    }
    
}
