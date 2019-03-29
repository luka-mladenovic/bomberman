using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private readonly float lifetime = 0.5f;

    /*
     * Destroy explosion object after expired lifetime
     */ 
    void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
