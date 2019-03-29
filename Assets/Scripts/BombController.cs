using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameObject explosion;
    public LayerMask solidBlocks;

    public float timer = 2f;
    public int radius = 2;

    private bool exploded = false;

    private readonly float decay = .2f;

    void Start()
    {
        Invoke("Explode", timer);
    }

    void Update()
    {
        
    }

    /*
     * Spawn an explosion 
     */ 
    void Explode()
    {

        Instantiate(explosion, transform.position, Quaternion.identity);

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        exploded = !exploded;
        Destroy(gameObject, decay);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        RaycastHit solidWallHit;

        for (int i = 1; i <= radius; i++)
        {    
            Physics.Raycast(transform.position, direction, out solidWallHit, i, solidBlocks);

            if(solidWallHit.collider)
            {
                break;
            }
  
            Instantiate(explosion, transform.position + (i * direction), explosion.transform.rotation);            
         
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
