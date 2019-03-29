using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosion;
    public LayerMask softBlocks;
    public LayerMask solidBlocks;

    public float timer = 2f;
    public int radius = 2;

    private readonly float decay = .2f;
    private bool exploded = false;

    void Start()
    {
        Invoke("Explode", timer);
    }

    void Update()
    {
        
    }

    /*
     * Explode a bomb when the timer expires
     */ 
    void Explode()
    {
        CreateExplosion();
        exploded = !exploded;

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        
        Destroy(gameObject, decay);
    }

    /*
     * Create an explosion
     */
    void CreateExplosion()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));
    }
    
    private IEnumerator CreateExplosions(Vector3 direction)
    {
        RaycastHit softBlockHit;
        RaycastHit solidBlockHit;

        for (int i = 1; i <= radius; i++)
        {    
            Physics.Raycast(transform.position, direction, out solidBlockHit, i, solidBlocks);
            Physics.Raycast(transform.position, direction, out softBlockHit, i, softBlocks);

            if(solidBlockHit.collider)
            {
                break;
            }
  
            Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);

            if (softBlockHit.collider)
            {
                break;
            }

            yield return new WaitForSeconds(.05f);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        /*
         * Explosion causes chain reaction
         */
        if (other.CompareTag("Explosion") && !exploded)
        {
            CancelInvoke("Explode"); 
            Explode();
        }
    }
}
