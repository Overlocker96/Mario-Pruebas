using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform mario;
    
    void Start()
    {
        mario = GameObject.Find("Mario").transform;
    }

    
    void Update()
    {
        if (mario.position.x >= transform.position.x)
        {
            transform.position = new Vector3(mario.position.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
