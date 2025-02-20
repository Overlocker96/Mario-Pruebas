using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBrick : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D Chunk_UL;
    [SerializeField]
    private Rigidbody2D Chunk_UR;
    [SerializeField]
    private Rigidbody2D Chunk_DL;
    [SerializeField]
    private Rigidbody2D Chunk_DR;

    private void Start()
    {
        Chunk_UL = GameObject.Find("Chunk_UL").GetComponent<Rigidbody2D>();
        Chunk_UR = GameObject.Find("Chunk_UR").GetComponent<Rigidbody2D>();
        Chunk_DL = GameObject.Find("Chunk_DL").GetComponent<Rigidbody2D>();
        Chunk_DR = GameObject.Find("Chunk_DR").GetComponent<Rigidbody2D>();

        Chunk_UL.velocity = new Vector2(-2f, 2f);
        Chunk_UR.velocity = new Vector2(2f, 2f);
        Chunk_DL.velocity = new Vector2(-2f, -2f);
        Chunk_DR.velocity = new Vector2(2f, -2f);

        Destroy(this.gameObject, 1);
    }
}
