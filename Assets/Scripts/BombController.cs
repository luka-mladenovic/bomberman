using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosion;
    public LayerMask softBlocks;
    public LayerMask solidBlocks;

    /*
     * Bomb timer before exploding after being placed
     */
    public float timer = 2f;

    /*
     * Explosion tile radius.
     */ 
    public int radius = 2;

    /*
     * Decay defines the timer in which the
     * bomb will be removed after exploded.
     */
    private readonly float decay = .2f;
    private bool exploded = false;

    /*
     * Explode a bomb when the timer expires
     */
    void Start()
    {
        Invoke("Explode", timer);
    }

    void Explode()
    {
        CreateExplosion();
        exploded = !exploded;

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        
        Destroy(gameObject, decay);
    }

    /*
     * Create an explosion.
     * Using the coroutine we will instantiate
     * explosions in all directions.
     */
    void CreateExplosion()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
    }
    
    /*
     * Create an explosion in given direction,
     * explosion is contained by the solid wall.
     */ 
    private IEnumerator CreateExplosions(Vector3 direction)
    {
        RaycastHit softBlockHit;
        RaycastHit solidBlockHit;

        for (int i = 1; i <= radius; i++)
        {    
            Physics.Raycast(transform.position, direction, out solidBlockHit, i, solidBlocks);
            Physics.Raycast(transform.position, direction, out softBlockHit, i, softBlocks);

            // Solid wall means no explosion
            if(solidBlockHit.collider)
            {
                break;
            }
  
            Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);

            // Soft wall is destroyed by an explosion and will dampen it
            if (softBlockHit.collider)
            {
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }

    /*
     * Getting in contact with an explosion will cause the
     * bomb to explode, creating a chain reaction of explosions.  
     */
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Explosion") && !exploded)
        {
            CancelInvoke("Explode"); 
            Explode();
        }
    }
}
