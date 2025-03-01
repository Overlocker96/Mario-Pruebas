using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioPunch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var blockScript = collision.GetComponent<Block>();

        if (blockScript != null)
        {
            blockScript.Hit();
        }
    }
}