using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(this.Bouncing());
    }

    private IEnumerator Bouncing()
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

        // Movemos el bloque hacia abajo
        while (time < duration)
        {
            this.transform.position = Vector2.Lerp(endPosition, startPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = startPosition;

        Destroy(this.gameObject);
    }
}