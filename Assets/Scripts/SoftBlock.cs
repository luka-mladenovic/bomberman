using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBlock : MonoBehaviour
{
    public GameObject[] pickups;

    /*
     * Soft block is destroyed by an explosion.
     */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion"))
        {
            Destroy(gameObject);
        }
    }

    /*
     * When a block is destroyed there is a 
     * random chance that a pickup will be dropped.
     */
    void OnDestroy()
    {
        if (Random.value <= 0.2)
        {
            Instantiate(pickups[0], transform.position, transform.rotation);
        }        
    }
}
