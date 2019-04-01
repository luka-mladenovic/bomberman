using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Player script controlling the movement, 
 * bomb placement and pickup interaction.
 */
public class PlayerController : MonoBehaviour
{
    /*
     * Player movement speed
     */
    public float speed = 10;

    /*
     * Amount of simultaneous number of placed bombs
     */
    public int bombNumber = 4;

    /*
     * Bomb prefab reference
     */ 
    public GameObject bomb;

    private Rigidbody rb;
    
    private Vector3 lastBombPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        { 
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
        }
        if (Input.GetKey(KeyCode.A))
        { 
            rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            DropBomb();
        }
    }

    void DropBomb()
    {
        var position = new Vector3(Mathf.RoundToInt(transform.position.x),0f, Mathf.RoundToInt(transform.position.z));
        
        if(position == lastBombPosition)
        {
            Debug.Log("Placing bomb on occupied postion, skipping.");
            return;
        }        
         
        Instantiate(bomb, lastBombPosition = position, bomb.transform.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick up"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
