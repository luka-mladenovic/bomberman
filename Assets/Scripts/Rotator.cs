using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script takes handling of object rotation  
 */
public class Rotator : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }
}
