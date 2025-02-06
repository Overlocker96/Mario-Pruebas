using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomp : MonoBehaviour
{
    Animator s_anim;
    private bool stomped;

    private void Awake()
    {
        s_anim = GetComponentInParent<Animator>();
        stomped = false;
    }

    private void Update()
    {
        s_anim.SetBool("Stomp", stomped);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.name == "Mario")
        {
            collider.attachedRigidbody.velocity = new Vector2(0, 10);
            stomped = true;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}