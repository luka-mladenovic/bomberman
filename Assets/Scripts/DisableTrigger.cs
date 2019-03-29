using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Disable bomb collider trigger when player walks from the bomb after being placed. 
 * This way the bomb can be laid down at the player's feet without knocking him around.
 */
public class DisableTrigger : MonoBehaviour
{
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider>().isTrigger = false; 
        }
    }
}
