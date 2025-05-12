using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;
    private Coroutine isShaking;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void StartShake(float duration, float strength)
    {
        if (isShaking != null)
        {
            StopCoroutine(isShaking);
            transform.localPosition = originalPos;
        }

        isShaking = StartCoroutine(Shaking(duration, strength));
    }

    public IEnumerator Shaking(float duration, float strength)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * strength;
            float y = Random.Range(-1f, 1f) * strength;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        isShaking = null;
    }
}
