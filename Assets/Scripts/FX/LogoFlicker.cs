using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogoFlicker : MonoBehaviour
{
    public RawImage Image;
    public float Max = 1f;

    void Awake()
    {
        Image = GetComponent<RawImage>();
        StartCoroutine(Flicker());
        StartCoroutine(UV());
    }

    IEnumerator Flicker()
    {
        for (;;)
        {
            yield return new WaitForSeconds(0.1f);
            float t = Random.Range(0.3f, 1f);
            Image.color = new Color(1f, 1f, 1f, t * t * t * t * Max);
        }
    }

    IEnumerator UV()
    {
        for (;;)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.25f));
            Image.uvRect = new Rect(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f));
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            Image.uvRect = new Rect(0, 0, 1, 1);
        }
    }
}
