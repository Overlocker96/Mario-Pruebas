using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeMushroom : MonoBehaviour
{
    Rigidbody2D mu_rb;

    [SerializeField]
    private float velocity;
    private bool moving = false;

    private void Awake()
    {
        mu_rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(this.Moving());
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            if (Mathf.Abs(mu_rb.velocity.x) < 0.05f)
            {
                ChangeDirection();
            }
            mu_rb.velocity = new Vector2(velocity, mu_rb.velocity.y);
        }
    }

    private void ChangeDirection()
    {
        this.velocity *= -1;
    }

    private IEnumerator Moving()
    {
        float time = 0f;
        float duration = 0.1f;

        Vector2 startPosition = this.transform.position;
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 2);

        // Movemos el bloque hacia arriba
        while (time < duration)
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = endPosition;

        time = 0f;
        moving = true;
    }
}
