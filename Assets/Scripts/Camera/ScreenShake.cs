using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalRotation = transform.localEulerAngles;

        while (duration > 0)
        {
            float shakeAmountX = Random.Range(-1f, 1f) * magnitude;
            float shakeAmountY = Random.Range(-1f, 1f) * magnitude;

            transform.localEulerAngles = new Vector3(originalRotation.x + shakeAmountX, originalRotation.y + shakeAmountY, originalRotation.z);

            duration -= Time.deltaTime;

            yield return null;
        }
        transform.localEulerAngles = originalRotation;
    }
    public void StartShake(float duration, float magnitude)
    {
        StartCoroutine(Shake(duration, magnitude));
    }
}