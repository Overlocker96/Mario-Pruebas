using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D b_rb;
    Vector2 oldPos;
    Animator b_anim;
    private bool hit;
    Collider2D b_col;

    // Start is called before the first frame update
    void Start()
    {
        b_rb = GetComponent<Rigidbody2D>();
        b_anim = GetComponent<Animator>();
        b_col = GetComponent<Collider2D>();
        oldPos.y = b_rb.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            b_rb.velocity = new Vector2(0f, 1f);
            b_rb.gravityScale = 1f;
            b_col.enabled = false;
        }
        else if (b_rb.position.y >= oldPos.y + 1f)
        {
            b_rb.velocity = new Vector2(0f, 0f);
        }
        else if (b_rb.position.y < oldPos.y)
        {
            b_rb.velocity = new Vector2(0f, 0f);
            hit = false;
            b_rb.gravityScale = 0f;
            b_anim.enabled = false;
            b_col.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Mario")
        {
            hit = true;
        } 
    }
}
