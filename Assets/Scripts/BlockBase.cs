using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    private bool isBouncing = false;
    private SpriteRenderer b_sr;
    protected Rigidbody2D b_rb;

    [SerializeField]
    private Sprite b_sp_blocked;
    [SerializeField]
    public int blockType;

    private bool isBlocked = false;

    private void Awake()
    {
        b_sr = GetComponent<SpriteRenderer>();
    }

    public void Hit()
    {
        Debug.Log("Hit de BloqueBase");
        if (isBouncing == false && isBlocked != true)
        {
            StartCoroutine(this.Bouncing());
        }
    }

    protected void SetSprite()
    {
        Debug.Log("Test");
        switch (blockType)
        {
            case 0:
                 b_sr.sprite = b_sp_blocked;
                isBlocked = true;
            break;
        }
    }


    private IEnumerator Bouncing()
    {
        float time = 0f;
        float duration = 0.1f;

        Vector2 startPosition = this.transform.position;
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 0.5f);

        // Movemos el bloque hacia arriba
        while (time < duration)
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = endPosition;

        time = 0f;

        // Movemos el bloque hacia abajo
        while (time < duration)
        {
            this.transform.position = Vector2.Lerp(endPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPosition;

        isBouncing = false;
    }
}
