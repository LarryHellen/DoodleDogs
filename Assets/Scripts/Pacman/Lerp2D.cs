using UnityEngine;
using System.Collections;

public class Lerp2D : MonoBehaviour
{
    public Vector2 positionToMoveTo;
    void Start()
    {
        StartCoroutine(LerpPosition(positionToMoveTo, 5));
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}