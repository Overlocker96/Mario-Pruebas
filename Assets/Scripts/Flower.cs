using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(this.Moving());
    }

    private IEnumerator Moving()
    {
        float time = 0f;
        float duration = 2f;

        Vector2 startPosition = this.transform.position;
        Vector2 endPosition = (Vector2)this.transform.position + (Vector2.up * 1);

        // Movemos la flor hacia arriba
        while (time < duration)
        {
            this.transform.position = Vector2.Lerp(startPosition, endPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        this.transform.position = endPosition;

        time = 0f;
    }
}
